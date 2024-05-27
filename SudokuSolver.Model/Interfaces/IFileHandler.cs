namespace SudokuSolver.Model.Interfaces
{
	internal interface IFileHandler
	{
		void SaveFile(IFileData fileData);
		string LoadFile(IFileData fileData);
	}
}
