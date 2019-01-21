using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapper
{
    public class SapperInstance
    {
        public struct Field
        {
            public short Value { get; set; }
            public bool IsVisible { get; set; }
        }
        public int Y_maxIndex;
        public int X_maxIndex;
        public Field[,] Grid;

        public const short Minefield = -1;
        public const short EmptyField = 0;

        public int TotalNumberOfMines { get; set; }
        public int RemainingMines { get; set; }

        public SapperInstance(int height = 20, int length = 10)
        {
            Y_maxIndex = length - 1;
            X_maxIndex = height - 1;
        }

        public void GenerateNewGrid()
        {
            Grid = new Field[X_maxIndex + 1, Y_maxIndex + 1];
            Random r = new Random();
            int currentMineNumber = 0;
            while (currentMineNumber < TotalNumberOfMines)
            {
                int x = r.Next(X_maxIndex + 1);
                int y = r.Next(Y_maxIndex + 1);
                if (Grid[x, y].Value != Minefield)
                {
                    for (int i = x - 1; i <= x + 1; i++)
                    {
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            if (IsPointInGrid(i, j))
                            {
                                Grid[i, j].Value++;
                            }
                        }
                    }
                    Grid[x, y].Value = Minefield;
                    currentMineNumber++;
                }
            }
            for (int i = 0; i <= X_maxIndex; i++)
            {
                for (int j = 0; j <= Y_maxIndex; j++)
                {
                    Debug.Write(Grid[i, j] + " ");
                }
                Debug.Write("\n");
            }
        }

        public void SquareSelected(int x, int y)
        {
            if (Grid[x, y].IsVisible)
            {
                return;
            }
            Grid[x, y].IsVisible = true;
            if (Grid[x, y].Value == Minefield)
            {
                for (int i = 0; i <= X_maxIndex; i++)
                {
                    for (int j = 0; j < Y_maxIndex; j++)
                    {
                        Grid[i, j].IsVisible = true;
                    }
                }
                //GAME OVER!!!
            }
            if (Grid[x, y].Value == EmptyField)
            {
                RevealEmptyFields(x, y);
            }
        }

        public void RevealEmptyFields(int x, int y)
        {
            if (IsPointInGrid(x, y) && Grid[x, y].IsVisible == false)
            {
                Grid[x, y].IsVisible = true;
                if (Grid[x, y].Value == EmptyField)
                {
                    RevealEmptyFields(x - 1, y);
                    RevealEmptyFields(x + 1, y);
                    RevealEmptyFields(x, y - 1);
                    RevealEmptyFields(x, y + 1);
                }
            }
        }

        private bool IsPointInGrid(int x, int y)
        {
            return x >= 0 && x <= X_maxIndex && y >= 0 && y <= Y_maxIndex;
        }
    };
}
