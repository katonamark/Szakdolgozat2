namespace ManagementClient
{
    partial class MessageForm
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
            btnSend = new Button();
            rtbChatHistory = new RichTextBox();
            lblTargetAgent = new Label();
            txtNewMessage = new TextBox();
            label1 = new Label();
            btnBack = new Button();
            panel1 = new Panel();
            label2 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Location = new Point(294, 449);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 26);
            btnSend.TabIndex = 0;
            btnSend.Text = "Küldés";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // rtbChatHistory
            // 
            rtbChatHistory.Location = new Point(44, 107);
            rtbChatHistory.Name = "rtbChatHistory";
            rtbChatHistory.ReadOnly = true;
            rtbChatHistory.Size = new Size(325, 224);
            rtbChatHistory.TabIndex = 1;
            rtbChatHistory.Text = "";
            // 
            // lblTargetAgent
            // 
            lblTargetAgent.AutoSize = true;
            lblTargetAgent.Location = new Point(12, 77);
            lblTargetAgent.Name = "lblTargetAgent";
            lblTargetAgent.Size = new Size(43, 17);
            lblTargetAgent.TabIndex = 2;
            lblTargetAgent.Text = "label1";
            // 
            // txtNewMessage
            // 
            txtNewMessage.AcceptsReturn = true;
            txtNewMessage.Location = new Point(44, 388);
            txtNewMessage.Multiline = true;
            txtNewMessage.Name = "txtNewMessage";
            txtNewMessage.ScrollBars = ScrollBars.Both;
            txtNewMessage.Size = new Size(325, 54);
            txtNewMessage.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 367);
            label1.Name = "label1";
            label1.Size = new Size(120, 17);
            label1.TabIndex = 6;
            label1.Text = "Írd ide az üzenetet:";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(13, 477);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 26);
            btnBack.TabIndex = 7;
            btnBack.Text = "Vissza";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(424, 58);
            panel1.TabIndex = 8;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            label2.Location = new Point(15, 10);
            label2.Name = "label2";
            label2.Size = new Size(178, 31);
            label2.TabIndex = 1;
            label2.Text = "Üzenetküldés";
            // 
            // MessageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            CancelButton = btnBack;
            ClientSize = new Size(424, 510);
            Controls.Add(panel1);
            Controls.Add(btnBack);
            Controls.Add(label1);
            Controls.Add(txtNewMessage);
            Controls.Add(lblTargetAgent);
            Controls.Add(rtbChatHistory);
            Controls.Add(btnSend);
            Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MessageForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Üzenetküldés";
            Load += MessageForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSend;
        private Label lblTargetAgent;
        private RichTextBox rtbChatHistory;
        private TextBox txtNewMessage;
        private Label label1;
        private Button btnBack;
        private Panel panel1;
        private Label label2;
    }
}