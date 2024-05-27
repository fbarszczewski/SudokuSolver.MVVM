namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuFile : IFileData
	{
		IEnumerable<byte[,]> Boards { get; set; }
		string Schema { get; set; }
	}
}
