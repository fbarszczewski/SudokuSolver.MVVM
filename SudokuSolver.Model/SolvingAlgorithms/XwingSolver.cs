using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Services;

namespace SudokuSolver.Model.SolvingAlgorithms
{
	internal class XwingSolver : ISudokuSolver
	{
		private byte[,] _board = new byte[9,9];

		public string GetName()
		{
			return "X-Wing";
		}

		public bool Solve(ref byte[,] sudokuBoard)
		{
			this._board = sudokuBoard;
			var isSolved = XWing();
			if(isSolved)
			{
				sudokuBoard = this._board;
			}
			return isSolved;
		}

		private bool XWing()
		{
			for(byte num = 1;num <= 9;num++)
			{
				for(var row = 0;row < 9;row++)
				{
					List<int> possibleColumns = FindPossiblePositions(num,row);
					if(possibleColumns.Count == 2)
					{
						for(var otherRow = row + 1;otherRow < 9;otherRow++)
						{
							List<int> otherPossibleColumns = FindPossiblePositions(num,otherRow);
							if(otherPossibleColumns.SequenceEqual(possibleColumns))
							{
								RemoveFromOtherRows(num,row,otherRow,possibleColumns);
							}
						}
					}
				}
			}

			return SudokuValidator.IsSolved(_board);
		}


		private List<int> FindPossiblePositions(byte num,int row)
		{
			var possibleColumns = new List<int>();
			for(var col = 0;col < 9;col++)
			{
				if(_board[row,col] == num)
				{
					return new List<int>();
				}
				if(_board[row,col] == 0)
				{
					possibleColumns.Add(col);
				}
			}
			return possibleColumns;
		}

		private void RemoveFromOtherRows(byte num,int row,int otherRow,List<int> possibleColumns)
		{
			for(var r = 0;r < 9;r++)
			{
				if(r != row && r != otherRow)
				{
					foreach(var col in possibleColumns)
					{
						if(_board[r,col] == num)
						{
							_board[r,col] = 0;
						}
					}
				}
			}
		}
	}
}
