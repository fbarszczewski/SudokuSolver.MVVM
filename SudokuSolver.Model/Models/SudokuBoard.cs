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


		public static bool IsValidBoard(SudokuBoard board)
		{
			if(board.Board.GetLength(0) != 9 || board.Board.GetLength(1) != 9)
			{
				return false;
			}

			for(var i = 0;i < 9;i++)
			{
				for(var j = 0;j < 9;j++)
				{
					var value = board.Board[i,j];
					if(value < 0 || value > 9)
					{
						return false;
					}
				}
			}
			return true;
		}

		public static implicit operator byte[,](SudokuBoard board) => board.Board;
	}
}
