namespace SudokuSolver.Model
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
        private void ClearBoard()
        {
            for (var i = 0; i < 9; i++)
            {
                for (var j = 0; j < 9; j++)
                {
                    Board[i, j] = 0;
                }
            }
        }
    }
}
