namespace Lab_DKV
{
    partial class hlm_DataUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hlm_DataUser));
            this.btnDataBarang = new System.Windows.Forms.Button();
            this.btnLDataPeminjam = new System.Windows.Forms.Button();
            this.btnDataUser = new System.Windows.Forms.Button();
            this.btn_keluar = new System.Windows.Forms.Button();
            this.btn_Hapus = new System.Windows.Forms.Button();
            this.btn_cari = new System.Windows.Forms.Button();
            this.btn_Tambah = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.txt_nis = new System.Windows.Forms.TextBox();
            this.txt_role = new System.Windows.Forms.TextBox();
            this.txt_angkatan = new System.Windows.Forms.TextBox();
            this.btn_Update = new System.Windows.Forms.Button();
            this.txt_id = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDataBarang
            // 
            this.btnDataBarang.BackColor = System.Drawing.Color.Transparent;
            this.btnDataBarang.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnDataBarang, "btnDataBarang");
            this.btnDataBarang.Name = "btnDataBarang";
            this.btnDataBarang.UseVisualStyleBackColor = false;
            this.btnDataBarang.Click += new System.EventHandler(this.BtnBarang_Click);
            // 
            // btnLDataPeminjam
            // 
            this.btnLDataPeminjam.BackColor = System.Drawing.Color.Transparent;
            this.btnLDataPeminjam.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btnLDataPeminjam, "btnLDataPeminjam");
            this.btnLDataPeminjam.ForeColor = System.Drawing.Color.Black;
            this.btnLDataPeminjam.Name = "btnLDataPeminjam";
            this.btnLDataPeminjam.UseVisualStyleBackColor = false;
            this.btnLDataPeminjam.Click += new System.EventHandler(this.btnDataPeminjam);
            // 
            // btnDataUser
            // 
            resources.ApplyResources(this.btnDataUser, "btnDataUser");
            this.btnDataUser.BackColor = System.Drawing.Color.Transparent;
            this.btnDataUser.FlatAppearance.BorderSize = 0;
            this.btnDataUser.ForeColor = System.Drawing.Color.White;
            this.btnDataUser.Name = "btnDataUser";
            this.btnDataUser.UseVisualStyleBackColor = false;
            this.btnDataUser.Click += new System.EventHandler(this.btnDataUser_Click);
            // 
            // btn_keluar
            // 
            this.btn_keluar.BackColor = System.Drawing.Color.Transparent;
            this.btn_keluar.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_keluar, "btn_keluar");
            this.btn_keluar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_keluar.Name = "btn_keluar";
            this.btn_keluar.UseVisualStyleBackColor = false;
            this.btn_keluar.Click += new System.EventHandler(this.btn_keluar_Click);
            // 
            // btn_Hapus
            // 
            this.btn_Hapus.BackColor = System.Drawing.Color.Transparent;
            this.btn_Hapus.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_Hapus, "btn_Hapus");
            this.btn_Hapus.ForeColor = System.Drawing.Color.White;
            this.btn_Hapus.Name = "btn_Hapus";
            this.btn_Hapus.UseVisualStyleBackColor = false;
            this.btn_Hapus.Click += new System.EventHandler(this.btn_Hapus_Click);
            // 
            // btn_cari
            // 
            this.btn_cari.BackColor = System.Drawing.Color.Transparent;
            this.btn_cari.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_cari, "btn_cari");
            this.btn_cari.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_cari.Name = "btn_cari";
            this.btn_cari.UseVisualStyleBackColor = false;
            // 
            // btn_Tambah
            // 
            this.btn_Tambah.BackColor = System.Drawing.Color.Transparent;
            this.btn_Tambah.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_Tambah, "btn_Tambah");
            this.btn_Tambah.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Tambah.Name = "btn_Tambah";
            this.btn_Tambah.UseVisualStyleBackColor = false;
            this.btn_Tambah.Click += new System.EventHandler(this.btn_Tambah_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // txt_username
            // 
            resources.ApplyResources(this.txt_username, "txt_username");
            this.txt_username.Name = "txt_username";
            this.txt_username.TextChanged += new System.EventHandler(this.username_TextChanged);
            // 
            // txt_nis
            // 
            resources.ApplyResources(this.txt_nis, "txt_nis");
            this.txt_nis.Name = "txt_nis";
            this.txt_nis.TextChanged += new System.EventHandler(this.txt_nis_TextChanged);
            // 
            // txt_role
            // 
            resources.ApplyResources(this.txt_role, "txt_role");
            this.txt_role.Name = "txt_role";
            this.txt_role.TextChanged += new System.EventHandler(this.txt_role_TextChanged);
            // 
            // txt_angkatan
            // 
            resources.ApplyResources(this.txt_angkatan, "txt_angkatan");
            this.txt_angkatan.Name = "txt_angkatan";
            this.txt_angkatan.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // btn_Update
            // 
            this.btn_Update.BackColor = System.Drawing.Color.Transparent;
            this.btn_Update.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_Update, "btn_Update");
            this.btn_Update.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.UseVisualStyleBackColor = false;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // txt_id
            // 
            this.txt_id.BackColor = System.Drawing.SystemColors.Window;
            this.txt_id.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_id, "txt_id");
            this.txt_id.Name = "txt_id";
            this.txt_id.ShortcutsEnabled = false;
            // 
            // hlm_DataUser
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lab_DKV.Properties.Resources.Asset_21_100;
            this.Controls.Add(this.txt_role);
            this.Controls.Add(this.txt_angkatan);
            this.Controls.Add(this.txt_nis);
            this.Controls.Add(this.txt_username);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.btn_Tambah);
            this.Controls.Add(this.btn_cari);
            this.Controls.Add(this.btn_Hapus);
            this.Controls.Add(this.btn_keluar);
            this.Controls.Add(this.btnDataUser);
            this.Controls.Add(this.btnLDataPeminjam);
            this.Controls.Add(this.btnDataBarang);
            this.Controls.Add(this.txt_id);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "hlm_DataUser";
            this.Load += new System.EventHandler(this.halamanAdmin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDataBarang;
        private System.Windows.Forms.Button btnLDataPeminjam;
        private System.Windows.Forms.Button btnDataUser;
        private System.Windows.Forms.Button btn_keluar;
        private System.Windows.Forms.Button btn_Hapus;
        private System.Windows.Forms.Button btn_cari;
        private System.Windows.Forms.Button btn_Tambah;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.TextBox txt_nis;
        private System.Windows.Forms.TextBox txt_role;
        private System.Windows.Forms.TextBox txt_angkatan;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.TextBox txt_id;
    }
}