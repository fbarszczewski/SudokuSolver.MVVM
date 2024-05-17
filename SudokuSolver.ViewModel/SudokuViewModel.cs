using SudokuSolver.Model;
using System.ComponentModel;

namespace SudokuSolver.ViewModel
{
    public class SudokuViewModel : INotifyPropertyChanged
    {
        private readonly SudokuBoard board;

        public byte this[byte i, byte j]
        {
            get => board[i, j];
            set
            {
                board[i, j] = value;
                OnPropertyChanged($"Item[{i},{j}]");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public SudokuViewModel()
        {
            board = new SudokuBoard();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
