using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Drawing.Text;

namespace Lab_DKV
{
    public partial class hlm_DataUser : Form
    {
        public hlm_DataUser()
        {
            InitializeComponent();
        }

        private void halamanAdmin_Load(object sender, EventArgs e)
        {
            LoadDataUser();
        }

        public void LoadDataUser()
        {
            try
            {
                using (MySqlConnection conn = DB.GetConnection()) //unuk mengakses Database
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbl_user", conn))
                    {
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void BtnBarang_Click(object sender, EventArgs e)
        {
            hlm_barang barang = new hlm_barang();
            barang.Show(); //buka halaman barang
            this.Hide();//sembunikan halaman user 
        }


        private void btnDataPeminjam(object sender, EventArgs e)
        {
            tbl_peminjaman pinjam = new tbl_peminjaman();
            pinjam.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            hlm_login login = new hlm_login();
            login.Show();
            this.Close();
        }

        private void btnDataUser_Click(object sender, EventArgs e)
        {

        }

        private void btn_keluar_Click(object sender, EventArgs e)
        {
            hlm_login keluar = new hlm_login();
            keluar.Show();
            this.Hide();
        }

        private void btn_EditData_Click(object sender, EventArgs e)
        {
            hlm_DataUser userForm = new hlm_DataUser();
            userForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
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

                LoadDataUser(); // refresh tabel
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void btn_Reload_Click(object sender, EventArgs e)
        {
            LoadDataUser();
        }

        private void btn_ImportData_Click(object sender, EventArgs e)
        {

        }

        private void btn_Tambah_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    String query = @"INSERT INTO tbl_user (username, nis, angkatan, role) 
                                    VALUES (@username,@nis, @angkatan, @role)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", txt_username.Text);
                        cmd.Parameters.AddWithValue("@nis", txt_nis.Text);
                        cmd.Parameters.AddWithValue("@angkatan", txt_angkatan.Text);
                        cmd.Parameters.AddWithValue("@role", txt_role.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("data User berhasil ditambahkan!");
                LoadDataUser();//refresh data table

                txt_username.Text = "";
                txt_nis.Text = "";
                txt_angkatan.Text = "";
                txt_role.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txt_username.Text = row.Cells["username"].Value.ToString();
                txt_nis.Text = row.Cells["nis"].Value.ToString();
                txt_angkatan.Text = row.Cells["angkatan"].Value.ToString();
                txt_role.Text = row.Cells["role"].Value.ToString();
                txt_id.Text = row.Cells["id_user"].Value.ToString();
            }
        }

        private void username_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_nis_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_role_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_id_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Hapus_Click_1(object sender, EventArgs e)
        {
            if (txt_id.Text == "")
            {
                MessageBox.Show("Pilih data terlebih dahulu!");
                return;
            }

            string query = "DELETE FROM tbl_user WHERE id_user=@id_user";

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_user", txt_id.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Data berhasil dihapus!");

                LoadDataUser(); // Refresh tabel
                txt_id.Clear();
                txt_username.Clear();
                txt_nis.Clear();
                txt_angkatan.Clear();
                txt_role.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void id_TextChanged(object sender, EventArgs e)
        {

        }
    }
} 

