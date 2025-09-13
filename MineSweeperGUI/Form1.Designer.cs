namespace MineSweeperGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlGameBoard = new Panel();
            lblStartTime = new Label();
            label2 = new Label();
            lblScore = new Label();
            label4 = new Label();
            btnRestart = new Button();
            SuspendLayout();
            // 
            // pnlGameBoard
            // 
            pnlGameBoard.Location = new Point(12, 40);
            pnlGameBoard.Name = "pnlGameBoard";
            pnlGameBoard.Size = new Size(1648, 993);
            pnlGameBoard.TabIndex = 0;
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Location = new Point(1683, 79);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(134, 32);
            lblStartTime.TabIndex = 1;
            lblStartTime.Text = "Start Time: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1690, 134);
            label2.Name = "label2";
            label2.Size = new Size(78, 32);
            label2.TabIndex = 2;
            label2.Text = "label2";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(1683, 246);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(85, 32);
            lblScore.TabIndex = 3;
            lblScore.Text = "Score: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1704, 303);
            label4.Name = "label4";
            label4.Size = new Size(78, 32);
            label4.TabIndex = 4;
            label4.Text = "label4";
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(1704, 477);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(150, 46);
            btnRestart.TabIndex = 5;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1915, 1045);
            Controls.Add(btnRestart);
            Controls.Add(label4);
            Controls.Add(lblScore);
            Controls.Add(label2);
            Controls.Add(lblStartTime);
            Controls.Add(pnlGameBoard);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlGameBoard;
        private Label lblStartTime;
        private Label label2;
        private Label lblScore;
        private Label label4;
        private Button btnRestart;
    }
}
