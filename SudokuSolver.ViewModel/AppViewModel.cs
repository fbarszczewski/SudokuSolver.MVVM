using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Models;

namespace SudokuSolver.ViewModel
{
	public class AppViewModel : INotifyPropertyChanged
	{
		private readonly IGameManager _gameManagerModel;
		private GameViewModel _gameViewModel;
		public event PropertyChangedEventHandler? PropertyChanged;
		public GameViewModel GameViewModel
		{
			get => _gameViewModel;
			set
			{
				if(_gameViewModel != value)
				{
					_gameViewModel = value;
					OnPropertyChanged(nameof(_gameViewModel));
				}
			}
		}
		public List<string> SudokuDifficultyLevels { get; set; }
		public string SelectedDifficulty { get; set; }
		public string SelectedAlgorithm { get; set; }
		public List<string> AlgorithmCollection { get; set; }
		public string? PageNumber { get; set; }

		public AppViewModel(IGameManager _model)
		{
			_gameManagerModel = _model;
			AlgorithmCollection = _gameManagerModel.GetSolvingAlgorithmsNames();
			SelectedAlgorithm = AlgorithmCollection[0];
			SudokuDifficultyLevels = new List<string> { "Easy","Medium","Hard" };
			SelectedDifficulty = SudokuDifficultyLevels[0];
			_gameViewModel = new GameViewModel(UpdateGame());
		}


		private SudokuBoard UpdateGame()
		{
			PageNumber = $"{_gameManagerModel.SelectedGameIndex + 1} of {_gameManagerModel.GameList.Count}";
			OnPropertyChanged(nameof(PageNumber));
			return _gameManagerModel.GetSelectedGame()!;
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
		}

		#region Commands
		private ICommand? solveCommand;
		private ICommand? clearCommand;
		private ICommand? saveCommand;
		private ICommand? loadFileCommand;
		private ICommand? previousCommand;
		private ICommand? nextCommand;
		private ICommand? getUnsolvedSudokuCommand;

		public ICommand SolveCommand
		{
			get
			{
				solveCommand = solveCommand ?? new RelayCommand(param => SolveSudoku(),param => _gameManagerModel.CanClearSelectedGame());
				return solveCommand;
			}
		}

		public ICommand ClearCommand
		{
			get
			{
				clearCommand = clearCommand ?? new RelayCommand(param => _gameManagerModel.ClearSelectedGame(),param => _gameManagerModel.CanClearSelectedGame());
				return clearCommand;
			}
		}

		public ICommand SaveCommand
		{
			get
			{
				saveCommand = saveCommand ?? new RelayCommand(param => SaveBoard(),param => true);
				return saveCommand;
			}
		}

		public ICommand LoadFileCommand
		{
			get
			{
				loadFileCommand = loadFileCommand ?? new RelayCommand(param => LoadFile(),param => true);
				return loadFileCommand;
			}
		}

		public ICommand GetUnsolvedSudokuCommand
		{
			get
			{
				getUnsolvedSudokuCommand = getUnsolvedSudokuCommand ?? new RelayCommand(param => GetUnsolvedSudoku(),param => !string.IsNullOrEmpty(SelectedDifficulty));
				return getUnsolvedSudokuCommand;
			}
		}

		public ICommand PreviousCommand
		{
			get
			{
				previousCommand = previousCommand ?? new RelayCommand(param => _gameManagerModel.PreviousGame(),param => _gameManagerModel.CanPreviousGame());
				return previousCommand;
			}
		}

		public ICommand NextCommand
		{
			get
			{
				nextCommand = nextCommand ?? new RelayCommand(param => _gameManagerModel.NextGame(),param => _gameManagerModel.CanNextGame());
				return nextCommand;
			}
		}

		private void GetUnsolvedSudoku()
		{
			if(string.IsNullOrEmpty(SelectedDifficulty))
			{
				MessageBox.Show("Select difficulty level first.");
				return;
			}
			try
			{
				_gameManagerModel.GetUnsolvedSudoku(SelectedDifficulty);
				UpdateGame();
			}
			catch(Exception ex)
			{
				MessageBox.Show($"Error getting sudoku to solve.\n{ex.Message}");
			}
		}

		private void SolveSudoku()
		{
			var algorithm = SelectedAlgorithm;
			algorithm ??= "Backtracking";

			try
			{
				if(_gameManagerModel.SolveSudoku(ref algorithm))
				{
					MessageBox.Show($"Sudoku solved with {algorithm} algorithm. :D");
					UpdateGame();
				}
				else
					MessageBox.Show("I cannot solve this sudoku.\nEither it is unsolvable or I just need to add more advanced sudoku solving algorithms. :(");
			}
			catch(Exception ex)
			{
				MessageBox.Show($"Sudoku solver error. :(\n{ex.Message}");
			}
		}
		private void LoadFile()
		{
			var openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = true;
			openFileDialog.Filter = "JSON (*.json)|*.json|Text file (*.txt)|*.txt|XML (*.xml)|*.xml";
			openFileDialog.ShowDialog();
			try
			{
				_gameManagerModel.LoadGamesFromFile(openFileDialog.FileName);
				UpdateGame();
			}
			catch(Exception ex)
			{
				MessageBox.Show($"Cant load file. {ex.Message}");
			}
		}

		private void SaveBoard()
		{
			var saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "JSON (*.json)|*.json|Text file (*.txt)|*.txt|XML (*.xml)|*.xml";
			saveFileDialog.ShowDialog();

			try
			{
				_gameManagerModel.SaveSelectedGame(saveFileDialog.FileName);
				MessageBox.Show("Saved");

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}


		#endregion
	}
}
