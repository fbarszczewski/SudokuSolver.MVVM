using SudokuSolver.Model;
using System.ComponentModel;

namespace SudokuSolver.ViewModel
{
    public class SudokuViewModel : INotifyPropertyChanged
    {
        public byte[,] SudokuBoard { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly SudokuBoard sudokuModel;


        public SudokuViewModel()
        {
            sudokuModel = new SudokuBoard();
            SudokuBoard = sudokuModel.Board;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
