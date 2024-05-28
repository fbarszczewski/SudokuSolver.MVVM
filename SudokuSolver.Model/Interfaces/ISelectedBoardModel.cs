namespace SudokuSolver.Model.Interfaces
{
	public interface ISelectedBoardModel : ISudokuBoard
	{
		event Action? BoardChanged;

		void ClearBoard();

		bool IsEmpty();

		void RaiseBoardChanged();
	}
}
