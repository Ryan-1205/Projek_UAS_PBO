using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Lab_DKV
{
    public partial class hlm_register : Form
    {
        public hlm_register()
        {
            InitializeComponent();                    
        }

        private void hlm_register_Load(object sender, EventArgs e)
        {
                
        }


        private void btn_Register_Click(object sender, EventArgs e)
        {
            
        }

        private void txtUsernameReg_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnReg_Click(object sender, EventArgs e)
        {

        }

        private void btndaftar_Click(object sender, EventArgs e)
        {
            string username = txtUsernameReg.Text.Trim();
            string nis = txtNisReg.Text.Trim();
            string role = "siswa"; // otomatis

            // Validasi Input
            if (username == "" || nis == "")
            {
                MessageBox.Show("Username dan NIS harus diisi!", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Pakai koneksi dari DB.cs
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    // 1. Cek apakah username sudah terdaftar
                    string checkQuery = "SELECT COUNT(*) FROM tbl_user WHERE username = @username";
                    using (MySqlCommand cmd = new MySqlCommand(checkQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Username sudah digunakan!",
                                "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // 2. Insert data baru
                    string insertQuery = "INSERT INTO tbl_user (username, nis, role) VALUES (@username, @nis, @role)";
                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@nis", nis);
                        cmd.Parameters.AddWithValue("@role", role);

                        cmd.ExecuteNonQuery();
                    }

                    // Clear input
                    txtUsernameReg.Text = "";
                    txtNisReg.Text = "";

                    MessageBox.Show("Register berhasil!", "Sukses",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    hlm_login loginPage = new hlm_login();
                    loginPage.Show();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi error: " + ex.Message);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            hlm_login registerPage = new hlm_login();
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
                txtNisReg.PasswordChar = '*';
                showingPassword = false;
                btnView.BackgroundImage = Properties.Resources.notview;
            }
            else
            {
                // mode tampilkan
                txtNisReg.PasswordChar = '\0';
                showingPassword = true;
                btnView.BackgroundImage = Properties.Resources.view;
            }

            // jaga caret tetap di akhir
            txtNisReg.SelectionStart = txtNisReg.Text.Length;
        }

        private void txtNisReg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                ShowNisWarning();
            }
        }

        private void lblNisWarning_Click(object sender, EventArgs e)
        {

        }
    }
}
