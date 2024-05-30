using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.Interfaces
{
	internal interface ISudokuSolver
	{
		bool Solve(SudokuBoard board);
		string GetName();
	}
}
