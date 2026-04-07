namespace ManagementClient
{
    partial class RegisterCodeForm
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
            lblInfo = new Label();
            btnNext = new Button();
            btnCancel = new Button();
            txtAdminCode = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(65, 52);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(43, 17);
            lblInfo.TabIndex = 0;
            lblInfo.Text = "label1";
            // 
            // btnNext
            // 
            btnNext.Location = new Point(188, 203);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(75, 26);
            btnNext.TabIndex = 1;
            btnNext.Text = "Tovább";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(65, 203);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 26);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Mégsem";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // txtAdminCode
            // 
            txtAdminCode.AcceptsReturn = true;
            txtAdminCode.Location = new Point(65, 125);
            txtAdminCode.Name = "txtAdminCode";
            txtAdminCode.Size = new Size(198, 23);
            txtAdminCode.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(65, 104);
            label1.Name = "label1";
            label1.Size = new Size(100, 17);
            label1.TabIndex = 4;
            label1.Text = "Írd ide a kódot:";
            // 
            // RegisterCodeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(331, 273);
            Controls.Add(label1);
            Controls.Add(txtAdminCode);
            Controls.Add(btnCancel);
            Controls.Add(btnNext);
            Controls.Add(lblInfo);
            Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Name = "RegisterCodeForm";
            Text = "Remotee-Autentikáció";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblInfo;
        private Button btnNext;
        private Button btnCancel;
        private TextBox txtAdminCode;
        private Label label1;
    }
}