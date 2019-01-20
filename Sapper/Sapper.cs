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
        public int Y_maxIndex;
        public int X_maxIndex;
        public int numberOfMines = 10;
        public short[,] Grid;

        public const short X = -1;
        public const short Empty = 0;

        public SapperInstance(int height = 20, int length = 10)
        {
            Y_maxIndex = length-1;
            X_maxIndex = height-1;
        }

        public void GenerateNewGrid()
        {
            Grid = new short[X_maxIndex + 1, Y_maxIndex + 1];
            Random r = new Random();
            int currentMineNumber = 0;
            while(currentMineNumber<numberOfMines)
            {
                int x = r.Next(X_maxIndex + 1);
                int y = r.Next(Y_maxIndex + 1);
                if(Grid[x,y] != X)
                {
                    for(int i = x - 1; i <= x + 1; i++)
                    {
                        if(i >= 0 && i <= X_maxIndex)
                        {
                            for (int j = y - 1; j <= y + 1; j++)
                            {
                                if (j >= 0 && j <= Y_maxIndex)
                                {
                                    Grid[i, j]++;
                                }
                            }
                        }
                        
                    }
                    Grid[x, y] = X;
                    currentMineNumber++;
                }
            }
            for(int i=0;i<=X_maxIndex;i++)
            {
                for(int j=0;j<=Y_maxIndex;j++)
                {
                    Debug.Write(Grid[i, j] + " ");
                }
                Debug.Write("\n");
            }
        }
    };
}
