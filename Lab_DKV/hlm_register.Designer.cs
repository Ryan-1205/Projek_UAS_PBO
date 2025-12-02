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
            this.txtUsernameReg = new System.Windows.Forms.TextBox();
            this.txtNisReg = new System.Windows.Forms.TextBox();
            this.btn_Register = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtUsernameReg
            // 
            this.txtUsernameReg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUsernameReg.Location = new System.Drawing.Point(266, 183);
            this.txtUsernameReg.MaximumSize = new System.Drawing.Size(800, 66);
            this.txtUsernameReg.MinimumSize = new System.Drawing.Size(259, 22);
            this.txtUsernameReg.Name = "txtUsernameReg";
            this.txtUsernameReg.Size = new System.Drawing.Size(259, 22);
            this.txtUsernameReg.TabIndex = 0;
            this.txtUsernameReg.Text = "Username";
            this.txtUsernameReg.TextChanged += new System.EventHandler(this.txtUsernameReg_TextChanged);
            // 
            // txtNisReg
            // 
            this.txtNisReg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNisReg.Location = new System.Drawing.Point(266, 220);
            this.txtNisReg.MaximumSize = new System.Drawing.Size(500, 66);
            this.txtNisReg.MinimumSize = new System.Drawing.Size(259, 22);
            this.txtNisReg.Name = "txtNisReg";
            this.txtNisReg.Size = new System.Drawing.Size(259, 22);
            this.txtNisReg.TabIndex = 0;
            this.txtNisReg.Text = "NIS";
            // 
            // btn_Register
            // 
            this.btn_Register.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Register.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_Register.Location = new System.Drawing.Point(345, 263);
            this.btn_Register.MaximumSize = new System.Drawing.Size(200, 50);
            this.btn_Register.MinimumSize = new System.Drawing.Size(75, 38);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(75, 38);
            this.btn_Register.TabIndex = 1;
            this.btn_Register.Text = "Regis";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // hlm_register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Register);
            this.Controls.Add(this.txtUsernameReg);
            this.Controls.Add(this.txtNisReg);
            this.MinimumSize = new System.Drawing.Size(818, 497);
            this.Name = "hlm_register";
            this.Text = "hlm_register";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.hlm_register_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtUsernameReg;
        private System.Windows.Forms.TextBox txtNisReg;
        private System.Windows.Forms.Button btn_Register;
    }
}