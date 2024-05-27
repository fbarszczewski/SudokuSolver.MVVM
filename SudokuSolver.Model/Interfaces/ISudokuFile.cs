namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuFile
	{
		IEnumerable<byte[,]> Boards { get; set; }
		string Schema { get; set; }
	}
}
