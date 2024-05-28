using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.ViewModel
{
	public class SudokuViewModel : INotifyPropertyChanged
	{
		private readonly IAppModel model;

		private byte[,] currentBoard => model.BoardsList[selectedBoardId].Board;

		private int selectedBoardId => model.SelectedBoardId;

		private ISelectedBoardModel selectedBoardModel => model.BoardsList[selectedBoardId];

		public ObservableCollection<SudokuCell> CellCollection
		{
			get;
			private set;
		}
		public string PageNumbers => $"{selectedBoardId + 1} of {model.BoardsList.Count} ";

		public event PropertyChangedEventHandler? PropertyChanged;

		public SudokuViewModel(IAppModel _model)
		{
			model = _model;
			selectedBoardModel.BoardChanged += SudokuModel_BoardChanged;
			CellCollection = new ObservableCollection<SudokuCell>();
			InitializeCellCollection();
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
		}

		private void InitializeCellCollection()
		{
			// Detaching event handlers from the previous CellCollection to avoid data leaks.
			if(CellCollection != null)
			{
				foreach(SudokuCell cell in CellCollection)
				{
					cell.PropertyChanged -= ListBoardItem_PropertyChanged;
				}

				CellCollection.CollectionChanged -= ListBoard_CollectionChanged;
			}

			// Filling the collection with the values from the model.
			// Value type is converted in SudokuCell constructor & every '0' is replaced with empty string.
			CellCollection = new ObservableCollection<SudokuCell>(
							currentBoard.OfType<byte>().Select(b => new SudokuCell(b)));

			// Attaching event handler to the CollectionChanged event of CellCollection.
			// This is necessary to synchronize the changes in the CellCollection with the model's Board.
			CellCollection.CollectionChanged += ListBoard_CollectionChanged;

			// Attaching event handler to the PropertyChanged event of each SudokuCell in CellCollection.
			// This is necessary to synchronize the changes in the Value property of the SudokuCell objects in the CellCollection
			foreach(SudokuCell cell in CellCollection)
			{
				cell.PropertyChanged += ListBoardItem_PropertyChanged;
			}
		}

		/// <summary>
		/// Manages the attachment and detachment of event handlers to SudokuCell objects in the CellCollection
		/// </summary>
		private void ListBoard_CollectionChanged(object? sender,NotifyCollectionChangedEventArgs e)
		{
			if(sender == null)
				return;
			if(e.NewItems != null)
			{
				foreach(var newItem in e.NewItems)
				{
					if(newItem is SudokuCell observableByte)
					{
						AttachPropertyChangedHandler(observableByte);
					}
				}
			}

			if(e.OldItems != null)
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

		private void AttachPropertyChangedHandler(SudokuCell cell)
		{
			cell.PropertyChanged += ListBoardItem_PropertyChanged;
		}

		private void DetachPropertyChangedHandler(SudokuCell cell)
		{
			cell.PropertyChanged -= ListBoardItem_PropertyChanged;
		}

		/// <summary>
		/// Synchronizes changes in the Value property of SudokuCell objects in the CellCollection  with the
		/// corresponding cells in the model.Board,  allowing the ViewModel to keep the model's state consistent with
		/// the view's state.
		/// </summary>
		private void ListBoardItem_PropertyChanged(object? sender,PropertyChangedEventArgs e)
		{
			if(sender == null)
				return;

			if(e.PropertyName == "Value")
			{
				var cell = (SudokuCell)sender;
				var index = CellCollection.IndexOf(cell);

				var row = index / 9;
				var col = index % 9;

				currentBoard[row,col] = cell;
			}
		}

		/// <summary>
		/// Updates the CellCollection when the model's Board changes . Changes are invoked by the model's BoardChanged
		/// event in SelectedBoardModel model.
		/// </summary>
		private void SudokuModel_BoardChanged()
		{
			InitializeCellCollection();
			OnPropertyChanged(nameof(CellCollection));
		}

		#region Commands
		private ICommand? solveCommand;
		private ICommand? clearCommand;
		private ICommand? saveCommand;
		private ICommand? loadFileCommand;
		private ICommand? previousCommand;
		private ICommand? nextCommand;


		public ICommand SolveCommand
		{
			get
			{
				solveCommand = solveCommand ?? new RelayCommand(param => SolveBoardAndNotify(),param => CanSolveBoard());
				return solveCommand;
			}
		}

		public ICommand ClearCommand
		{
			get
			{
				clearCommand = clearCommand ?? new RelayCommand(param => ClearBoardAndNotify(),param => CanClearBoard());
				return clearCommand;
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				saveCommand = saveCommand ?? new RelayCommand(param => SaveBoardAndNotify(),param => CanSaveBoard());
				return saveCommand;
			}
		}

		public ICommand LoadFileCommand
		{
			get
			{
				loadFileCommand = loadFileCommand ??
					new RelayCommand(param => LoadFileAndNotify(),param => CanLoadFile());
				return loadFileCommand;
			}
		}

		public ICommand PreviousCommand
		{
			get
			{
				previousCommand = previousCommand ?? new RelayCommand(param => PreviousAndNotify(),param => CanPrevious());
				return previousCommand;
			}
		}

		public ICommand NextCommand
		{
			get
			{
				nextCommand = nextCommand ?? new RelayCommand(param => NextAndNotify(),param => CanNext());
				return nextCommand;
			}
		}

		private void PreviousAndNotify()
		{
			throw new NotImplementedException();
		}

		private bool CanPrevious()
		{
			// Not Implemented;
			return true;
		}

		private void NextAndNotify()
		{
			throw new NotImplementedException();
		}

		private bool CanNext()
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
			return false;
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
			selectedBoardModel.ClearBoard();
		}

		private bool CanClearBoard()
		{
			return !selectedBoardModel.IsEmpty();
		}
		#endregion
	}
}