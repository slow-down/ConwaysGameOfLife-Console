using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLifeConsole
{
    public class GameOfLife
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        private List<bool> cells = new List<bool>();
        private static Random rnd = new Random();
        private int width;
        private int height;

        public GameOfLife(int width, int height)
        {
            this.width = width;
            this.height = height;
            GenerateMap();

        }

        public void Draw(char alive, char dead)
        {
            ClearScreen();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = GetIndex(x, y);
                    Console.Write(cells[index] ? alive : dead);
                }
                Console.WriteLine();
            }
            NextGeneration();
        }

        private void NextGeneration()
        {
            List<bool> temp = new List<bool>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int neighborCount = CountNeighbors(x, y);
                    bool isAlive = cells[GetIndex(x, y)];

                    // 1. Any live cell with fewer than two live neighbours dies, as if caused by underpopulation.
                    if (isAlive && neighborCount < 2)
                    {
                        temp.Add(false);
                    }
                    // 2. Any live cell with two or three live neighbours lives on to the next generation.
                    else if (isAlive && (neighborCount == 2 || neighborCount == 3))
                    {
                        temp.Add(true);
                    }
                    // 3. Any live cell with more than three live neighbours dies, as if by overpopulation.
                    else if (isAlive && neighborCount > 3)
                    {
                        temp.Add(false);
                    }
                    // 4. Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
                    else if (!isAlive && neighborCount == 3)
                    {
                        temp.Add(true);
                    }
                    // 5. Nothing happens
                    else
                    {
                        temp.Add(isAlive);
                    }

                }
            }

            cells.Clear();

            foreach (var cell in temp)
            {
                cells.Add(cell);
            }
        }

        private int CountNeighbors(int x, int y)
        {
            int sum = 0;

            for (int row = -1; row <= 1; row++)
            {
                for (int col = -1; col <= 1; col++)
                {
                    if (row == 0 && col == 0) continue;
                    if (row == -1 && y == 0) continue;
                    if (row == 1 && y == height - 1) continue;
                    if (col == -1 && x == 0) continue;
                    if (col == 1 && x == width - 1) continue;

                    if (cells[GetIndex(x + col, y + row)])
                    {
                        sum++;
                    }
                }
            }
            return sum;
        }

        private void GenerateMap()
        {
            for(int i = 0; i < width * height; i++)
            {
                cells.Add(rnd.Next(3) < 1);
            }
        }

        private void ClearScreen()
        {
            Console.SetCursorPosition(0, 0);
        }

        private int GetIndex(int x, int y)
        {
            return y * width + x;
        }

        public static void Maximize()
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3); //SW_MAXIMIZE = 3
        }

    }
}
