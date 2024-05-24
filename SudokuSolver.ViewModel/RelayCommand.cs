using System.Diagnostics;
using System.Windows.Input;

namespace SudokuSolver.ViewModel
{
	public class RelayCommand : ICommand
	{
		#region Fields

		private readonly Action<object> _execute;
		private readonly Predicate<object>? _canExecute;

		#endregion // Fields

		#region Constructor

		public RelayCommand(Action<object> execute,Predicate<object>? canExecute = null)
		{
			if(execute==null) throw new ArgumentNullException(nameof(execute));
			_execute=execute;
			_canExecute=canExecute;
		}

		#endregion // Constructor

		#region ICommand Members

		[DebuggerStepThrough]
#pragma warning disable CS8604 // Possible null reference argument.
		public bool CanExecute(object? parameter) => _canExecute==null||_canExecute(parameter);
#pragma warning restore CS8604 // Possible null reference argument.

		public event EventHandler? CanExecuteChanged
		{
			add
			{
				if(_canExecute!=null) CommandManager.RequerySuggested+=value;
			}
			remove
			{
				if(_canExecute!=null) CommandManager.RequerySuggested-=value;
			}

		}

#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
		public void Execute(object parameter) => _execute(parameter);
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).

		#endregion // ICommand Members
	}
}
