using System;
using MineSweeperClasses;
using MineSweeperConsole;

namespace MineSweeperConsole
{
    internal class Program
    {

        /// <summary>
        /// Application entry point. 
        /// Creates a new <see cref="GameSession"/> with a fixed size and difficulty,
        /// then runs the game loop. A flag is provided to optionally show the 
        /// answer key before play begins (useful for testing and debugging).
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        static void Main(string[] args)
        {
            var session = new GameSession(size: 3, difficulty: 0.10f);

            // Set to true during testing, false for real play
            session.Run(showAnswerKey: true);
        }
    }
}