using System.Text;
using System.Xml.Linq;
using SudokuSolver.Model.Interfaces;

namespace SudokuSolver.Model.Services.ParseFactory.ParseStrategies
{
	internal class XmlSudokuParser : ISudokuParser
	{
		public void LoadBoards(ISudokuFile sudokuData)
		{
			var doc = XDocument.Parse(sudokuData.Content);
			sudokuData.Boards = new List<byte[,]>();

			foreach(XElement game in doc.Descendants("game"))
			{
				XAttribute? dataAttribute = game.Attribute("data");
				if(dataAttribute == null)
					throw new ArgumentException("Missing data attribute in game element.");

				var data = dataAttribute.Value;

				if(data.Length != 81)
					throw new ArgumentException("Invalid Sudoku data. Expected 81 characters.");

				var board = new byte[9,9];
				for(var i = 0;i < 9;i++)
				{
					for(var j = 0;j < 9;j++)
					{
						if(!byte.TryParse(data[i * 9 + j].ToString(),out var number) || number < 0 || number > 9)
							throw new ArgumentException($"Invalid number at position {i * 9 + j + 1}.");

						board[i,j] = number;
					}
				}

				if(sudokuData.Boards == null)
				{
					sudokuData.Boards = new List<byte[,]>();
				}
				sudokuData.Boards.Add(board);
			}
		}

		public void SaveBoards(ISudokuFile sudokuData)
		{
			var doc = new XDocument();
			var root = new XElement("SudokuSolver");

			foreach(var board in sudokuData.Boards)
			{
				var data = new StringBuilder(81);
				for(var i = 0;i < 9;i++)
				{
					for(var j = 0;j < 9;j++)
					{
						data.Append(board[i,j]);
					}
				}

				var game = new XElement("game");
				game.SetAttributeValue("data",data.ToString());
				root.Add(game);
			}

			doc.Add(root);
			sudokuData.Content = doc.ToString();
		}


	}
}
