using MineSweeperClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeperGUI
{
    /// <summary>
    /// High scores window that binds a list of GameStat records to a grid,
    /// supports saving and loading from disk, and provides simple sorting.
    /// </summary>
    public partial class Form4 : Form
    {
        /// <summary>
        /// In-memory list of scores that supports change notifications for data binding.
        /// </summary>
        private readonly BindingList<GameStat> _stats;

        /// <summary>
        /// Binding source used to connect the BindingList to the DataGridView.
        /// </summary>
        private readonly BindingSource _binding = new BindingSource();

        /// <summary>
        /// Location where scores are saved and loaded (per user, per application).
        /// </summary>
        private static string SavePath =>
            Path.Combine(Application.LocalUserAppDataPath, "highscores.json");

        /// <summary>
        /// Convenience constructor used when opening the scores window immediately after a win.
        /// Adds a single incoming score to the list before displaying.
        /// </summary>
        /// <param name="name">Player name captured from the win dialog.</param>
        /// <param name="score">Elapsed time in seconds for the winning game.</param>
        public Form4(string name, int score)
            : this()
        {
            // Add the incoming score as a new record.
            var gs = new GameStat
            {
                Name = name,
                Score = score,
                Date = DateTime.Now
            };
            AddWithId(gs);
            RecomputeAggregates();
        }

        /// <summary>
        /// Default constructor used by the designer and when opening the scores window standalone.
        /// Initializes bindings, wires menu events, and attempts to load existing scores.
        /// </summary>
        public Form4()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;

            // Bind the grid to the binding source and the binding source to the list.
            _stats = new BindingList<GameStat>();
            _binding.DataSource = _stats;
            dgvScores.AutoGenerateColumns = true;
            dgvScores.DataSource = _binding;

            // Wire up menu items to persistence and sorting actions.
            mnuFileSave.Click += (_, __) => SaveToDisk();
            mnuFileLoad.Click += (_, __) => LoadFromDisk();
            mnuFileExit.Click += (_, __) => Close();

            mnuSortByName.Click += (_, __) => Reorder(s => s.Name, asc: true);
            mnuSortByScore.Click += (_, __) => Reorder(s => s.Score, asc: true);
            mnuSortByDate.Click += (_, __) => Reorder(s => s.Date, asc: false);

            // Close the window from the OK button.
            btnOK.Click += (_, __) => Close();

            // Attempt to auto-load existing scores silently on open.
            LoadFromDisk(silent: true);
            RecomputeAggregates();
        }

        /// <summary>
        /// Adds a new score and assigns a sequential Id based on current contents.
        /// </summary>
        private void AddWithId(GameStat gs)
        {
            gs.Id = _stats.Count == 0 ? 1 : (_stats.Max(s => s.Id) + 1);
            _stats.Add(gs);
            RecomputeAggregates();
        }

        /// <summary>
        /// Reorders the list by the given key selector and direction, then reassigns Ids.
        /// </summary>
        private void Reorder<TKey>(Func<GameStat, TKey> key, bool asc)
        {
            IEnumerable<GameStat> ordered = asc
                ? _stats.OrderBy(key)
                : _stats.OrderByDescending(key);

            var snapshot = ordered.ToList();
            _stats.Clear();
            int id = 1;
            foreach (var s in snapshot)
            {
                s.Id = id++;
                _stats.Add(s);
            }

            // after repopulating _stats
            RecomputeAggregates();

        }

        /// <summary>
        /// Serializes the current scores to JSON and writes them to disk.
        /// </summary>
        private void SaveToDisk()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);
                var json = JsonSerializer.Serialize(_stats.ToList(),
                    new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SavePath, json);
                MessageBox.Show("Scores saved.", "Save",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save failed:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads scores from disk if present and replaces the current in-memory list.
        /// Optionally suppresses the success message when used during startup.
        /// </summary>
        private void LoadFromDisk(bool silent = false)
        {
            try
            {
                if (!File.Exists(SavePath))
                {
                    return;
                }

                var json = File.ReadAllText(SavePath);
                var list = JsonSerializer.Deserialize<List<GameStat>>(json) ?? new List<GameStat>();

                _stats.Clear();
                foreach (var s in list.OrderBy(s => s.Id))
                {
                    _stats.Add(s);
                }

                if (!silent)
                {
                    MessageBox.Show("Scores loaded.", "Load",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                RecomputeAggregates();

            }
            catch (Exception ex)
            {
                if (!silent)
                {
                    MessageBox.Show($"Load failed:\n{ex.Message}", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                   
            }
        }

        /// <summary>
        /// Recomputes and displays aggregate stats (count, average/best/worst time).
        /// Safe when the list is empty or when points are not tracked.
        /// </summary>
        private void RecomputeAggregates()
        {
            // Count of games.
            int n = _stats.Count;
            lblGames.Text = $"Games: {n}";

            if (n == 0)
            {
                lblAvgTime.Text = "Avg Time: 0 s";
                lblBestTime.Text = "Best Time: 0 s";
                lblWorstTime.Text = "Worst Time: 0 s";
                
                return;
            }

            // Time aggregates.
            var times = _stats.Select(s => s.Score).ToArray();
            double avgSec = times.Average();
            int best = times.Min();
            int worst = times.Max();

            lblAvgTime.Text = $"Avg Time: {Math.Round(avgSec, 1)} s";
            lblBestTime.Text = $"Best Time: {best} s";
            lblWorstTime.Text = $"Worst Time: {worst} s";
            
        }

    }
}

