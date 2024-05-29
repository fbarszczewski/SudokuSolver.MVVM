using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Models
{
	public class SudokuFile : ISudokuFile
	{
		public List<SudokuBoard> Boards { get; set; }
		public string DataPath { get; set; }
		public string FileType { get; set; }
		public string? Content { get; set; }

		public SudokuFile(List<SudokuBoard> boards,string dataPath,string fileType)
		{
			Boards = boards;
			DataPath = dataPath;
			FileType = fileType;
		}
	}
}
