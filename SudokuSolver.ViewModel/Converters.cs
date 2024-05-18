using System.Globalization;
using System.Windows.Data;

namespace SudokuSolver.ViewModel
{
    internal class Converters : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[,] board)
            {
                var list = new List<byte>();
                for (var i = 0; i < board.GetLength(0); i++)
                {
                    for (var j = 0; j < board.GetLength(1); j++)
                    {
                        list.Add(board[i, j]);
                    }
                }
                return list;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
