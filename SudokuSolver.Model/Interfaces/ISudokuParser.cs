namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuParser
	{
		void LoadBoards(ISudokuFile sudokuData);
		void SaveBoards(ISudokuFile sudokuData);
	}
}
