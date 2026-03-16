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
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Location = new Point(293, 390);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 0;
            btnSend.Text = "Küldés";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // rtbChatHistory
            // 
            rtbChatHistory.Location = new Point(43, 38);
            rtbChatHistory.Name = "rtbChatHistory";
            rtbChatHistory.ReadOnly = true;
            rtbChatHistory.Size = new Size(325, 248);
            rtbChatHistory.TabIndex = 1;
            rtbChatHistory.Text = "";
            // 
            // lblTargetAgent
            // 
            lblTargetAgent.AutoSize = true;
            lblTargetAgent.Location = new Point(12, 9);
            lblTargetAgent.Name = "lblTargetAgent";
            lblTargetAgent.Size = new Size(38, 15);
            lblTargetAgent.TabIndex = 2;
            lblTargetAgent.Text = "label1";
            // 
            // txtNewMessage
            // 
            txtNewMessage.AcceptsReturn = true;
            txtNewMessage.Location = new Point(43, 336);
            txtNewMessage.Multiline = true;
            txtNewMessage.Name = "txtNewMessage";
            txtNewMessage.ScrollBars = ScrollBars.Both;
            txtNewMessage.Size = new Size(325, 48);
            txtNewMessage.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(43, 318);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 6;
            label1.Text = "Írd ide az üzenetet:";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(12, 415);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 7;
            btnBack.Text = "Vissza";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // MessageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(424, 450);
            Controls.Add(btnBack);
            Controls.Add(label1);
            Controls.Add(txtNewMessage);
            Controls.Add(lblTargetAgent);
            Controls.Add(rtbChatHistory);
            Controls.Add(btnSend);
            Name = "MessageForm";
            Text = "Üzenet";
            Load += MessageForm_Load;
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
    }
}