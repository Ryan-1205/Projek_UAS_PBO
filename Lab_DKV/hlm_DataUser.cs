using System;
using System.Collections.Generic; // Penting untuk List<>
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Lab_DKV
{
    public partial class hlm_DataUser : Form
    {
        // 1. Variabel Global untuk menampung data (agar pencarian lancar)
        private DataTable dtUser = new DataTable();

        public hlm_DataUser()
        {
            InitializeComponent();
        }

        private void halamanAdmin_Load(object sender, EventArgs e)
        {
            LoadDataUser();
            ClearInput();
        }

        // ==========================================
        // 2. FUNGSI LOAD DATA
        // ==========================================
        public void LoadDataUser()
        {
            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    // Ambil semua kolom
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_user", conn))
                    {
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            dtUser = new DataTable(); // Reset DataTable
                            da.Fill(dtUser);          // Isi data
                            dataGridView1.DataSource = dtUser; // Tampilkan ke Grid

                            // Sembunyikan kolom ID agar rapi
                            if (dataGridView1.Columns["id_user"] != null)
                                dataGridView1.Columns["id_user"].Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Load Data: " + ex.Message);
            }
        }

        // ==========================================
        // 3. FITUR PENCARIAN (CARI)
        // ==========================================
        // Hubungkan tombol "Cari" Anda ke event ini (misal button1_Click)
        private void btn_Cari_Click(object sender, EventArgs e)
        {
            string user = txt_username.Text.Trim();
            string nis = txt_nis.Text.Trim();
            string angkatan = txt_angkatan.Text.Trim();

            // List untuk menampung filter
            List<string> filters = new List<string>();

            // Cek satu per satu, jika textbox terisi, tambahkan ke filter
            if (!string.IsNullOrEmpty(user))
                filters.Add(string.Format("username LIKE '%{0}%'", user));

            if (!string.IsNullOrEmpty(nis))
                filters.Add(string.Format("nis LIKE '%{0}%'", nis));

            if (!string.IsNullOrEmpty(angkatan))
                filters.Add(string.Format("angkatan LIKE '%{0}%'", angkatan));

            // Gabungkan filter dengan logika "AND" (User harus memenuhi semua kriteria yg diketik)
            // Jika ingin "OR" (salah satu cocok), ganti " AND " menjadi " OR "
            if (filters.Count > 0)
            {
                dtUser.DefaultView.RowFilter = string.Join(" AND ", filters);
            }
            else
            {
                // Jika semua kosong, reset filter (tampilkan semua)
                dtUser.DefaultView.RowFilter = "";
                MessageBox.Show("Menampilkan semua data (Input pencarian kosong).");
            }
        }

        // ==========================================
        // 4. CRUD (TAMBAH, UPDATE, HAPUS)
        // ==========================================

        private void btn_Tambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_username.Text) || string.IsNullOrWhiteSpace(txt_nis.Text))
            {
                MessageBox.Show("Username dan NIS wajib diisi!");
                return;
            }

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO tbl_user (username, nis, angkatan, role) 
                                     VALUES (@username, @nis, @angkatan, @role)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txt_username.Text);
                        cmd.Parameters.AddWithValue("@nis", txt_nis.Text);
                        cmd.Parameters.AddWithValue("@angkatan", txt_angkatan.Text);
                        // Default role jika kosong adalah 'siswa'
                        string role = string.IsNullOrWhiteSpace(txt_role.Text) ? "siswa" : txt_role.Text;
                        cmd.Parameters.AddWithValue("@role", role);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Data User berhasil ditambahkan!");
                LoadDataUser();
                ClearInput();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_id.Text))
            {
                MessageBox.Show("Pilih data user terlebih dahulu dari tabel!");
                return;
            }

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE tbl_user 
                                     SET username=@username, nis=@nis, angkatan=@angkatan, role=@role 
                                     WHERE id_user=@id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txt_username.Text);
                        cmd.Parameters.AddWithValue("@nis", txt_nis.Text);
                        cmd.Parameters.AddWithValue("@angkatan", txt_angkatan.Text);
                        cmd.Parameters.AddWithValue("@role", txt_role.Text);
                        cmd.Parameters.AddWithValue("@id", txt_id.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Data berhasil diperbarui!");
                LoadDataUser();
                ClearInput();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }

        private void btn_Hapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_id.Text))
            {
                MessageBox.Show("Pilih data terlebih dahulu!");
                return;
            }

            if (MessageBox.Show("Yakin hapus user ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = DB.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM tbl_user WHERE id_user=@id_user";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id_user", txt_id.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Data berhasil dihapus!");
                    LoadDataUser();
                    ClearInput();
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        // ==========================================
        // 5. EVENT HANDLER & NAVIGASI
        // ==========================================

        private void btn_Reload_Click(object sender, EventArgs e)
        {
            dtUser.DefaultView.RowFilter = ""; // Reset filter
            LoadDataUser();
            ClearInput();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Redirect ke CellClick agar klik di area kosong sel tetap terbaca
            DataGridView1_CellClick(sender, e);
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txt_id.Text = row.Cells["id_user"].Value.ToString();
                txt_username.Text = row.Cells["username"].Value.ToString();
                txt_nis.Text = row.Cells["nis"].Value.ToString();
                txt_angkatan.Text = row.Cells["angkatan"].Value.ToString();
                txt_role.Text = row.Cells["role"].Value.ToString();
            }
        }

        private void ClearInput()
        {
            txt_id.Clear(); txt_username.Clear(); txt_nis.Clear();
            txt_angkatan.Clear(); txt_role.Clear();
        }

        // --- Navigasi Menu ---
        private void BtnBarang_Click(object sender, EventArgs e) { hlm_barang p = new hlm_barang(); p.Show(); this.Hide(); }
        private void btnDataPeminjam(object sender, EventArgs e) { tbl_peminjaman p = new tbl_peminjaman(); p.Show(); this.Hide(); }
        private void btn_keluar_Click(object sender, EventArgs e) { hlm_login p = new hlm_login(); p.Show(); this.Close(); }

        // --- Mapping Tombol ---
        private void button1_Click(object sender, EventArgs e) => btn_Cari_Click(sender, e); // Asumsi button1 adalah tombol Cari
        private void btnLogout_Click(object sender, EventArgs e) => btn_keluar_Click(sender, e);
        private void btnDataUser_Click(object sender, EventArgs e) => btn_Reload_Click(sender, e);

        // --- Placeholder Kosong ---
        private void btn_EditData_Click(object sender, EventArgs e) { }
        private void txt_id_TextChanged(object sender, EventArgs e) { }
        private void username_TextChanged(object sender, EventArgs e) { }
        private void txt_nis_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void txt_role_TextChanged(object sender, EventArgs e) { }
    }
}