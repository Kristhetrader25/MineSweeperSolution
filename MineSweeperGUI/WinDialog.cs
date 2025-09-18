using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeperGUI
{
    /// <summary>
    /// Modal dialog that appears after a win to collect the player's name and
    /// display the final score in seconds. The form returns DialogResult.OK
    /// when the user provides a non-empty name and clicks OK.
    /// </summary>
    public partial class WinDialog : Form
    {
        /// <summary>
        /// Gets the trimmed player name from the input textbox.
        /// </summary>
        public string WinnerName => txtName.Text.Trim();

        /// <summary>
        /// Initializes the dialog, seeds the score label, and wires default UI behavior.
        /// </summary>
        /// <param name="scoreSeconds">Elapsed time for the winning game in seconds.</param>
        public WinDialog(int scoreSeconds)
        {
            InitializeComponent();

            // Show the player's score as "N s" (seconds).
            lblScoreValue.Text = $"{scoreSeconds} s";

            // Make the Enter key activate the OK button by default.
            AcceptButton = btnOK;

            // Center the dialog relative to its owner window.
            StartPosition = FormStartPosition.CenterParent;

            // Handle validation and closure when the OK button is clicked.
            btnOK.Click += btnOK_Click;
        }

        /// <summary>
        /// Validates the name field and prevents closing the dialog if the name is empty.
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WinnerName))
            {
                MessageBox.Show(
                    "Please enter a name.",
                    "Name required",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Keep the dialog open by cancelling the OK result.
                this.DialogResult = DialogResult.None;
            }
        }
    }
}
