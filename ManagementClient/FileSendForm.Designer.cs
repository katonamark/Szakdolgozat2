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
            btnBack = new Button();
            lblTargetPath = new Label();
            txtTargetPath = new TextBox();
            SuspendLayout();
            // 
            // lblTargetAgent
            // 
            lblTargetAgent.AutoSize = true;
            lblTargetAgent.Location = new Point(44, 26);
            lblTargetAgent.Name = "lblTargetAgent";
            lblTargetAgent.Size = new Size(38, 15);
            lblTargetAgent.TabIndex = 0;
            lblTargetAgent.Text = "label1";
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(44, 105);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(259, 23);
            txtFilePath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(140, 168);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Tallózás";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(228, 168);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 3;
            btnSend.Text = "Küldés";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(7, 415);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 4;
            btnBack.Text = "Vissza";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // lblTargetPath
            // 
            lblTargetPath.AutoSize = true;
            lblTargetPath.Location = new Point(12, 236);
            lblTargetPath.Name = "lblTargetPath";
            lblTargetPath.Size = new Size(142, 15);
            lblTargetPath.TabIndex = 5;
            lblTargetPath.Text = "Célmappa a kliens gépen:";
            // 
            // txtTargetPath
            // 
            txtTargetPath.Location = new Point(171, 236);
            txtTargetPath.Name = "txtTargetPath";
            txtTargetPath.Size = new Size(100, 23);
            txtTargetPath.TabIndex = 6;
            // 
            // FileSendForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 450);
            Controls.Add(txtTargetPath);
            Controls.Add(lblTargetPath);
            Controls.Add(btnBack);
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
        private Button btnBack;
        private Label lblTargetPath;
        private TextBox txtTargetPath;
    }
}