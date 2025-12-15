using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement; // Boleh dihapus kalau gak ada error

namespace Lab_DKV
{
    public partial class hlm_pengembalian : Form
    {
        // =================================================================
        // 🚨 1. PROPERTI DAN KONFIGURASI
        // =================================================================
        private string connString = "Server=localhost;Database=lab_dkv;Uid=root;Pwd=;"; // GANTI INI SESUAI DB LU
        private string _nisAtauIdUser;

        // =================================================================
        // 🚨 2. CONSTRUCTOR
        // =================================================================
        public hlm_pengembalian()
        {
            // Panggil InitializeComponent()
            // Asumsi method ini ada di hlm_pengembalian.Designer.cs
            InitializeComponent();
        }

        public hlm_pengembalian(string nisAtauIdUser)
        {
            InitializeComponent();
            _nisAtauIdUser = nisAtauIdUser;
        }

        // =================================================================
        // 🚨 3. EVENT HANDLER UTAMA
        // =================================================================

        private void Form1_Load(object sender, EventArgs e)
        {
            // Panggil getData() untuk menampilkan data saat form pertama kali dibuka
            getData();
        }

        // Method ini tidak dipakai di desain lu, tapi di-retain aja
        private void buttonGetData_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kosong, biasanya dipakai untuk handling klik di dalam grid, misal check box
        }

        // Event handler untuk textBox1_NamaPenerima
        private void textBox1_NamaPenerima_TextChanged(object sender, EventArgs e) // Nama method diubah biar lebih sesuai (asumsi nama aslinya TextChanged)
        {
            // Biasanya kosong, atau bisa dipakai buat validasi input real-time
        }

        // Event handler Tanggal_ValueChanged tidak dipakai
        private void Tanggal_ValueChanged(object sender, EventArgs e)
        {
            // Kosong
        }


        // =================================================================
        // 🚨 4. FUNGSI UTILITY & BUSINESS LOGIC
        // =================================================================

        private void getData()
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    // Query: Ambil detail barang yang statusnya BELUM KEMBALI (status_kembali = 0)
                    string query = @"
                        SELECT 
                            D.id_detailpb, 
                            P.no_pb,
                            B.kode_barang, 
                            B.nama_barang, 
                            D.unit, 
                            D.keterangan,
                            P.id_peminjaman 
                        FROM tbl_detailpb D
                        INNER JOIN tbl_peminjaman P ON D.id_peminjaman = P.id_peminjaman
                        INNER JOIN tbl_barang B ON D.id_barang = B.id_barang
                        WHERE P.id_user = @IdUser AND D.status_kembali = 0; 
                    ";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdUser", _nisAtauIdUser);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Tambahin kolom check box kalau belum ada (Asumsi nama kolomnya 'chkBox')
                    if (dataGridView1.Columns["chkBox"] == null)
                    {
                        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                        chk.HeaderText = "Pilih";
                        chk.Name = "chkBox";
                        dataGridView1.Columns.Insert(0, chk);
                    }

                    // Sembunyikan ID yang tidak perlu dilihat user
                    if (dataGridView1.Columns.Contains("id_detailpb"))
                        dataGridView1.Columns["id_detailpb"].Visible = false;
                    if (dataGridView1.Columns.Contains("id_peminjaman"))
                        dataGridView1.Columns["id_peminjaman"].Visible = false;

                    if (dt.Rows.Count == 0)
                    {
                        // Sesuai logika lu: kalau data kosong, barang sudah dikembalikan semua
                        MessageBox.Show("Tidak ada barang pinjaman aktif yang perlu dikembalikan.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat mengambil data pinjaman: " + ex.Message, "Error Koneksi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =================================================================
        // 🚨 5. BUTTON CLICKS
        // =================================================================

        private void Bt_Pilih_Semua_Click(object sender, EventArgs e)
        {
            // Logic buat Check/Uncheck semua baris
            bool checkStatus = true;
            int checkedCount = 0;

            // Cek status current
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["chkBox"] != null && row.Cells["chkBox"].Value != null && (bool)row.Cells["chkBox"].Value == true)
                {
                    checkedCount++;
                }
            }

            if (checkedCount == dataGridView1.Rows.Count)
            {
                checkStatus = false; // Kalau semua udah ke-check, uncheck semua
            }

            // Terapkan status
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["chkBox"] != null)
                {
                    row.Cells["chkBox"].Value = checkStatus;
                }
            }
        }

        private void Kirim_Click(object sender, EventArgs e)
        {
            // 1. Validasi Input
            if (string.IsNullOrWhiteSpace(NamaPenerima.Text))
            {
                MessageBox.Show("Nama Petugas Penerima wajib diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NamaPenerima.Focus();
                return;
            }

            List<string> detailIdToProcess = new List<string>();
            string namaPenerima = NamaPenerima.Text;

            // Kumpulkan ID Detail Pinjaman (id_detailpb) yang dicentang
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Pastikan bukan row kosong
                if (row.Cells["chkBox"] != null && row.Cells["chkBox"].Value != null && (bool)row.Cells["chkBox"].Value == true)
                {
                    detailIdToProcess.Add(row.Cells["id_detailpb"].Value.ToString());
                }
            }

            if (detailIdToProcess.Count == 0)
            {
                MessageBox.Show("Pilih minimal satu barang yang akan dikembalikan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Konfirmasi
            DialogResult dialogResult = MessageBox.Show($"Yakin mau memproses pengembalian {detailIdToProcess.Count} barang?",
                "Konfirmasi Pengembalian", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                // 3. Eksekusi Transaksi Database
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    MySqlTransaction trans = conn.BeginTransaction();

                    try
                    {
                        int successfulUpdates = 0;
                        string tglKembali = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        // Loop dan Proses UPDATE
                        foreach (string idDetail in detailIdToProcess)
                        {
                            // A. Ambil ID Barang, Unit, dan ID Peminjaman dari detail
                            string sqlGetDetail = "SELECT id_barang, unit, id_peminjaman FROM tbl_detailpb WHERE id_detailpb = @IdDetail";
                            MySqlCommand cmdGetDetail = new MySqlCommand(sqlGetDetail, conn, trans);
                            cmdGetDetail.Parameters.AddWithValue("@IdDetail", idDetail);

                            string idBarang = "";
                            string idPeminjaman = "";
                            int unitDikembalikan = 0;

                            using (MySqlDataReader reader = cmdGetDetail.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    idBarang = reader["id_barang"].ToString();
                                    idPeminjaman = reader["id_peminjaman"].ToString();
                                    unitDikembalikan = Convert.ToInt32(reader["unit"]);
                                }
                                reader.Close();
                            }

                            // B. Update status_kembali di tbl_detailpb jadi 1 (Sudah Kembali)
                            string sqlUpdateDetail = "UPDATE tbl_detailpb SET status_kembali = 1 WHERE id_detailpb = @IdDetail;";
                            MySqlCommand cmdUpdateDetail = new MySqlCommand(sqlUpdateDetail, conn, trans);
                            cmdUpdateDetail.Parameters.AddWithValue("@IdDetail", idDetail);
                            cmdUpdateDetail.ExecuteNonQuery();

                            // C. Update stok barang di tbl_barang (stok bertambah)
                            string sqlUpdateStok = "UPDATE tbl_barang SET jumlah_barang = jumlah_barang + @Unit WHERE id_barang = @IdBarang;";
                            MySqlCommand cmdUpdateStok = new MySqlCommand(sqlUpdateStok, conn, trans);
                            cmdUpdateStok.Parameters.AddWithValue("@Unit", unitDikembalikan);
                            cmdUpdateStok.Parameters.AddWithValue("@IdBarang", idBarang);
                            cmdUpdateStok.ExecuteNonQuery();

                            // D. Insert ke tbl_pengembalian (Dicatat sebagai riwayat)
                            string sqlInsertPengembalian = @"
                                INSERT INTO tbl_pengembalian (id_peminjaman, tgl_kembali, nama_petugaskembali, catatan) 
                                VALUES (@IdPeminjaman, @TglKembali, @NamaPetugas, @Catatan);";

                            MySqlCommand cmdInsertPengembalian = new MySqlCommand(sqlInsertPengembalian, conn, trans);
                            cmdInsertPengembalian.Parameters.AddWithValue("@IdPeminjaman", idPeminjaman);
                            cmdInsertPengembalian.Parameters.AddWithValue("@TglKembali", tglKembali);
                            cmdInsertPengembalian.Parameters.AddWithValue("@NamaPetugas", namaPenerima);
                            cmdInsertPengembalian.Parameters.AddWithValue("@Catatan", $"Pengembalian 1 detail barang ({idDetail}).");
                            cmdInsertPengembalian.ExecuteNonQuery();

                            successfulUpdates++;
                        }

                        trans.Commit(); // Semua berhasil, simpan
                        MessageBox.Show($"Pengembalian {successfulUpdates} barang berhasil diproses.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getData(); // Refresh Grid View

                        // Setelah semua selesai, mungkin lu mau cek apakah pinjaman ini sudah lunas?
                        // (Tidak wajib, tapi bagus buat sistem yang lebih rapi)

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback(); // Batalkan semua kalau ada error
                        MessageBox.Show("Gagal memproses pengembalian: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
            // Kalau perlu buka form menu siswa, tambahin kodingannya di sini
        }

        private void NamaPenerima_TextChanged(object sender, EventArgs e)
        {

        }
    }
}