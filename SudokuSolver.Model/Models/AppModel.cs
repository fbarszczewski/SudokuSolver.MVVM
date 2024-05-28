using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Models
{
	public class AppModel : IAppModel
	{
		public List<ISudokuBoard> BoardsList { get; set; }
		public int CurrentBoardIndex { get; set; }

		public AppModel()
		{
			BoardsList = new List<ISudokuBoard>();
			AddBoard(new CurrentBoardModel());
		}

		public void AddBoard(ISudokuBoard board)
		{
			BoardsList.Add(board);
		}


	}
}
