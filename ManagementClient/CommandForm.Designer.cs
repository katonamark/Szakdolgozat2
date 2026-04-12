namespace ManagementClient
{
    partial class CommandForm
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
            lblTargetAgent = new Label();
            txtCommand = new TextBox();
            btnRun = new Button();
            btnBack = new Button();
            rtbCommandResult = new RichTextBox();
            label1 = new Label();
            panel1 = new Panel();
            label2 = new Label();
            btnClear = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTargetAgent
            // 
            lblTargetAgent.AutoSize = true;
            lblTargetAgent.Location = new Point(68, 67);
            lblTargetAgent.Name = "lblTargetAgent";
            lblTargetAgent.Size = new Size(43, 17);
            lblTargetAgent.TabIndex = 0;
            lblTargetAgent.Text = "label1";
            // 
            // txtCommand
            // 
            txtCommand.Location = new Point(68, 103);
            txtCommand.Name = "txtCommand";
            txtCommand.PlaceholderText = "Írd ide a futtatni kívánt parancsot";
            txtCommand.Size = new Size(334, 23);
            txtCommand.TabIndex = 1;
            txtCommand.Tag = "";
            // 
            // btnRun
            // 
            btnRun.Location = new Point(327, 151);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 26);
            btnRun.TabIndex = 2;
            btnRun.Text = "Futtatás";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(21, 473);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 26);
            btnBack.TabIndex = 3;
            btnBack.Text = "Vissza";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // rtbCommandResult
            // 
            rtbCommandResult.Location = new Point(68, 196);
            rtbCommandResult.Name = "rtbCommandResult";
            rtbCommandResult.ReadOnly = true;
            rtbCommandResult.Size = new Size(334, 262);
            rtbCommandResult.TabIndex = 4;
            rtbCommandResult.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(68, 156);
            label1.Name = "label1";
            label1.Size = new Size(120, 17);
            label1.TabIndex = 5;
            label1.Text = "A futás eredménye:";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(473, 52);
            panel1.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(204, 31);
            label2.TabIndex = 1;
            label2.Text = "Parancsfuttatás";
            // 
            // btnClear
            // 
            btnClear.Location = new Point(353, 475);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 7;
            btnClear.Text = "Törlés";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // CommandForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            CancelButton = btnBack;
            ClientSize = new Size(473, 510);
            Controls.Add(btnClear);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(rtbCommandResult);
            Controls.Add(btnBack);
            Controls.Add(btnRun);
            Controls.Add(txtCommand);
            Controls.Add(lblTargetAgent);
            Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "CommandForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Parancsfuttatás";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTargetAgent;
        private TextBox txtCommand;
        private Button btnRun;
        private Button btnBack;
        private RichTextBox rtbCommandResult;
        private Label label1;
        private Panel panel1;
        private Label label2;
        private Button btnClear;
    }
}