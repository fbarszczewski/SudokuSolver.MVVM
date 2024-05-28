namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuBoard
	{
		byte[,] Board { get; set; }
		string? DifficultyLevel { get; set; }
	}
}
