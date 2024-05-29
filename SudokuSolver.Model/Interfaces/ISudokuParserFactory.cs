namespace SudokuSolver.Model.Interfaces
{
	internal interface ISudokuParserFactory
	{
		ISudokuParser GetParser(string fileType);
	}
}
