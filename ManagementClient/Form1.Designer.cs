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
            lstAgents = new ListBox();
            btnRefresh = new Button();
            SuspendLayout();
            // 
            // lstAgents
            // 
            lstAgents.FormattingEnabled = true;
            lstAgents.ItemHeight = 15;
            lstAgents.Location = new Point(204, 165);
            lstAgents.Name = "lstAgents";
            lstAgents.Size = new Size(120, 94);
            lstAgents.TabIndex = 0;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(570, 337);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 23);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Frissítés";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRefresh);
            Controls.Add(lstAgents);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private ListBox lstAgents;
        private Button btnRefresh;
    }
}
