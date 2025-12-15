using System;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class hlm_siswa : Form
    {
        private int loggedUserId;
        private string loggedUserName;

        // konstruktor default buat Designer
        public hlm_siswa()
        {
            InitializeComponent();
        }

        // konstruktor yang dipakai waktu membuka form dari login
        public hlm_siswa(int userId, string userName) : this()
        {
            loggedUserId = userId;
            loggedUserName = userName ?? string.Empty;
        }

        private void hlm_siswa_Load(object sender, EventArgs e)
        {
            // tampilkan nama user jika ada label bernama lblWelcome (opsional)
            var lbls = this.Controls.Find("lblWelcome", true);
            if (lbls.Length > 0 && lbls[0] is Label lblWelcome)
            {
                lblWelcome.Text = $"Halo, {loggedUserName}";
            }

            // normal: jangan otomatis buka form lain di Load.
            // Biarkan user memilih menu / tombol.
        }

        // Event handler tombol/ menu: buka halaman peminjaman
        private void btnPinjamBarang_Click(object sender, EventArgs e)
        {
            if (loggedUserId <= 0) { MessageBox.Show("User tidak terdeteksi."); return; }
            var frm = new hlm_peminjaman(loggedUserId, loggedUserName); // <- teruskan id
            frm.Show();
        }

        // Jika ada menu pengembalian, contoh handler:
        private void btnPengembalian_Click(object sender, EventArgs e)
        {
            if (!EnsureUserValid()) return;

            // asumsikan kamu juga punya hlm_pengembalian(int userId, string userName)
            // jika belum, kamu bisa panggil konstruktor tanpa parameter lalu SetCurrentUser
            try
            {
              /*
                var frmPengembalian = new hlm_pengembalian(loggedUserId, loggedUserName);
                frmPengembalian.Show();
              */
            }
            catch
            {
                // fallback: kalau kelas hlm_pengembalian belum punya konstruktor itu
                var frmPeng = new hlm_pengembalian();
                // jika ada method SetCurrentUser, panggil secara aman
                var mi = frmPeng.GetType().GetMethod("SetCurrentUser");
                if (mi != null)
                {
                    mi.Invoke(frmPeng, new object[] { loggedUserId, loggedUserName });
                }
                frmPeng.Show();
            }
        }

        // public helper: kalau caller membuat hlm_siswa() via parameterless, mereka bisa set usernya
        public void SetLoggedUser(int userId, string userName)
        {
            loggedUserId = userId;
            loggedUserName = userName ?? string.Empty;
        }

        // small helper untuk memeriksa validitas user
        private bool EnsureUserValid()
        {
            if (loggedUserId <= 0)
            {
                MessageBox.Show("User tidak terdeteksi. Silakan login ulang.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
