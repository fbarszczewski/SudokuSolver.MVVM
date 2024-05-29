using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Models
{

	public class AppModel : IAppModel
	{
		private int _nextBoardId = 0;

		public List<SelectedBoardModel> BoardsList { get; private set; }
		/// <summary>
		/// Gets the next board id and increments the id counter.
		/// </summary>
		public int NextBoardId => _nextBoardId++;
		/// <summary>
		/// Represents the id of the selected board in view from BoardsList.
		/// </summary>
		public int SelectedBoardId { get; set; }

		public AppModel()
		{
			BoardsList = new List<SelectedBoardModel>();

			AddBoardToList(new SudokuBoard());
		}

		/// <summary>
		/// Adds board to the BoardsList.
		/// </summary>
		/// <param name="board">The board to add.</param>
		public void AddBoardToList(ISudokuBoard board)
		{
			BoardsList.Add(new SelectedBoardModel(board,NextBoardId));
		}

		/// <summary>
		/// Removes a board with a specific id from the BoardsList.
		/// </summary>
		/// <param name="id">The id of the board to remove.</param>
		public void RemoveBoardFromList(int id)
		{
			SelectedBoardModel? boardToRemove = BoardsList.FirstOrDefault(board => board.Id == id);
			if(boardToRemove != null)
			{
				BoardsList.Remove(boardToRemove);
			}
		}

		/// <summary>
		/// Selects the next board in the BoardsList.
		/// </summary>
		public void SelectNextBoardFromList()
		{
			var currentIndex = BoardsList.FindIndex(board => board.Id == SelectedBoardId);
			if(currentIndex >= 0 && currentIndex < BoardsList.Count - 1)
			{
				SelectedBoardId = BoardsList[currentIndex + 1].Id;
			}
		}

		public bool CanSelectNextBoardFromList()
		{
			var currentIndex = BoardsList.FindIndex(board => board.Id == SelectedBoardId);
			return currentIndex >= 0 && currentIndex < BoardsList.Count - 1;
		}

		/// <summary>
		/// Selects the previous board in the BoardsList.
		/// </summary>
		public void SelectPreviousBoardFromList()
		{
			var currentIndex = BoardsList.FindIndex(board => board.Id == SelectedBoardId);
			if(currentIndex > 0)
			{
				SelectedBoardId = BoardsList[currentIndex - 1].Id;
			}
		}

		public bool CanSelectPreviousBoardFromList()
		{
			var currentIndex = BoardsList.FindIndex(board => board.Id == SelectedBoardId);
			return currentIndex > 0;
		}
	}
}
