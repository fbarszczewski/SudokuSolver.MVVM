using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Services.ParseFactory.ParseStrategies;

namespace SudokuSolver.Model.Services.ParseFactory
{
	public class SudokuParserFactory : ISudokuParserFactory
	{
		public ISudokuParser GetParser(string fileType)
		{
			return fileType.ToLower() switch
			{
				"json" => new JsonSudokuParser(),
				"xml" => new XmlSudokuParser(),
				"txt" => new TxtSudokuParser(),
				_ => throw new ArgumentException("Unsupported file type"),
			};
		}
	}
}
