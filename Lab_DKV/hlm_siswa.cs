using System;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class hlm_siswa : Form
    {
        private int loggedUserId;
        private string loggedUserName;

        // Konstruktor default buat Designer
        public hlm_siswa()
        {
            InitializeComponent();
        }

        // Konstruktor yang dipakai waktu membuka form dari login
        public hlm_siswa(int userId, string userName) : this()
        {
            loggedUserId = userId;
            loggedUserName = userName ?? string.Empty;
        }

        private void hlm_siswa_Load(object sender, EventArgs e)
        {
            // Tampilkan nama user di label (Pastikan label lblWelcome ada di Designer)
            var lbls = this.Controls.Find("lblWelcome", true);
            if (lbls.Length > 0 && lbls[0] is Label lblWelcome)
            {
                lblWelcome.Text = $"Halo, {loggedUserName}";
            }
        }

        // ==========================================
        // 1. TOMBOL PINJAM BARANG
        // ==========================================
        private void btnPinjamBarang_Click(object sender, EventArgs e)
        {
            if (!EnsureUserValid()) return;

            this.Hide(); // Sembunyikan dashboard siswa

            // Buka halaman peminjaman dengan membawa ID user
            var frmPeminjaman = new hlm_peminjaman(loggedUserId, loggedUserName);
            frmPeminjaman.Show();
        }

        // ==========================================
        // 2. TOMBOL KEMBALIKAN BARANG
        // ==========================================
        private void btnKembalikanBarang_Click(object sender, EventArgs e)
        {
            if (!EnsureUserValid()) return;

            try
            {
                this.Hide(); // Sembunyikan dashboard siswa

                // Asumsi: hlm_pengembalian memiliki konstruktor yang sama (int userId, string userName)
                // Jika error merah disini, pastikan Anda sudah membuat form hlm_pengembalian
                var frmPengembalian = new hlm_pengembalian(loggedUserId, loggedUserName);
                frmPengembalian.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka halaman pengembalian: " + ex.Message);
                this.Show(); // Munculkan lagi dashboard jika error
            }
        }

        // ==========================================
        // 3. TOMBOL KELUAR / LOGOUT
        // ==========================================
        // Pastikan Anda sudah double-click tombol btnKeluar di Designer untuk membuat event ini
        private void btnKeluar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Apakah Anda yakin ingin keluar dari aplikasi?",
                "Konfirmasi Keluar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dr == DialogResult.Yes)
            {
                // 1. Bersihkan Sesi Global
                Session.UserId = 0;
                Session.UserName = "";
                Session.Role = "";

                // 2. Sembunyikan Form Siswa
                this.Hide();

                // 3. Buka Form Login
                var frmLogin = new hlm_login();
                frmLogin.Show();
            }
        }

        // ==========================================
        // HELPER FUNCTIONS
        // ==========================================

        // Public helper untuk setting user manual jika diperlukan
        public void SetLoggedUser(int userId, string userName)
        {
            loggedUserId = userId;
            loggedUserName = userName ?? string.Empty;
        }

        // Validasi apakah user ID valid
        private bool EnsureUserValid()
        {
            if (loggedUserId <= 0)
            {
                MessageBox.Show("User tidak terdeteksi. Silakan login ulang.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Event handler lama (btnPengembalian) bisa dihapus jika sudah digantikan btnKembalikanBarang
        private void btnPengembalian_Click(object sender, EventArgs e)
        {
            // Alihkan logic ke tombol yang baru jika nama tombol di designer berbeda
            btnKembalikanBarang_Click(sender, e);
        }
    }
}