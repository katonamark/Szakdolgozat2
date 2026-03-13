namespace ManagementClient
{
    partial class FileSendForm
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
            txtFilePath = new TextBox();
            btnBrowse = new Button();
            btnSend = new Button();
            SuspendLayout();
            // 
            // lblTargetAgent
            // 
            lblTargetAgent.AutoSize = true;
            lblTargetAgent.Location = new Point(211, 93);
            lblTargetAgent.Name = "lblTargetAgent";
            lblTargetAgent.Size = new Size(38, 15);
            lblTargetAgent.TabIndex = 0;
            lblTargetAgent.Text = "label1";
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(230, 195);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(259, 23);
            txtFilePath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(517, 257);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "button1";
            btnBrowse.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(535, 327);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 3;
            btnSend.Text = "button2";
            btnSend.UseVisualStyleBackColor = true;
            // 
            // FileSendForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSend);
            Controls.Add(btnBrowse);
            Controls.Add(txtFilePath);
            Controls.Add(lblTargetAgent);
            Name = "FileSendForm";
            Text = "FileSendForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTargetAgent;
        private TextBox txtFilePath;
        private Button btnBrowse;
        private Button btnSend;
    }
}