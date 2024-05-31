using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuData : IFileData
	{
		List<SudokuBoard> Boards { get; set; }
	}
}
