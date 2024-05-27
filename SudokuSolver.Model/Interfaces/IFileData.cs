namespace SudokuSolver.Model.Interfaces
{
	internal interface IFileData
	{
		string DataPath { get; set; }
		string Content { get; set; }
		string FileType { get; set; }
	}
}
