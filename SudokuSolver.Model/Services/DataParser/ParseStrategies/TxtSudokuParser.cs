using System.Text;
using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.Services.DataParser.ParseStrategies
{
	internal class TxtSudokuParser : ISudokuParser
	{
		public void LoadBoards(ISudokuData sudokuData)
		{
			FileHandler.ReadFile(sudokuData);


			if(!CanConvert(sudokuData.Content))
				throw new ArgumentException("Invalid Sudoku data format.");


			sudokuData.Boards = ConvertToSudokuBoard(sudokuData.Content);
		}

		public void SaveBoards(ISudokuData sudokuData)
		{
			if(!sudokuData.Boards.Any())
				throw new ArgumentNullException("Nothing to save.");
			sudokuData.Content = ConvertSudokuBoardsToString(sudokuData.Boards);

			FileHandler.WriteFile(sudokuData);
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

				if(!line.All(c => char.IsDigit(c) && c >= '0' && c <= '9'))
				{
					return false;
				}
			}

			return true;
		}

		private List<SudokuBoard> ConvertToSudokuBoard(string inputData)
		{
			var sudokuBoards = new List<SudokuBoard>();
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
				sudokuBoards.Add(new SudokuBoard(board));
			}
			return sudokuBoards;
		}

		public string ConvertSudokuBoardsToString(List<SudokuBoard> sudokuBoards)
		{
			var sb = new StringBuilder();
			var lineCount = 0;
			foreach(SudokuBoard board in sudokuBoards)
			{
				for(var i = 0;i < board.Board.GetLength(0);i++)
				{
					for(var j = 0;j < board.Board.GetLength(1);j++)
					{
						sb.Append((char)board.Board[i,j]);
						if((j + 1) % 9 == 0)
						{
							sb.AppendLine();
							lineCount++;
							if(lineCount % 9 == 0)
								sb.AppendLine();
						}
					}
				}
			}
			return sb.ToString();
		}
	}
}
