using SudokuSolver.Model;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace SudokuSolver.ViewModel
{
    public class SudokuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<SudokuCell> CellCollection { get; set; }

        private readonly SudokuBoard sudokuModel;

        public SudokuViewModel()
        {
            sudokuModel = new SudokuBoard();
            CellCollection = new ObservableCollection<SudokuCell>(sudokuModel.Board.Cast<byte>().Select(b => new SudokuCell(b)));
            CellCollection.CollectionChanged += ListBoard_CollectionChanged;

            foreach (SudokuCell item in CellCollection)
            {
                item.PropertyChanged += ListBoardItem_PropertyChanged;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ListBoard_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems)
                {
                    if (newItem is SudokuCell observableByte)
                    {
                        observableByte.PropertyChanged += ListBoardItem_PropertyChanged;
                    }
                }
            }

            if (e.OldItems != null)
            {
                foreach (var oldItem in e.OldItems)
                {
                    if (oldItem is SudokuCell observableByte)
                    {
                        observableByte.PropertyChanged -= ListBoardItem_PropertyChanged;
                    }
                }
            }
        }

        /// <summary>
        /// Updates correct position in Sudoku Board in model.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoardItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Value")
            {
                var item = (SudokuCell)sender;
                var index = CellCollection.IndexOf(item);

                var row = index / 9;
                var col = index % 9;

                sudokuModel.Board[row, col] = item.Value;

            }
        }
    }
}