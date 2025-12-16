using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class hlm_barang : Form
    {
        // Variabel global untuk menampung data (agar pencarian lancar)
        private DataTable dtBarang = new DataTable();

        public hlm_barang()
        {
            InitializeComponent();

            // Auto-load saat form dibuka
            this.Load += hlm_barang_Load;

            // Event Double Click untuk melihat siapa yang meminjam barang ini
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
        }

        private void hlm_barang_Load(object sender, EventArgs e)
        {
            LoadDataBarang();
        }

        // ==========================================
        // 1. FUNGSI LOAD DATA (VERSI OPSI A: STOK FISIK)
        // ==========================================
        public void LoadDataBarang()
        {
            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    // Query disederhanakan agar tidak terjadi Double Counting.
                    // 'jumlah_barang' di database sekarang dianggap sebagai 'Stok Tersedia di Rak'.
                    string query = @"
                        SELECT 
                            id_barang,        -- Hidden ID untuk referensi
                            kode_barang, 
                            nama_barang, 
                            merek, 
                            kondisi_barang, 
                            jumlah_barang AS 'Stok Tersedia' 
                        FROM tbl_barang 
                        ORDER BY nama_barang ASC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        dtBarang = new DataTable();
                        da.Fill(dtBarang);
                        dataGridView1.DataSource = dtBarang;

                        // Sembunyikan ID Barang agar tampilan bersih
                        if (dataGridView1.Columns["id_barang"] != null)
                            dataGridView1.Columns["id_barang"].Visible = false;

                        // Rapikan lebar kolom
                        if (dataGridView1.Columns["nama_barang"] != null)
                            dataGridView1.Columns["nama_barang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        // ==========================================
        // 2. FITUR CEK PEMINJAM (DOUBLE CLICK)
        // ==========================================
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Ambil ID Barang dari baris yang diklik
                int idBarang = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id_barang"].Value);
                string namaBarang = dataGridView1.Rows[e.RowIndex].Cells["nama_barang"].Value.ToString();

                ShowDetailPeminjam(idBarang, namaBarang);
            }
        }

        private void ShowDetailPeminjam(int idBarang, string namaBarang)
        {
            // Buat Popup Form secara coding (on-the-fly)
            Form frmDetail = new Form();
            frmDetail.Text = $"Siapa yang meminjam '{namaBarang}'?";
            frmDetail.Size = new Size(600, 300);
            frmDetail.StartPosition = FormStartPosition.CenterParent;

            DataGridView gridDetail = new DataGridView();
            gridDetail.Dock = DockStyle.Fill;
            gridDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridDetail.ReadOnly = true;

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    // Cari di tabel detail & peminjaman siapa yang belum mengembalikan (status_kembali = 0)
                    string query = @"
                        SELECT 
                            P.no_pb AS 'No. Pinjam',
                            P.tgl_pinjam AS 'Tgl Pinjam',
                            U.username AS 'Peminjam',
                            U.nis AS 'NIS',
                            D.unit AS 'Jml'
                        FROM tbl_detailpb D
                        JOIN tbl_peminjaman P ON D.id_peminjaman = P.id_peminjaman
                        JOIN tbl_user U ON P.id_user = U.id_user
                        WHERE D.id_barang = @id AND (D.status_kembali = 0 OR D.status_kembali IS NULL)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idBarang);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            gridDetail.DataSource = dt;

                            if (dt.Rows.Count == 0)
                            {
                                MessageBox.Show("Barang ini ada di rak semua (Tidak ada yang sedang meminjam).");
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal cek detail: " + ex.Message);
                return;
            }

            frmDetail.Controls.Add(gridDetail);
            frmDetail.ShowDialog();
        }

        // ==========================================
        // 3. CRUD (TAMBAH, UPDATE, HAPUS/KURANG)
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
                LoadDataBarang();
                ClearInput();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text == "") { MessageBox.Show("Pilih data dulu!"); return; }

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
                LoadDataBarang();
                ClearInput();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }

        // FUNGSI KURANGI STOK (HAPUS)
        private void btn_hapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_kode_barang.Text))
            {
                MessageBox.Show("Pilih barang terlebih dahulu!");
                return;
            }

            // Validasi Input Jumlah (Berapa yang mau dikurangi?)
            if (!int.TryParse(txt_jumlah_barang.Text, out int jumlahKurang) || jumlahKurang <= 0)
            {
                MessageBox.Show("Masukkan jumlah stok yang ingin dikurangi pada kolom Jumlah Barang.");
                return;
            }

            if (MessageBox.Show($"Kurangi stok '{txt_nama_barang.Text}' sebanyak {jumlahKurang} unit?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = DB.GetConnection())
                    {
                        conn.Open();
                        // 1. Cek stok dulu
                        string checkSql = "SELECT jumlah_barang FROM tbl_barang WHERE kode_barang=@kode";
                        int stokSekarang = 0;
                        using (MySqlCommand cmdCheck = new MySqlCommand(checkSql, conn))
                        {
                            cmdCheck.Parameters.AddWithValue("@kode", txt_kode_barang.Text);
                            object result = cmdCheck.ExecuteScalar();
                            if (result != null) stokSekarang = Convert.ToInt32(result);
                        }

                        if (stokSekarang < jumlahKurang)
                        {
                            MessageBox.Show($"Stok tidak cukup. Saat ini cuma ada: {stokSekarang}");
                            return;
                        }

                        // 2. Update DB
                        string updateSql = "UPDATE tbl_barang SET jumlah_barang = jumlah_barang - @kurang WHERE kode_barang=@kode";
                        using (MySqlCommand cmd = new MySqlCommand(updateSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@kurang", jumlahKurang);
                            cmd.Parameters.AddWithValue("@kode", txt_kode_barang.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Stok berhasil dikurangi!");
                    LoadDataBarang();
                    ClearInput();
                }
                catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
            }
        }

        // ==========================================
        // 4. NAVIGASI & UTILS
        // ==========================================

        private void btn_cari_Click(object sender, EventArgs e)
        {
            string keyword = txt_nama_barang.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) dtBarang.DefaultView.RowFilter = "";
            else dtBarang.DefaultView.RowFilter = string.Format("nama_barang LIKE '%{0}%' OR kode_barang LIKE '%{0}%' OR merek LIKE '%{0}%'", keyword);
        }

        private void btn_reload_Click(object sender, EventArgs e) { LoadDataBarang(); ClearInput(); }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txt_kode_barang.Text = row.Cells["kode_barang"].Value.ToString();
                txt_nama_barang.Text = row.Cells["nama_barang"].Value.ToString();
                txt_merk.Text = row.Cells["merek"].Value.ToString();
                txt_kondisi_barang.Text = row.Cells["kondisi_barang"].Value.ToString();
                if (row.Cells["Stok Tersedia"].Value != null)
                    txt_jumlah_barang.Text = row.Cells["Stok Tersedia"].Value.ToString();
            }
        }

        private void ClearInput()
        {
            txt_kode_barang.Clear(); txt_nama_barang.Clear(); txt_merk.Clear();
            txt_kondisi_barang.Clear(); txt_jumlah_barang.Clear();
        }

        private void btn_data_user_Click(object sender, EventArgs e) { hlm_DataUser p = new hlm_DataUser(); p.Show(); this.Hide(); }
        private void btn_data_peminjam_Click(object sender, EventArgs e) { tbl_peminjaman p = new tbl_peminjaman(); p.Show(); this.Hide(); }
        private void btn_keluar_Click(object sender, EventArgs e) { hlm_login p = new hlm_login(); p.Show(); this.Close(); }

        // Mappings
        private void btn_hapus_Click_1(object sender, EventArgs e) => btn_hapus_Click(sender, e);
        private void DataBarang_CellContentClick(object sender, DataGridViewCellEventArgs e) => dataGridView1_CellClick(sender, e);

        // Placeholders
        private void btn_databarangClick(object sender, EventArgs e) { }
        private void hlm_barang_Load_1(object sender, EventArgs e) { }
        private void txt_kode_barang_TextChanged(object sender, EventArgs e) { }
        private void txt_nama_barang_TextChanged(object sender, EventArgs e) { }
        private void txt_merk_TextChanged(object sender, EventArgs e) { }
        private void txt_kondisi_barang_TextChanged(object sender, EventArgs e) { }
        private void txt_unit_TextChanged(object sender, EventArgs e) { }
        private void btn_edit_data_Click(object sender, EventArgs e) { }
    }
}