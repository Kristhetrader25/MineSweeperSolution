using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace MineSweeperClasses
{
    /// <summary>
    /// Represents the Minesweeper game board.
    /// The Board class owns the 2D grid of <see cref="Cell"/> objects,
    /// manages bomb and reward placement, calculates neighbor counts,
    /// and tracks game metadata such as size, difficulty, timing, and state.
    /// </summary>
    public class Board
    {
       
        public int Size { get; set; }

     
        public float Difficulty { get; set; }

        public float Percentage { get; set; }

        
        public Cell[,] Cells { get; set; }

        
        public int RewardsRemaining { get; set; }

        
        public DateTime StartTime { get; set; }

        
        public DateTime EndTime { get; set; }

        
        public enum GameStatus { InProgress, Won, Lost }

        
        Random random = new Random();

        /// <summary>
        /// Creates a new board of the given size and difficulty.
        /// Allocates the grid and initializes bombs, rewards, and neighbor counts.
        /// </summary>
        /// <param name="size">Board dimension (board is size × size).</param>
        /// <param name="difficulty">Bomb probability between 0.0 and 0.25.</param>
        public Board(int size, float difficulty)
        {
            Size = size;
            Difficulty = difficulty;
            Cells = new Cell[size, size];
            RewardsRemaining = 0;
            InitializeBoard();
        }

        /// <summary>
        /// Initializes the board: places bombs, places rewards,
        /// calculates neighbor counts, and sets the game start time.
        /// </summary>
        private void InitializeBoard()
        {
            SetupBombs();
            SetupRewards();
            CalculateNumberOfBombNeighbors();
            StartTime = DateTime.Now;
        }

        
        /// <summary>
        /// Consumes a reward (if available) to undo a reveal at the given cell.
        /// This is the "mulligan" feature: it hides the cell again instead of ending the game.
        /// </summary>
        /// <param name="row">Row index of the target cell.</param>
        /// <param name="col">Column index of the target cell.</param>
        public void UseSpecialBonus(int row, int col)
        {
            if (RewardsRemaining > 0 && IsCellOnBoard(row, col))
            {
                Cell cell = Cells[row, col];
                if (cell.IsRevealed)
                {
                    if (cell.UndoReveal())
                    {
                        RewardsRemaining--;
                        Console.WriteLine($"Undo used at ({row},{col}). Rewards left: {RewardsRemaining}");
                    }
                }
            }
        }

        
        /// <summary>
        /// Calculates the final score after the game ends.
        /// Currently a stub. Later versions should factor in difficulty,
        /// elapsed time, board size, and rewards used.
        /// </summary>
        /// <returns>Score as an integer (currently always 0).</returns>
        public int DetermineFinalScore()
        {
            return 0;
        }


        /// <summary>
        /// Helper function to determine if a cell is out of bounds
        /// Returns true if the given row and column indices are within board boundaries.
        /// </summary>
        private bool IsCellOnBoard(int row, int col)
        {
            return row >= 0 && row < Size && col >= 0 && col < Size;
        }

        
        /// <summary>
        /// For each cell in the grid, calculate how many bombs surround it.
        /// Bomb cells are assigned a special value of 9 in <see cref="Cell.LiveNeighbors"/>.
        /// </summary>
        private void CalculateNumberOfBombNeighbors()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (Cells[i, j].Live)
                    {
                        // Convention: bombs use neighbor count of 9
                        Cells[i, j].LiveNeighbors = 9;
                    }
                    else
                    {
                        Cells[i, j].LiveNeighbors = GetNumberOfBombNeighbors(i, j);
                    }
                }
            }
        }

        /// <summary>
        /// Helper function to determine the number of bomb neighbors for a cell
        /// Counts how many bombs surround the given cell by checking
        /// all 8 neighboring positions using delta-row (dr) and delta-column (dc).
        /// </summary>
        /// <param name="i">Row index of the cell.</param>
        /// <param name="j">Column index of the cell.</param>
        /// <returns>The number of bomb neighbors (0–8).</returns>
        private int GetNumberOfBombNeighbors(int i, int j)
        {
            int count = 0;

            for (int dr = -1; dr <= 1; dr++)
            {
                for (int dc = -1; dc <= 1; dc++)
                {
                    // Skip the cell itself
                    if (dr == 0 && dc == 0) continue;

                    int r = i + dr;
                    int c = j + dc;

                    // If neighbor is on the board and live, increment count
                    if (IsCellOnBoard(r, c) && Cells[r, c].Live)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

       
        /// <summary>
        /// Initializes the grid and assigns bombs based on difficulty probability.
        /// First creates non-live cells, then assigns bombs independently
        /// with probability p (clamped between 0 and 0.25).
        /// </summary>
        private void SetupBombs()
        {
            // Initialize all cells first
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Cells[i, j] = new Cell
                    {
                        Live = false,
                        LiveNeighbors = 0,
                        HasReward = false
                    };
                }
            }

            // We interpret Difficulty as a probability (0.0–0.25).
            float p = Math.Max(0f, Math.Min(0.25f, Difficulty));

            // Assign bombs independently by probability p
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (random.NextDouble() < p)
                    {
                        Cells[i, j].Live = true;
                    }
                }
            }
        }

        
        /// <summary>
        /// Randomly places a small number of rewards (≈ 2% of cells) on the grid.
        /// Rewards never overlap bombs and are guaranteed to place at least one.
        /// </summary>
        private void SetupRewards()
        {
            // Place a small number of rewards (≈ 2% of cells), never on bombs.
            int totalCells = Size * Size;
            int rewardTargets = Math.Max(1, (int)Math.Round(totalCells * 0.02));

            int placed = 0;
            int safety = totalCells * 5; // avoid infinite loops on tiny boards
            while (placed < rewardTargets && safety-- > 0)
            {
                int r = random.Next(Size);
                int c = random.Next(Size);

                if (!Cells[r, c].Live && !Cells[r, c].HasReward)
                {
                    Cells[r, c].HasReward = true;
                    placed++;
                }
            }
        }

       
        /// <summary>
        /// Returns the current game status.
        /// Currently always returns <see cref="GameStatus.InProgress"/>.
        /// Future versions will detect win/loss conditions.
        /// </summary>
        public GameStatus DetermineGameState()
        {
            return GameStatus.InProgress;
        }
    }
}
