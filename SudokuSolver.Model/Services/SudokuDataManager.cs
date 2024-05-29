using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Services
{
	public class SudokuDataManager : ISudokuDataManager
	{
		private readonly ISudokuParser _parser;

		public SudokuDataManager(ISudokuParser parser)
		{
			_parser = parser;
		}

		public void LoadSudoku(ISudokuFile sudokuFile)
		{
			_parser.LoadBoards(sudokuFile);
		}

		public void SaveSudoku(ISudokuFile sudokuFile)
		{
			_parser.SaveBoards(sudokuFile);
		}
	}
}
