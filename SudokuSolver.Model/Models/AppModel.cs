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
			BoardsList.Add(new CurrentBoardModel());
		}



	}
}
