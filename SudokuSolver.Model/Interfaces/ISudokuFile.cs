namespace SudokuSolver.Model.Interfaces
{
	internal interface ISudokuFile
	{
		IEnumerable<byte[,]> Boards { get; set; }
		string Schema { get; set; }
	}
}
