namespace AgentUI
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
            lblStatus = new Label();
            rtbChatHistory = new RichTextBox();
            txtNewMessage = new TextBox();
            btnSend = new Button();
            label1 = new Label();
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
            rtbChatHistory.Location = new Point(43, 38);
            rtbChatHistory.Name = "rtbChatHistory";
            rtbChatHistory.ReadOnly = true;
            rtbChatHistory.Size = new Size(325, 248);
            rtbChatHistory.TabIndex = 1;
            rtbChatHistory.Text = "";
            // 
            // txtNewMessage
            // 
            txtNewMessage.Location = new Point(43, 350);
            txtNewMessage.Multiline = true;
            txtNewMessage.Name = "txtNewMessage";
            txtNewMessage.Size = new Size(325, 23);
            txtNewMessage.TabIndex = 2;
            // 
            // btnSend
            // 
            btnSend.Location = new Point(293, 390);
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
            label1.Location = new Point(43, 318);
            label1.Name = "label1";
            label1.Size = new Size(105, 15);
            label1.TabIndex = 4;
            label1.Text = "Írd ide az üzenetet:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(424, 450);
            Controls.Add(label1);
            Controls.Add(btnSend);
            Controls.Add(txtNewMessage);
            Controls.Add(rtbChatHistory);
            Controls.Add(lblStatus);
            Name = "Form1";
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
    }
}
