using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class hlm_login : Form
    {
        public hlm_login()
        {
            InitializeComponent();
        }

        private void hlm_login_Load(object sender, EventArgs e) { }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string nis = txtNis.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(nis))
            {
                MessageBox.Show("Username dan NIS harus diisi.");
                return;
            }

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    // ambil id_user juga
                    string query = "SELECT id_user, username, role FROM tbl_user WHERE username=@user AND nis=@nis LIMIT 1";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@nis", nis);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            // ... (kode koneksi database sebelumnya)
                            if (dr.Read())
                            {
                                // 1. Ambil data dari database dengan aman
                                int idUser = dr["id_user"] == DBNull.Value ? 0 : Convert.ToInt32(dr["id_user"]);
                                string username = dr["username"]?.ToString() ?? string.Empty;
                                string role = dr["role"]?.ToString() ?? string.Empty;

                                // 2. DEBUGGING: Cek apakah ID tertangkap (Boleh dihapus nanti)
                                if (idUser == 0)
                                {
                                    MessageBox.Show("Login berhasil tapi ID User 0. Cek Database tbl_user!");
                                    return;
                                }

                                // 3. SIMPAN KE SESSION (PENTING)
                                Session.UserId = idUser;
                                Session.UserName = username;
                                Session.Role = role;

                                MessageBox.Show($"Login Berhasil! ID: {idUser}", "Info"); // Debug info

                                this.Hide();

                                
                                // 4. Buka form sesuai role
                                if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                                {
                                    /*
                                        // Pastikan hlm_admin punya konstruktor yang menerima ID juga
                                    var adminPage = new hlm_admin(idUser, username);
                                    adminPage.Show();
                                    */
                                }
                                else // siswa
                                {
                                    var siswaPage = new hlm_siswa(idUser, username);
                                    siswaPage.Show();
                                }
                            }
                            // ... (sisa kode error handling)
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            this.Hide();
            var registerPage = new hlm_register();
            registerPage.Show();
        }

        // toggle view password
        bool showingPassword = false;
        private void btnView_Click(object sender, EventArgs e)
        {
            showingPassword = !showingPassword;
            txtNis.PasswordChar = showingPassword ? '\0' : '*';
            btnView.BackgroundImage = showingPassword ? Properties.Resources.view : Properties.Resources.notview;
            txtNis.SelectionStart = txtNis.Text.Length;
        }
    }
}
