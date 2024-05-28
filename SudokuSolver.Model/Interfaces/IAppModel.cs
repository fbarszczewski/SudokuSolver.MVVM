namespace SudokuSolver.Model.Interfaces
{
	public interface IAppModel
	{
		public List<ISudokuBoard> BoardsList { get; set; }
		// Id of the selected board in view from BoardsList
		public int CurrentBoardIndex { get; set; }


		void AddBoard(ISudokuBoard board);
		void RemoveBoard(int index);
		void SelectCurrentBoard(int index);
	}
}
