using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Services;

namespace SudokuSolver.Model.Models
{
	public class GamesManager : IGameManager
	{
		private readonly ISudokuDataManager _dataManager;
		private readonly SudokuSolverManager _solverManager;
		private readonly SugokuAPI _SugokuAPI;
		private int _nextBoardId = 0;
		private int NextBoardId => _nextBoardId++;

		public List<SudokuBoard> GameList { get; private set; }
		public int SelectedGameIndex { get; private set; }

		public event Action? GameChanged;

		public GamesManager(ISudokuDataManager dataManager)
		{
			_solverManager = new SudokuSolverManager();
			_dataManager = dataManager;
			_SugokuAPI = new SugokuAPI();
			GameList = new List<SudokuBoard>();
			AddGame(new SudokuBoard());
		}

		public void NextGame()
		{
			if(CanNextGame())
				SelectedGameIndex++;

			GameChanged?.Invoke();
		}

		public bool CanNextGame()
		{
			return SelectedGameIndex >= 0 && SelectedGameIndex < GameList.Count - 1;
		}
		public void PreviousGame()
		{
			if(CanPreviousGame())
				SelectedGameIndex--;

			GameChanged?.Invoke();
		}

		public bool CanPreviousGame()
		{
			return SelectedGameIndex > 0;
		}

		public void LoadGamesFromFile(string path)
		{
			var loadSudoku = new SudokuData(path);
			_dataManager.LoadSudoku(loadSudoku);

			foreach(SudokuBoard board in loadSudoku.Boards)
			{
				AddGame(board);
			}
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
			SudokuBoard board = GetSelectedGame();

			if(board == null)
				throw new Exception("Cant find selected game.");


			ISudokuData sudokuFile = new SudokuData(new List<SudokuBoard> { board },path);

			_dataManager.SaveSudoku(sudokuFile);
		}

		public SudokuBoard GetSelectedGame()
		{
			return GameList[SelectedGameIndex];
		}

		private void AddGame(SudokuBoard sudoku)
		{
			var newGame = new SudokuBoard(sudoku);
			GameList.Add(newGame);
			GameChanged?.Invoke();
		}

		public void ClearSelectedGame()
		{
			SudokuBoard game = GetSelectedGame();
			if(game == null)
				throw new Exception("Cant find selected game.");

			Array.Clear(game.Board,0,game.Board.Length);
			GameChanged?.Invoke();
		}

		public bool CanClearSelectedGame()
		{
			SudokuBoard game = GetSelectedGame();
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
			SudokuBoard selectedGame = GetSelectedGame();
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

		public async void GetUnsolvedSudoku(string difficultyLevel)
		{
			var contentToParse = await _SugokuAPI.GetSudoku(difficultyLevel.ToLower());

			if(contentToParse == null)
				throw new Exception("No data retrieved from suGOku API.");

			var sudokuFile = new SudokuData(contentToParse,"json");
			_dataManager.LoadSudoku(sudokuFile);

			foreach(SudokuBoard board in sudokuFile.Boards)
			{
				AddGame(board);
			}
		}
	}

}
