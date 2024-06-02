using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using SudokuSolver.Model.Models;

namespace SudokuSolver.ViewModel
{
	public class GameViewModel : INotifyPropertyChanged
	{
		private SudokuBoard? _selectedGameBoard;
		public SudokuBoard? SelectedGameBoard
		{
			get => _selectedGameBoard;
			set
			{
				if(_selectedGameBoard != value)
				{
					DetachEventHandlers();
					_selectedGameBoard = value;
					InitializeGameCells(value);
					OnPropertyChanged(nameof(SelectedGameBoard));
				}
			}
		}
		public ObservableCollection<SudokuCell>? GameCells { get; private set; }

		public event PropertyChangedEventHandler? PropertyChanged;

		public GameViewModel()
		{

		}

		public void UpdateGameBoard(SudokuBoard updatedBoard)
		{
			SelectedGameBoard = updatedBoard;
			InitializeGameCells(updatedBoard);
			OnPropertyChanged(nameof(SelectedGameBoard));
		}

		private void InitializeGameCells(SudokuBoard selectedGame)
		{
			// Filling collection with the values from the model.
			var cells = new List<SudokuCell>(81);
			foreach(var b in selectedGame.Board)
			{
				var cell = new SudokuCell(b);
				cell.PropertyChanged += GameCell_PropertyChanged;
				cells.Add(cell);
			}
			GameCells = new ObservableCollection<SudokuCell>(cells);

			// Attaching event handler to the CollectionChanged event of GameCells.
			// This is necessary to synchronize the changes in the GameCells with the model's Board.
			GameCells.CollectionChanged += GameCells_CollectionChanged;
		}
		private void DetachEventHandlers()
		{
			if(GameCells != null)
			{
				foreach(SudokuCell cell in GameCells)
				{
					cell.PropertyChanged -= GameCell_PropertyChanged;
				}

				GameCells.CollectionChanged -= GameCells_CollectionChanged;
			}
		}

		private void GameCells_CollectionChanged(object? sender,NotifyCollectionChangedEventArgs e)
		{
			if(sender == null)
				return;

			if(e.NewItems != null)
				foreach(var newItem in e.NewItems)
					if(newItem is SudokuCell observableByte)
						observableByte.PropertyChanged += GameCell_PropertyChanged;

			if(e.OldItems != null)
				foreach(var oldItem in e.OldItems)
					if(oldItem is SudokuCell observableByte)
						observableByte.PropertyChanged -= GameCell_PropertyChanged;
		}

		/// <summary>
		/// Synchronizes changes in the Value property of SudokuCell objects in the GameCells  with the
		/// corresponding cells in the model.Board,  allowing the ViewModel to keep the model's state consistent with
		/// the view's state.
		/// </summary>
		private void GameCell_PropertyChanged(object? sender,PropertyChangedEventArgs e)
		{
			if(sender == null)
				return;

			if(e.PropertyName == "Value")
			{
				var cell = (SudokuCell)sender;
				var index = GameCells.IndexOf(cell);

				var row = index / 9;
				var col = index % 9;

				SelectedGameBoard!.Board[row,col] = cell;
			}
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
		}
	}
}
