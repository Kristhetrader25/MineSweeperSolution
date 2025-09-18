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
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvScores).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, sortToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(961, 40);
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
            // Form4
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 692);
            Controls.Add(btnOK);
            Controls.Add(dgvScores);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form4";
            Text = "Form4";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvScores).EndInit();
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
    }
}