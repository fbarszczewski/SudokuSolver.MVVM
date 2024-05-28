namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuBoard
	{
		int Id { get; }
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
