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

        public SapperInstance Game { get; set; }

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
            InitializeVariables();
            Game = new SapperInstance(GridHeight, GridLength);
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
            Button square = sender as Button;
            var position = (SquarePosition)square.Tag;
            bool isGameOver = Game.RevealSquare(position.X, position.Y);
            if (!(isGameOver && Game.DidUserWin))
            {
                UpdateBoard();
            }
        }

        private void UpdateBoard()
        {
            foreach (Button square in Board.Children)
            {
                var position = (SquarePosition)square.Tag;
                if (Game.Board[position.X, position.Y].IsVisible == true)
                {
                    switch (Game.Board[position.X, position.Y].Value)
                    {
                        case SapperInstance.Minefield:
                            square.Style = Resources["MineField"] as Style;
                            break;
                        case SapperInstance.EmptyField:
                            square.Style = Resources["EmptyField"] as Style;
                            break;
                        default:
                            square.Style = Resources["EmptyField"] as Style;
                            square.Content = Game.Board[position.X, position.Y].Value;
                            break;
                    }
                }
            }
        }

        private void Unrevealed_RightClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("prawy");

        }
    }
}
