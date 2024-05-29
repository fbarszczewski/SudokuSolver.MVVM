using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Models
{

	public class AppModel : IAppModel
	{
		private readonly ISudokuDataManager _dataManager;
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

		public AppModel(ISudokuDataManager dataManager)
		{
			_dataManager = dataManager;
			BoardsList = new List<SelectedBoardModel>();

			AddBoardToListAndSetAsSelected(new SudokuBoard());
		}


		public void LoadBoardsFromFile(string path)
		{
			var loadSudoku = new SudokuFile(path);
			_dataManager.LoadSudoku(loadSudoku);

			foreach(SudokuBoard board in loadSudoku.Boards)
			{
				AddBoardToListAndSetAsSelected(board);
			}

		}
		public void SaveCurrentBoard(string path)
		{
			SelectedBoardModel? selectedBoard = BoardsList.FirstOrDefault(board => board.Id == SelectedBoardId);

			if(selectedBoard == null)
				throw new Exception("No board is currently selected.");

			SudokuBoard board = selectedBoard;
			ISudokuFile sudokuFile = new SudokuFile(new List<SudokuBoard>() { board },path);

			_dataManager.SaveSudoku(sudokuFile);
		}

		/// <summary>
		/// Adds board to the BoardsList & set it as selected board
		/// </summary>
		/// <param name="board">The board to add.</param>
		public void AddBoardToListAndSetAsSelected(ISudokuBoard board)
		{
			var boardModel = new SelectedBoardModel(board,NextBoardId);
			BoardsList.Add(boardModel);
			SelectedBoardId = boardModel.Id;
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
