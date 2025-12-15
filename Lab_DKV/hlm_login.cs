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

                    // Ambil id_user, username, dan role
                    string query = "SELECT id_user, username, role FROM tbl_user WHERE username=@user AND nis=@nis LIMIT 1";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@nis", nis);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                // 1. Ambil data dari database dengan aman
                                int idUser = dr["id_user"] == DBNull.Value ? 0 : Convert.ToInt32(dr["id_user"]);
                                string username = dr["username"]?.ToString() ?? string.Empty;
                                string role = dr["role"]?.ToString() ?? string.Empty;

                                // 2. SIMPAN KE SESSION (PENTING)
                                Session.UserId = idUser;
                                Session.UserName = username;
                                Session.Role = role;

                                // --- TAMBAHAN: MULAI AUTO LOGOUT (Jika Class SessionWatcher sudah ada) ---
                                if (Program.InactivityTimer != null)
                                    Program.InactivityTimer.Start();
                                // ------------------------------------------------------------------------

                                MessageBox.Show($"Login Berhasil! Selamat datang, {username}.", "Info");
                                this.Hide();

                                // 3. Buka form sesuai role
                                if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                                {
                                    // === PERUBAHAN DI SINI ===
                                    // Langsung ke hlm_DataUser, tidak pakai hlm_admin lagi
                                    var frmDataUser = new hlm_DataUser();
                                    frmDataUser.Show();
                                }
                                else // siswa
                                {
                                    var siswaPage = new hlm_siswa(idUser, username);
                                    siswaPage.Show();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Username atau NIS salah/tidak ditemukan.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            this.Hide();
            var registerPage = new hlm_register();
            registerPage.Show();
        }

        // Toggle view password
        bool showingPassword = false;
        private void btnView_Click(object sender, EventArgs e)
        {
            showingPassword = !showingPassword;
            // Jika showingPassword true, karakter null (kelihatan). Jika false, karakter '*' (tersembunyi)
            txtNis.PasswordChar = showingPassword ? '\0' : '*';

            // Ganti icon jika ada resources, jika tidak, kode di bawah bisa di-skip atau disesuaikan
            if (Properties.Resources.view != null && Properties.Resources.notview != null)
            {
                btnView.BackgroundImage = showingPassword ? Properties.Resources.view : Properties.Resources.notview;
            }

            // Kembalikan kursor ke akhir teks
            txtNis.SelectionStart = txtNis.Text.Length;
        }
    }
}