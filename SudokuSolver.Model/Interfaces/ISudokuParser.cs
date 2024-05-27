namespace SudokuSolver.Model.Interfaces
{
	internal interface ISudokuParser
	{
		void Serialize(ISudokuFile sudokuData);
		void Deserialize(ISudokuFile sudokuData);
	}
}
