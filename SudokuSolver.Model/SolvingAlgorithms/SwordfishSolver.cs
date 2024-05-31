using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Services;

namespace SudokuSolver.Model.SolvingAlgorithms
{
	internal class SwordfishSolver : ISudokuSolver
	{
		private byte[,] board;

		public bool Solve(ref byte[,] sudokuBoard)
		{
			this.board = sudokuBoard;
			var isSolved = Swordfish();
			if(isSolved)
			{
				sudokuBoard = this.board;
			}
			return isSolved;
		}

		public string GetName()
		{
			return "Swordfish";
		}

		private bool Swordfish()
		{
			for(byte num = 1;num <= 9;num++)
			{
				for(var row = 0;row < 9;row++)
				{
					List<int> possibleColumns = FindPossiblePositions(num,row);
					if(possibleColumns.Count == 3)
					{
						for(var otherRow1 = row + 1;otherRow1 < 9;otherRow1++)
						{
							List<int> otherPossibleColumns1 = FindPossiblePositions(num,otherRow1);
							if(otherPossibleColumns1.SequenceEqual(possibleColumns))
							{
								for(var otherRow2 = otherRow1 + 1;otherRow2 < 9;otherRow2++)
								{
									List<int> otherPossibleColumns2 = FindPossiblePositions(num,otherRow2);
									if(otherPossibleColumns2.SequenceEqual(possibleColumns))
									{
										RemoveFromOtherRows(num,new int[] { row,otherRow1,otherRow2 },possibleColumns);
									}
								}
							}
						}
					}
				}
			}

			return SudokuValidator.IsSolved(board);
		}

		private List<int> FindPossiblePositions(byte num,int row)
		{
			var possibleColumns = new List<int>();
			for(var col = 0;col < 9;col++)
			{
				if(board[row,col] == num)
				{
					return new List<int>();
				}
				if(board[row,col] == 0)
				{
					possibleColumns.Add(col);
				}
			}
			return possibleColumns;
		}

		private void RemoveFromOtherRows(byte num,int[] rows,List<int> possibleColumns)
		{
			for(var r = 0;r < 9;r++)
			{
				if(!rows.Contains(r))
				{
					foreach(var col in possibleColumns)
					{
						if(board[r,col] == num)
						{
							board[r,col] = 0;
						}
					}
				}
			}
		}
	}
}
