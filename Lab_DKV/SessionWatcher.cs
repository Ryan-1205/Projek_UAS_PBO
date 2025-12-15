using System;
using System.Windows.Forms;

namespace Lab_DKV
{
    // Class ini memantau aktivitas Mouse dan Keyboard di seluruh aplikasi
    public class SessionWatcher : IMessageFilter
    {
        private Timer _timer;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;

        // Atur durasi timeout di sini (dalam milidetik)
        // 1 menit = 60000 ms
        // 5 menit = 300000 ms
        private int _timeoutMs = 300000; // Default 5 Menit

        public SessionWatcher(int timeoutMinutes)
        {
            _timeoutMs = timeoutMinutes * 60 * 1000;

            _timer = new Timer();
            _timer.Interval = _timeoutMs;
            _timer.Tick += Timer_Tick;
            _timer.Enabled = false; // Awalnya mati, dinyalakan saat Login
        }

        // Mulai pemantauan (Panggil ini saat Login Berhasil)
        public void Start()
        {
            _timer.Start();
            Application.AddMessageFilter(this); // Mulai sadap aktivitas
        }

        // Stop pemantauan (Panggil ini saat Logout manual)
        public void Stop()
        {
            _timer.Stop();
            Application.RemoveMessageFilter(this);
        }

        // Logic saat waktu habis
        private void Timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            Application.RemoveMessageFilter(this);

            // 1. Bersihkan Data Sesi
            Session.UserId = 0;
            Session.UserName = "";
            Session.Role = "";

            // 2. Tampilkan Pesan
            MessageBox.Show("Sesi Anda telah berakhir karena tidak ada aktivitas.\nSilakan Login kembali.",
                            "Auto Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // 3. Restart Aplikasi ke Halaman Awal (Login)
            // Ini cara paling bersih untuk menutup semua form yang terbuka
            Application.Restart();
        }

        // Fungsi deteksi aktivitas (Dijalankan otomatis oleh Windows)
        public bool PreFilterMessage(ref Message m)
        {
            // Jika ada tombol keyboard ditekan atau mouse digerakkan/diklik
            if (m.Msg == WM_KEYDOWN || m.Msg == WM_MOUSEMOVE || m.Msg == WM_LBUTTONDOWN || m.Msg == WM_RBUTTONDOWN)
            {
                // Reset Timer (Ulangi hitungan mundur dari awal)
                if (_timer.Enabled)
                {
                    _timer.Stop();
                    _timer.Start();
                }
            }
            return false; // Biarkan pesan lanjut diproses oleh aplikasi
        }
    }
}