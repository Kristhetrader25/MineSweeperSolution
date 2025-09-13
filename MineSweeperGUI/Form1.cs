using MineSweeperClasses;
using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace MineSweeperGUI
{
    /// <summary>
    /// Windows Forms UI for the Minesweeper board.
    /// Creates and lays out the button grid, wires user input,
    /// and renders the visual state based on the underlying <see cref="Board"/>.
    /// </summary>
    public partial class Form1 : Form
    {
        

        /// <summary> Image used for unrevealed tiles (stone texture).</summary>
        private Image imgTileHidden;

        /// <summary> Image used for revealed empty tiles (flat texture).</summary>
        private Image imgTileEmpty;

        /// <summary> Image shown when a bomb is revealed.</summary>
        private Image imgBomb;

        /// <summary> Imageused to briefly show a collected reward (optional).</summary>
        private Image imgReward;

        /// <summary> Images for numbers 1..8 indicating adjacent bomb counts.</summary>
        private Image[] imgNumbers;

        // --- Game model and UI state -------------------------------------------

        /// <summary>The game board model containing cells, bombs, rewards, etc.</summary>
        Board board;

        /// <summary>2D array of buttons mirrored to <see cref="Board.Cells"/>.</summary>
        private Button[,] buttons = null;

        /// <summary>Minimum cell size in pixels to keep the UI usable when the panel is small.</summary>
        private const int MinCell = 18;

        /// <summary>Coordinates of the last visited cell (handy for “Undo” UX).</summary>
        private int lastRow = -1, lastCol = -1;

        // --- Construction & initialization -------------------------------------

        /// <summary>
        /// Creates the playable form and initializes the UI according to the given
        /// board size and bomb probability.
        /// </summary>
        /// <param name="size">Square dimension of the board (size × size).</param>
        /// <param name="difficulty">Bomb probability in the range [0.0, 0.25].</param>
        public Form1(int size, float difficulty)
        {
            board = new Board(size, difficulty);
            InitializeComponent();

            // Make the board area fill the center panel and re-layout on resize.
            pnlGameBoard.Dock = DockStyle.Fill;
            pnlGameBoard.Resize += (_, __) => LayoutBoard();

            // Show the game start time.
            label2.Text = board.StartTime.ToString("g");

            // Helper to build a path into the copied Resources folder next to the EXE.
            string R(string file) => Path.Combine(Application.StartupPath, "Resources", file);

            // Load images from the Resources folder (copied by MSBuild at build time).
            imgTileHidden = Image.FromFile(R("Tile 1.png"));
            imgTileEmpty = Image.FromFile(R("Tile Flat.png"));
            imgBomb = Image.FromFile(R("Skull.png"));
            imgReward = Image.FromFile(R("Gold.png"));

            // Initialize number sprites array (1..8).
            imgNumbers = new Image[9];
            imgNumbers[1] = Image.FromFile(R("Number 1.png"));
            imgNumbers[2] = Image.FromFile(R("Number 2.png"));
            imgNumbers[3] = Image.FromFile(R("Number 3.png"));
            imgNumbers[4] = Image.FromFile(R("Number 4.png"));
            imgNumbers[5] = Image.FromFile(R("Number 5.png"));
            imgNumbers[6] = Image.FromFile(R("Number 6.png"));
            imgNumbers[7] = Image.FromFile(R("Number 7.png"));
            imgNumbers[8] = Image.FromFile(R("Number 8.png"));

            RenderBoard();
        }

        /// <summary>
        /// Creates the button grid and wires click events. Also performs initial layout.
        /// </summary>
        private void RenderBoard()
        {
            int n = board.Size;

            // Reset the panel and button cache.
            pnlGameBoard.Controls.Clear();
            buttons = new Button[n, n];

            // Create buttons mapped to each cell.
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    // Create a cell button with consistent visual defaults.
                    var btn = new Button
                    {
                        Tag = (r, c),
                        AutoSize = false,
                        UseMnemonic = false,
                        AutoEllipsis = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        FlatStyle = FlatStyle.Standard,
                        Margin = Padding.Empty,
                        Padding = Padding.Empty,
                        TabStop = false,

                        // Set unrevealed look.
                        BackgroundImage = imgTileHidden,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Text = "?"
                    };

                    // Wire left-click to reveal/visit.
                    btn.Click += Cell_LeftClick;

                    // Wire right-click to toggle a flag.
                    btn.MouseUp += Cell_RightClick;

                    pnlGameBoard.Controls.Add(btn);
                    buttons[r, c] = btn;
                }
            }

            // Size and position the grid now that controls exist.
            LayoutBoard();
        }

        /// <summary>
        /// Computes cell size to fit the panel, centers the grid, and adjusts fonts to fit.
        /// </summary>
        private void LayoutBoard()
        {
            if (buttons == null) return;

            int n = board.Size;

            // Determine available client area.
            int w = pnlGameBoard.ClientSize.Width;
            int h = pnlGameBoard.ClientSize.Height;

            // Compute a square cell size that fits both dimensions; clamp to a usable minimum.
            int cell = Math.Max(MinCell, Math.Min(w / n, h / n));

            // Center the grid inside the panel.
            int gridW = cell * n;
            int gridH = cell * n;
            int left = (w - gridW) / 2;
            int top = (h - gridH) / 2;

            // Apply bounds and font sizing to each button.
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    var btn = buttons[r, c];
                    btn.SetBounds(left + c * cell, top + r * cell, cell, cell);

                    var newFont = GetFittingFont(btn.Font, cell);
                    if (Math.Abs(btn.Font.Size - newFont.Size) > 0.1f || btn.Font.Style != newFont.Style)
                    {
                        btn.Font = newFont;
                    }
                }
            }
        }

        /// <summary>
        /// Returns a font that fits inside a square cell for single-character overlays (e.g., “8”, “F”, “?”).
        /// </summary>
        private Font GetFittingFont(Font baseFont, int cell)
        {
            // Start with a conservative guess, then shrink until it fits.
            float size = Math.Max(7.0f, cell * 0.42f);
            using var g = pnlGameBoard.CreateGraphics();

            // Measure a wide glyph to ensure headroom.
            string probe = "8";
            SizeF measured = g.MeasureString(probe, new Font(baseFont.FontFamily, size, FontStyle.Bold));

            // Leave a small margin.
            float maxW = cell - 4;
            float maxH = cell - 4;

            // Decrease font size until it fits both width and height.
            while ((measured.Width > maxW || measured.Height > maxH) && size > 6.0f)
            {
                size -= 0.5f;
                measured = g.MeasureString(probe, new Font(baseFont.FontFamily, size, FontStyle.Bold));
            }

            // Use regular style when tiny; bold can bloat glyphs at small sizes.
            var style = size < 9.0f ? FontStyle.Regular : FontStyle.Bold;
            return new Font(baseFont.FontFamily, size, style);
        }

        // --- Event handlers -----------------------------------------------------

        /// <summary>
        /// Handles left-click (visit/reveal). Offers an undo if the player hits a bomb,
        /// performs flood fill for zero-neighbor cells, and updates the board UI and state.
        /// </summary>
        private void Cell_LeftClick(object? sender, EventArgs e)
        {
            var btn = (Button)sender!;
            var (r, c) = ((int, int))btn.Tag;
            var cell = board.Cells[r, c];

            lastRow = r; lastCol = c;

            // Ignore if already revealed or flagged.
            if (cell.IsRevealed || cell.IsFlagged) return;

            // Bomb case: reveal, then offer mulligan if available.
            if (cell.Live)
            {
                cell.Reveal();
                UpdateCellButton(r, c);

                if (board.RewardsRemaining > 0)
                {
                    var use = MessageBox.Show(
                        $"You hit a bomb at ({r},{c}). Use a reward to undo?",
                        "Boom!",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (use == DialogResult.Yes)
                    {
                        board.UseSpecialBonus(r, c);   // Decrements reward if successful.
                        RefreshBoardUI();
                        CheckGameState();
                        return;
                    }
                }

                EndGame(lost: true);
                return;
            }

            // Safe click: reveal the cell and handle rewards.
            bool safe = cell.Reveal();

            if (cell.CollectReward())
            {
                board.RewardsRemaining++;
                MessageBox.Show($"You found a reward! Rewards now: {board.RewardsRemaining}",
                                "Reward", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // If no adjacent bombs, reveal the region.
            if (cell.LiveNeighbors == 0)
            {
                board.FloodFill(r, c);
            }

            RefreshBoardUI();
            CheckGameState();
        }

        /// <summary>
        /// Handles right-click (flag/unflag). Flags are ignored on already revealed cells.
        /// </summary>
        private void Cell_RightClick(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var btn = (Button)sender!;
            var (r, c) = ((int, int))btn.Tag;
            var cell = board.Cells[r, c];

            if (cell.IsRevealed) return;  
            cell.ToggleFlag();

            UpdateCellButton(r, c);

            // Check for a win that occurs by correct flagging
            CheckGameState();
        }


        // --- Rendering helpers --------------------------------------------------

        /// <summary>
        /// Repaints the entire board by updating each button to match its cell state.
        /// </summary>
        private void RefreshBoardUI()
        {
            int n = board.Size;
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                    UpdateCellButton(r, c);
        }

        /// <summary>
        /// Maps a single cell’s state to the appropriate sprite/overlay and button state.
        /// </summary>
        private void UpdateCellButton(int r, int c)
        {
            var cell = board.Cells[r, c];
            var btn = buttons[r, c];

            // Always stretch the background image to fill the button.
            btn.BackgroundImageLayout = ImageLayout.Stretch;

            // Default overlay text/color for this repaint.
            btn.Text = string.Empty;
            btn.ForeColor = SystemColors.ControlText;

            // Unrevealed state.
            if (!cell.IsRevealed)
            {
                btn.Enabled = true;
                btn.BackgroundImage = imgTileHidden;

                // Show a flag overlay when flagged; otherwise, a question mark.
                if (cell.IsFlagged)
                {
                    btn.Text = "F";
                    btn.ForeColor = Color.Goldenrod;
                }
                else
                {
                    btn.Text = "?";
                }
                return;
            }

            // Revealed state.
            btn.Enabled = false;

            // Bomb tile.
            if (cell.Live)
            {
                btn.BackgroundImage = imgBomb;
                return;
            }

            // Safe revealed: choose empty or number sprite.
            if (cell.LiveNeighbors == 0)
            {
                btn.BackgroundImage = imgTileEmpty;
            }
            else
            {
                btn.BackgroundImage = imgNumbers[cell.LiveNeighbors];
            }
        }

        /// <summary>
        /// Provides a color hint for numeric overlays (kept for parity with console variant).
        /// </summary>
        private Color NumberColor(int n) => n switch
        {
            1 => Color.CadetBlue,
            2 => Color.ForestGreen,
            3 => Color.Firebrick,
            4 => Color.DarkGoldenrod,
            5 => Color.MediumVioletRed,
            6 => Color.Teal,
            7 => Color.DimGray,
            8 => Color.Gray,
            _ => SystemColors.ControlText
        };

        // --- Game state & end-of-game ------------------------------------------

        /// <summary>
        /// Checks the model for win/loss and finalizes the game if either condition is met.
        /// </summary>
        private void CheckGameState()
        {
            var state = board.DetermineGameState();
            if (state == Board.GameStatus.Won)
            {
                EndGame(lost: false);
            }
            else if (state == Board.GameStatus.Lost)
            {
                EndGame(lost: true);
            }
        }

        /// <summary>
        /// Finalizes the game, shows a message, updates time/score labels,
        /// and disables further interaction.
        /// </summary>
        private void EndGame(bool lost)
        {
            // Stop the clock and compute elapsed seconds (classic scoring: lower is better).
            board.EndTime = DateTime.Now;
            var elapsed = (int)(board.EndTime - board.StartTime).TotalSeconds;

            // Update score label with elapsed time.
            label4.Text = $"{elapsed} s";

            // Ensure the latest state is visible.
            RefreshBoardUI();

            var msg = lost
                ? $"Game Over! You hit a bomb.\nTime: {elapsed} s"
                : $"Victory! You cleared the board.\nTime: {elapsed} s";

            MessageBox.Show(msg,
                            lost ? "Defeat" : "Victory",
                            MessageBoxButtons.OK,
                            lost ? MessageBoxIcon.Error : MessageBoxIcon.Information);

            // Disable all buttons to prevent further moves.
            foreach (var b in buttons) b.Enabled = false;
        }

        // --- Navigation ---------------------------------------------------------

        /// <summary>
        /// Returns to the start form and allows the player to choose a new size/difficulty.
        /// </summary>
        private void btnRestart_Click(object sender, EventArgs e)
        {
            StartGameForm startForm = new StartGameForm();
            startForm.Show();
            this.Close();
        }
    }
}


