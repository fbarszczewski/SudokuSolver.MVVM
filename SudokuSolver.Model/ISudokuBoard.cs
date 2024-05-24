namespace SudokuSolver.Model
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
