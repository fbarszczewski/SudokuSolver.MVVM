using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Models
{
	public class SudokuBoard : ISudokuBoard
	{
		public byte[,] Board { get; set; }
		public string? DifficultyLevel { get; set; }

		public SudokuBoard()
		{
			Board = new byte[9,9];
		}

		public SudokuBoard(byte[,] board)
		{
			Board = board;
		}

		public SudokuBoard(byte[,] board,string difficultyLevel)
		{
			Board = board;
			DifficultyLevel = difficultyLevel;
		}

		public SudokuBoard(byte[,] board,string difficultyLevel,int id)
		{
			Board = board;
			DifficultyLevel = difficultyLevel;
		}
	}
}
