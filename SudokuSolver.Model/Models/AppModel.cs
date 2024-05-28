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

		public void RemoveBoard(int index)
		{
			throw new NotImplementedException();
		}

		public void SelectCurrentBoard(int index)
		{
			throw new NotImplementedException();
		}
	}
}
