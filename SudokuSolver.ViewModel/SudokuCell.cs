// Ignore Spelling: Sudoku

using System.ComponentModel;

namespace SudokuSolver.ViewModel
{
	public class SudokuCell : INotifyPropertyChanged
	{
		private string value;

		public SudokuCell(byte _value)
		{
			this.value=_value==0 ? string.Empty : _value.ToString();
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public string Value
		{
			get => value;
			set
			{
				if(this.value!=value)
				{
					this.value=value=="0" ? string.Empty : value;

					OnPropertyChanged(nameof(Value));
				}
			}
		}

		public static implicit operator byte(SudokuCell cellValue) => !string.IsNullOrEmpty(cellValue.Value)
																   &&int.TryParse(cellValue.Value,out var num)
																   ? (byte)num : (byte)0;

		protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
	}
}
