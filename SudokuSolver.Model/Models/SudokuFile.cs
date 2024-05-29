using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Models
{
	public class SudokuFile : ISudokuFile
	{
		public List<SudokuBoard> Boards { get; set; }
		public string DataPath { get; set; }
		public string FileType => Path.GetExtension(DataPath).TrimStart('.');
		public string? Content { get; set; }

		public SudokuFile(string dataPath)
		{
			Boards = new List<SudokuBoard>();
			DataPath = dataPath;
		}
		public SudokuFile(List<SudokuBoard> boards,string dataPath)
		{
			Boards = boards;
			DataPath = dataPath;
		}
	}
}
