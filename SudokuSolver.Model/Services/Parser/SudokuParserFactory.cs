using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Services.ParseFactory.ParseStrategies;

namespace SudokuSolver.Model.Services.ParseFactory
{
	public class SudokuParserFactory : ISudokuParserFactory
	{
		private const string JsonFileType = "json";
		private const string XmlFileType = "xml";
		private const string TxtFileType = "txt";

		public ISudokuParser GetParser(string fileType)
		{
			return fileType.ToLower() switch
			{
				JsonFileType => new JsonSudokuParser(),
				XmlFileType => new XmlSudokuParser(),
				TxtFileType => new TxtSudokuParser(),
				_ => throw new ArgumentException("Unsupported file type"),
			};
		}
	}
}
