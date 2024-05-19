using System.ComponentModel;

namespace SudokuSolver.ViewModel
{
    public class SudokuCell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public byte Value
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

        public SudokuCell(byte value)
        {
            _value = value;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static implicit operator byte(SudokuCell observableByte)
        {
            return observableByte.Value;
        }

        public static implicit operator SudokuCell(byte value)
        {
            return new SudokuCell(value);
        }
    }
}
