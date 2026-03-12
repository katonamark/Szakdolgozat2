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
            SuspendLayout();
            // 
            // btnSend
            // 
            btnSend.Location = new Point(557, 308);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(75, 23);
            btnSend.TabIndex = 0;
            btnSend.Text = "Küldés";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // rtbChatHistory
            // 
            rtbChatHistory.Location = new Point(197, 84);
            rtbChatHistory.Name = "rtbChatHistory";
            rtbChatHistory.Size = new Size(382, 165);
            rtbChatHistory.TabIndex = 1;
            rtbChatHistory.Text = "";
            // 
            // lblTargetAgent
            // 
            lblTargetAgent.AutoSize = true;
            lblTargetAgent.Location = new Point(197, 46);
            lblTargetAgent.Name = "lblTargetAgent";
            lblTargetAgent.Size = new Size(38, 15);
            lblTargetAgent.TabIndex = 2;
            lblTargetAgent.Text = "label1";
            // 
            // txtNewMessage
            // 
            txtNewMessage.Location = new Point(92, 325);
            txtNewMessage.Multiline = true;
            txtNewMessage.Name = "txtNewMessage";
            txtNewMessage.Size = new Size(298, 23);
            txtNewMessage.TabIndex = 5;
            // 
            // MessageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtNewMessage);
            Controls.Add(lblTargetAgent);
            Controls.Add(rtbChatHistory);
            Controls.Add(btnSend);
            Name = "MessageForm";
            Text = "MessageForm";
            Load += MessageForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSend;
        private Label lblTargetAgent;
        private RichTextBox rtbChatHistory;
        private TextBox txtNewMessage;
    }
}