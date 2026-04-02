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
            SuspendLayout();
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(190, 88);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(216, 23);
            txtFullName.TabIndex = 0;
            // 
            // txtPasswordAgain
            // 
            txtPasswordAgain.Location = new Point(190, 267);
            txtPasswordAgain.Name = "txtPasswordAgain";
            txtPasswordAgain.Size = new Size(216, 23);
            txtPasswordAgain.TabIndex = 1;
            txtPasswordAgain.UseSystemPasswordChar = true;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(190, 203);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(216, 23);
            txtPassword.TabIndex = 2;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(190, 145);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(216, 23);
            txtUsername.TabIndex = 3;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(360, 322);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(95, 23);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Regisztrálás";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(116, 34);
            label1.Name = "label1";
            label1.Size = new Size(253, 15);
            label1.TabIndex = 5;
            label1.Text = "Regisztráció a Remotee rendszer használatához";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(46, 96);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 6;
            label2.Text = "Teljes név:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(46, 153);
            label3.Name = "label3";
            label3.Size = new Size(90, 15);
            label3.TabIndex = 7;
            label3.Text = "Felhasználónév:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(46, 211);
            label4.Name = "label4";
            label4.Size = new Size(40, 15);
            label4.TabIndex = 8;
            label4.Text = "Jelszó:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(46, 275);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 9;
            label5.Text = "Jelszó újra:";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 387);
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
            Name = "RegisterForm";
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
    }
}