namespace SudokuSolver.Model.Interfaces
{
	internal interface ISudokuSolver
	{
		bool Solve(ref byte[,] sudokuBoard);
		string GetName();
	}
}
