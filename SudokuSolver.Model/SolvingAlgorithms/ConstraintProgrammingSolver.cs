using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Services;

namespace SudokuSolver.Model.SolvingAlgorithms
{
	internal class ConstraintProgrammingSolver : ISudokuSolver
	{
		private byte[,] _solvedBoard = new byte[9,9];

		public string GetName()
		{
			return "Constraint Programming";
		}


		public bool Solve(ref byte[,] sudokuBoard)
		{
			if(ConstraintSolver(sudokuBoard))
			{
				sudokuBoard = _solvedBoard;
				return true;
			}
			else
				return false;
		}

		private bool ConstraintSolver(byte[,] board)
		{
			(int, int)? emptyCell = FindEmptyCell(board);
			if(!emptyCell.HasValue)
			{
				return SudokuValidator.IsSolved(board);
			}

			var row = emptyCell.Value.Item1;
			var col = emptyCell.Value.Item2;

			for(byte num = 1;num <= 9;num++)
			{
				if(IsSafe(board,row,col,num))
				{
					board[row,col] = num;

					if(ConstraintSolver(board))
					{
						_solvedBoard = board;
						return true;
					}

					board[row,col] = 0;
				}
			}

			return false;
		}

		private bool IsSafe(byte[,] board,int row,int col,byte num)
		{

			for(var i = 0;i < 9;i++)
			{
				if(board[row,i] == num)
				{
					return false;
				}
			}

			for(var i = 0;i < 9;i++)
			{
				if(board[i,col] == num)
				{
					return false;
				}
			}

			var boxStartRow = row - row % 3;
			var boxStartCol = col - col % 3;
			for(var i = 0;i < 3;i++)
			{
				for(var j = 0;j < 3;j++)
				{
					if(board[i + boxStartRow,j + boxStartCol] == num)
					{
						return false;
					}
				}
			}

			return true;
		}

		private (int, int)? FindEmptyCell(byte[,] board)
		{
			for(var row = 0;row < 9;row++)
			{
				for(var col = 0;col < 9;col++)
				{
					if(board[row,col] == 0)
					{
						return (row, col);
					}
				}
			}
			return null;
		}


	}
}
