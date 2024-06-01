using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Services
{
	public static class FileHandler
	{
		/// <summary>
		/// Reads the file and stores the content in the fileData object.
		/// </summary>
		/// <param name="fileData">Provide file path, store extension type & content</param>
		/// <exception cref="FileNotFoundException">The file at the specified path could not be found.</exception>
		/// <exception cref="DirectoryNotFoundException">The specified path is invalid.</exception>
		/// <exception cref="UnauthorizedAccessException">The application does not have permission to read the file.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		public static void ReadFile(IFileData fileData)
		{
			try
			{
				fileData.Content = File.ReadAllText(fileData.DataPath);
			}
			catch(FileNotFoundException)
			{
				throw new Exception($"File under path {fileData.DataPath} doesn't exist.");
			}
			catch(DirectoryNotFoundException)
			{
				throw new Exception($"Path {fileData.DataPath} has not been found.");
			}
			catch(UnauthorizedAccessException)
			{
				throw new Exception($"Application don't have permission to read file{fileData.DataPath}.");
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Writes the content to the file.
		/// </summary>
		/// <param name="fileData">provide content & path to save file</param>
		/// <exception cref="DirectoryNotFoundException">The specified path is invalid.</exception>
		/// <exception cref="UnauthorizedAccessException">The application does not have permission to write to the file.</exception>
		/// <exception cref="IOException">An I/O error occurred.</exception>
		public static void WriteFile(IFileData fileData)
		{
			try
			{
				File.WriteAllText(fileData.DataPath,fileData.Content);
			}
			catch(DirectoryNotFoundException)
			{
				throw new Exception($"Path {fileData.DataPath} has not been found.");
			}
			catch(UnauthorizedAccessException)
			{
				throw new Exception($"Application don't have permission to write file{fileData.DataPath}.");
			}
			catch(IOException)
			{
				throw new Exception($"An I/O error occurred while writing {fileData.DataPath}.");
			}
			catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}
