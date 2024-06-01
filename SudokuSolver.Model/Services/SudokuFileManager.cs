using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Services
{
	public class SudokuFileManager : ISudokuDataManager
	{
		private readonly ISudokuParserFactory _parserFactory;

		public SudokuFileManager(ISudokuParserFactory parserFactory)
		{
			_parserFactory = parserFactory;
		}

		public void LoadSudoku(ISudokuData sudokuFile)
		{
			ISudokuParser parser = _parserFactory.GetParser(sudokuFile.DataType);
			parser.LoadBoards(sudokuFile);
		}

		public void SaveSudoku(ISudokuData sudokuFile)
		{
			ISudokuParser parser = _parserFactory.GetParser(sudokuFile.DataType);
			parser.SaveBoards(sudokuFile);
		}
	}
}
