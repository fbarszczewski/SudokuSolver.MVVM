using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Models
{
	public class SudokuData : ISudokuData
	{
		public List<SudokuBoard> Boards { get; set; }
		public string DataPath { get; set; }
		public string DataType { get; set; }
		public string Content { get; set; }


		public SudokuData(string dataPath)
		{
			Boards = new List<SudokuBoard>();
			DataPath = dataPath;
			DataType = SetDataType;
			Content = string.Empty;
		}
		public SudokuData(string content,string dataType)
		{
			Content = content;
			DataType = dataType;
			DataPath = string.Empty;
			Boards = new List<SudokuBoard>();
		}

		public SudokuData(List<SudokuBoard> boards,string dataPath)
		{
			Boards = boards;
			DataPath = dataPath;
			DataType = SetDataType;
			Content = string.Empty;
		}



		private string SetDataType => Path.GetExtension(DataPath).TrimStart('.');

	}
}
