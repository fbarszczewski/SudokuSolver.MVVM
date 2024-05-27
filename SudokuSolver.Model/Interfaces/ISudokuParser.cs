namespace SudokuSolver.Model.Interfaces
{
	public interface ISudokuParser
	{
		void Serialize(ISudokuFile sudokuData);
		void Deserialize(ISudokuFile sudokuData);
	}
}
