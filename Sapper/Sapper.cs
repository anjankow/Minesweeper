﻿using System;
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
            public bool IsMarked { get; set; }
        }
        public int Y_maxIndex;
        public int X_maxIndex;
        public Field[,] Grid;

        public const short Minefield = -1;
        public const short EmptyField = 0;

        public int TotalNumberOfMines { get; private set; }
        public int RemainingMines { get; private set; }
        public int RemainingFieldsToReveal { get; private set; }
        public bool DidUserWin { get; private set; }

        public SapperInstance(int height = 20, int length = 10, int numberOfMines = 15)
        {
            Y_maxIndex = length - 1;
            X_maxIndex = height - 1;
            TotalNumberOfMines = numberOfMines;
            RemainingMines = numberOfMines;
            RemainingFieldsToReveal = height * length - numberOfMines;
            DidUserWin = false;
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

        /// <summary>
        /// Reveals choosen field. In case of mine reveals all fields,
        /// in case of empty square reveals surrounding empty fields
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <returns>Returns true in case of end of the game and sets the DidUserWin flag indicating the result</returns>
        public bool RevealSquare(int x, int y)
        {
            if (Grid[x, y].IsVisible || Grid[x, y].IsMarked)
            {
                return false;
            }
            switch(Grid[x,y].Value)
            {
                case Minefield:
                    for (int i = 0; i <= X_maxIndex; i++)
                    {
                        for (int j = 0; j < Y_maxIndex; j++)
                        {
                            Grid[i, j].IsVisible = true;
                        }
                    }
                    DidUserWin = false;
                    return true;
                case EmptyField:
                    RevealEmptyFields(x, y);
                    break;
                default:
                    Grid[x, y].IsVisible = true;
                    RemainingFieldsToReveal--;
                    break;
            }
            if(RemainingMines==0)
            {
                DidUserWin = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MarkFieldAsMine(int x, int y)
        {
            if(!Grid[x,y].IsVisible)
            {
                Grid[x, y].IsMarked = true;
                RemainingMines--;
            }
        }

        public void UnmarkField(int x, int y)
        {
            if (Grid[x, y].IsMarked)
            {
                Grid[x, y].IsMarked = false;
                RemainingMines++;
            }
        }

        public void RevealEmptyFields(int x, int y)
        {
            if (IsPointInGrid(x, y) && Grid[x, y].IsVisible == false)
            {
                Grid[x, y].IsVisible = true;
                RemainingFieldsToReveal--;
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
