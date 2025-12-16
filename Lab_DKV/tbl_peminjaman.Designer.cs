namespace Lab_DKV
{
    partial class tbl_peminjaman
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(tbl_peminjaman));
            this.btn_DataUser = new System.Windows.Forms.Button();
            this.btn_DataBarang = new System.Windows.Forms.Button();
            this.btn_DataPeminjam = new System.Windows.Forms.Button();
            this.btn_keluar = new System.Windows.Forms.Button();
            this.btn_hapus = new System.Windows.Forms.Button();
            this.btn_reload = new System.Windows.Forms.Button();
            this.btn_cari = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_no_pb = new System.Windows.Forms.TextBox();
            this.txt_tgl_pinjam = new System.Windows.Forms.TextBox();
            this.txt_nama_petugaspeminjam = new System.Windows.Forms.TextBox();
            this.txt_id_user = new System.Windows.Forms.TextBox();
            this.txt_tujuan = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_DataUser
            // 
            this.btn_DataUser.BackColor = System.Drawing.Color.Transparent;
            this.btn_DataUser.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_DataUser, "btn_DataUser");
            this.btn_DataUser.Name = "btn_DataUser";
            this.btn_DataUser.UseVisualStyleBackColor = false;
            this.btn_DataUser.Click += new System.EventHandler(this.btn_DataUser_Click);
            // 
            // btn_DataBarang
            // 
            this.btn_DataBarang.BackColor = System.Drawing.Color.Transparent;
            this.btn_DataBarang.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_DataBarang, "btn_DataBarang");
            this.btn_DataBarang.Name = "btn_DataBarang";
            this.btn_DataBarang.UseVisualStyleBackColor = false;
            this.btn_DataBarang.Click += new System.EventHandler(this.btn_DataBarang_Click);
            // 
            // btn_DataPeminjam
            // 
            this.btn_DataPeminjam.BackColor = System.Drawing.Color.Transparent;
            this.btn_DataPeminjam.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_DataPeminjam, "btn_DataPeminjam");
            this.btn_DataPeminjam.ForeColor = System.Drawing.Color.White;
            this.btn_DataPeminjam.Name = "btn_DataPeminjam";
            this.btn_DataPeminjam.UseVisualStyleBackColor = false;
            this.btn_DataPeminjam.Click += new System.EventHandler(this.btn_DataPeminjam_Click);
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
            this.btn_hapus.Click += new System.EventHandler(this.btn_hapus_Click);
            // 
            // btn_reload
            // 
            this.btn_reload.BackColor = System.Drawing.Color.Transparent;
            this.btn_reload.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_reload, "btn_reload");
            this.btn_reload.ForeColor = System.Drawing.Color.White;
            this.btn_reload.Name = "btn_reload";
            this.btn_reload.UseVisualStyleBackColor = false;
            this.btn_reload.Click += new System.EventHandler(this.btn_reload_Click);
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
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.Click += new System.EventHandler(this.tbl_peminjaman_Load);
            // 
            // txt_no_pb
            // 
            resources.ApplyResources(this.txt_no_pb, "txt_no_pb");
            this.txt_no_pb.Name = "txt_no_pb";
            this.txt_no_pb.TextChanged += new System.EventHandler(this.txt_no_pb_TextChanged);
            // 
            // txt_tgl_pinjam
            // 
            resources.ApplyResources(this.txt_tgl_pinjam, "txt_tgl_pinjam");
            this.txt_tgl_pinjam.Name = "txt_tgl_pinjam";
            this.txt_tgl_pinjam.TextChanged += new System.EventHandler(this.txt_tgl_pinjam_TextChanged);
            // 
            // txt_nama_petugaspeminjam
            // 
            resources.ApplyResources(this.txt_nama_petugaspeminjam, "txt_nama_petugaspeminjam");
            this.txt_nama_petugaspeminjam.Name = "txt_nama_petugaspeminjam";
            this.txt_nama_petugaspeminjam.TextChanged += new System.EventHandler(this.txt_nama_petugaspeminjam_TextChanged);
            // 
            // txt_id_user
            // 
            resources.ApplyResources(this.txt_id_user, "txt_id_user");
            this.txt_id_user.Name = "txt_id_user";
            this.txt_id_user.TextChanged += new System.EventHandler(this.txt_id_user_TextChanged);
            // 
            // txt_tujuan
            // 
            resources.ApplyResources(this.txt_tujuan, "txt_tujuan");
            this.txt_tujuan.Name = "txt_tujuan";
            this.txt_tujuan.TextChanged += new System.EventHandler(this.txt_tujuan_TextChanged);
            // 
            // tbl_peminjaman
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lab_DKV.Properties.Resources.Asset_27_100;
            this.Controls.Add(this.txt_tujuan);
            this.Controls.Add(this.txt_nama_petugaspeminjam);
            this.Controls.Add(this.txt_id_user);
            this.Controls.Add(this.txt_tgl_pinjam);
            this.Controls.Add(this.txt_no_pb);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_cari);
            this.Controls.Add(this.btn_reload);
            this.Controls.Add(this.btn_hapus);
            this.Controls.Add(this.btn_keluar);
            this.Controls.Add(this.btn_DataPeminjam);
            this.Controls.Add(this.btn_DataBarang);
            this.Controls.Add(this.btn_DataUser);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "tbl_peminjaman";
            this.Load += new System.EventHandler(this.tbl_peminjaman_Load);
            this.Click += new System.EventHandler(this.tbl_peminjaman_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_DataUser;
        private System.Windows.Forms.Button btn_DataBarang;
        private System.Windows.Forms.Button btn_DataPeminjam;
        private System.Windows.Forms.Button btn_keluar;
        private System.Windows.Forms.Button btn_hapus;
        private System.Windows.Forms.Button btn_reload;
        private System.Windows.Forms.Button btn_cari;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_no_pb;
        private System.Windows.Forms.TextBox txt_tgl_pinjam;
        private System.Windows.Forms.TextBox txt_nama_petugaspeminjam;
        private System.Windows.Forms.TextBox txt_id_user;
        private System.Windows.Forms.TextBox txt_tujuan;
    }
}