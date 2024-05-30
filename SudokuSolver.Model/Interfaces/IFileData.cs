namespace SudokuSolver.Model.Interfaces
{
	public interface IFileData
	{
		string DataPath { get; set; }
		string Content { get; set; }
		string FileType { get; }

	}
}
