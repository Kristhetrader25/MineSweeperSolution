namespace MineSweeperGUI
{
    /// <summary>
    /// Application entry point for the Minesweeper GUI.
    /// Configures high-DPI/default font settings and launches the start screen.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configure application defaults (DPI awareness, default fonts, etc.).
            ApplicationConfiguration.Initialize();

            // Launch the start form where the player selects size and difficulty.
            Application.Run(new StartGameForm());
        }
    }
}
