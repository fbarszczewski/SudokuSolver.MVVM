using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.Interfaces
{
	public interface IAppModel
	{
		List<SelectedBoardModel> BoardsList { get; }
		/// <summary>
		/// Represents the id of the selected board in view from BoardsList.
		/// </summary>
		int SelectedBoardId { get; set; }

		bool SaveCurrentBoard(string path);
	}
}
