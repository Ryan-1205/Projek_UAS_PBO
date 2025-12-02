namespace Lab_DKV
{
    partial class hlm_admin
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDataBarang = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnLaporanPeminjaman = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(287, 39);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTitle.Size = new System.Drawing.Size(231, 36);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ADMIN PANEL";
            // 
            // btnDataBarang
            // 
            this.btnDataBarang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataBarang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataBarang.Location = new System.Drawing.Point(25, 134);
            this.btnDataBarang.Name = "btnDataBarang";
            this.btnDataBarang.Size = new System.Drawing.Size(180, 45);
            this.btnDataBarang.TabIndex = 1;
            this.btnDataBarang.Text = "Data Barang";
            this.btnDataBarang.UseVisualStyleBackColor = true;
            this.btnDataBarang.Click += new System.EventHandler(this.BtnBarang_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Red;
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnLogout.Location = new System.Drawing.Point(25, 405);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 33);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnLaporanPeminjaman
            // 
            this.btnLaporanPeminjaman.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLaporanPeminjaman.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaporanPeminjaman.Location = new System.Drawing.Point(25, 263);
            this.btnLaporanPeminjaman.Name = "btnLaporanPeminjaman";
            this.btnLaporanPeminjaman.Size = new System.Drawing.Size(180, 45);
            this.btnLaporanPeminjaman.TabIndex = 1;
            this.btnLaporanPeminjaman.Text = "Laporan Peminjaman";
            this.btnLaporanPeminjaman.UseVisualStyleBackColor = true;
            this.btnLaporanPeminjaman.Click += new System.EventHandler(this.btnLaporanPeminjaman_Click);
            // 
            // hlm_admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnLaporanPeminjaman);
            this.Controls.Add(this.btnDataBarang);
            this.Controls.Add(this.lblTitle);
            this.Name = "hlm_admin";
            this.Text = "halamanAdmin";
            this.Load += new System.EventHandler(this.halamanAdmin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDataBarang;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnLaporanPeminjaman;
    }
}