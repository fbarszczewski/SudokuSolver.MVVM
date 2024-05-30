using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.Interfaces
{
	public interface IGameManager
	{
		List<SudokuGame> GameList { get; }
		/// <summary>
		/// Represents the id of the selected board in view from BoardsList.
		/// </summary>
		int SelectedGameId { get; }
		event Action? GameChanged;
		void SaveSelectedGame(string path);
		void LoadGamesFromFile(string path);
		void RemoveGame();
		void AddEmptyGame();
		void NextGame();
		bool CanNextGame();
		void PreviousGame();
		bool CanPreviousGame();

		void ClearSelectedGame();
		bool CanClearSelectedGame();


		SudokuGame? ReturnSelectedGame();
	}
}
