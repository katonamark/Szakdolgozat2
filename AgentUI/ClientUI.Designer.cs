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
            btnBack = new Button();
            notifyIcon1 = new NotifyIcon(components);
            lblMachineName = new Label();
            lblUserName = new Label();
            lblOsVersion = new Label();
            rtbLog = new RichTextBox();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 9);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(38, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "label1";
            // 
            // rtbChatHistory
            // 
            rtbChatHistory.Location = new Point(605, 29);
            rtbChatHistory.Name = "rtbChatHistory";
            rtbChatHistory.ReadOnly = true;
            rtbChatHistory.Size = new Size(325, 248);
            rtbChatHistory.TabIndex = 1;
            rtbChatHistory.Text = "";
            // 
            // txtNewMessage
            // 
            txtNewMessage.AcceptsReturn = true;
            txtNewMessage.Location = new Point(605, 326);
            txtNewMessage.Multiline = true;
            txtNewMessage.Name = "txtNewMessage";
            txtNewMessage.ScrollBars = ScrollBars.Both;
            txtNewMessage.Size = new Size(325, 48);
            txtNewMessage.TabIndex = 2;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(855, 396);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 3;
            btnSend.Text = "Küldés";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(605, 297);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 4;
            label1.Text = "Írd ide az üzenetet:";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(12, 415);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 8;
            btnBack.Text = "Vissza";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // lblMachineName
            // 
            lblMachineName.AutoSize = true;
            lblMachineName.Location = new Point(12, 32);
            lblMachineName.Name = "lblMachineName";
            lblMachineName.Size = new Size(38, 15);
            lblMachineName.TabIndex = 9;
            lblMachineName.Text = "label2";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(12, 56);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(38, 15);
            lblUserName.TabIndex = 10;
            lblUserName.Text = "label3";
            // 
            // lblOsVersion
            // 
            lblOsVersion.AutoSize = true;
            lblOsVersion.Location = new Point(12, 80);
            lblOsVersion.Name = "lblOsVersion";
            lblOsVersion.Size = new Size(38, 15);
            lblOsVersion.TabIndex = 11;
            lblOsVersion.Text = "label4";
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(12, 128);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(482, 246);
            rtbLog.TabIndex = 12;
            rtbLog.Text = "";
            // 
            // ClientUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 450);
            Controls.Add(rtbLog);
            Controls.Add(lblOsVersion);
            Controls.Add(lblUserName);
            Controls.Add(lblMachineName);
            Controls.Add(btnBack);
            Controls.Add(label1);
            Controls.Add(btnSend);
            Controls.Add(txtNewMessage);
            Controls.Add(rtbChatHistory);
            Controls.Add(lblStatus);
            Name = "ClientUI";
            Text = "Üzenetek";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private RichTextBox rtbChatHistory;
        private TextBox txtNewMessage;
        private Button btnSend;
        private Label label1;
        private Button btnBack;
        private NotifyIcon notifyIcon1;
        private Label lblMachineName;
        private Label lblUserName;
        private Label lblOsVersion;
        private RichTextBox rtbLog;
    }
}
