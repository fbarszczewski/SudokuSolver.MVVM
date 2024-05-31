using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.SolvingAlgorithms
{
	internal class BacktrackingSolver : ISudokuSolver
	{

		private readonly int _arraySize = 9;
		private readonly byte[,] _solvedBoard = new byte[9,9];
		public string GetName()
		{
			return "Backtracking";
		}

		public bool Solve(ref byte[,] sudokuBoard)
		{
			if(SolveSudoku(sudokuBoard,0,0))
			{
				sudokuBoard = _solvedBoard;
				return true;
			}
			else
				return false;
		}



		private bool SolveSudoku(byte[,] grid,int row,int col)
		{

			if(row == _arraySize - 1 && col == _arraySize)
				return true;

			if(col == _arraySize)
			{
				row++;
				col = 0;
			}

			if(grid[row,col] != 0)
				return SolveSudoku(grid,row,col + 1);

			for(byte num = 1;num < 10;num++)
			{
				if(IsSafe(grid,row,col,num))
				{
					grid[row,col] = num;

					if(SolveSudoku(grid,row,col + 1))
						return true;
				}

				grid[row,col] = 0;
			}
			return false;
		}

		private bool IsSafe(byte[,] grid,int row,int col,int num)
		{
			for(var x = 0;x <= 8;x++)
				if(grid[row,x] == num)
					return false;

			for(var x = 0;x <= 8;x++)
				if(grid[x,col] == num)
					return false;

			int startRow = row - row % 3, startCol
			  = col - col % 3;
			for(var i = 0;i < 3;i++)
				for(var j = 0;j < 3;j++)
					if(grid[i + startRow,j + startCol] == num)
						return false;

			return true;
		}
	}
}
