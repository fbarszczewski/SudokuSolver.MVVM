// Ignore Spelling: Sudoku

using System.ComponentModel;

namespace SudokuSolver.ViewModel
{
    public class SudokuCell : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string Value
        {
            get => value;
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        private string value;

        public SudokuCell(byte _value)
        {
            this.value = _value == 0 ? string.Empty : _value.ToString();
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


    }
}
