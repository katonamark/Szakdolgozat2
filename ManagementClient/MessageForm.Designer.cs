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
            rtbMessage = new RichTextBox();
            lblTargetAgent = new Label();
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
            // rtbMessage
            // 
            rtbMessage.Location = new Point(197, 84);
            rtbMessage.Name = "rtbMessage";
            rtbMessage.Size = new Size(382, 165);
            rtbMessage.TabIndex = 1;
            rtbMessage.Text = "";
            // 
            // lblTargetAgent
            // 
            lblTargetAgent.AutoSize = true;
            lblTargetAgent.Location = new Point(218, 51);
            lblTargetAgent.Name = "lblTargetAgent";
            lblTargetAgent.Size = new Size(38, 15);
            lblTargetAgent.TabIndex = 2;
            lblTargetAgent.Text = "label1";
            // 
            // MessageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblTargetAgent);
            Controls.Add(rtbMessage);
            Controls.Add(btnSend);
            Name = "MessageForm";
            Text = "MessageForm";
            Load += MessageForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSend;
        private RichTextBox rtbMessage;
        private Label lblTargetAgent;
    }
}