using System;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class hlm_admin : Form
    {
        public hlm_admin()
        {
            InitializeComponent();
        }

        private void halamanAdmin_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnBarang_Click(object sender, EventArgs e)
        {
            hlm_pengembalian fb = new hlm_pengembalian();
            fb.ShowDialog();
        }


        private void btnLaporanPeminjaman_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            hlm_login login = new hlm_login();
            login.Show();
            this.Close();
        }
    }
}
