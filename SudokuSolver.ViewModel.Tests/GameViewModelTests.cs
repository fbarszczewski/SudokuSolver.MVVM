using Moq;
using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Models;
using SudokuSolver.Model.Services;
using SudokuSolver.Model.Services.DataParser;

namespace SudokuSolver.ViewModel.Tests
{
	public class GameViewModelTests
	{
		private readonly Mock<IGameManager> _gameManagerMock;
		private GamesManager? _gamesManager;
		private AppViewModel? _appViewModel;
		private GameViewModel? _gameViewModel;


		public GameViewModelTests()
		{
			_gameManagerMock = new Mock<IGameManager>();
		}

		[Fact]
		public void UpdateGameBoard_RaisesPropertyChangedFor_SelectedGameBoard()
		{
			// Arrange
			ClearTestEnvironment();
			SetupTestEnvironment();
			var sudokuBoard = new SudokuBoard();
			var eventRaised = false;
			_gameViewModel!.PropertyChanged += (sender,args) => eventRaised = args.PropertyName == "SelectedGameBoard";

			//Act
			_gameViewModel.UpdateGameBoard(sudokuBoard);

			//Assert
			Assert.True(eventRaised);
		}

		[Fact]
		public void UpdateGameBoard_UpdateWhenRun_SelectedGameBoard()
		{
			//Arrange
			ClearTestEnvironment();
			SetupTestEnvironment();
			var sudokuBoard = new SudokuBoard();

			//Act
			_gameViewModel!.UpdateGameBoard(sudokuBoard);


			//Assert
			Assert.Equal(sudokuBoard,_gameViewModel.SelectedGameBoard);
		}

		[Fact]
		public void UpdateGameBoard_Initialize_GameCells()
		{
			//Arrange
			ClearTestEnvironment();
			SetupTestEnvironment();
			var sudokuBoard = new SudokuBoard();

			//Act
			_gameViewModel!.UpdateGameBoard(sudokuBoard);


			//Assert
			Assert.NotNull(_gameViewModel.GameCells);
			Assert.Equal(81,_gameViewModel.GameCells.Count);
			for(var i = 0;i < 9;i++)
			{
				for(var j = 0;j < 9;j++)
				{
					Assert.Equal("",_gameViewModel.GameCells![i * 9 + j].Value);
				}
			}
		}


		[Fact]
		public void InitializeCellCollection_WhenCalledWithDifferentBoard_UpdateGameCells()
		{
			//Arrange
			ClearTestEnvironment();
			SetupTestEnvironment();
			var customBoard = new byte[9,9]
			{
					{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
					{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
					{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
					{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
					{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
					{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
					{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
					{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
					{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
			};

			//Act
			// To call InitializeGameCells() we need to use UpdateGameBoard()
			_gameViewModel!.UpdateGameBoard(new SudokuBoard(customBoard));

			//Assert
			Assert.NotNull(_gameViewModel.GameCells);
			Assert.Equal(81,_gameViewModel.GameCells.Count);

			Assert_GameCells_With_Board(customBoard,_gameViewModel);
		}


		[Fact]
		public void GameCell_PropertyChanged_UpdatesModelCorrectly()
		{
			// Arrange
			ClearTestEnvironment();
			SetupTestEnvironment();
			var k = 0;
			var customBoard = new byte[9,9]
			{
				{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
				{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
				{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
				{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
				{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
				{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
				{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
				{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
				{ 1, 2, 3, 4, 5, 6, 7, 8, 9 },
			};
			var sudokuBoard = new SudokuBoard(customBoard);

			// Act


			for(var i = 0;i < 9;i++)
			{
				for(var j = 0;j < 9;j++)
				{
					_gameViewModel!.GameCells![k].Value = customBoard[i,j].ToString();
					k++;
				}
			}
			// Assert
			Assert_GameCells_With_Board(_gameManagerMock.Object.GetSelectedGame()!.Board,_gameViewModel);
		}








		private void SetupTestEnvironment()
		{

			_gamesManager = new GamesManager(new SudokuFileManager(new SudokuParserFactory()));

			_gameManagerMock.Setup(gm => gm.GetSolvingAlgorithmsNames()).Returns(_gamesManager.GetSolvingAlgorithmsNames());
			_gameManagerMock.Setup(gm => gm.GameList).Returns(_gamesManager.GameList);
			_gameManagerMock.Setup(gm => gm.SelectedGameIndex).Returns(_gamesManager.SelectedGameIndex);
			_gameManagerMock.Setup(gm => gm.GetSelectedGame()).Returns(_gamesManager.GetSelectedGame());

			_appViewModel = new AppViewModel(_gameManagerMock.Object);
			_gameViewModel = _appViewModel.GameViewModel;

		}

		private void ClearTestEnvironment()
		{
			_gameManagerMock?.Reset();
			if(_gamesManager != null)
			{
				_gamesManager = null!;
				_appViewModel = null!;
				_gameViewModel = null!;
			}

		}

		private void Assert_GameCells_With_Board(byte[,] board,GameViewModel testedModel)
		{
			for(var i = 0;i < 9;i++)
			{
				for(var j = 0;j < 9;j++)
				{
					Assert.Equal(board[i,j].ToString(),testedModel.GameCells![i * 9 + j].Value);
				}
			}
		}
	}
}
