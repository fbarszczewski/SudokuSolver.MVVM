using System.Windows;
using System.Windows.Controls;

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
    }
}
