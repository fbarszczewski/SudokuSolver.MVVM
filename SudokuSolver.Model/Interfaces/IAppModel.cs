﻿using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.Interfaces
{
	public interface IAppModel
	{
		List<SelectedBoardModel> BoardsList { get; }
		/// <summary>
		/// Represents the id of the selected board in view from BoardsList.
		/// </summary>
		int SelectedBoardId { get; set; }
		event Action? ModelChanged;

		void SaveCurrentBoard(string path);

		void LoadBoardsFromFile(string path);
	}
}
