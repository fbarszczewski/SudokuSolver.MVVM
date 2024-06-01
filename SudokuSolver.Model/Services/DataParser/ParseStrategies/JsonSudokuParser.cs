using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SudokuSolver.Model.Interfaces;
using SudokuSolver.Model.Models;

namespace SudokuSolver.Model.Services.DataParser.ParseStrategies
{

	public class JsonSudokuParser : ISudokuParser
	{
		public void LoadBoards(ISudokuData sudokuData)
		{
			var boards = new List<SudokuBoard>();
			//if we don`t have content but we have path we read file to get content
			if(string.IsNullOrEmpty(sudokuData.Content) && !string.IsNullOrEmpty(sudokuData.DataPath))
				FileHandler.ReadFile(sudokuData);

			try
			{
				boards = JsonConvert.DeserializeObject<List<SudokuBoard>>(sudokuData.Content)!;

			}
			catch(Exception)
			{
				List<int[][]> intArrays = TryExtractIntArrays(sudokuData.Content);

				foreach(var intArray in intArrays)
				{
					boards.Add(new SudokuBoard(ConvertIntArrayToByteArray(intArray)));
				}
			}

			foreach(SudokuBoard board in sudokuData.Boards)
			{
				if(!SudokuValidator.IsValidBoard(board.Board))
					throw new Exception("Board have incorrect format");
			}


			sudokuData.Boards = boards != null ? boards : throw new Exception($"No sudoku to load.");
		}

		public void SaveBoards(ISudokuData sudokuData)
		{
			sudokuData.Content = JsonConvert.SerializeObject(sudokuData.Boards);
			FileHandler.WriteFile(sudokuData);
		}


		/// <summary>
		/// Validates given JSON file content against the provided schema.
		/// </summary>
		/// <param name="schema">JSON schema to validate against.</param>
		/// <param name="fileContent">content of JSON file.</param>
		/// <returns>True if the JSON file content is valid according to the schema, otherwise false.</returns>

		private List<int[][]> TryExtractIntArrays(string jsonString)
		{
			var intArrayList = new List<int[][]>();

			try
			{
				var jsonObject = JObject.Parse(jsonString);

				foreach(JProperty property in jsonObject.Properties())
				{
					JToken token = property.Value;
					if(token.Type == JTokenType.Array)
					{
						if(IsInt2DArray(token))
						{
							var intArray = token.ToObject<int[][]>()!;
							if(CheckArrayDimensions(intArray))
							{
								intArrayList.Add(intArray);
							}
						}
						else if(IsInt1DArray(token))
						{
							var intArray = token.ToObject<int[]>()!;
							if(intArray.Length == 81)
							{
								var convertedArray = Convert1DArrayTo2D(intArray);
								intArrayList.Add(convertedArray);
							}
						}
					}
				}
			}
			catch(JsonReaderException)
			{
				throw new Exception("File have incorrect format");
			}

			return intArrayList;
		}

		private byte[,] ConvertIntArrayToByteArray(int[][] intArray)
		{
			var byteArray = new byte[9,9];

			if(intArray.Length != 9)
				throw new Exception("Board have incorrect format");

			for(var i = 0;i < 9;i++)
			{
				if(intArray[i].Length != 9)
					throw new Exception("Board have incorrect format");

				for(var j = 0;j < 9;j++)
				{
					byteArray[i,j] = intArray[i][j] >= byte.MinValue && intArray[i][j] <= byte.MaxValue
						? (byte)intArray[i][j]
						: throw new Exception("Board have incorrect format");
				}
			}

			return byteArray;
		}

		private bool IsInt2DArray(JToken token)
		{
			try
			{
				var array = token.ToObject<int[][]>();
				return true;
			}
			catch(JsonSerializationException)
			{
				return false;
			}
		}

		private bool IsInt1DArray(JToken token)
		{
			try
			{
				var array = token.ToObject<int[]>();
				return true;
			}
			catch(JsonSerializationException)
			{
				return false;
			}
		}
		private bool CheckArrayDimensions(int[][] array)
		{
			if(array.Length != 9)
			{
				return false;
			}

			foreach(var innerArray in array)
			{
				if(innerArray.Length != 9)
				{
					return false;
				}
			}

			return true;
		}

		private int[][] Convert1DArrayTo2D(int[] array)
		{
			var result = new int[9][];
			for(var i = 0;i < 9;i++)
			{
				result[i] = new int[9];
				Array.Copy(array,i * 9,result[i],0,9);
			}
			return result;
		}

	}

}
