using Moq;
using SudokuSolver.Model;

namespace SudokuSolver.ViewModel.Tests
{
	public class SudokuViewModelTests
	{
		private readonly Mock<ISudokuBoard> mockModel;

		public SudokuViewModelTests()
		{
			mockModel = new Mock<ISudokuBoard>(MockBehavior.Strict);
			mockModel.SetupProperty(m => m.Board,new byte[9,9]);
		}

		[Fact]
		public void Board_Updates_After_CellCollection_Change()
		{
			// Arrange
			var viewModel = new SudokuViewModel(mockModel.Object);


			// Act
			viewModel.CellCollection[0].Value = "1";

			// Assert
			Assert.Equal(1,mockModel.Object.Board[0,0]);
		}
		[Fact]
		public void CellCollection_Updates_On_Demand()
		{
			// Arrange
			var viewModel = new SudokuViewModel(mockModel.Object);
			var expectedBoard = new byte[9,9];
			expectedBoard[0,0] = 1;
			mockModel.Setup(m => m.Board).Returns(expectedBoard);

			// Act
			mockModel.Raise(m => m.BoardChanged += null);

			// Assert
			Assert.Equal("1",viewModel.CellCollection[0].Value);
		}
		[Fact]
		public void ClearCommand_CanExecute_Returns_False_When_Board_Is_Clear()
		{
			// Arrange
			var viewModel = new SudokuViewModel(mockModel.Object);
			var expectedBoard = new byte[9,9];
			mockModel.Setup(m => m.Board).Returns(expectedBoard);
			mockModel.Setup(m => m.IsEmpty()).Returns(true);


			// Act
			var canExecute = viewModel.ClearCommand.CanExecute(null);

			// Assert
			Assert.False(canExecute);
		}
		[Fact]
		public void ClearCommand_CanExecute_Returns_True_When_Board_Have_Values()
		{
			// Arrange
			var viewModel = new SudokuViewModel(mockModel.Object);
			mockModel.Setup(m => m.IsEmpty()).Returns(false);

			// Act
			var canExecute = viewModel.ClearCommand.CanExecute(null);

			// Assert
			Assert.True(canExecute);
		}
		[Fact]
		public void Constructor_Initializes_Properties()
		{
			// Arrange
			SudokuViewModel viewModel;

			// Act
			viewModel = new SudokuViewModel(mockModel.Object);

			// Assert
			Assert.NotNull(viewModel.CellCollection);
			Assert.Equal(81,viewModel.CellCollection.Count);
			Assert.NotNull(viewModel.SolveCommand);
			Assert.NotNull(viewModel.ClearCommand);
			Assert.NotNull(viewModel.SaveCommand);
			Assert.NotNull(viewModel.LoadFileCommand);
			Assert.NotNull(viewModel.ExitCommand);
		}
	}
}
