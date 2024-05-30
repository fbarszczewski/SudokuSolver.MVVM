namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuParserFactory
	{
		ISudokuParser GetParser(string fileType);
	}
}
