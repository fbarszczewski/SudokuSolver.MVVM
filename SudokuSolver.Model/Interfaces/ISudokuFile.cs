using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuFile : IFileData
	{
		List<SudokuBoard> Boards { get; set; }
	}
}
