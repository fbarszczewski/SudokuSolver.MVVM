using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.Services
{
	internal class SudokuValidator
	{
		/// <summary>
		/// Checks if the given Sudoku board is correctly solved.
		/// </summary>
		/// <param name="board">The Sudoku board to check.</param>
		/// <returns>True if the Sudoku board is correctly solved, false otherwise.</returns>
		public static bool IsSolved(SudokuBoard board)
		{
			if(!IsValidBoard(board))
			{
				return false;
			}

			// checks each row & column 
			for(var i = 0;i < 9;i++)
			{
				if(!HasAllNumbers(board,i,0,i,8) || !HasAllNumbers(board,0,i,8,i))
				{
					return false;
				}
			}

			// checks each 3x3 block
			for(var i = 0;i < 9;i += 3)
			{
				for(var j = 0;j < 9;j += 3)
				{
					if(!HasAllNumbers(board,i,j,i + 2,j + 2))
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Checks if the given Sudoku board is a valid 9x9 grid and all values are between 0 and 9.
		/// </summary>
		/// <param name="board">The Sudoku board to check.</param>
		/// <returns>True if the Sudoku board is valid, false otherwise.</returns>
		public static bool IsValidBoard(SudokuBoard board)
		{
			return IsCorrectSize(board) && ContainsValidValues(board);
		}

		/// <summary>
		/// Checks if all numbers from 1 to 9 are present in the specified range of the board.
		/// </summary>
		/// <param name="board">The Sudoku board to check.</param>
		/// <param name="startRow">The start row of the range.</param>
		/// <param name="startCol">The start column of the range.</param>
		/// <param name="endRow">The end row of the range.</param>
		/// <param name="endCol">The end column of the range.</param>
		/// <returns>True if all numbers from 1 to 9 are present, false otherwise.</returns>
		private static bool HasAllNumbers(byte[,] board,int startRow,int startCol,int endRow,int endCol)
		{
			var numbers = new bool[10];

			for(var i = startRow;i <= endRow;i++)
			{
				for(var j = startCol;j <= endCol;j++)
				{
					numbers[board[i,j]] = true;
				}
			}

			for(var i = 1;i < numbers.Length;i++)
			{
				if(!numbers[i])
				{
					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// Checks if the given Sudoku board is a 9x9 grid.
		/// </summary>
		/// <param name="board">The Sudoku board to check.</param>
		/// <returns>True if the Sudoku board is a 9x9 grid, false otherwise.</returns>
		private static bool IsCorrectSize(SudokuBoard board)
		{
			return board.Board.GetLength(0) == 9 && board.Board.GetLength(1) == 9;
		}

		/// <summary>
		/// Checks if all values in the given Sudoku board are between 0 and 9.
		/// </summary>
		/// <param name="board">The Sudoku board to check.</param>
		/// <returns>True if all values are between 0 and 9, false otherwise.</returns>
		private static bool ContainsValidValues(SudokuBoard board)
		{
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
	}
}
