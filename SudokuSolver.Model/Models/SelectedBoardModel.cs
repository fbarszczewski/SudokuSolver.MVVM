﻿using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Models
{
	public class SelectedBoardModel : SudokuBoard
	{

		public int Id { get; private set; }

		public event Action? BoardChanged;

		public SelectedBoardModel(ISudokuBoard sudokuBoard,int id)
		{
			Board = sudokuBoard.Board;
			DifficultyLevel = sudokuBoard.DifficultyLevel;
			Id = id;

		}

		/// <summary>
		/// Clears sudoku board by replacing every value with 0.
		/// </summary>
		public void ClearBoard()
		{
			Array.Clear(Board,0,Board.Length);
			BoardChanged?.Invoke();
		}

		/// <summary>
		/// Checks if the Sudoku Board is empty.
		/// </summary>
		/// <returns>Returns true when all values are set to 0.If any value is not 0 returns false</returns>
		public bool IsEmpty()
		{
			foreach(var value in Board)
			{
				if(value != 0)
				{
					return false;
				}
			}

			return true;
		}

		public void RaiseBoardChanged()
		{
			BoardChanged?.Invoke();
		}
	}
}
