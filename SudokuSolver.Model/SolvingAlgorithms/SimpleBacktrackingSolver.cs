﻿using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.SolvingAlgorithms
{
	internal class SimpleBacktrackingSolver : ISudokuSolver
	{
		public string GetName()
		{
			return "Simple Backtracking";
		}

		public bool Solve(SudokuBoard board)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Recursively solves the Sudoku board using a backtracking algorithm.
		/// </summary>
		/// <param name="board">The Sudoku board to solve.</param>
		/// <param name="row">The current row in the recursion.</param>
		/// <param name="col">The current column in the recursion.</param>
		/// <returns>True if the board was solved successfully, false otherwise.</returns>
		private bool RecursiveSolver(byte[,] board,int row,int col)
		{
			if(row == 9)
				return true;
			if(col == 9)
				return RecursiveSolver(board,row + 1,0);
			if(board[row,col] != 0)
				return RecursiveSolver(board,row,col + 1);

			for(byte num = 1;num <= 9;num++)
			{
				if(IsSafe(board,row,col,num))
				{
					board[row,col] = num;
					if(RecursiveSolver(board,row,col + 1))
						return true;
					board[row,col] = 0;
				}
			}
			return false;
		}

		/// <summary>
		/// Checks if it is safe to place a number in a specific cell.
		/// </summary>
		/// <param name="board">The Sudoku board.</param>
		/// <param name="row">The row of the cell.</param>
		/// <param name="col">The column of the cell.</param>
		/// <param name="num">The number to check.</param>
		/// <returns>True if it is safe to place the number, false otherwise.</returns>
		private bool IsSafe(byte[,] board,int row,int col,byte num)
		{
			for(var x = 0;x < 9;x++)
				if(board[row,x] == num || board[x,col] == num || board[row / 3 * 3 + x / 3,col / 3 * 3 + x % 3] == num)
					return false;
			return true;
		}

	}
}
