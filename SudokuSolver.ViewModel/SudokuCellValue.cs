using System.ComponentModel;

namespace SudokuSolver.ViewModel
{
    public class SudokuCellValue : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal byte Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        private byte _value;

        public SudokuCellValue(byte value)
        {
            _value = value;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static implicit operator byte(SudokuCellValue observableByte)
        {
            return observableByte.Value;
        }

        public static implicit operator SudokuCellValue(byte value)
        {
            return new SudokuCellValue(value);
        }
    }
}
