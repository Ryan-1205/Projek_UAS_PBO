namespace Lab_DKV
{
    partial class hlm_barang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hlm_barang));
            this.txt_kode_barang = new System.Windows.Forms.TextBox();
            this.btn_data_user = new System.Windows.Forms.Button();
            this.btn_databarang = new System.Windows.Forms.Button();
            this.btn_data_peminjam = new System.Windows.Forms.Button();
            this.btn_keluar = new System.Windows.Forms.Button();
            this.btn_hapus = new System.Windows.Forms.Button();
            this.btn_tambah = new System.Windows.Forms.Button();
            this.btn_update = new System.Windows.Forms.Button();
            this.txt_nama_barang = new System.Windows.Forms.TextBox();
            this.txt_merk = new System.Windows.Forms.TextBox();
            this.txt_kondisi_barang = new System.Windows.Forms.TextBox();
            this.txt_jumlah_barang = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.oldbtn_hapus = new System.Windows.Forms.Button();
            this.btn_cari = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_kode_barang
            // 
            resources.ApplyResources(this.txt_kode_barang, "txt_kode_barang");
            this.txt_kode_barang.Name = "txt_kode_barang";
            this.txt_kode_barang.TextChanged += new System.EventHandler(this.txt_kode_barang_TextChanged);
            // 
            // btn_data_user
            // 
            this.btn_data_user.BackColor = System.Drawing.Color.Transparent;
            this.btn_data_user.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_data_user, "btn_data_user");
            this.btn_data_user.Name = "btn_data_user";
            this.btn_data_user.UseVisualStyleBackColor = false;
            this.btn_data_user.Click += new System.EventHandler(this.btn_data_user_Click);
            // 
            // btn_databarang
            // 
            this.btn_databarang.BackColor = System.Drawing.Color.Transparent;
            this.btn_databarang.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_databarang, "btn_databarang");
            this.btn_databarang.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_databarang.Name = "btn_databarang";
            this.btn_databarang.UseVisualStyleBackColor = false;
            this.btn_databarang.Click += new System.EventHandler(this.btn_databarangClick);
            // 
            // btn_data_peminjam
            // 
            this.btn_data_peminjam.BackColor = System.Drawing.Color.Transparent;
            this.btn_data_peminjam.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_data_peminjam, "btn_data_peminjam");
            this.btn_data_peminjam.Name = "btn_data_peminjam";
            this.btn_data_peminjam.UseVisualStyleBackColor = false;
            this.btn_data_peminjam.Click += new System.EventHandler(this.btn_data_peminjam_Click);
            // 
            // btn_keluar
            // 
            this.btn_keluar.BackColor = System.Drawing.Color.Transparent;
            this.btn_keluar.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_keluar, "btn_keluar");
            this.btn_keluar.ForeColor = System.Drawing.Color.White;
            this.btn_keluar.Name = "btn_keluar";
            this.btn_keluar.UseVisualStyleBackColor = false;
            this.btn_keluar.Click += new System.EventHandler(this.btn_keluar_Click);
            // 
            // btn_hapus
            // 
            this.btn_hapus.BackColor = System.Drawing.Color.Transparent;
            this.btn_hapus.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_hapus, "btn_hapus");
            this.btn_hapus.ForeColor = System.Drawing.Color.White;
            this.btn_hapus.Name = "btn_hapus";
            this.btn_hapus.UseVisualStyleBackColor = false;
            this.btn_hapus.Click += new System.EventHandler(this.btn_hapus_Click_1);
            // 
            // btn_tambah
            // 
            this.btn_tambah.BackColor = System.Drawing.Color.Transparent;
            this.btn_tambah.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_tambah, "btn_tambah");
            this.btn_tambah.ForeColor = System.Drawing.Color.Transparent;
            this.btn_tambah.Name = "btn_tambah";
            this.btn_tambah.UseVisualStyleBackColor = false;
            this.btn_tambah.Click += new System.EventHandler(this.btn_tambah_Click);
            // 
            // btn_update
            // 
            this.btn_update.BackColor = System.Drawing.Color.Transparent;
            this.btn_update.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_update, "btn_update");
            this.btn_update.ForeColor = System.Drawing.Color.White;
            this.btn_update.Name = "btn_update";
            this.btn_update.UseVisualStyleBackColor = false;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // txt_nama_barang
            // 
            resources.ApplyResources(this.txt_nama_barang, "txt_nama_barang");
            this.txt_nama_barang.Name = "txt_nama_barang";
            this.txt_nama_barang.TextChanged += new System.EventHandler(this.txt_nama_barang_TextChanged);
            // 
            // txt_merk
            // 
            resources.ApplyResources(this.txt_merk, "txt_merk");
            this.txt_merk.Name = "txt_merk";
            this.txt_merk.TextChanged += new System.EventHandler(this.txt_merk_TextChanged);
            // 
            // txt_kondisi_barang
            // 
            resources.ApplyResources(this.txt_kondisi_barang, "txt_kondisi_barang");
            this.txt_kondisi_barang.Name = "txt_kondisi_barang";
            this.txt_kondisi_barang.TextChanged += new System.EventHandler(this.txt_kondisi_barang_TextChanged);
            // 
            // txt_jumlah_barang
            // 
            resources.ApplyResources(this.txt_jumlah_barang, "txt_jumlah_barang");
            this.txt_jumlah_barang.Name = "txt_jumlah_barang";
            this.txt_jumlah_barang.TextChanged += new System.EventHandler(this.txt_unit_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataBarang_CellContentClick);
            // 
            // oldbtn_hapus
            // 
            this.oldbtn_hapus.BackColor = System.Drawing.Color.Transparent;
            this.oldbtn_hapus.FlatAppearance.BorderSize = 0;
            this.oldbtn_hapus.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.oldbtn_hapus, "oldbtn_hapus");
            this.oldbtn_hapus.Name = "oldbtn_hapus";
            this.oldbtn_hapus.UseVisualStyleBackColor = false;
            this.oldbtn_hapus.Click += new System.EventHandler(this.btn_hapus_Click);
            // 
            // btn_cari
            // 
            this.btn_cari.BackColor = System.Drawing.Color.Transparent;
            this.btn_cari.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_cari, "btn_cari");
            this.btn_cari.ForeColor = System.Drawing.Color.White;
            this.btn_cari.Name = "btn_cari";
            this.btn_cari.UseVisualStyleBackColor = false;
            this.btn_cari.Click += new System.EventHandler(this.btn_cari_Click);
            // 
            // hlm_barang
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lab_DKV.Properties.Resources.Asset_20_100;
            this.Controls.Add(this.txt_jumlah_barang);
            this.Controls.Add(this.txt_kondisi_barang);
            this.Controls.Add(this.txt_merk);
            this.Controls.Add(this.txt_nama_barang);
            this.Controls.Add(this.txt_kode_barang);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.btn_tambah);
            this.Controls.Add(this.btn_hapus);
            this.Controls.Add(this.btn_cari);
            this.Controls.Add(this.btn_keluar);
            this.Controls.Add(this.btn_data_peminjam);
            this.Controls.Add(this.btn_databarang);
            this.Controls.Add(this.btn_data_user);
            this.Controls.Add(this.oldbtn_hapus);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "hlm_barang";
            this.Load += new System.EventHandler(this.hlm_barang_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_kode_barang;
        private System.Windows.Forms.Button btn_data_user;
        private System.Windows.Forms.Button btn_databarang;
        private System.Windows.Forms.Button btn_data_peminjam;
        private System.Windows.Forms.Button btn_keluar;
        private System.Windows.Forms.Button btn_hapus;
        private System.Windows.Forms.Button btn_tambah;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.TextBox txt_nama_barang;
        private System.Windows.Forms.TextBox txt_merk;
        private System.Windows.Forms.TextBox txt_kondisi_barang;
        private System.Windows.Forms.TextBox txt_jumlah_barang;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button oldbtn_hapus;
        private System.Windows.Forms.Button btn_cari;
    }
}