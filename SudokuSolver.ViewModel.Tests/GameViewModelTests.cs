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

		public static IEnumerable<object[]> TestBoard =>
		new List<object[]>
		{
			new object[] { new byte[,] { {2,8,4,6,3,7,1,5,9}, {7,1,5,2,9,8,6,4,3}, {3,6,9,1,4,5,2,7,8}, {2,3,7,1,8,5,9,4,6}, {4,9,1,7,6,2,8,3,5}, {5,1,9,2,6,8,4,3,7}, {3,7,6,4,5,2,1,8,9}, {4,2,8,7,3,1,2,9,6}, {6,1,3,5,9,4,7,2,8} } },
			new object[] { new byte[,] { {4,7,8,2,6,1,9,5,3}, {1,2,3,4,5,6,7,8,9}, {7,3,1,9,2,6,5,4,8}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {4,9,2,8,5,3,9,1,7}, {1,6,3,7,4,9,6,8,2}, {8,5,9,1,7,2,6,3,4}, {5,6,8,7,1,9,2,8,3} } },
			new object[] { new byte[,] { {5,3,1,7,4,9,6,2,8}, {2,7,4,6,3,8,1,9,5}, {9,4,5,2,8,1,3,7,6}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {1,8,7,3,6,5,4,9,2}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {3,2,6,4,7,8,9,5,1} } },
			new object[] { new byte[,] { {9,4,2,5,7,3,6,1,8}, {8,6,1,9,2,4,3,7,5}, {7,5,3,8,1,6,9,4,2}, {2,3,7,1,8,5,9,4,6}, {6,8,4,3,9,7,5,2,1}, {5,1,9,2,6,8,4,3,7}, {3,7,6,4,5,2,1,8,9}, {4,2,8,7,3,1,2,9,6}, {6,1,3,5,9,4,7,2,8} } },
			new object[] { new byte[,] { {4,7,8,2,6,1,9,5,3}, {3,9,6,5,4,2,8,1,7}, {7,3,1,9,2,6,5,4,8}, {1,5,2,6,7,9,3,8,4}, {9,4,7,8,3,5,6,2,1}, {4,9,2,8,5,3,9,1,7}, {1,6,3,7,4,9,6,8,2}, {8,5,9,1,7,2,6,3,4}, {5,6,8,7,1,9,2,8,3} } },
			new object[] { new byte[,] { {6,9,1,3,7,4,2,5,8}, {2,3,7,1,8,5,9,4,6}, {8,5,4,9,2,6,7,3,1}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {2,6,3,9,7,4,8,1,5}, {9,7,8,2,5,1,3,4,7}, {4,2,5,6,3,8,4,9,1} } },
			new object[] { new byte[,] { {5,4,9,8,6,2,1,7,3}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9}, {1,2,3,4,5,6,7,8,9} } },
			new object[] { new byte[,] { {4,7,8,2,6,1,9,5,3}, {3,9,6,5,4,2,8,1,7}, {7,3,1,9,2,6,5,4,8}, {1,5,2,6,7,9,3,8,4}, {9,4,7,8,3,5,6,2,1}, {4,9,2,8,5,3,9,1,7}, {1,6,3,7,4,9,6,8,2}, {8,5,9,1,7,2,6,3,4}, {5,6,8,7,1,9,2,8,3} } },
			new object[] { new byte[,] { {5,3,1,7,4,9,6,2,8}, {2,7,4,6,3,8,1,9,5}, {9,4,5,2,8,1,3,7,6}, {8,1,7,3,5,2,4,6,9}, {6,2,9,4,1,7,8,5,3}, {1,8,7,3,6,5,4,9,2}, {7,6,3,9,2,4,5,1,8}, {1,2,3,4,5,6,7,8,9}, {3,2,6,4,7,8,9,5,1} } },
			new object[] { new byte[,] { {6,9,1,3,7,4,2,5,8}, {2,3,7,1,8,5,9,4,6}, {8,5,4,9,2,6,7,3,1}, {4,7,6,2,9,1,3,8,5}, {5,1,8,6,4,3,7,9,2}, {3,6,9,5,7,2,8,1,4}, {2,6,3,9,7,4,8,1,5}, {9,7,8,2,5,1,3,4,7}, {4,2,5,6,3,8,4,9,1} } }
		};

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


		[Theory]
		[MemberData(nameof(TestBoard))]
		public void InitializeCellCollection_WhenCalledWithDifferentBoard_UpdateGameCells(byte[,] board)
		{
			//Arrange
			ClearTestEnvironment();
			SetupTestEnvironment();

			//Act
			// To call InitializeGameCells() we need to use UpdateGameBoard()
			_gameViewModel!.UpdateGameBoard(new SudokuBoard(board));

			//Assert
			Assert.NotNull(_gameViewModel.GameCells);
			Assert.Equal(81,_gameViewModel.GameCells.Count);

			Assert_GameCells_With_Board(board,_gameViewModel);
		}


		[Theory]
		[MemberData(nameof(TestBoard))]
		public void GameCell_PropertyChanged_UpdatesModelCorrectly(byte[,] board)
		{
			// Arrange
			ClearTestEnvironment();
			SetupTestEnvironment();
			var k = 0;

			// Act

			for(var i = 0;i < 9;i++)
			{
				for(var j = 0;j < 9;j++)
				{
					_gameViewModel!.GameCells![k].Value = board[i,j].ToString();
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
