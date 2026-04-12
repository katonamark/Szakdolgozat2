namespace ManagementClient
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lstAgents = new ListBox();
            btnRefresh = new Button();
            label1 = new Label();
            btnMessage = new Button();
            lblMachineName = new Label();
            lblOsVersion = new Label();
            lblUserName = new Label();
            lblAgentStatus = new Label();
            label2 = new Label();
            btnFile = new Button();
            btnCommand = new Button();
            btnScreenshot = new Button();
            notifyIcon1 = new NotifyIcon(components);
            lblWelcome = new Label();
            btnLogout = new Button();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            lblConnectionStatus = new Label();
            panel3 = new Panel();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // lstAgents
            // 
            lstAgents.FormattingEnabled = true;
            lstAgents.HorizontalScrollbar = true;
            lstAgents.ItemHeight = 19;
            lstAgents.Location = new Point(14, 42);
            lstAgents.Margin = new Padding(3, 4, 3, 4);
            lstAgents.Name = "lstAgents";
            lstAgents.Size = new Size(278, 403);
            lstAgents.TabIndex = 0;
            lstAgents.Click += lstAgents_SelectedIndexChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(207, 461);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(86, 29);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Frissítés";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 8);
            label1.Name = "label1";
            label1.Size = new Size(159, 19);
            label1.TabIndex = 2;
            label1.Text = "Aktív gépek a hálózaton:";
            // 
            // btnMessage
            // 
            btnMessage.Font = new Font("Segoe UI", 9.75F);
            btnMessage.Location = new Point(340, 148);
            btnMessage.Margin = new Padding(3, 4, 3, 4);
            btnMessage.Name = "btnMessage";
            btnMessage.RightToLeft = RightToLeft.No;
            btnMessage.Size = new Size(135, 29);
            btnMessage.TabIndex = 3;
            btnMessage.Text = "Üzenetküldés";
            btnMessage.UseVisualStyleBackColor = true;
            btnMessage.Click += btnMessage_Click;
            // 
            // lblMachineName
            // 
            lblMachineName.AutoSize = true;
            lblMachineName.Location = new Point(632, 251);
            lblMachineName.Name = "lblMachineName";
            lblMachineName.Size = new Size(59, 19);
            lblMachineName.TabIndex = 4;
            lblMachineName.Text = "Gépnév:";
            // 
            // lblOsVersion
            // 
            lblOsVersion.AutoSize = true;
            lblOsVersion.Location = new Point(632, 303);
            lblOsVersion.Name = "lblOsVersion";
            lblOsVersion.Size = new Size(31, 19);
            lblOsVersion.TabIndex = 5;
            lblOsVersion.Text = "OS:";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(632, 348);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(82, 19);
            lblUserName.TabIndex = 6;
            lblUserName.Text = "Felhasználó:";
            // 
            // lblAgentStatus
            // 
            lblAgentStatus.AutoSize = true;
            lblAgentStatus.Location = new Point(632, 394);
            lblAgentStatus.Name = "lblAgentStatus";
            lblAgentStatus.Size = new Size(55, 19);
            lblAgentStatus.TabIndex = 7;
            lblAgentStatus.Text = "Állapot:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 466);
            label2.Name = "label2";
            label2.Size = new Size(173, 19);
            label2.TabIndex = 8;
            label2.Text = "Kattints a frissítés gombra!";
            // 
            // btnFile
            // 
            btnFile.Font = new Font("Segoe UI", 9.75F);
            btnFile.Location = new Point(341, 407);
            btnFile.Margin = new Padding(3, 4, 3, 4);
            btnFile.Name = "btnFile";
            btnFile.Size = new Size(134, 29);
            btnFile.TabIndex = 9;
            btnFile.Text = "Fájlküldés";
            btnFile.UseVisualStyleBackColor = true;
            btnFile.Click += btnFile_Click;
            // 
            // btnCommand
            // 
            btnCommand.Font = new Font("Segoe UI", 9.75F);
            btnCommand.Location = new Point(341, 273);
            btnCommand.Margin = new Padding(3, 4, 3, 4);
            btnCommand.Name = "btnCommand";
            btnCommand.Size = new Size(134, 29);
            btnCommand.TabIndex = 10;
            btnCommand.Text = "Parancsfuttatás";
            btnCommand.UseVisualStyleBackColor = true;
            btnCommand.Click += btnCommand_Click;
            // 
            // btnScreenshot
            // 
            btnScreenshot.Font = new Font("Segoe UI", 9.75F);
            btnScreenshot.Location = new Point(341, 522);
            btnScreenshot.Margin = new Padding(3, 4, 3, 4);
            btnScreenshot.Name = "btnScreenshot";
            btnScreenshot.Size = new Size(134, 29);
            btnScreenshot.TabIndex = 11;
            btnScreenshot.Text = "Távoli vezérlés";
            btnScreenshot.UseVisualStyleBackColor = true;
            btnScreenshot.Click += btnScreenshot_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Remotee";
            notifyIcon1.Visible = true;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
            lblWelcome.Location = new Point(369, 41);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(45, 17);
            lblWelcome.TabIndex = 12;
            lblWelcome.Text = "label3";
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(930, 35);
            btnLogout.Margin = new Padding(3, 4, 3, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(119, 29);
            btnLogout.TabIndex = 13;
            btnLogout.Text = "Kijelentkezés";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.GradientActiveCaption;
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(btnLogout);
            panel2.Controls.Add(lblWelcome);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1063, 106);
            panel2.TabIndex = 15;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(3, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(309, 87);
            pictureBox1.TabIndex = 14;
            pictureBox1.TabStop = false;
            // 
            // lblConnectionStatus
            // 
            lblConnectionStatus.AutoSize = true;
            lblConnectionStatus.Font = new Font("Microsoft YaHei UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblConnectionStatus.Location = new Point(852, 592);
            lblConnectionStatus.Name = "lblConnectionStatus";
            lblConnectionStatus.Size = new Size(38, 16);
            lblConnectionStatus.TabIndex = 17;
            lblConnectionStatus.Text = "label3";
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.GradientInactiveCaption;
            panel3.Controls.Add(label1);
            panel3.Controls.Add(lstAgents);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(btnRefresh);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 106);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(312, 511);
            panel3.TabIndex = 16;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(1063, 617);
            Controls.Add(lblConnectionStatus);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(btnScreenshot);
            Controls.Add(btnCommand);
            Controls.Add(btnFile);
            Controls.Add(lblAgentStatus);
            Controls.Add(lblUserName);
            Controls.Add(lblOsVersion);
            Controls.Add(lblMachineName);
            Controls.Add(btnMessage);
            Font = new Font("Microsoft YaHei UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Remotee";
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstAgents;
        private Button btnRefresh;
        private Label label1;
        private Button btnMessage;
        private Label lblMachineName;
        private Label lblOsVersion;
        private Label lblUserName;
        private Label lblAgentStatus;
        private Label label2;
        private Button btnFile;
        private Button btnCommand;
        private Button btnScreenshot;
        private NotifyIcon notifyIcon1;
        private Label lblWelcome;
        private Button btnLogout;
        private Panel panel2;
        private Panel panel3;
        private PictureBox pictureBox1;
        private Label lblConnectionStatus;
    }
}
