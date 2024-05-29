namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuDataManager
	{
		void LoadSudoku(ISudokuFile sudokuFile);
		void SaveSudoku(ISudokuFile sudokuFile);
	}
}
