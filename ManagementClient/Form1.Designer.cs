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
            lblStatus = new Label();
            label2 = new Label();
            btnFile = new Button();
            btnCommand = new Button();
            btnScreenshot = new Button();
            notifyIcon1 = new NotifyIcon(components);
            SuspendLayout();
            // 
            // lstAgents
            // 
            lstAgents.FormattingEnabled = true;
            lstAgents.ItemHeight = 15;
            lstAgents.Location = new Point(58, 39);
            lstAgents.Name = "lstAgents";
            lstAgents.Size = new Size(280, 289);
            lstAgents.TabIndex = 0;
            lstAgents.Click += lstAgents_SelectedIndexChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(303, 344);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 23);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Frissítés";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 16);
            label1.Name = "label1";
            label1.Size = new Size(136, 15);
            label1.TabIndex = 2;
            label1.Text = "Aktív gépek a hálózaton:";
            // 
            // btnMessage
            // 
            btnMessage.Location = new Point(368, 12);
            btnMessage.Name = "btnMessage";
            btnMessage.Size = new Size(95, 23);
            btnMessage.TabIndex = 3;
            btnMessage.Text = "Üzenetküldés";
            btnMessage.UseVisualStyleBackColor = true;
            btnMessage.Click += btnMessage_Click;
            // 
            // lblMachineName
            // 
            lblMachineName.AutoSize = true;
            lblMachineName.Location = new Point(438, 117);
            lblMachineName.Name = "lblMachineName";
            lblMachineName.Size = new Size(50, 15);
            lblMachineName.TabIndex = 4;
            lblMachineName.Text = "Gépnév:";
            // 
            // lblOsVersion
            // 
            lblOsVersion.AutoSize = true;
            lblOsVersion.Location = new Point(438, 158);
            lblOsVersion.Name = "lblOsVersion";
            lblOsVersion.Size = new Size(25, 15);
            lblOsVersion.TabIndex = 5;
            lblOsVersion.Text = "OS:";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Location = new Point(438, 194);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(71, 15);
            lblUserName.TabIndex = 6;
            lblUserName.Text = "Felhasználó:";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(438, 230);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(48, 15);
            lblStatus.TabIndex = 7;
            lblStatus.Text = "Állapot:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(151, 348);
            label2.Name = "label2";
            label2.Size = new Size(146, 15);
            label2.TabIndex = 8;
            label2.Text = "Kattints a frissítés gombra!";
            // 
            // btnFile
            // 
            btnFile.Location = new Point(491, 12);
            btnFile.Name = "btnFile";
            btnFile.Size = new Size(75, 23);
            btnFile.TabIndex = 9;
            btnFile.Text = "Fájlküldés";
            btnFile.UseVisualStyleBackColor = true;
            btnFile.Click += btnFile_Click;
            // 
            // btnCommand
            // 
            btnCommand.Location = new Point(595, 12);
            btnCommand.Name = "btnCommand";
            btnCommand.Size = new Size(117, 23);
            btnCommand.TabIndex = 10;
            btnCommand.Text = "Parancs futtatás";
            btnCommand.UseVisualStyleBackColor = true;
            btnCommand.Click += btnCommand_Click;
            // 
            // btnScreenshot
            // 
            btnScreenshot.Location = new Point(369, 52);
            btnScreenshot.Name = "btnScreenshot";
            btnScreenshot.Size = new Size(94, 23);
            btnScreenshot.TabIndex = 11;
            btnScreenshot.Text = "Képernyőkép";
            btnScreenshot.UseVisualStyleBackColor = true;
            btnScreenshot.Click += btnScreenshot_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "notifyIcon1";
            notifyIcon1.Visible = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(785, 379);
            Controls.Add(btnScreenshot);
            Controls.Add(btnCommand);
            Controls.Add(btnFile);
            Controls.Add(label2);
            Controls.Add(lblStatus);
            Controls.Add(lblUserName);
            Controls.Add(lblOsVersion);
            Controls.Add(lblMachineName);
            Controls.Add(btnMessage);
            Controls.Add(label1);
            Controls.Add(btnRefresh);
            Controls.Add(lstAgents);
            Name = "Form1";
            Text = "Remotee";
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
        private Label lblStatus;
        private Label label2;
        private Button btnFile;
        private Button btnCommand;
        private Button btnScreenshot;
        private NotifyIcon notifyIcon1;
    }
}
