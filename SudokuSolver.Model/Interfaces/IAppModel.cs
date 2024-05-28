namespace SudokuSolver.Model.Interfaces
{
	public interface IAppModel
	{
		public List<ISudokuBoard> BoardsList { get; set; }
		/// <summary>
		/// Represents the id of the selected board in view from BoardsList.
		/// </summary>
		public int CurrentBoardId { get; set; }


	}
}
