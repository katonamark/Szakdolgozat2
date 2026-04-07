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
            cmbTargetPath = new ComboBox();
            lblStatus = new Label();
            label1 = new Label();
            panel1 = new Panel();
            label2 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblTargetAgent
            // 
            lblTargetAgent.AutoSize = true;
            lblTargetAgent.Location = new Point(57, 95);
            lblTargetAgent.Name = "lblTargetAgent";
            lblTargetAgent.Size = new Size(45, 19);
            lblTargetAgent.TabIndex = 0;
            lblTargetAgent.Text = "label1";
            // 
            // txtFilePath
            // 
            txtFilePath.Location = new Point(57, 196);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(295, 24);
            txtFilePath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(268, 245);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(86, 29);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Tallózás";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(304, 525);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(86, 29);
            btnSend.TabIndex = 3;
            btnSend.Text = "Küldés";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(8, 525);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(86, 29);
            btnBack.TabIndex = 4;
            btnBack.Text = "Vissza";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // lblTargetPath
            // 
            lblTargetPath.AutoSize = true;
            lblTargetPath.Location = new Point(21, 361);
            lblTargetPath.Name = "lblTargetPath";
            lblTargetPath.Size = new Size(164, 19);
            lblTargetPath.TabIndex = 5;
            lblTargetPath.Text = "Célmappa a kliens gépen:";
            // 
            // txtTargetPath
            // 
            txtTargetPath.Location = new Point(202, 417);
            txtTargetPath.Name = "txtTargetPath";
            txtTargetPath.Size = new Size(170, 24);
            txtTargetPath.TabIndex = 6;
            // 
            // cmbTargetPath
            // 
            cmbTargetPath.FormattingEnabled = true;
            cmbTargetPath.Location = new Point(202, 361);
            cmbTargetPath.Name = "cmbTargetPath";
            cmbTargetPath.Size = new Size(138, 27);
            cmbTargetPath.TabIndex = 7;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(8, 458);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(45, 19);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(19, 153);
            label1.Name = "label1";
            label1.Size = new Size(187, 19);
            label1.TabIndex = 9;
            label1.Text = "Válaszd ki az elküldendő fájlt:";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(403, 60);
            panel1.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            label2.Location = new Point(19, 18);
            label2.Name = "label2";
            label2.Size = new Size(134, 31);
            label2.TabIndex = 0;
            label2.Text = "Fájlküldés";
            // 
            // FileSendForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            CancelButton = btnBack;
            ClientSize = new Size(403, 570);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(lblStatus);
            Controls.Add(cmbTargetPath);
            Controls.Add(txtTargetPath);
            Controls.Add(lblTargetPath);
            Controls.Add(btnBack);
            Controls.Add(btnSend);
            Controls.Add(btnBrowse);
            Controls.Add(txtFilePath);
            Controls.Add(lblTargetAgent);
            Font = new Font("Microsoft YaHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FileSendForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Fájlküldés";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
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
        private ComboBox cmbTargetPath;
        private Label lblStatus;
        private Label label1;
        private Panel panel1;
        private Label label2;
    }
}