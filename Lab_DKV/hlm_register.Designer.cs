namespace Lab_DKV
{
    partial class hlm_register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hlm_register));
            this.txtUsernameReg = new System.Windows.Forms.TextBox();
            this.txtNisReg = new System.Windows.Forms.TextBox();
            this.btndaftar = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.mySqlCommand1 = new MySql.Data.MySqlClient.MySqlCommand();
            this.btnView = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsernameReg
            // 
            resources.ApplyResources(this.txtUsernameReg, "txtUsernameReg");
            this.txtUsernameReg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtUsernameReg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsernameReg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUsernameReg.Name = "txtUsernameReg";
            this.txtUsernameReg.TextChanged += new System.EventHandler(this.txtUsernameReg_TextChanged);
            // 
            // txtNisReg
            // 
            resources.ApplyResources(this.txtNisReg, "txtNisReg");
            this.txtNisReg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtNisReg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNisReg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNisReg.Name = "txtNisReg";
            this.txtNisReg.TextChanged += new System.EventHandler(this.txtNisReg_TextChanged);
            // 
            // btndaftar
            // 
            resources.ApplyResources(this.btndaftar, "btndaftar");
            this.btndaftar.BackColor = System.Drawing.Color.Transparent;
            this.btndaftar.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btndaftar.FlatAppearance.BorderSize = 0;
            this.btndaftar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.btndaftar.Name = "btndaftar";
            this.btndaftar.UseVisualStyleBackColor = false;
            this.btndaftar.Click += new System.EventHandler(this.btndaftar_Click);
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // mySqlCommand1
            // 
            this.mySqlCommand1.CacheAge = 0;
            this.mySqlCommand1.Connection = null;
            this.mySqlCommand1.EnableCaching = false;
            this.mySqlCommand1.Transaction = null;
            // 
            // btnView
            // 
            this.btnView.BackgroundImage = global::Lab_DKV.Properties.Resources.notview;
            resources.ApplyResources(this.btnView, "btnView");
            this.btnView.Name = "btnView";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // hlm_register
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btndaftar);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUsernameReg);
            this.Controls.Add(this.txtNisReg);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "hlm_register";
            this.Load += new System.EventHandler(this.hlm_register_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtUsernameReg;
        private System.Windows.Forms.TextBox txtNisReg;
        private System.Windows.Forms.Button btndaftar;
        private System.Windows.Forms.Button btnLogin;
        private MySql.Data.MySqlClient.MySqlCommand mySqlCommand1;
        private System.Windows.Forms.Button btnView;
    }
}