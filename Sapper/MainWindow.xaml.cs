using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int MaxGridHeight = 20;
        public const int MinGridHeight = 7;
        public const int MaxGridLength = 20;
        public const int MinGridLength = 7;

        public int GridHeight { get; set; }
        public int GridLength { get; set; }

        public struct SquarePosition
        {
            public int X;
            public int Y;
            public SquarePosition(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            var a = new SapperInstance();
            a.RevealSquare(5, 5);
            InitializeVariables();
            DrawBoard();
        }

        private void InitializeVariables()
        {
            GridLength = 12;
            GridHeight = 12;
        }

        private void DrawBoard()
        {
            for (int j = 0; j < GridLength; j++)
            {
                Board.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < GridHeight; i++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < GridLength; j++)
                {
                    var square = new Button();
                    square.Style = Resources["UnrevealedField"] as Style;
                    square.Tag = new SquarePosition(i, j);
                    Board.Children.Add(square);
                    Grid.SetRow(square, i);
                    Grid.SetColumn(square, j);
                }
            }
        }

        private void Unrevealed_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Unrevealed_RightClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
