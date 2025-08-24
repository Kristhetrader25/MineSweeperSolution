using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweeperClasses;

namespace MineSweeperConsole
{
    /// <summary>
    /// Responsible for console rendering of Minesweeper boards.
    /// This class is UI-only: it formats and prints grids, headers, borders,
    /// and cells. It does not contain any game logic.
    /// </summary>
    public static class Renderer
    {
        private const int CellWidth = 3;        // " x " = 3 chars
        private const string Margin = "   ";    // aligns with row labels "{r,2} "

        /// <summary>
        /// Renders the full <b>answer key</b> for a board to the console.
        /// Shows bombs ('B'), empty cells ('.'), and counts ('1'..'8') with borders and row/column indices.
        /// </summary>
        /// <param name="board">The board whose answer key will be printed.</param>
        public static void PrintAnswers(Board board)
        {
            int n = board.Size;

            // Column headers (0..n-1), aligned to cell centers
            PrintColumnHeaders(n);

            // Top border
            PrintBorder(n);

            // Body rows
            for (int r = 0; r < n; r++)
            {
                // Row label (left margin aligns with border)
                Console.Write($"{r,2} ");

                // Leading vertical bar for the grid
                Console.Write("|");

                // Row contents: cell -> bar -> cell -> bar ...
                for (int c = 0; c < n; c++)
                {
                    char ch = board.Cells[r, c].GetAnswerChar();
                    WriteCell(ch);
                    Console.Write("|");
                }

                // End of row + horizontal border
                Console.WriteLine();
                PrintBorder(n);
            }
        }

        /// <summary>
        /// Writes the centered column indices above the grid.
        /// Each index is centered within the 3-character cell width and
        /// accounts for the vertical bar that appears on data rows.
        /// </summary>
        /// <param name="n">Number of columns (board size).</param>
        private static void PrintColumnHeaders(int n)
        {
            // Left margin aligns with row labels/border
            Console.Write(Margin);

            for (int c = 0; c < n; c++)
            {
                // 1) compensate for the '|' that appears on data rows
                Console.Write(" ");

                // 2) center the index in the 3‑char cell area
                string s = c.ToString();
                int left = (CellWidth - s.Length) / 2;
                int right = CellWidth - s.Length - left;

                Console.Write(new string(' ', left));
                Console.Write(s);
                Console.Write(new string(' ', right));
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Prints a horizontal border line for the grid, scaled to the given size.
        /// Uses "+---" per column plus a trailing "+" to close the row.
        /// </summary>
        /// <param name="size">Number of columns (board size).</param>
        private static void PrintBorder(int size)
        {
            // Left margin to align with row label column
            Console.Write(Margin);

            // One "+---" segment per column (CellWidth dashes), then final "+"
            string segment = "+" + new string('-', CellWidth);
            for (int i = 0; i < size; i++)
                Console.Write(segment);
            Console.WriteLine("+");
        }

        /// <summary>
        /// Writes a single cell using the shared 3-character contract (" x ").
        /// Applies classic Minesweeper colors for digits and bombs, and restores
        /// the previous console color to prevent bleed into surrounding text.
        /// </summary>
        /// <param name="ch">
        /// The display character for the cell:
        /// 'B' for bomb, '.' for empty, '1'..'8' for neighbor counts.
        /// </param>
        private static void WriteCell(char ch)
        {
            // Leading space (part of the 3-char cell design: " x ")
            Console.Write(" ");

            // Preserve current color so we can restore after printing the cell
            var prev = Console.ForegroundColor;

            if (ch >= '1' && ch <= '8')
            {
                // Numbered cells use a simple color palette for readability
                Console.ForegroundColor = ch switch
                {
                    '1' => ConsoleColor.Cyan,
                    '2' => ConsoleColor.Green,
                    '3' => ConsoleColor.Red,
                    '4' => ConsoleColor.DarkYellow,
                    '5' => ConsoleColor.Magenta,
                    '6' => ConsoleColor.DarkCyan,
                    '7' => ConsoleColor.DarkGray,
                    '8' => ConsoleColor.Gray,
                    _ => prev
                };
                Console.Write(ch);
                Console.ForegroundColor = prev; // restore
            }
            else if (ch == 'B')
            {
                // Bombs in a distinct color
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write('B');
                Console.ForegroundColor = prev; // restore
            }
            else
            {
                // '.' or other neutral characters
                Console.Write(ch);
            }

            // Trailing space (completes the 3-char cell design)
            Console.Write(" ");
        }
    }
}
