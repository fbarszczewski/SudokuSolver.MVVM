using Moq;
using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.ViewModel.Tests
{
	public class MainViewModel_Tests
	{
		private readonly Mock<IGameManager> _gameManagerMock;
		//private readonly GamesManager _gameManager;
		//private readonly SudokuBoard _game;
		//private readonly SudokuFileManager sudokuFileManagerMock;
		//private readonly SudokuParserFactory SudokuParserFactoryMock;
		//public MainViewModel_Tests()
		//{
		//	_gameManagerMock = new Mock<IGameManager>();
		//	_game = new SudokuBoard(new SudokuBoard(),0);

		//	_gameManager = new GamesManager(new SudokuFileManager(new SudokuParserFactory()));

		//	_gameManagerMock.Setup(gm => gm.GameList).Returns(_gameManager.GameList);
		//	_gameManagerMock.Setup(gm => gm.SelectedGameIndex).Returns(0);
		//	_gameManagerMock.Setup(gm => gm.GetSolvingAlgorithmsNames()).Returns(_gameManager.GetSolvingAlgorithmsNames());
		//	_gameManagerMock.Setup(gm => gm.GetSelectedGame()).Returns(_game);
		//}

		//[Fact]
		//public void CellCollection_InitializesCorrectly()
		//{
		//	// Arrange & Act
		//	var _viewModel = new AppViewModel(_gameManagerMock.Object);

		//	// Assert
		//	Assert.NotNull(_viewModel.CellCollection);
		//	Assert.Equal(81,_viewModel.CellCollection.Count); // 9x9 Sudoku board

		//	for(var i = 0;i < 9;i++)
		//	{
		//		for(var j = 0;j < 9;j++)
		//		{
		//			Assert.Equal("",_viewModel.CellCollection[i * 9 + j].Value);
		//		}
		//	}
		//}

		//[Fact]
		//public void CellCollection_ChangesValuesWhenModelUpdates()
		//{
		//	// Arrange
		//	var _viewModel = new AppViewModel(_gameManagerMock.Object);
		//	var board = new byte[9,9]
		//	{
		//		{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		//		{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		//		{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		//		{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		//		{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		//		{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		//		{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		//		{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		//		{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
		//	};

		//	//Act
		//	_gameManagerMock.Setup(gm => gm.GetSelectedGame()).Returns(new SudokuBoard(new SudokuBoard(board),2));

		//	// Assert

		//	for(var i = 0;i < 9;i++)
		//	{
		//		for(var j = 0;j < 9;j++)
		//		{
		//			Assert.Equal(board[i,j].ToString(),_viewModel.CellCollection[i * 9 + j].Value);
		//		}
		//	}

	}
}
