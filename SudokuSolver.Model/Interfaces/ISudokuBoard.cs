namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuBoard
	{
		byte[,] Board
		{
			get;
			set;
		}

		event Action? BoardChanged;

		void ClearBoard();
		bool IsEmpty();
		void RaiseBoardChanged();
	}
}
