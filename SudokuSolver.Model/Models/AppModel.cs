using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Models
{

	public class AppModel : IAppModel
	{
		private int _boardId = 0;
		public List<ISudokuBoard> BoardsList { get; set; }
		/// <summary>
		/// Represents the id of the selected board in view from BoardsList.
		/// </summary>
		public int CurrentBoardId { get; set; }

		public AppModel()
		{
			BoardsList = new List<ISudokuBoard>();
			BoardsList.Add(new CurrentBoardModel(new byte[9,9],_boardId));
		}

		/// <summary>
		/// Adds a new board to the BoardsList.
		/// </summary>
		/// <param name="board">The board to add.</param>
		public void AddNewBoardToList(ISudokuBoard board)
		{
			BoardsList.Add(board);
			_boardId++;
			CurrentBoardId = _boardId;
		}

		/// <summary>
		/// Removes a board with a specific id from the BoardsList.
		/// </summary>
		/// <param name="id">The id of the board to remove.</param>
		public void RemoveCurrentBoardFromList(int id)
		{
			ISudokuBoard? boardToRemove = BoardsList.FirstOrDefault(board => board.Id == id);
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
			var currentIndex = BoardsList.FindIndex(board => board.Id == CurrentBoardId);
			if(currentIndex >= 0 && currentIndex < BoardsList.Count - 1)
			{
				CurrentBoardId = BoardsList[currentIndex + 1].Id;
			}
		}

		public bool CanSelectNextBoardFromList()
		{
			var currentIndex = BoardsList.FindIndex(board => board.Id == CurrentBoardId);
			return currentIndex >= 0 && currentIndex < BoardsList.Count - 1;
		}

		/// <summary>
		/// Selects the previous board in the BoardsList.
		/// </summary>
		public void SelectPreviousBoardFromList()
		{
			var currentIndex = BoardsList.FindIndex(board => board.Id == CurrentBoardId);
			if(currentIndex > 0)
			{
				CurrentBoardId = BoardsList[currentIndex - 1].Id;
			}
		}

		public bool CanSelectPreviousBoardFromList()
		{
			var currentIndex = BoardsList.FindIndex(board => board.Id == CurrentBoardId);
			return currentIndex > 0;
		}
	}
}
