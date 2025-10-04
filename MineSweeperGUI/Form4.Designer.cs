namespace MineSweeperGUI
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            mnuFileSave = new ToolStripMenuItem();
            mnuFileLoad = new ToolStripMenuItem();
            mnuFileExit = new ToolStripMenuItem();
            sortToolStripMenuItem = new ToolStripMenuItem();
            mnuSortByName = new ToolStripMenuItem();
            mnuSortByScore = new ToolStripMenuItem();
            mnuSortByDate = new ToolStripMenuItem();
            dgvScores = new DataGridView();
            btnOK = new Button();
            pnlStats = new Panel();
            lblWorstTime = new Label();
            lblBestTime = new Label();
            lblAvgTime = new Label();
            lblGames = new Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvScores).BeginInit();
            pnlStats.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, sortToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1020, 40);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuFileSave, mnuFileLoad, mnuFileExit });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(71, 36);
            fileToolStripMenuItem.Text = "File";
            // 
            // mnuFileSave
            // 
            mnuFileSave.Name = "mnuFileSave";
            mnuFileSave.Size = new Size(198, 44);
            mnuFileSave.Text = "Save";
            // 
            // mnuFileLoad
            // 
            mnuFileLoad.Name = "mnuFileLoad";
            mnuFileLoad.Size = new Size(198, 44);
            mnuFileLoad.Text = "Load";
            // 
            // mnuFileExit
            // 
            mnuFileExit.Name = "mnuFileExit";
            mnuFileExit.Size = new Size(198, 44);
            mnuFileExit.Text = "Exit";
            // 
            // sortToolStripMenuItem
            // 
            sortToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuSortByName, mnuSortByScore, mnuSortByDate });
            sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            sortToolStripMenuItem.Size = new Size(77, 36);
            sortToolStripMenuItem.Text = "Sort";
            // 
            // mnuSortByName
            // 
            mnuSortByName.Name = "mnuSortByName";
            mnuSortByName.Size = new Size(244, 44);
            mnuSortByName.Text = "By Name";
            // 
            // mnuSortByScore
            // 
            mnuSortByScore.Name = "mnuSortByScore";
            mnuSortByScore.Size = new Size(244, 44);
            mnuSortByScore.Text = "By Score";
            // 
            // mnuSortByDate
            // 
            mnuSortByDate.Name = "mnuSortByDate";
            mnuSortByDate.Size = new Size(244, 44);
            mnuSortByDate.Text = "By Date";
            // 
            // dgvScores
            // 
            dgvScores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvScores.Location = new Point(12, 94);
            dgvScores.Name = "dgvScores";
            dgvScores.RowHeadersWidth = 82;
            dgvScores.Size = new Size(923, 503);
            dgvScores.TabIndex = 1;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(12, 622);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(150, 46);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // pnlStats
            // 
            pnlStats.Controls.Add(lblWorstTime);
            pnlStats.Controls.Add(lblBestTime);
            pnlStats.Controls.Add(lblAvgTime);
            pnlStats.Controls.Add(lblGames);
            pnlStats.Dock = DockStyle.Bottom;
            pnlStats.Location = new Point(0, 681);
            pnlStats.Name = "pnlStats";
            pnlStats.Size = new Size(1020, 200);
            pnlStats.TabIndex = 3;
            // 
            // lblWorstTime
            // 
            lblWorstTime.AutoSize = true;
            lblWorstTime.Location = new Point(257, 90);
            lblWorstTime.Name = "lblWorstTime";
            lblWorstTime.Size = new Size(177, 32);
            lblWorstTime.TabIndex = 3;
            lblWorstTime.Text = "Worst Time: 0 s";
            // 
            // lblBestTime
            // 
            lblBestTime.AutoSize = true;
            lblBestTime.Location = new Point(257, 16);
            lblBestTime.Name = "lblBestTime";
            lblBestTime.Size = new Size(161, 32);
            lblBestTime.TabIndex = 2;
            lblBestTime.Text = "Best Time: 0 s";
            // 
            // lblAvgTime
            // 
            lblAvgTime.AutoSize = true;
            lblAvgTime.Location = new Point(27, 90);
            lblAvgTime.Name = "lblAvgTime";
            lblAvgTime.Size = new Size(157, 32);
            lblAvgTime.TabIndex = 1;
            lblAvgTime.Text = "Avg Time: 0 s";
            // 
            // lblGames
            // 
            lblGames.AutoSize = true;
            lblGames.Location = new Point(27, 16);
            lblGames.Name = "lblGames";
            lblGames.Size = new Size(111, 32);
            lblGames.TabIndex = 0;
            lblGames.Text = "Games: 0";
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1020, 881);
            Controls.Add(pnlStats);
            Controls.Add(btnOK);
            Controls.Add(dgvScores);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form4";
            Text = "Form4";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvScores).EndInit();
            pnlStats.ResumeLayout(false);
            pnlStats.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem sortToolStripMenuItem;
        private DataGridView dgvScores;
        private Button btnOK;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileLoad;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuSortByName;
        private ToolStripMenuItem mnuSortByScore;
        private ToolStripMenuItem mnuSortByDate;
        private Panel pnlStats;
        private Label lblWorstTime;
        private Label lblBestTime;
        private Label lblAvgTime;
        private Label lblGames;
    }
}