﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.Services.ParseFactory.ParseStrategies
{

	public class JsonSudokuParser : ISudokuParser
	{
		public void LoadBoards(ISudokuFile sudokuData)
		{
			FileHandler.ReadFile(sudokuData);
			List<SudokuBoard> boards;
			try
			{
				boards = JsonConvert.DeserializeObject<List<SudokuBoard>>(sudokuData.Content);
			}
			catch(JsonReaderException)
			{
				throw new Exception($" JSON File cannot be converted to sudoku format.");
			}

			foreach(SudokuBoard board in sudokuData.Boards)
			{
				if(!SudokuBoard.IsValidBoard(board))
					throw new Exception("Board have incorrect format");
			}

			sudokuData.Boards = boards != null ? boards : throw new Exception($"No sudoku to load.");
		}

		public void SaveBoards(ISudokuFile sudokuData)
		{
			sudokuData.Content = JsonConvert.SerializeObject(sudokuData.Boards);
			FileHandler.WriteFile(sudokuData);
		}


		/// <summary>
		/// Validates given JSON file content against the provided schema.
		/// </summary>
		/// <param name="schema">JSON schema to validate against.</param>
		/// <param name="fileContent">content of JSON file.</param>
		/// <returns>True if the JSON file content is valid according to the schema, otherwise false.</returns>
		private bool SchemaValidation(string schema,string fileContent)
		{
			var _schema = JSchema.Parse(schema);
			var jObject = JObject.Parse(fileContent);

			return jObject.IsValid(_schema);
		}
	}

}
