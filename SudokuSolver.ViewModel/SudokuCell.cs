using System.ComponentModel;

namespace SudokuSolver.ViewModel
{
    public class SudokuCell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Value
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

        private string _value;

        public SudokuCell(string value)
        {
            _value = value;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static implicit operator byte(SudokuCell cellValue)
        {
            //convert to int
            int num;

            return !string.IsNullOrEmpty(cellValue.Value) && int.TryParse(cellValue.Value, out num) ? (byte)num : (byte)0;
        }

        public static implicit operator SudokuCell(byte value)
        {

            return 7;
        }
    }
}
