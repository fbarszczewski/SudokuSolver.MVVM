namespace SudokuSolver.Model
{
    public class SudokuBoard
    {
        public byte[,] Board { get; set; }

        public byte this[byte i, byte j]
        {
            get => Board[i, j];
            set => Board[i, j] = value;
        }

        public SudokuBoard()
        {
            Board = new byte[9, 9];
        }
    }
}
