namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuParser
	{
		void LoadBoards(ISudokuData sudokuData);
		void SaveBoards(ISudokuData sudokuData);
	}
}
