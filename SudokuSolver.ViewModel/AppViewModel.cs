﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.ViewModel
{
	public class AppViewModel : INotifyPropertyChanged
	{
		private readonly IGameManager _gameManagerModel;

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
				clearCommand = clearCommand ?? new RelayCommand(param => ClearBoard(),param => _gameManagerModel.CanClearSelectedGame());
				return clearCommand;
			}
		}
		public ICommand GetUnsolvedSudokuCommand
		{
			get
			{
				getUnsolvedSudokuCommand = getUnsolvedSudokuCommand ?? new RelayCommand(param => GetUnsolvedSudokuAndUpdateGame(),param => !string.IsNullOrEmpty(SelectedDifficulty));
				return getUnsolvedSudokuCommand;
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
		public ICommand NextCommand
		{
			get
			{
				nextCommand = nextCommand ?? new RelayCommand(param => SelectNextGame(),param => _gameManagerModel.CanNextGame());
				return nextCommand;
			}
		}
		public ICommand PreviousCommand
		{
			get
			{
				previousCommand = previousCommand ?? new RelayCommand(param => SelectPreviousGame(),param => _gameManagerModel.CanPreviousGame());
				return previousCommand;
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
		public string? PageNumber { get; private set; }
		public GameViewModel GameViewModel { get; private set; }
		public string SelectedAlgorithm { get; set; }
		public string SelectedDifficulty { get; set; }
		public List<string> AlgorithmCollection { get; private set; }
		public List<string> SudokuDifficultyLevels { get; private set; }

		public event PropertyChangedEventHandler? PropertyChanged;

		public AppViewModel(IGameManager _model)
		{
			_gameManagerModel = _model;
			AlgorithmCollection = _gameManagerModel.GetSolvingAlgorithmsNames();
			SelectedAlgorithm = AlgorithmCollection[0];
			SudokuDifficultyLevels = new List<string> { "Easy","Medium","Hard" };
			SelectedDifficulty = SudokuDifficultyLevels[0];
			GameViewModel = new GameViewModel();
			UpdateGame();
		}

		private void UpdateGame()
		{
			PageNumber = $"{_gameManagerModel.SelectedGameIndex + 1} of {_gameManagerModel.GameList.Count}";

			GameViewModel.UpdateGameBoard(_gameManagerModel.GetSelectedGame()!);
			OnPropertyChanged(nameof(PageNumber));
			OnPropertyChanged(nameof(GameViewModel));

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

		private async Task GetUnsolvedSudoku()
		{
			if(string.IsNullOrEmpty(SelectedDifficulty))
			{
				MessageBox.Show("Select difficulty level first.");
				return;
			}
			try
			{
				await _gameManagerModel.GetUnsolvedSudoku(SelectedDifficulty);

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

		private void ClearBoard()
		{
			_gameManagerModel.ClearSelectedGame();
			UpdateGame();
		}

		private void SelectNextGame()
		{
			_gameManagerModel.NextGame();
			UpdateGame();
		}

		private void SelectPreviousGame()
		{
			_gameManagerModel.PreviousGame();
			UpdateGame();
		}

		private async void GetUnsolvedSudokuAndUpdateGame()
		{
			await GetUnsolvedSudoku();
			UpdateGame();
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName));
		}
	}
}
