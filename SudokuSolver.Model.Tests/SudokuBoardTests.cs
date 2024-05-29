
namespace SudokuSolver.Model.Tests
{
	public class SudokuBoardTests
	{
		//[Fact]
		//public void Board_Has81Fields()
		//{
		//	// Arrange
		//	var sudokuBoard = new SudokuBoard();

		//	// Act
		//	var totalFields = sudokuBoard.Board.Length;

		//	// Assert
		//	Assert.Equal(81,totalFields);
		//}
		//[Fact]
		//public void Board_InitializedWithZeroes()
		//{
		//	// Arrange
		//	var sudokuBoard = new SudokuBoard();

		//	// Act
		//	var totalZeroes = 0;
		//	for(var i = 0;i < 9;i++)
		//	{
		//		for(var j = 0;j < 9;j++)
		//		{
		//			if(sudokuBoard.Board[i,j] == 0)
		//			{
		//				totalZeroes++;
		//			}
		//		}
		//	}

		//	// Assert
		//	Assert.Equal(81,totalZeroes);
		//}
		//[Fact]
		//public void ClearBoard_SetsAllFieldsToZero()
		//{
		//	// Arrange
		//	var sudokuBoard = new SudokuBoard();
		//	var clearBoard = new byte[9,9];
		//	var number = 1;
		//	for(var i = 0;i < 9;i++)
		//	{
		//		for(var j = 0;j < 9;j++)
		//		{
		//			sudokuBoard.Board[i,j] = (byte)number++;
		//		}
		//	}

		//	// Act
		//	sudokuBoard.ClearBoard();

		//	// Assert
		//	Assert.Equal(clearBoard,sudokuBoard.Board);
		//}
		//[Fact]
		//public void IsEmpty_ReturnsTrue_WhenBoardIsEmpty()
		//{
		//	// Arrange
		//	var sudokuBoard = new SudokuBoard();
		//	sudokuBoard.ClearBoard();

		//	// Act
		//	var result = sudokuBoard.IsEmpty();

		//	// Assert
		//	Assert.True(result);
		//}
		//[Fact]
		//public void IsEmpty_ReturnsFalse_WhenOneFieldInBoardIsPopulated()
		//{
		//	// Arrange
		//	var sudokuBoard = new SudokuBoard();
		//	sudokuBoard.Board[0,0] = 1;

		//	// Act
		//	var result = sudokuBoard.IsEmpty();

		//	// Assert
		//	Assert.False(result);
		//}
		//[Fact]
		//public void IsEmpty_ReturnsFalse_WhenOnlyOneFieldIsNotEmpty()
		//{
		//	// Arrange
		//	var sudokuBoard = new SudokuBoard();
		//	sudokuBoard.ClearBoard();
		//	sudokuBoard.Board[8,8] = 1; // Set a value to only one field

		//	// Act
		//	var result = sudokuBoard.IsEmpty();

		//	// Assert
		//	Assert.False(result);
		//}
	}
}
