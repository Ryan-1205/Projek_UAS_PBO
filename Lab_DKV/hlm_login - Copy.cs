using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class hlm_login: Form
    {
        // Connection string ke database
        string conString = "server=localhost;uid=root;pwd=;database=lab_dkv;";
        MySqlConnection conn;

        public hlm_login()
        {
            InitializeComponent();
            conn = new MySqlConnection(conString); // inisialisasi koneksi
        }

        private void hlm_login_Load(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtNis.Text.Trim();

            if (user == "" || pass == "")
            {
                MessageBox.Show("Username dan NIS harus diisi.");
                return;
            }

            try
            {
                conn.Open();

                string query = "SELECT username, role FROM tbl_user WHERE username=@user AND nis=@nis LIMIT 1";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@nis", pass);

                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    string role = dr["role"].ToString();



                    this.Hide();

                    if (role == "admin")
                    {
                        hlm_admin adminPage = new hlm_admin();
                        MessageBox.Show("Login sebagai admin berhasil!");
                        adminPage.Show();
                    }
                    else if (role == "siswa")
                    {
                        hlm_siswa siswaPage = new hlm_siswa();
                        MessageBox.Show("Login berhasil!");
                        siswaPage.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Username atau NIS salah!");
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

       

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            this.Hide();
            hlm_register registerPage = new hlm_register();
            registerPage.Show();
        }


        private async void ShowNisWarning()
        {
            lblNisWarning.Text = "NIM berupa angka";
            lblNisWarning.Visible = true;

            await Task.Delay(1200);
            lblNisWarning.Visible = false;
        }


        bool showingPassword = false;

        private void btnView_Click(object sender, EventArgs e)
        {
            if (showingPassword)
            {
                // mode sembunyikan
                txtNis.PasswordChar = '*';
                showingPassword = false;
                btnView.BackgroundImage = Properties.Resources.notview;
            }
            else
            {
                // mode tampilkan
                txtNis.PasswordChar = '\0';
                showingPassword = true;
                btnView.BackgroundImage = Properties.Resources.view;
            }

            // jaga caret tetap di akhir
            txtNis.SelectionStart = txtNis.Text.Length;
        }
    }
}
