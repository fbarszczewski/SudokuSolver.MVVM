using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Models;

namespace SudokuSolver.ViewModel
{
	public class GameManagerViewModel : INotifyPropertyChanged
	{
		private readonly IGameManager gameManagerModel;
		public event PropertyChangedEventHandler? PropertyChanged;
		private SudokuGame? selectedGame;
		public string? PageNumber { get; set; }
		public ObservableCollection<SudokuCell> CellCollection
		{
			get;
			private set;
		}


		public GameManagerViewModel(IGameManager _model)
		{
			gameManagerModel = _model;

			UpdateSelectedGame();
			UpdatePageNumber();
			gameManagerModel.GameChanged += RefreshView;
			CellCollection = new ObservableCollection<SudokuCell>();
			InitializeCellCollection();
		}

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
		}

		private void UpdatePageNumber()
		{
			PageNumber = $"{gameManagerModel.SelectedGameId + 1} of {gameManagerModel.GameList.Count}";
		}
		private void RefreshView()
		{
			UpdateSelectedGame();

			InitializeCellCollection();
			UpdatePageNumber();
			OnPropertyChanged(nameof(CellCollection));
			OnPropertyChanged(nameof(PageNumber));
		}
		private void UpdateSelectedGame()
		{
			SudokuGame? selectedGame = gameManagerModel.ReturnSelectedGame();
			try
			{
				if(selectedGame == null)
					throw new Exception("Cant find selected game.");
			}
			catch(Exception ex)
			{
				MessageBox.Show($"Error:{ex.Message}\nRedirecting to default board.");
				if(gameManagerModel.GameList.Count == 0)
				{
					gameManagerModel.AddEmptyGame();
				}
			}


			this.selectedGame = selectedGame;
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

			//getting selected game

			// Filling the collection with the values from the model.
			// Value type is converted in SudokuCell constructor & every '0' is replaced with empty string.
			CellCollection = new ObservableCollection<SudokuCell>(
							selectedGame.Board.OfType<byte>().Select(b => new SudokuCell(b)));

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

				selectedGame.Board[row,col] = cell;
			}
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
				clearCommand = clearCommand ?? new RelayCommand(param => gameManagerModel.ClearSelectedGame(),param => gameManagerModel.CanClearSelectedGame());
				return clearCommand;
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				saveCommand = saveCommand ?? new RelayCommand(param => SaveBoard(),param => true);
				return saveCommand;
			}
		}

		public ICommand LoadFileCommand
		{
			get
			{
				loadFileCommand = loadFileCommand ?? new RelayCommand(param => LoadFile(),param => true);
				return loadFileCommand;
			}
		}

		public ICommand PreviousCommand
		{
			get
			{
				previousCommand = previousCommand ?? new RelayCommand(param => gameManagerModel.PreviousGame(),param => gameManagerModel.CanPreviousGame());
				return previousCommand;
			}
		}

		public ICommand NextCommand
		{
			get
			{
				nextCommand = nextCommand ?? new RelayCommand(param => gameManagerModel.NextGame(),param => gameManagerModel.CanNextGame());
				return nextCommand;
			}
		}

		private void LoadFile()
		{
			var openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			openFileDialog.Filter = "JSON (*.json)|*.json|Text file (*.txt)|*.txt|XML (*.xml)|*.xml";
			openFileDialog.ShowDialog();
			try
			{
				gameManagerModel.LoadGamesFromFile(openFileDialog.FileName);
			}
			catch(Exception ex)
			{
				MessageBox.Show($"Cant load file. {ex.Message}");
			}
		}

		private void SaveBoard()
		{
			var saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "JSON (*.json)|*.json|Text file (*.txt)|*.txt|XML (*.xml)|*.xml";
			saveFileDialog.ShowDialog();

			try
			{
				gameManagerModel.SaveSelectedGame(saveFileDialog.FileName);
				MessageBox.Show("Saved");
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		private void SolveBoardAndNotify()
		{
			MessageBox.Show("Not implemented");
		}

		private bool CanSolveBoard()
		{
			// Not Implemented;
			return true;
		}

		#endregion
	}
}
