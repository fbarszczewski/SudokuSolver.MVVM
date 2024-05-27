using System.Text;
using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Services.ParseFactory.ParseStrategies
{
	internal class TxtSudokuParser : ISudokuParser
	{
		public void LoadBoards(ISudokuFile sudokuData)
		{
			FileHandler.ReadFile(sudokuData);

			if(!CanConvert(sudokuData.Content))
			{
				throw new InvalidDataException("Provided Text file cannot be converted to a Sudoku board." +
					"Ensure that file has 9 lines of 9 digits (0-9)");
			}
		}

		public void SaveBoards(ISudokuFile sudokuData)
		{
			if(!sudokuData.Boards.Any())
				throw new ArgumentNullException("Nothing to save.");

			var sb = new StringBuilder();

			foreach(var board in sudokuData.Boards)
			{
				for(var i = 0;i < 9;i++)
				{
					for(var j = 0;j < 9;j++)
					{
						sb.Append(board[i,j]);
						if(j < 8) sb.Append(" ");
					}
					sb.AppendLine();
				}
				sb.AppendLine();
			}
		}


		private bool CanConvert(string inputData)
		{
			var lines = inputData.Split(new[] { "\r\n","\r","\n" },StringSplitOptions.None)
				  .Where(line => !string.IsNullOrWhiteSpace(line))
				  .ToArray();

			if(lines.Length % 9 != 0)
			{
				return false;
			}

			for(var i = 0;i < lines.Length;i++)
			{
				var line = lines[i].Replace(" ",string.Empty);

				if(line.Length != 9)
				{
					return false;
				}

				if(!line.All(char.IsDigit))
				{
					return false;
				}
			}

			return true;
		}

		private IEnumerable<byte[,]> ConvertToBoard(string inputData)
		{
			var boards = new List<byte[,]>();
			var lines = inputData.Split(new[] { "\r\n","\r","\n" },StringSplitOptions.None)
							 .Where(line => !string.IsNullOrWhiteSpace(line))
							 .ToArray();

			for(var i = 0;i < lines.Length;i += 9)
			{
				var board = new byte[9,9];
				for(var j = 0;j < 9;j++)
				{
					var line = lines[i + j].Replace(" ",string.Empty);
					for(var k = 0;k < 9;k++)
					{
						board[j,k] = byte.Parse(line[k].ToString());
					}
				}
				boards.Add(board);
			}

			return boards;
		}
	}
}
