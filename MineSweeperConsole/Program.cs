using System;
using MineSweeperClasses;

namespace MineSweeperConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to Minesweeper");

            var board1 = new Board(10, 0.10f);
            Console.WriteLine("Here is the answer key for the first board");
            Renderer.PrintAnswers(board1);

            var board2 = new Board(15, 0.15f);
            Console.WriteLine("Here is the answer key for the second board");
            Renderer.PrintAnswers(board2);
        }
    }
}
