namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuDataManager
	{
		void LoadSudoku(ISudokuData sudokuFile);
		void SaveSudoku(ISudokuData sudokuFile);
	}
}
