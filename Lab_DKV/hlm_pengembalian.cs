using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class hlm_pengembalian : Form
    {
        private int _currentUserId;
        private string _currentUserName;

        public hlm_pengembalian()
        {
            InitializeComponent();
        }

        public hlm_pengembalian(int userId, string userName) : this()
        {
            SetCurrentUser(userId, userName);
        }

        public void SetCurrentUser(int userId, string userName)
        {
            _currentUserId = userId;
            _currentUserName = userName;
        }

        private void hlm_pengembalian_Load(object sender, EventArgs e)
        {
            if (_currentUserId <= 0)
            {
                if (Session.UserId > 0)
                {
                    _currentUserId = Session.UserId;
                    _currentUserName = Session.UserName;
                }
                else
                {
                    MessageBox.Show("User tidak terdeteksi. Silakan login ulang.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }
            }
            getData();
        }

        private void buttonGetData_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            using (MySqlConnection conn = DB.GetConnection())
            {
                try
                {
                    conn.Open();
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
                        WHERE P.id_user = @uid AND (D.status_kembali IS NULL OR D.status_kembali = 0)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@uid", _currentUserId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Columns["chkBox"] == null)
                    {
                        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                        chk.HeaderText = "Pilih";
                        chk.Name = "chkBox";
                        dataGridView1.Columns.Insert(0, chk);
                    }

                    if (dataGridView1.Columns.Contains("id_detailpb")) dataGridView1.Columns["id_detailpb"].Visible = false;
                    if (dataGridView1.Columns.Contains("id_peminjaman")) dataGridView1.Columns["id_peminjaman"].Visible = false;
                    if (dataGridView1.Columns.Contains("nama_barang")) dataGridView1.Columns["nama_barang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error mengambil data: " + ex.Message, "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Bt_Pilih_Semua_Click(object sender, EventArgs e)
        {
            bool checkStatus = true;
            int checkedCount = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["chkBox"].Value != null && (bool)row.Cells["chkBox"].Value == true)
                {
                    checkedCount++;
                }
            }

            if (checkedCount == dataGridView1.Rows.Count) checkStatus = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells["chkBox"].Value = checkStatus;
            }
        }

        private void Kirim_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NamaPenerima.Text))
            {
                MessageBox.Show("Nama Petugas Penerima wajib diisi.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                NamaPenerima.Focus();
                return;
            }

            List<string> detailIdList = new List<string>();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                object val = row.Cells["chkBox"].Value;
                if (val != null && (bool)val == true)
                {
                    if (row.Cells["id_detailpb"].Value != null)
                    {
                        detailIdList.Add(row.Cells["id_detailpb"].Value.ToString());
                    }
                }
            }

            if (detailIdList.Count == 0)
            {
                MessageBox.Show("Pilih minimal satu barang yang akan dikembalikan.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Kembalikan {detailIdList.Count} barang terpilih?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            using (MySqlConnection conn = DB.GetConnection())
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        string namaPetugas = NamaPenerima.Text.Trim();
                        string tglKembali = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        int suksesCount = 0;

                        foreach (string idDetail in detailIdList)
                        {
                            string qGet = "SELECT id_barang, unit, id_peminjaman FROM tbl_detailpb WHERE id_detailpb = @id";
                            int idBarang = 0;
                            int unit = 0;
                            int idPeminjaman = 0;

                            using (MySqlCommand cmdGet = new MySqlCommand(qGet, conn, trans))
                            {
                                cmdGet.Parameters.AddWithValue("@id", idDetail);
                                using (MySqlDataReader dr = cmdGet.ExecuteReader())
                                {
                                    if (dr.Read())
                                    {
                                        idBarang = Convert.ToInt32(dr["id_barang"]);
                                        unit = Convert.ToInt32(dr["unit"]);
                                        idPeminjaman = Convert.ToInt32(dr["id_peminjaman"]);
                                    }
                                }
                            }

                            if (idBarang > 0)
                            {
                                string qUpdateDetail = "UPDATE tbl_detailpb SET status_kembali = 1 WHERE id_detailpb = @id";
                                using (MySqlCommand cmdUpd = new MySqlCommand(qUpdateDetail, conn, trans))
                                {
                                    cmdUpd.Parameters.AddWithValue("@id", idDetail);
                                    cmdUpd.ExecuteNonQuery();
                                }

                                string qStok = "UPDATE tbl_barang SET jumlah_barang = jumlah_barang + @u WHERE id_barang = @idb";
                                using (MySqlCommand cmdStok = new MySqlCommand(qStok, conn, trans))
                                {
                                    cmdStok.Parameters.AddWithValue("@u", unit);
                                    cmdStok.Parameters.AddWithValue("@idb", idBarang);
                                    cmdStok.ExecuteNonQuery();
                                }

                                string qLog = @"INSERT INTO tbl_pengembalian (id_peminjaman, tgl_kembali, nama_petugaskembali, catatan) 
                                                VALUES (@idp, @tgl, @petugas, @cat)";
                                using (MySqlCommand cmdLog = new MySqlCommand(qLog, conn, trans))
                                {
                                    cmdLog.Parameters.AddWithValue("@idp", idPeminjaman);
                                    cmdLog.Parameters.AddWithValue("@tgl", tglKembali);
                                    cmdLog.Parameters.AddWithValue("@petugas", namaPetugas);
                                    cmdLog.Parameters.AddWithValue("@cat", $"Mengembalikan detail ID: {idDetail}");
                                    cmdLog.ExecuteNonQuery();
                                }

                                suksesCount++;
                            }
                        }

                        trans.Commit();

                        // --- MODIFIKASI: POPUP KONFIRMASI KELUAR ---
                        DialogResult result = MessageBox.Show(
                            $"Berhasil mengembalikan {suksesCount} barang.\n\nApakah Anda ingin keluar dari aplikasi dan kembali ke Login?",
                            "Pengembalian Sukses",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question
                        );

                        if (result == DialogResult.Yes)
                        {
                            // Logout & ke Login
                            Session.Clear(); // Bersihkan sesi (jika ada class Session)
                            hlm_login loginPage = new hlm_login();
                            loginPage.Show();
                            this.Close();
                        }
                        else
                        {
                            // Tetap di halaman ini, refresh data
                            getData();
                            NamaPenerima.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show("Gagal memproses pengembalian: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnKembali_Click(object sender, EventArgs e)
        {
            hlm_siswa frmSiswa = new hlm_siswa(_currentUserId, _currentUserName);
            frmSiswa.Show();
            this.Close();
        }

        // Placeholder Handlers
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox1_NamaPenerima_TextChanged(object sender, EventArgs e) { }
        private void Tanggal_ValueChanged(object sender, EventArgs e) { }
        private void NamaPenerima_TextChanged(object sender, EventArgs e) { }
    }
}