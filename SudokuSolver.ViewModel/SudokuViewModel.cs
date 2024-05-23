using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using SudokuSolver.Model;

namespace SudokuSolver.ViewModel
{
	public class SudokuViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public ObservableCollection<SudokuCell> CellCollection
		{
			get; private set;
		}

		private readonly ISudokuBoard sudokuModel;


		public SudokuViewModel(ISudokuBoard _model)
		{
			sudokuModel=_model;
			sudokuModel.BoardChanged+=SudokuModel_BoardChanged;
			CellCollection=new ObservableCollection<SudokuCell>();
			InitializeCellCollection();

		}


		protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));

		private void InitializeCellCollection()
		{
			// Detaching event handlers from the previous CellCollection to avoid data leaks.
			if(CellCollection!=null)
			{
				foreach(SudokuCell cell in CellCollection)
				{
					cell.PropertyChanged-=ListBoardItem_PropertyChanged;
				}

				CellCollection.CollectionChanged-=ListBoard_CollectionChanged;
			}

			// Filling the collection with the values from the model.
			// Value type is converted in SudokuCell constructor & every '0' is replaced with empty string.
			CellCollection=new ObservableCollection<SudokuCell>(
				sudokuModel.Board.OfType<byte>().Select(b => new SudokuCell(b)));
			// Attaching event handler to the CollectionChanged event of CellCollection.
			// This is necessary to synchronize the changes in the CellCollection with the model's Board.
			CellCollection.CollectionChanged+=ListBoard_CollectionChanged;
			// Attaching event handler to the PropertyChanged event of each SudokuCell in CellCollection.
			// This is necessary to synchronize the changes in the Value property of the SudokuCell objects in the CellCollection
			foreach(SudokuCell cell in CellCollection)
			{
				cell.PropertyChanged+=ListBoardItem_PropertyChanged;
			}
		}

		/// <summary>
		/// Manages the attachment and detachment of event handlers to SudokuCell objects in the CellCollection
		/// </summary>
		private void ListBoard_CollectionChanged(object? sender,NotifyCollectionChangedEventArgs e)
		{
			if(sender==null)
				return;
			{

			}
			if(e.NewItems!=null)
			{
				foreach(var newItem in e.NewItems)
				{
					if(newItem is SudokuCell observableByte)
					{
						AttachPropertyChangedHandler(observableByte);
					}
				}
			}

			if(e.OldItems!=null)
			{
				foreach(var oldItem in e.OldItems)
				{
					if(oldItem is SudokuCell observableByte)
					{
						DetachPropertyChangedHandler(observableByte);
					}
				}
			}
		}

		/// <summary>
		/// Updates the CellCollection when the model's Board changes . Changes are invoked by the model's BoardChanged
		/// event in SudokuBoard model.
		/// </summary>
		private void SudokuModel_BoardChanged()
		{
			InitializeCellCollection();
			OnPropertyChanged(nameof(CellCollection));
		}

		private void AttachPropertyChangedHandler(SudokuCell cell) => cell.PropertyChanged+=ListBoardItem_PropertyChanged;

		private void DetachPropertyChangedHandler(SudokuCell cell) => cell.PropertyChanged-=ListBoardItem_PropertyChanged;

		/// <summary>
		/// Synchronizes changes in the Value property of SudokuCell objects in the CellCollection  with the
		/// corresponding cells in the sudokuModel.Board,  allowing the ViewModel to keep the model's state consistent
		/// with the view's state.
		/// </summary>
		private void ListBoardItem_PropertyChanged(object? sender,PropertyChangedEventArgs e)
		{
			if(sender==null)
				return;

			if(e.PropertyName=="Value")
			{
				var cell = (SudokuCell)sender;
				var index = CellCollection.IndexOf(cell);

				var row = index/9;
				var col = index%9;

				sudokuModel.Board[row,col]=cell;
			}
		}

		#region Commands
		private ICommand? solveCommand;
		private ICommand? clearCommand;
		private ICommand? saveCommand;
		private ICommand? loadFileCommand;
		private ICommand? exitCommand;

		public ICommand SolveCommand
		{
			get
			{
				solveCommand=solveCommand??new RelayCommand(param => SolveBoardAndNotify(),param => CanSolveBoard());
				return solveCommand;
			}
		}

		public ICommand ClearCommand
		{
			get
			{
				clearCommand=clearCommand??new RelayCommand(param => ClearBoardAndNotify(),param => CanClearBoard());
				return clearCommand;
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				saveCommand=saveCommand??new RelayCommand(param => SaveBoardAndNotify(),param => CanSaveBoard());
				return saveCommand;
			}
		}

		public ICommand LoadFileCommand
		{
			get
			{
				loadFileCommand=loadFileCommand??
					new RelayCommand(param => LoadFileAndNotify(),param => CanLoadFile());
				return loadFileCommand;
			}
		}

		public ICommand ExitCommand
		{
			get
			{
				exitCommand=exitCommand??new RelayCommand(param => ExitApplication(),param => CanExitApplication());
				return exitCommand;
			}
		}


		private void ExitApplication() => throw new NotImplementedException();

		private bool CanExitApplication() =>
			// Not Implemented;
			true;

		private void LoadFileAndNotify() => throw new NotImplementedException();

		private bool CanLoadFile() =>
			// Not Implemented;
			true;

		private void SaveBoardAndNotify() => throw new NotImplementedException();

		private bool CanSaveBoard() =>
			// Not Implemented;
			true;

		private void SolveBoardAndNotify() => throw new NotImplementedException();

		private bool CanSolveBoard() =>
			// Not Implemented;
			true;

		private void ClearBoardAndNotify() => sudokuModel.ClearBoard();

		private bool CanClearBoard() => !sudokuModel.IsEmpty();
		#endregion
	}
}