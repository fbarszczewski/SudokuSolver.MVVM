using System.Text;
using Newtonsoft.Json;

namespace SudokuSolver.Model.Services
{
	internal class SugokuAPI
	{
		private readonly HttpClient _client;
		private const string BaseUrl = "https://sugoku.onrender.com";

		public SugokuAPI()
		{
			_client = new HttpClient
			{
				BaseAddress = new Uri(BaseUrl)
			};
		}

		public async Task<string> GetSudoku(string difficulty)
		{
			HttpResponseMessage response = await _client.GetAsync($"/board?difficulty={difficulty}");
			if(!response.IsSuccessStatusCode)
			{
				throw new Exception("Unable to download sudoku.");
			}

			var sudoku = await response.Content.ReadAsStringAsync();
			return sudoku;
		}

		public async Task<string> SolveSudoku(int[][] board)
		{
			var data = new { board = board };
			var content = new StringContent(JsonConvert.SerializeObject(data),Encoding.UTF8,"application/x-www-form-urlencoded");
			HttpResponseMessage response = await _client.PostAsync("/solve",content);
			if(!response.IsSuccessStatusCode)
			{
				throw new Exception("Unable to solve sudoku.");
			}

			var solution = await response.Content.ReadAsStringAsync();
			return solution;
		}
	}
}
