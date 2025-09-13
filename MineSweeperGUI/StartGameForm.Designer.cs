namespace MineSweeperGUI
{
    partial class StartGameForm
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
            tbSize = new TrackBar();
            tbDifficulty = new TrackBar();
            groupBox1 = new GroupBox();
            lblDifficultyValue = new Label();
            lblSizeValue = new Label();
            lblBombPercent = new Label();
            lblSize = new Label();
            btnPlay = new Button();
            ((System.ComponentModel.ISupportInitialize)tbSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbDifficulty).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // tbSize
            // 
            tbSize.Location = new Point(84, 92);
            tbSize.Maximum = 20;
            tbSize.Name = "tbSize";
            tbSize.Size = new Size(560, 90);
            tbSize.TabIndex = 0;
            tbSize.Value = 10;
            tbSize.Scroll += tbSize_Scroll;
            // 
            // tbDifficulty
            // 
            tbDifficulty.Location = new Point(97, 264);
            tbDifficulty.Maximum = 70;
            tbDifficulty.Name = "tbDifficulty";
            tbDifficulty.Size = new Size(511, 90);
            tbDifficulty.TabIndex = 1;
            tbDifficulty.Scroll += tbDifficulty_Scroll;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblDifficultyValue);
            groupBox1.Controls.Add(lblSizeValue);
            groupBox1.Controls.Add(lblBombPercent);
            groupBox1.Controls.Add(lblSize);
            groupBox1.Controls.Add(tbSize);
            groupBox1.Controls.Add(tbDifficulty);
            groupBox1.Location = new Point(29, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(739, 374);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Play MineSweeper";
            // 
            // lblDifficultyValue
            // 
            lblDifficultyValue.AutoSize = true;
            lblDifficultyValue.Location = new Point(333, 201);
            lblDifficultyValue.Name = "lblDifficultyValue";
            lblDifficultyValue.Size = new Size(78, 32);
            lblDifficultyValue.TabIndex = 5;
            lblDifficultyValue.Text = "label2";
            // 
            // lblSizeValue
            // 
            lblSizeValue.AutoSize = true;
            lblSizeValue.Location = new Point(318, 57);
            lblSizeValue.Name = "lblSizeValue";
            lblSizeValue.Size = new Size(78, 32);
            lblSizeValue.TabIndex = 4;
            lblSizeValue.Text = "label1";
            // 
            // lblBombPercent
            // 
            lblBombPercent.AutoSize = true;
            lblBombPercent.Location = new Point(97, 201);
            lblBombPercent.Name = "lblBombPercent";
            lblBombPercent.Size = new Size(207, 32);
            lblBombPercent.TabIndex = 3;
            lblBombPercent.Text = "Bomb Percentage:";
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Location = new Point(97, 57);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(62, 32);
            lblSize.TabIndex = 2;
            lblSize.Text = "Size:";
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(308, 392);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(150, 46);
            btnPlay.TabIndex = 3;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // StartGameForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnPlay);
            Controls.Add(groupBox1);
            Name = "StartGameForm";
            Text = "Start a new Game";
            ((System.ComponentModel.ISupportInitialize)tbSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbDifficulty).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TrackBar tbSize;
        private TrackBar tbDifficulty;
        private GroupBox groupBox1;
        private Label lblBombPercent;
        private Label lblSize;
        private Button btnPlay;
        private Label lblDifficultyValue;
        private Label lblSizeValue;
    }
}