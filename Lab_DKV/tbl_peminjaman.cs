using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class tbl_peminjaman : Form
    {
        public tbl_peminjaman()
        {
            InitializeComponent();
        }
        private void tbl_peminjaman_Load(object sender, EventArgs e)
        {
            LoadDataPeminjaman();
            SetPlaceholder();
        }

        public void LoadDataPeminjaman()
        {
            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    //p adalah tbl_peminjaman, u untuk tbl_user dan user_name untuk kolom u.useername
                    string query = @"
                            SELECT
                                p.no_pb, 
                                p.tgl_pinjam,
                                u.username AS user_name, -- Mengambil nama user dari tbl_user
                                p.nama_petugaspeminjam,
                                p.tujuan,
                                p.id_user  -- Tetap sertakan id_user untuk keperluan Edit/Update (opsional, tapi disarankan)
                             FROM
                                  tbl_peminjaman p
                            LEFT JOIN
                                 tbl_user u ON p.id_user = u.id_user";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;

                        if (dataGridView1.Columns.Contains("user_name"))
                        {
                            dataGridView1.Columns["user_name"].HeaderText = "Nama User"; // Ubah header kolom
                        }
                        if (dataGridView1.Columns.Contains("id_user"))
                        {
                            dataGridView1.Columns["id_user"].Visible = false; // Sembunyikan kolom id_user
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btn_DataUser_Click(object sender, EventArgs e)
        {
            hlm_DataUser userPage = new hlm_DataUser();
            userPage.Show();
            this.Hide();

        }

        private void btn_DataBarang_Click(object sender, EventArgs e)
        {
            hlm_barang barang = new hlm_barang();
            barang.Show(); //buka halaman barang
            this.Hide();//sembunikan halaman user 
        }

        private void btn_DataPeminjam_Click(object sender, EventArgs e)
        {

        }

        private void btn_keluar_Click(object sender, EventArgs e)
        {
            hlm_login login = new hlm_login();
            login.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            LoadDataPeminjaman();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txt_no_pb.Text = row.Cells["no_pb"].Value.ToString();
                txt_tgl_pinjam.Text = row.Cells["tgl_pinjam"].Value.ToString();
                txt_id_user.Text = row.Cells["user_name"].Value.ToString();
                txt_nama_petugaspeminjam.Text = row.Cells["nama_petugaspeminjam"].Value.ToString();
                txt_tujuan.Text = row.Cells["tujuan"].Value.ToString();

                RemovePlaceholder();
            }
        }

        private void txt_no_pb_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_tgl_pinjam_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_id_user_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_nama_petugaspeminjam_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_tujuan_TextChanged(object sender, EventArgs e)
        {

        }
        private void btn_tambah_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    string query = @"INSERT INTO tbl_peminjaman 
                        (no_pb, tgl_pinjam, id_user, nama_petugaspeminjam, tujuan) 
                        VALUES (@no_pb, @tgl_pinjam, @id_user, @nama_petugaspeminjam, @tujuan)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@no_pb", txt_no_pb.Text);
                        cmd.Parameters.AddWithValue("@tgl_pinjam", txt_tgl_pinjam.Text);
                        cmd.Parameters.AddWithValue("@id_user", txt_id_user.Text);
                        cmd.Parameters.AddWithValue("@nama_petugaspeminjam", txt_nama_petugaspeminjam.Text);
                        cmd.Parameters.AddWithValue("@tujuan", txt_tujuan.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data berhasil ditambahkan!");
                SetPlaceholder();
                LoadDataPeminjaman();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }
        private void btn_hapus_Click(object sender, EventArgs e)
        {
            if (txt_no_pb.Text == "")
            {
                MessageBox.Show("Pilih data terlebih dahulu!");
                return;
            }

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    string query = "DELETE FROM tbl_peminjaman WHERE no_pb=@no_pb";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@no_pb", txt_no_pb.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data berhasil dihapus!");
                LoadDataPeminjaman();
                ClearInput();
                SetPlaceholder();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }
        private void ResetPlaceholder()
        {
            txt_no_pb.Text = "no_pb";
            txt_no_pb.ForeColor = Color.Gray;

            txt_tgl_pinjam.Text = "tgl_pinjam";
            txt_tgl_pinjam.ForeColor = Color.Gray;

            txt_id_user.Text = "id_user";
            txt_id_user.ForeColor = Color.Gray;

            txt_nama_petugaspeminjam.Text = "nama_petugaspeminjam";
            txt_nama_petugaspeminjam.ForeColor = Color.Gray;

            txt_tujuan.Text = "tujuan";
            txt_tujuan.ForeColor = Color.Gray;
        }
        private void ClearInput()
        {
            txt_no_pb.Clear();
            txt_tgl_pinjam.Clear();
            txt_id_user.Clear();
            txt_nama_petugaspeminjam.Clear();
            txt_tujuan.Clear();
        }

        private void btn_edit_data_Click(object sender, EventArgs e)
        {
            if (txt_no_pb.Text == "")
            {
                MessageBox.Show("Pilih data dulu!");
                return;
            }

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    string query = @"UPDATE tbl_peminjaman SET 
                        tgl_pinjam=@tgl_pinjam, 
                        id_user=@id_user, 
                        nama_petugaspeminjam=@nama_petugaspeminjam,
                        tujuan=@tujuan
                        WHERE no_pb=@no_pb";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@no_pb", txt_no_pb.Text);
                        cmd.Parameters.AddWithValue("@tgl_pinjam", txt_tgl_pinjam.Text);
                        cmd.Parameters.AddWithValue("@id_user", txt_id_user.Text);
                        cmd.Parameters.AddWithValue("@nama_petugaspeminjam", txt_nama_petugaspeminjam.Text);
                        cmd.Parameters.AddWithValue("@tujuan", txt_tujuan.Text);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data berhasil diupdate!");
                LoadDataPeminjaman();
                ClearInput();
                SetPlaceholder();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            LoadDataPeminjaman();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {

        }

        private void SetPlaceholder()
        {
            SetPH(txt_no_pb, "No PB");
            SetPH(txt_tgl_pinjam, "Tanggal Pinjam");
            SetPH(txt_id_user, "ID User");
            SetPH(txt_nama_petugaspeminjam, "Nama Petugas");
            SetPH(txt_tujuan, "Tujuan");
        }
        private void RemovePlaceholder()
        {
            txt_no_pb.ForeColor = Color.Gray;
            txt_tgl_pinjam.ForeColor = Color.Gray;
            txt_id_user.ForeColor = Color.Gray;
            txt_nama_petugaspeminjam.ForeColor = Color.Gray;
            txt_tujuan.ForeColor = Color.Gray;
        }
        private void SetPH(TextBox txt, string text)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Text = text;
                txt.ForeColor = Color.Gray;
            }
        }

        private void dataG(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
         
         
        }
    }
}
