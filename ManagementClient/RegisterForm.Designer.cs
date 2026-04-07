namespace ManagementClient
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            txtFullName = new TextBox();
            txtPasswordAgain = new TextBox();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            btnRegister = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            panel1 = new Panel();
            btnBack = new Button();
            SuspendLayout();
            // 
            // txtFullName
            // 
            txtFullName.BackColor = SystemColors.ControlLight;
            txtFullName.Location = new Point(210, 125);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(216, 23);
            txtFullName.TabIndex = 0;
            // 
            // txtPasswordAgain
            // 
            txtPasswordAgain.BackColor = SystemColors.ControlLight;
            txtPasswordAgain.Location = new Point(210, 328);
            txtPasswordAgain.Name = "txtPasswordAgain";
            txtPasswordAgain.Size = new Size(216, 23);
            txtPasswordAgain.TabIndex = 1;
            txtPasswordAgain.UseSystemPasswordChar = true;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = SystemColors.ControlLight;
            txtPassword.Location = new Point(210, 255);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(216, 23);
            txtPassword.TabIndex = 2;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = SystemColors.ControlLight;
            txtUsername.Location = new Point(210, 189);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(216, 23);
            txtUsername.TabIndex = 3;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(356, 372);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(95, 26);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Regisztrálás";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.Location = new Point(56, 81);
            label1.Name = "label1";
            label1.Size = new Size(383, 17);
            label1.TabIndex = 5;
            label1.Text = "Regisztráció a Remotee Management rendszer használatához";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(66, 134);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 6;
            label2.Text = "Teljes név:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(66, 198);
            label3.Name = "label3";
            label3.Size = new Size(98, 17);
            label3.TabIndex = 7;
            label3.Text = "Felhasználónév:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(66, 264);
            label4.Name = "label4";
            label4.Size = new Size(46, 17);
            label4.TabIndex = 8;
            label4.Text = "Jelszó:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(66, 337);
            label5.Name = "label5";
            label5.Size = new Size(72, 17);
            label5.TabIndex = 9;
            label5.Text = "Jelszó újra:";
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Zoom;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 60);
            panel1.TabIndex = 10;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(12, 404);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(75, 23);
            btnBack.TabIndex = 11;
            btnBack.Text = "Vissza";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnBack;
            ClientSize = new Size(500, 439);
            Controls.Add(btnBack);
            Controls.Add(panel1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnRegister);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(txtPasswordAgain);
            Controls.Add(txtFullName);
            Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 238);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Regisztráció";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtFullName;
        private TextBox txtPasswordAgain;
        private TextBox txtPassword;
        private TextBox txtUsername;
        private Button btnRegister;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Panel panel1;
        private Button btnBack;
    }
}