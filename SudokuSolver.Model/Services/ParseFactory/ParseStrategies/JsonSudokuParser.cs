using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Services.ParseFactory.ParseStrategies
{

	public class JsonSudokuParser : ISudokuParser
	{
		public void LoadBoards(ISudokuFile sudokuData)
		{
			FileHandler.ReadFile(sudokuData);

			sudokuData.Boards = SchemaValidation(sudokuData.Schema,sudokuData.Content)
								? JsonConvert.DeserializeObject<IEnumerable<byte[,]>>(sudokuData.Content)
								: throw new InvalidDataException("Invalid JSON file content.");
		}

		public void SaveBoards(ISudokuFile sudokuData)
		{
			sudokuData.Content = JsonConvert.SerializeObject(sudokuData.Boards);
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
