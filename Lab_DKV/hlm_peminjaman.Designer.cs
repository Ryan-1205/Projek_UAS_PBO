namespace Lab_DKV
{
    partial class hlm_peminjaman
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hlm_peminjaman));
            this.txtKodeBarang = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnCari = new System.Windows.Forms.Button();
            this.txtNamaBarang = new System.Windows.Forms.TextBox();
            this.txtMerek = new System.Windows.Forms.TextBox();
            this.txtJumlahBarang = new System.Windows.Forms.TextBox();
            this.btnKirim = new System.Windows.Forms.Button();
            this.gridListPinjamBarang = new System.Windows.Forms.DataGridView();
            this.gridKetersediaanBarang = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridListPinjamBarang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridKetersediaanBarang)).BeginInit();
            this.SuspendLayout();
            // 
            // txtKodeBarang
            // 
            resources.ApplyResources(this.txtKodeBarang, "txtKodeBarang");
            this.txtKodeBarang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtKodeBarang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtKodeBarang.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKodeBarang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtKodeBarang.Name = "txtKodeBarang";
            this.txtKodeBarang.TextChanged += new System.EventHandler(this.txtKodeBarang_TextChanged);
            // 
            // btnTambah
            // 
            resources.ApplyResources(this.btnTambah, "btnTambah");
            this.btnTambah.BackColor = System.Drawing.Color.Transparent;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.UseVisualStyleBackColor = false;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            resources.ApplyResources(this.btnHapus, "btnHapus");
            this.btnHapus.BackColor = System.Drawing.Color.Transparent;
            this.btnHapus.FlatAppearance.BorderSize = 0;
            this.btnHapus.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.UseVisualStyleBackColor = false;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnCari
            // 
            resources.ApplyResources(this.btnCari, "btnCari");
            this.btnCari.BackColor = System.Drawing.Color.Transparent;
            this.btnCari.FlatAppearance.BorderSize = 0;
            this.btnCari.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCari.Name = "btnCari";
            this.btnCari.UseVisualStyleBackColor = false;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // txtNamaBarang
            // 
            resources.ApplyResources(this.txtNamaBarang, "txtNamaBarang");
            this.txtNamaBarang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtNamaBarang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNamaBarang.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNamaBarang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtNamaBarang.Name = "txtNamaBarang";
            this.txtNamaBarang.TextChanged += new System.EventHandler(this.txtNamaBarang_TextChanged);
            // 
            // txtMerek
            // 
            resources.ApplyResources(this.txtMerek, "txtMerek");
            this.txtMerek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtMerek.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMerek.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMerek.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtMerek.Name = "txtMerek";
            this.txtMerek.TextChanged += new System.EventHandler(this.txtMerek_TextChanged);
            // 
            // txtJumlahBarang
            // 
            resources.ApplyResources(this.txtJumlahBarang, "txtJumlahBarang");
            this.txtJumlahBarang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.txtJumlahBarang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtJumlahBarang.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJumlahBarang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtJumlahBarang.Name = "txtJumlahBarang";
            this.txtJumlahBarang.TextChanged += new System.EventHandler(this.txtJumlahBarang_TextChanged);
            // 
            // btnKirim
            // 
            resources.ApplyResources(this.btnKirim, "btnKirim");
            this.btnKirim.BackColor = System.Drawing.Color.Transparent;
            this.btnKirim.FlatAppearance.BorderSize = 0;
            this.btnKirim.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnKirim.Name = "btnKirim";
            this.btnKirim.UseVisualStyleBackColor = false;
            this.btnKirim.Click += new System.EventHandler(this.btnKirim_Click);
            // 
            // gridListPinjamBarang
            // 
            this.gridListPinjamBarang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.gridListPinjamBarang, "gridListPinjamBarang");
            this.gridListPinjamBarang.Name = "gridListPinjamBarang";
            this.gridListPinjamBarang.RowTemplate.Height = 24;
            // 
            // gridKetersediaanBarang
            // 
            this.gridKetersediaanBarang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.gridKetersediaanBarang, "gridKetersediaanBarang");
            this.gridKetersediaanBarang.Name = "gridKetersediaanBarang";
            this.gridKetersediaanBarang.RowTemplate.Height = 24;
            this.gridKetersediaanBarang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridKetersediaanBarang_CellContentClick);
            // 
            // hlm_peminjaman
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lab_DKV.Properties.Resources.hlm_peminjaman;
            this.Controls.Add(this.gridKetersediaanBarang);
            this.Controls.Add(this.gridListPinjamBarang);
            this.Controls.Add(this.btnKirim);
            this.Controls.Add(this.txtJumlahBarang);
            this.Controls.Add(this.txtMerek);
            this.Controls.Add(this.txtNamaBarang);
            this.Controls.Add(this.btnCari);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtKodeBarang);
            this.MaximizeBox = false;
            this.Name = "hlm_peminjaman";
            this.Load += new System.EventHandler(this.hlm_peminjaman_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridListPinjamBarang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridKetersediaanBarang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKodeBarang;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.TextBox txtNamaBarang;
        private System.Windows.Forms.TextBox txtMerek;
        private System.Windows.Forms.TextBox txtJumlahBarang;
        private System.Windows.Forms.Button btnKirim;
        private System.Windows.Forms.DataGridView gridListPinjamBarang;
        private System.Windows.Forms.DataGridView gridKetersediaanBarang;
    }
}