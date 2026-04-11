namespace AgentUI
{
    partial class ClientUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientUI));
            lblStatus = new Label();
            rtbChatHistory = new RichTextBox();
            txtNewMessage = new TextBox();
            btnSend = new Button();
            label1 = new Label();
            notifyIcon1 = new NotifyIcon(components);
            lblMachineName = new Label();
            lblUserName = new Label();
            lblOsVersion = new Label();
            rtbLog = new RichTextBox();
            panel1 = new Panel();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            pictureBox2 = new PictureBox();
            label4 = new Label();
            panel2 = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 102);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(43, 17);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "label1";
            // 
            // rtbChatHistory
            // 
            rtbChatHistory.Location = new Point(30, 33);
            rtbChatHistory.Name = "rtbChatHistory";
            rtbChatHistory.ReadOnly = true;
            rtbChatHistory.Size = new Size(325, 213);
            rtbChatHistory.TabIndex = 1;
            rtbChatHistory.Text = "";
            // 
            // txtNewMessage
            // 
            txtNewMessage.AcceptsReturn = true;
            txtNewMessage.Location = new Point(30, 301);
            txtNewMessage.Multiline = true;
            txtNewMessage.Name = "txtNewMessage";
            txtNewMessage.ScrollBars = ScrollBars.Both;
            txtNewMessage.Size = new Size(325, 54);
            txtNewMessage.TabIndex = 2;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(299, 380);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 26);
            btnSend.TabIndex = 3;
            btnSend.Text = "Küldés";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(30, 269);
            label1.Name = "label1";
            label1.Size = new Size(120, 17);
            label1.TabIndex = 4;
            label1.Text = "Írd ide az üzenetet:";
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Remotee Agent";
            notifyIcon1.Visible = true;
            notifyIcon1.DoubleClick += notifyIcon1_DoubleClick;
            // 
            // lblMachineName
            // 
            lblMachineName.AutoSize = true;
            lblMachineName.Location = new Point(12, 128);
            lblMachineName.Name = "lblMachineName";
            lblMachineName.Size = new Size(43, 17);
            lblMachineName.TabIndex = 9;
            lblMachineName.Text = "label2";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(12, 156);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(43, 17);
            lblUserName.TabIndex = 10;
            lblUserName.Text = "label3";
            // 
            // lblOsVersion
            // 
            lblOsVersion.AutoSize = true;
            lblOsVersion.Location = new Point(12, 183);
            lblOsVersion.Name = "lblOsVersion";
            lblOsVersion.Size = new Size(43, 17);
            lblOsVersion.TabIndex = 11;
            lblOsVersion.Text = "label4";
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(37, 245);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(444, 253);
            rtbLog.TabIndex = 12;
            rtbLog.Text = "";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(942, 96);
            panel1.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label2.Location = new Point(295, 30);
            label2.Name = "label2";
            label2.Size = new Size(616, 31);
            label2.TabIndex = 1;
            label2.Text = "Üdvözlünk a Remotee Management rendszerben!";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(12, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(262, 81);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 9.75F, FontStyle.Underline, GraphicsUnit.Point, 238);
            label3.Location = new Point(37, 223);
            label3.Name = "label3";
            label3.Size = new Size(77, 19);
            label3.TabIndex = 14;
            label3.Text = "Értesítések:";
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(15, 216);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(26, 26);
            pictureBox2.TabIndex = 15;
            pictureBox2.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label4.Location = new Point(29, 8);
            label4.Name = "label4";
            label4.Size = new Size(203, 19);
            label4.TabIndex = 16;
            label4.Text = "Üzenet küldése a szervernek:";
            // 
            // panel2
            // 
            panel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel2.BackColor = SystemColors.GradientInactiveCaption;
            panel2.Controls.Add(rtbChatHistory);
            panel2.Controls.Add(txtNewMessage);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(btnSend);
            panel2.Controls.Add(label1);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(565, 96);
            panel2.Name = "panel2";
            panel2.Size = new Size(377, 414);
            panel2.TabIndex = 17;
            // 
            // ClientUI
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(942, 510);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(label3);
            Controls.Add(rtbLog);
            Controls.Add(lblOsVersion);
            Controls.Add(lblUserName);
            Controls.Add(lblMachineName);
            Controls.Add(lblStatus);
            Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ClientUI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Remotee";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private RichTextBox rtbChatHistory;
        private TextBox txtNewMessage;
        private Button btnSend;
        private Label label1;
        private NotifyIcon notifyIcon1;
        private Label lblMachineName;
        private Label lblUserName;
        private Label lblOsVersion;
        private RichTextBox rtbLog;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label3;
        private PictureBox pictureBox2;
        private Label label4;
        private Panel panel2;
    }
}
