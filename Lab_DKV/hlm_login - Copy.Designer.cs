namespace Lab_DKV
{
    partial class hlm_login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hlm_login));
            this.txtNis = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnDaftar = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.lblNisWarning = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNis
            // 
            resources.ApplyResources(this.txtNis, "txtNis");
            this.txtNis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtNis.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNis.Name = "txtNis";
            // 
            // txtUsername
            // 
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtUsername.Name = "txtUsername";
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnDaftar
            // 
            resources.ApplyResources(this.btnDaftar, "btnDaftar");
            this.btnDaftar.BackColor = System.Drawing.Color.Transparent;
            this.btnDaftar.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnDaftar.FlatAppearance.BorderSize = 0;
            this.btnDaftar.ForeColor = System.Drawing.Color.Black;
            this.btnDaftar.Name = "btnDaftar";
            this.btnDaftar.UseVisualStyleBackColor = false;
            this.btnDaftar.Click += new System.EventHandler(this.btnDaftar_Click);
            // 
            // btnView
            // 
            this.btnView.BackgroundImage = global::Lab_DKV.Properties.Resources.notview;
            resources.ApplyResources(this.btnView, "btnView");
            this.btnView.Name = "btnView";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lblNisWarning
            // 
            resources.ApplyResources(this.lblNisWarning, "lblNisWarning");
            this.lblNisWarning.BackColor = System.Drawing.Color.Transparent;
            this.lblNisWarning.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblNisWarning.Name = "lblNisWarning";
            // 
            // hlm_login
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lab_DKV.Properties.Resources.hlm_login;
            this.Controls.Add(this.lblNisWarning);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.btnDaftar);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.txtNis);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "hlm_login";
            this.Load += new System.EventHandler(this.hlm_login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtNis;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnDaftar;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label lblNisWarning;
    }
}