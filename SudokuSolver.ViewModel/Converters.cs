using System.Globalization;

namespace SudokuSolver.ViewModel
{
    public class Converters
    {
        public class MatrixToListConverter : IValueConverter
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

        public class IndexConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is ItemsControl itemsControl && parameter is FrameworkElement element)
                {
                    var index = itemsControl.ItemContainerGenerator.IndexFromContainer(element);
                    return new Tuple<int, string>(index, parameter.ToString());
                }
                return null;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
