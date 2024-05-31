using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Services;

namespace SudokuSolver.Model.Models
{
	public class SudokuGameManager : IGameManager
	{
		private readonly ISudokuDataManager _dataManager;
		private int _nextBoardId = 0;
		private readonly SudokuSolverManager _solverManager;
		private int NextBoardId => _nextBoardId++;

		public List<SudokuGame> GameList { get; private set; }
		public int SelectedGameId { get; private set; }

		public event Action? GameChanged;

		public SudokuGameManager(ISudokuDataManager dataManager)
		{
			_solverManager = new SudokuSolverManager();
			_dataManager = dataManager;
			GameList = new List<SudokuGame>();
			AddGame(new SudokuBoard());
		}

		public bool CanNextGame()
		{
			var currentIndex = GameList.FindIndex(board => board.Id == SelectedGameId);
			return currentIndex >= 0 && currentIndex < GameList.Count - 1;
		}

		public bool CanPreviousGame()
		{
			var currentIndex = GameList.FindIndex(board => board.Id == SelectedGameId);
			return currentIndex > 0;
		}

		public void LoadGamesFromFile(string path)
		{
			var loadSudoku = new SudokuFile(path);
			_dataManager.LoadSudoku(loadSudoku);

			foreach(SudokuBoard board in loadSudoku.Boards)
			{
				AddGame(board);
			}
		}

		public void NextGame()
		{
			var currentIndex = GameList.FindIndex(board => board.Id == SelectedGameId);
			if(currentIndex >= 0 && currentIndex < GameList.Count - 1)
			{
				SelectedGameId = GameList[currentIndex + 1].Id;
			}
			GameChanged?.Invoke();
		}

		public void PreviousGame()
		{
			var currentIndex = GameList.FindIndex(board => board.Id == SelectedGameId);
			if(currentIndex > 0)
			{
				SelectedGameId = GameList[currentIndex - 1].Id;
			}
			GameChanged?.Invoke();
		}

		public void RemoveGame()
		{
			throw new NotImplementedException();
		}

		public void AddEmptyGame()
		{
			AddGame(new SudokuBoard());
		}


		public void SaveSelectedGame(string path)
		{
			SudokuBoard? board = ReturnSelectedGame();

			if(board == null)
				throw new Exception("Cant find selected game.");


			ISudokuFile sudokuFile = new SudokuFile(new List<SudokuBoard> { board },path);

			_dataManager.SaveSudoku(sudokuFile);
		}

		public SudokuGame? ReturnSelectedGame()
		{
			return GameList.FirstOrDefault(board => board.Id == SelectedGameId);
		}

		private void AddGame(SudokuBoard sudoku)
		{
			var newGame = new SudokuGame(sudoku,NextBoardId);
			GameList.Add(newGame);
			SelectedGameId = newGame.Id;
			GameChanged?.Invoke();
		}

		public void ClearSelectedGame()
		{
			SudokuGame? game = ReturnSelectedGame();
			if(game == null)
				throw new Exception("Cant find selected game.");

			Array.Clear(game.Board,0,game.Board.Length);
			GameChanged?.Invoke();
		}

		public bool CanClearSelectedGame()
		{
			SudokuGame? game = ReturnSelectedGame();
			if(game == null)
				throw new Exception("Cant find selected game.");

			foreach(var value in game.Board)
			{
				if(value != 0)
				{
					return true;
				}
			}

			return false;
		}

		public bool SolveSudoku(ref string firstAlgorithm)
		{
			SudokuGame? selectedGame = ReturnSelectedGame();
			var board = selectedGame.Board;

			if(selectedGame == null)
				throw new Exception("Cant find selected game.");

			var isSolved = _solverManager.Solve(ref board,ref firstAlgorithm);

			if(isSolved)
			{
				selectedGame.Board = board;
			}

			return isSolved;


		}

		public List<string> GetSolvingAlgorithmsNames()
		{
			return _solverManager.GetSolverNames();
		}
	}

}
