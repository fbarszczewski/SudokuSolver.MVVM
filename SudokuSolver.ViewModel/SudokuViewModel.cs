using SudokuSolver.Model;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;

namespace SudokuSolver.ViewModel
{
    public class SudokuViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<SudokuCell> CellCollection { get; private set; }

        private readonly SudokuBoard sudokuModel;

        public SudokuViewModel()
        {
            sudokuModel = new SudokuBoard();

            // Filling the collection with the values from the model
            CellCollection = new ObservableCollection<SudokuCell>(sudokuModel.Board.OfType<byte>().Select(b => new SudokuCell(b)));

            // Attaching event handler to the CollectionChanged event of CellCollection.
            // This is necessary to synchronize the changes in the CellCollection with the model's Board.
            CellCollection.CollectionChanged += ListBoard_CollectionChanged;

            // Attaching event handler to the PropertyChanged event of each SudokuCell in CellCollection.
            // This is necessary to synchronize the changes in the Value property of the SudokuCell objects in the CellCollection
            foreach (SudokuCell cell in CellCollection)
            {
                cell.PropertyChanged += ListBoardItem_PropertyChanged;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Manages the attachment and detachment of event handlers to SudokuCell objects 
        /// as they are added and removed from the CellCollection, 
        /// allowing the ViewModel to respond to changes in the cells of the Sudoku board.
        /// </summary>
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
        /// Synchronizes changes in the Value property of SudokuCell objects in the CellCollection 
        /// with the corresponding cells in the sudokuModel.Board, 
        /// allowing the ViewModel to keep the model's state consistent with the view's state.
        /// </summary>
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

        #region Commands

        private ICommand solveCommand;
        private ICommand clearCommand;
        private ICommand saveCommand;
        private ICommand loadFileCommand;
        private ICommand exitCommand;

        public ICommand SolveCommand
        {
            get
            {
                if (solveCommand == null)
                {
                    solveCommand = new RelayCommand(param => ClearBoardAndNotify(), param => CanClearBoard());
                }
                return solveCommand;
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                if (clearCommand == null)
                {
                    clearCommand = new RelayCommand(param => ClearBoardAndNotify(), param => CanClearBoard());
                }
                return clearCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new RelayCommand(param => SaveBoardAndNotify(), param => CanSaveBoard());
                }
                return saveCommand;
            }
        }

        public ICommand LoadFileCommand
        {
            get
            {
                if (loadFileCommand == null)
                {
                    loadFileCommand = new RelayCommand(param => LoadFileAndNotify(), param => CanLoadFile());
                }
                return loadFileCommand;
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                {
                    exitCommand = new RelayCommand(param => ExitApplication(), param => CanExitApplication());
                }
                return exitCommand;
            }
        }


        private void ExitApplication()
        {
            throw new NotImplementedException();
        }
        private bool CanExitApplication()
        {
            // Not Implemented;
            return true;
        }

        private void LoadFileAndNotify()
        {
            throw new NotImplementedException();
        }

        private bool CanLoadFile()
        {
            // Not Implemented;
            return true;
        }

        private void SaveBoardAndNotify()
        {
            throw new NotImplementedException();
        }
        private bool CanSaveBoard()
        {
            // Not Implemented;
            return true;
        }

        private void SolveBoardAndNotify()
        {
            throw new NotImplementedException();
        }
        private bool CanSolveBoard()
        {
            // Not Implemented;
            return true;
        }

        private void ClearBoardAndNotify()
        {
            throw new NotImplementedException();
        }

        private bool CanClearBoard()
        {
            // Not Implemented;
            return true;
        }

        #endregion
    }
}