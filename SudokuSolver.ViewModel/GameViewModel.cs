using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using SudokuSolver.Model.Models;

namespace SudokuSolver.ViewModel
{
	public class GameViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<SudokuCell>? GameCells { get; private set; }
		public SudokuBoard? SelectedGameBoard { get; set; }

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
			// Detaching event handlers from the previous GameCells to avoid data leaks.
			if(GameCells != null)
			{
				foreach(SudokuCell cell in GameCells)
				{
					cell.PropertyChanged -= CollectionCell_PropertyChanged;
				}

				GameCells.CollectionChanged -= GameCells_CollectionChanged;
			}

			// Filling collection with the values from the model.
			GameCells = new ObservableCollection<SudokuCell>(
							selectedGame.Board.OfType<byte>().Select(b => new SudokuCell(b)));

			// Attaching event handler to the CollectionChanged event of GameCells.
			// This is necessary to synchronize the changes in the GameCells with the model's Board.
			GameCells.CollectionChanged += GameCells_CollectionChanged;

			// Attaching event handler to the PropertyChanged event of each SudokuCell in GameCells.
			// This is necessary to synchronize the changes in the Value property of the SudokuCell objects in the GameCells
			foreach(SudokuCell cell in GameCells)
			{
				cell.PropertyChanged += CollectionCell_PropertyChanged;
			}
		}

		private void GameCells_CollectionChanged(object? sender,NotifyCollectionChangedEventArgs e)
		{
			if(sender == null)
				return;

			if(e.NewItems != null)
				foreach(var newItem in e.NewItems)
					if(newItem is SudokuCell observableByte)
						observableByte.PropertyChanged += CollectionCell_PropertyChanged;

			if(e.OldItems != null)
				foreach(var oldItem in e.OldItems)
					if(oldItem is SudokuCell observableByte)
						observableByte.PropertyChanged -= CollectionCell_PropertyChanged;
		}

		/// <summary>
		/// Synchronizes changes in the Value property of SudokuCell objects in the GameCells  with the
		/// corresponding cells in the model.Board,  allowing the ViewModel to keep the model's state consistent with
		/// the view's state.
		/// </summary>
		private void CollectionCell_PropertyChanged(object? sender,PropertyChangedEventArgs e)
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
