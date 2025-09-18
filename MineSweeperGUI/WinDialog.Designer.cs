namespace MineSweeperGUI
{
    partial class WinDialog
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
            lblPrompt = new Label();
            txtName = new TextBox();
            lblScoreCaption = new Label();
            lblScoreValue = new Label();
            btnOK = new Button();
            SuspendLayout();
            // 
            // lblPrompt
            // 
            lblPrompt.AutoSize = true;
            lblPrompt.Location = new Point(50, 29);
            lblPrompt.Name = "lblPrompt";
            lblPrompt.Size = new Size(468, 32);
            lblPrompt.TabIndex = 0;
            lblPrompt.Text = "Congratulations you win. Enter your name.";
            // 
            // txtName
            // 
            txtName.Location = new Point(50, 104);
            txtName.Name = "txtName";
            txtName.Size = new Size(594, 39);
            txtName.TabIndex = 1;
            // 
            // lblScoreCaption
            // 
            lblScoreCaption.AutoSize = true;
            lblScoreCaption.Location = new Point(59, 158);
            lblScoreCaption.Name = "lblScoreCaption";
            lblScoreCaption.Size = new Size(78, 32);
            lblScoreCaption.TabIndex = 2;
            lblScoreCaption.Text = "Score:";
            // 
            // lblScoreValue
            // 
            lblScoreValue.AutoSize = true;
            lblScoreValue.Location = new Point(216, 158);
            lblScoreValue.Name = "lblScoreValue";
            lblScoreValue.Size = new Size(78, 32);
            lblScoreValue.TabIndex = 3;
            lblScoreValue.Text = "label3";
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(267, 217);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(150, 46);
            btnOK.TabIndex = 4;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // WinDialog
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(672, 307);
            Controls.Add(btnOK);
            Controls.Add(lblScoreValue);
            Controls.Add(lblScoreCaption);
            Controls.Add(txtName);
            Controls.Add(lblPrompt);
            Name = "WinDialog";
            Text = "WinDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPrompt;
        private TextBox txtName;
        private Label lblScoreCaption;
        private Label lblScoreValue;
        private Button btnOK;
    }
}