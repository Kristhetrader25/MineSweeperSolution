using System;
using MineSweeperClasses;
using MineSweeperConsole;

namespace MineSweeperConsole
{
    /// <summary>
    /// Orchestrates a single console Minesweeper game:
    /// handles input, calls Board for logic, and uses Renderer for output.
    /// </summary>
    public class GameSession
    {
        private readonly Board board;

        // Track last visited for Undo convenience
        private int lastRow = -1, lastCol = -1;

        /// <summary>
        /// Creates a new game session with a fresh board of the given size and difficulty.
        /// </summary>
        /// <param name="size">Square dimension of the board (Size × Size).</param>
        /// <param name="difficulty">
        /// Bomb probability in the range [0.0, 0.25]; e.g., 0.10 = ~10% bombs.
        /// </param>
        public GameSession(int size, float difficulty)
        {
            board = new Board(size, difficulty);
        }

        /// <summary>
        /// Runs the interactive game loop.
        /// Optionally prints the answer key up front (for testing), then
        /// repeatedly prompts the player for a move until the game is won or lost.
        /// </summary>
        /// <param name="showAnswerKey">
        /// If <c>true</c>, prints the full answer key before gameplay for debugging/testing.
        /// </param>
        public void Run(bool showAnswerKey = false)
        {
            Console.WriteLine("Hello, welcome to Minesweeper!");

            if (showAnswerKey)
            {
                Console.WriteLine("Answer key (for testing):");
                Renderer.PrintAnswers(board);
            }

            Console.WriteLine("\nPlayable board:");
            Renderer.PrintBoard(board);

            bool victory = false;
            bool death = false;

            // Main turn loop: read input -> apply action -> check state -> re-render
            while (!victory && !death)
            {
                int row = ReadInt($"\nEnter row (0..{board.Size - 1}): ", 0, board.Size - 1);
                int col = ReadInt($"Enter col (0..{board.Size - 1}): ", 0, board.Size - 1);

                var action = ReadAction("Choose action [V=Visit, F=Flag, R=Use Reward]: ");

                var cell = board.Cells[row, col];

                switch (action)
                {
                    case PlayerAction.Visit:
                        {
                            lastRow = row; lastCol = col;

                            bool safe = cell.Reveal();

                            if (safe)
                            {
                                // reward discovery on safe reveal
                                if (cell.CollectReward())
                                {
                                    board.RewardsRemaining++;
                                    Console.WriteLine($"You found a reward! Rewards now: {board.RewardsRemaining}");
                                }
                            }
                            else
                            {
                                // Bomb revealed — offer immediate mulligan BEFORE state is checked
                                Console.WriteLine("You revealed a BOMB!");

                                if (board.RewardsRemaining > 0)
                                {
                                    Console.Write($"Use a reward to undo this reveal? (Y/N) [Rewards: {board.RewardsRemaining}]: ");
                                    var ans = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

                                    if (ans == "Y" || ans == "YES")
                                    {
                                        board.UseSpecialBonus(row, col); 
                                    }
                                    else
                                    {
                                        // Player declined to use reward -> end the game
                                        death = true;
                                    }
                                }
                                else
                                {
                                    // No rewards available -> end the game
                                    Console.WriteLine("You do not have any rewards to use.");
                                    Console.WriteLine($"Rewards remaining: {board.RewardsRemaining}");
                                    death = true;
                                }
                            }

                            break;
                        }


                    case PlayerAction.Flag:
                        {
                            cell.ToggleFlag();
                            break;
                        }

                    case PlayerAction.UseReward:
                        {
                            // Guard: only allow reward usage if any are available
                            if (board.RewardsRemaining <= 0)
                            {
                                Console.WriteLine("You do not have any rewards to use.");
                                Console.WriteLine($"Rewards remaining: {board.RewardsRemaining}");
                                break; 
                            }

                            Console.WriteLine($"Rewards remaining: {board.RewardsRemaining}");

                            // allow quick Enter to target last visited
                            int rr = ReadInt($"Undo row (0..{board.Size - 1}) [Enter for last: {lastRow}]: ",
                                             -1, board.Size - 1, allowBlank: true);
                            int cc = ReadInt($"Undo col (0..{board.Size - 1}) [Enter for last: {lastCol}]: ",
                                             -1, board.Size - 1, allowBlank: true);
                            if (rr == -1 && cc == -1 && lastRow >= 0 && lastCol >= 0)
                            {
                                rr = lastRow; cc = lastCol;
                            }

                            board.UseSpecialBonus(rr, cc); // this will decrement if successful
                            break;
                        }

                }

                var state = board.DetermineGameState();
                victory = (state == Board.GameStatus.Won);
                death = (state == Board.GameStatus.Lost);

                Renderer.PrintBoard(board);
            }

            Console.WriteLine(victory
                ? "\nVictory! You cleared the board."
                : "\nGame Over! You hit a bomb.");
        }


        // ---------- Input helpers (UI concern) ----------

        /// <summary>
        /// Reads an integer from the console within the inclusive range [min, max].
        /// Optionally allows a blank entry to return -1 (used for "use last" behavior).
        /// </summary>
        /// <param name="prompt">Prompt text to display before reading input.</param>
        /// <param name="min">Minimum acceptable value (inclusive).</param>
        /// <param name="max">Maximum acceptable value (inclusive).</param>
        /// <param name="allowBlank">
        /// If <c>true</c>, an empty input returns -1; otherwise the user is reprompted.
        /// </param>
        /// <returns>
        /// An integer within [min, max], or -1 when <paramref name="allowBlank"/> is true and the input is empty.
        /// </returns>
        private static int ReadInt(string prompt, int min, int max, bool allowBlank = false)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();

                if (allowBlank && string.IsNullOrWhiteSpace(s))
                {
                    return -1;
                }

                if (int.TryParse(s, out int v) && v >= min && v <= max)
                {
                    return v;
                }

                Console.WriteLine($"Please enter an integer between {min} and {max}.");
            }
        }

        /// <summary>
        /// Reads a player action from the console and normalizes to a <see cref="PlayerAction"/>.
        /// Accepts short forms (V/F/R) and full words (Visit/Flag/Use Reward).
        /// </summary>
        /// <param name="prompt">Prompt text to display before reading input.</param>
        /// <returns>The selected <see cref="PlayerAction"/>.</returns>
        private static PlayerAction ReadAction(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

                if (s == "V" || s == "VISIT")
                {
                    return PlayerAction.Visit;
                }
                if (s == "F" || s == "FLAG")
                {
                    return PlayerAction.Flag;
                }
                if (s == "R" || s == "USE REWARD" || s == "REWARD")
                {
                    return PlayerAction.UseReward;
                }

                Console.WriteLine("Please enter V, F, or R.");
            }
        }
    }

    /// <summary>Commands the player can take each turn.</summary>
    public enum PlayerAction { Visit, Flag, UseReward }
}
