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
            SuspendLayout();
            // 
            // lblTargetAgent
            // 
            lblTargetAgent.AutoSize = true;
            lblTargetAgent.Location = new Point(99, 27);
            lblTargetAgent.Name = "lblTargetAgent";
            lblTargetAgent.Size = new Size(38, 15);
            lblTargetAgent.TabIndex = 0;
            lblTargetAgent.Text = "label1";
            // 
            // txtCommand
            // 
            txtCommand.Location = new Point(99, 72);
            txtCommand.Name = "txtCommand";
            txtCommand.Size = new Size(334, 23);
            txtCommand.TabIndex = 1;
            // 
            // btnRun
            // 
            btnRun.Location = new Point(436, 111);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(75, 23);
            btnRun.TabIndex = 2;
            btnRun.Text = "Futtatás";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(26, 406);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 3;
            btnBack.Text = "Vissza";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // rtbCommandResult
            // 
            rtbCommandResult.Location = new Point(99, 154);
            rtbCommandResult.Name = "rtbCommandResult";
            rtbCommandResult.ReadOnly = true;
            rtbCommandResult.Size = new Size(334, 232);
            rtbCommandResult.TabIndex = 4;
            rtbCommandResult.Text = "";
            // 
            // CommandForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(549, 450);
            Controls.Add(rtbCommandResult);
            Controls.Add(btnBack);
            Controls.Add(btnRun);
            Controls.Add(txtCommand);
            Controls.Add(lblTargetAgent);
            Name = "CommandForm";
            Text = "Parancs futtatás";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTargetAgent;
        private TextBox txtCommand;
        private Button btnRun;
        private Button btnBack;
        private RichTextBox rtbCommandResult;
    }
}