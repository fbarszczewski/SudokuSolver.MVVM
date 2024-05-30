namespace SudokuSolver.Model.Models
{

	public class SudokuGame : SudokuBoard
	{
		public int Id
		{
			get;
			private set;
		}

		public SudokuGame(SudokuBoard sudokuBoard,int id)
		{
			Board = sudokuBoard.Board;
			DifficultyLevel = sudokuBoard.DifficultyLevel;
			Id = id;
		}
	}
}
