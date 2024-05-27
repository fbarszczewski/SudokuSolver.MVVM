namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuFile : IFileData
	{
		List<byte[,]> Boards { get; set; }
	}
}
