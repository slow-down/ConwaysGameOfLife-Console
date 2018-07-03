using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


// https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
namespace ConwaysGameOfLifeConsole
{
    class Program
    {
        private static GameOfLife game;

        static void Main(string[] args)
        {
            GameOfLife.Maximize();

            game = new GameOfLife(Console.WindowWidth / 2, Console.WindowHeight / 2);

            while (true)
            {
                //game.Draw('█', '░');
                game.Draw('█', ' ');

                Thread.Sleep(10);
            }

            Console.ReadKey();
        }
    }
}
