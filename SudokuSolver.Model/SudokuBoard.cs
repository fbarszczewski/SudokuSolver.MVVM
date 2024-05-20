﻿namespace SudokuSolver.Model
{
    public class SudokuBoard
    {
        public byte[,] Board { get; set; }

        public SudokuBoard()
        {
            Board = new byte[9, 9];
            ClearBoard();
        }

        /// <summary>
        /// Clears sudoku board by replacing every value with 0.
        /// </summary>
        public void ClearBoard()
        {
            Array.Clear(Board, 0, Board.Length);
        }
    }
}
