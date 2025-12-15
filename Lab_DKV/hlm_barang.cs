using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Lab_DKV
{
    public partial class hlm_barang : Form
    {
        public hlm_barang()
        {
            InitializeComponent();
        }
        private void hlm_barang_Load(object sender, EventArgs e)
        {
            LoadDataBarang();
        }

        public void LoadDataBarang()
        {
            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM tbl_barang";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btn_databarangClick(object sender, EventArgs e)
        {

        }

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

        private void btn_tambah_Click(object sender, EventArgs e)
        {
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
                ResetPlaceholder();
                LoadDataBarang();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void btn_edit_data_Click(object sender, EventArgs e)
        {

        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            LoadDataBarang() ;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text == "")
            {
                MessageBox.Show("Pilih data dulu!");
                return;
            }

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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void txt_kode_barang_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_nama_barang_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_merk_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_kondisi_barang_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_unit_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadDataBarang();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txt_kode_barang.Text = row.Cells["kode_barang"].Value.ToString();
                txt_nama_barang.Text = row.Cells["nama_barang"].Value.ToString();
                txt_merk.Text = row.Cells["merek"].Value.ToString();
                txt_kondisi_barang.Text = row.Cells["kondisi_barang"].Value.ToString();
                txt_jumlah_barang.Text = row.Cells["jumlah_barang"].Value.ToString();
            }
        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            if (txt_kode_barang.Text == "")
            {
                MessageBox.Show("Pilih data terlebih dahulu!");
                return;
            }

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
                LoadDataBarang();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void ClearInput()
        {
            txt_kode_barang.Clear();
            txt_nama_barang.Clear();
            txt_merk.Clear();
            txt_kondisi_barang.Clear();
            txt_jumlah_barang.Clear();
        }

        private void hlm_barang_Load_1(object sender, EventArgs e) //tabel dari db agar muncul
        {
            LoadDataBarang();
        }

        private void ResetPlaceholder()
        {
            txt_kode_barang.Text = "Kode Barang";
            txt_kode_barang.ForeColor = Color.Gray;

            txt_nama_barang.Text = "Nama Barang";
            txt_nama_barang.ForeColor = Color.Gray;

            txt_merk.Text = "Merek";
            txt_merk.ForeColor = Color.Gray;

            txt_kondisi_barang.Text = "Kondisi Barang";
            txt_kondisi_barang.ForeColor = Color.Gray;

            txt_jumlah_barang.Text = "jumlah_barang";
            txt_jumlah_barang.ForeColor = Color.Gray;
        }

    }
}
