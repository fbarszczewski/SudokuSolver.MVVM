using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SudokuSolver.View.CustomControls
{
    /// <summary>
    /// Interaction logic for SudokuCell.xaml
    /// </summary>
    public partial class SudokuCell : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SudokuCell), new PropertyMetadata(default(string)));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public SudokuCell()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.Clear();
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var textBox = sender as TextBox;
            if (!textBox.IsFocused)
            {
                textBox.Focus();
                e.Handled = true;
            }
        }
    }
}
