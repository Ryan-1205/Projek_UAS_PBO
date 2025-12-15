using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class hlm_barang : Form
    {
        // Variabel global untuk menampung data (agar pencarian lancar tanpa reload DB terus)
        private DataTable dtBarang = new DataTable();

        public hlm_barang()
        {
            InitializeComponent();

            // --- PERBAIKAN UTAMA DISINI ---
            // Baris ini memaksa data loading otomatis saat form dibuka
            // Jadi tidak perlu setting manual di "Properties -> Events"
            this.Load += hlm_barang_Load;
        }

        private void hlm_barang_Load(object sender, EventArgs e)
        {
            // Panggil fungsi pemuat data saat aplikasi pertama kali jalan
            LoadDataBarang();
        }

        // ==========================================
        // 1. FUNGSI LOAD DATA (Auto Refresh)
        // ==========================================
        public void LoadDataBarang()
        {
            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    
                    // Query lengkap dengan perhitungan stok
                    string query = @"
                        SELECT 
                            B.kode_barang, 
                            B.nama_barang, 
                            B.merek, 
                            B.kondisi_barang,
                            B.jumlah_barang AS 'Total Stok',
                            COALESCE(SUM(CASE WHEN D.status_kembali = 0 THEN D.unit ELSE 0 END), 0) AS 'Dipinjam',
                            (B.jumlah_barang - COALESCE(SUM(CASE WHEN D.status_kembali = 0 THEN D.unit ELSE 0 END), 0)) AS 'Tersedia'
                        FROM tbl_barang B
                        LEFT JOIN tbl_detailpb D ON B.id_barang = D.id_barang
                        GROUP BY B.id_barang, B.kode_barang, B.nama_barang, B.merek, B.kondisi_barang, B.jumlah_barang";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        dtBarang = new DataTable(); // Reset DataTable
                        da.Fill(dtBarang);
                        dataGridView1.DataSource = dtBarang;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        // ==========================================
        // 2. TOMBOL TAMBAH (Create)
        // ==========================================
        private void btn_tambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_kode_barang.Text) || string.IsNullOrWhiteSpace(txt_nama_barang.Text))
            {
                MessageBox.Show("Kode dan Nama Barang wajib diisi!");
                return;
            }

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO tbl_barang 
                        (kode_barang, nama_barang, merek, kondisi_barang, jumlah_barang) 
                        VALUES (@kode, @nama, @merek, @kondisi, @jumlah_barang)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kode", txt_kode_barang.Text);
                        cmd.Parameters.AddWithValue("@nama", txt_nama_barang.Text);
                        cmd.Parameters.AddWithValue("@merek", txt_merk.Text);
                        cmd.Parameters.AddWithValue("@kondisi", txt_kondisi_barang.Text);
                        cmd.Parameters.AddWithValue("@jumlah_barang", txt_jumlah_barang.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Data berhasil ditambahkan!");
                
                LoadDataBarang(); // Auto Load setelah tambah
                ClearInput();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }

        // ==========================================
        // 3. TOMBOL UPDATE (Edit)
        // ==========================================
        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text == "") { MessageBox.Show("Pilih data dulu dari tabel!"); return; }

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE tbl_barang SET 
                        nama_barang=@nama, 
                        merek=@merek, 
                        kondisi_barang=@kondisi, 
                        jumlah_barang=@jumlah_barang
                        WHERE kode_barang=@kode";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kode", txt_kode_barang.Text);
                        cmd.Parameters.AddWithValue("@nama", txt_nama_barang.Text);
                        cmd.Parameters.AddWithValue("@merek", txt_merk.Text);
                        cmd.Parameters.AddWithValue("@kondisi", txt_kondisi_barang.Text);
                        cmd.Parameters.AddWithValue("@jumlah_barang", txt_jumlah_barang.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Data berhasil diupdate!");
                
                LoadDataBarang(); // Auto Load setelah update
                ClearInput();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }

        // ==========================================
        // 4. TOMBOL HAPUS (Delete)
        // ==========================================
        private void btn_hapus_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text == "") { MessageBox.Show("Pilih data terlebih dahulu!"); return; }

            if (MessageBox.Show("Yakin hapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = DB.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM tbl_barang WHERE kode_barang=@kode";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@kode", txt_kode_barang.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Data berhasil dihapus!");
                    
                    LoadDataBarang(); // Auto Load setelah hapus
                    ClearInput();
                }
                catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
            }
        }

        // ==========================================
        // 5. FITUR PENCARIAN & RELOAD
        // ==========================================
        private void btn_cari_Click(object sender, EventArgs e)
        {
            // Asumsi textbox pencarian adalah txt_nama_barang (bisa diganti textbox khusus search)
            string keyword = txt_nama_barang.Text.Trim(); 
            
            if (string.IsNullOrEmpty(keyword))
            {
                dtBarang.DefaultView.RowFilter = ""; 
            }
            else
            {
                // Filter di memori (cepat)
                dtBarang.DefaultView.RowFilter = string.Format("nama_barang LIKE '%{0}%' OR kode_barang LIKE '%{0}%' OR merek LIKE '%{0}%'", keyword);
            }
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            LoadDataBarang();
            ClearInput();
        }

        // ==========================================
        // 6. EVENT GRID KLIK (Isi Textbox)
        // ==========================================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txt_kode_barang.Text = row.Cells["kode_barang"].Value.ToString();
                txt_nama_barang.Text = row.Cells["nama_barang"].Value.ToString();
                txt_merk.Text = row.Cells["merek"].Value.ToString();
                txt_kondisi_barang.Text = row.Cells["kondisi_barang"].Value.ToString();
                
                if (row.Cells["Total Stok"].Value != null)
                    txt_jumlah_barang.Text = row.Cells["Total Stok"].Value.ToString();
            }
        }

        // ==========================================
        // 7. NAVIGASI
        // ==========================================
        private void btn_data_user_Click(object sender, EventArgs e)
        {
            hlm_DataUser userPage = new hlm_DataUser();
            userPage.Show();
            this.Hide();
        }

        private void btn_data_peminjam_Click(object sender, EventArgs e)
        {
            tbl_peminjaman pinjam = new tbl_peminjaman();
            pinjam.Show();
            this.Hide();
        }

        private void btn_keluar_Click(object sender, EventArgs e)
        {
            hlm_login login = new hlm_login();
            login.Show();
            this.Close();
        }

        // ==========================================
        // HELPER LAINNYA
        // ==========================================
        private void ClearInput()
        {
            txt_kode_barang.Clear();
            txt_nama_barang.Clear();
            txt_merk.Clear();
            txt_kondisi_barang.Clear();
            txt_jumlah_barang.Clear();
        }

        // Mapping event handler (jika ada tombol lain yang pakai nama ini)
        private void btn_hapus_Click_1(object sender, EventArgs e) => btn_hapus_Click(sender, e);
        private void DataBarang_CellContentClick(object sender, DataGridViewCellEventArgs e) => dataGridView1_CellClick(sender, e);
        private void btn_databarangClick(object sender, EventArgs e) { } // Kosongkan
        private void hlm_barang_Load_1(object sender, EventArgs e) { }
        private void txt_kode_barang_TextChanged(object sender, EventArgs e) { }
        private void txt_nama_barang_TextChanged(object sender, EventArgs e) { }
        private void txt_merk_TextChanged(object sender, EventArgs e) { }
        private void txt_kondisi_barang_TextChanged(object sender, EventArgs e) { }
        private void txt_unit_TextChanged(object sender, EventArgs e) { }
        private void btn_edit_data_Click(object sender, EventArgs e) { }
    }
}