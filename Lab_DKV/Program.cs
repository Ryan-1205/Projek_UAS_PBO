using System;
using System.Windows.Forms;

namespace Lab_DKV
{
    static class Program
    {
        // Variabel Global untuk Watcher
        public static SessionWatcher InactivityTimer;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ===============================================
            // SETTING WAKTU AUTO LOGOUT DISINI (Contoh: 5 Menit)
            // ===============================================
            InactivityTimer = new SessionWatcher(5);
            // ===============================================

            // Jalankan form login pertama kali
            Application.Run(new hlm_login());
        }
    }
}