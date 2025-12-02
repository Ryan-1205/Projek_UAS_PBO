using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class hlm_pengembalian : Form
    {
        public hlm_pengembalian()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonGetData_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void getData()
        {
            string conString = "server=localhost;uid=root;database=lab_dkv;";
            MySqlConnection con = new MySqlConnection(conString);
            con.Open();
            string query = "select * from barang";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
