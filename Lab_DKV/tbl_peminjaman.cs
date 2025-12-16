using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class tbl_peminjaman : Form
    {
        private DataTable dtPeminjaman = new DataTable();

        public tbl_peminjaman()
        {
            InitializeComponent();

            // Auto Load saat form dibuka
            this.Load += tbl_peminjaman_Load;

            // Event Double Click untuk lihat detail
            dataGridView1.CellDoubleClick += DataGridView1_CellDoubleClick;
            // Event Klik untuk isi textbox
            dataGridView1.CellClick += DataGridView1_CellClick;

            // Event untuk mewarnai baris (Status)
            dataGridView1.CellFormatting += DataGridView1_CellFormatting;

            // Setup Placeholder
            SetupPlaceholderEvents();
        }

        private void tbl_peminjaman_Load(object sender, EventArgs e)
        {
            LoadDataPeminjaman();
            SetPlaceholder(); // Set awal
        }

        // ==========================================
        // 1. LOAD DATA DENGAN STATUS LOGIC
        // ==========================================

        // ... (Bagian atas kode sama) ...

        // ==========================================
        // 1. LOAD DATA DENGAN LAYOUT RAPI
        // ==========================================
        public void LoadDataPeminjaman()
        {
            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();

                    // QUERY UPDATE: Menambahkan logika Status + Data Header
                    string query = @"
                        SELECT 
                            P.id_peminjaman,    -- [0] Hidden
                            P.no_pb AS 'No. PB', 
                            P.tgl_pinjam AS 'Tgl Pinjam', 
                            P.id_user,          -- [3] Hidden
                            U.username AS 'Peminjam', 
                            P.nama_petugaspeminjam AS 'Petugas', 
                            P.tujuan AS 'Tujuan',
                            (CASE 
                                WHEN EXISTS (SELECT 1 FROM tbl_detailpb D WHERE D.id_peminjaman = P.id_peminjaman AND (D.status_kembali = 0 OR D.status_kembali IS NULL)) 
                                THEN 'Belum Kembali' 
                                ELSE 'Selesai' 
                            END) AS 'Status'
                        FROM tbl_peminjaman P
                        LEFT JOIN tbl_user U ON P.id_user = U.id_user
                        ORDER BY P.tgl_pinjam DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        dtPeminjaman = new DataTable();
                        da.Fill(dtPeminjaman);
                        dataGridView1.DataSource = dtPeminjaman;

                        // --- FORMATTING KOLOM ---

                        // 1. Sembunyikan ID Internal
                        if (dataGridView1.Columns["id_peminjaman"] != null) dataGridView1.Columns["id_peminjaman"].Visible = false;
                        if (dataGridView1.Columns["id_user"] != null) dataGridView1.Columns["id_user"].Visible = false;

                        // 2. Atur Lebar Kolom agar Proporsional
                        if (dataGridView1.Columns["No. PB"] != null) dataGridView1.Columns["No. PB"].Width = 120;
                        if (dataGridView1.Columns["Tgl Pinjam"] != null) dataGridView1.Columns["Tgl Pinjam"].Width = 120;
                        if (dataGridView1.Columns["Peminjam"] != null) dataGridView1.Columns["Peminjam"].Width = 100;
                        if (dataGridView1.Columns["Petugas"] != null) dataGridView1.Columns["Petugas"].Width = 100;
                        if (dataGridView1.Columns["Status"] != null) dataGridView1.Columns["Status"].Width = 100;
                                                
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Load Data: " + ex.Message);
            }
        }

        // ... (Sisa kode sama) ...

        // ==========================================
        // 2. FITUR WARNA BARIS (MERAH/HIJAU)
        // ==========================================
        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Warnai baris berdasarkan Status
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Status")
                {
                    string status = Convert.ToString(e.Value);

                    if (status == "Belum Kembali")
                    {
                        e.CellStyle.BackColor = Color.MistyRose; // Merah muda
                        e.CellStyle.ForeColor = Color.DarkRed;
                        e.CellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                    }
                    else if (status == "Selesai")
                    {
                        e.CellStyle.BackColor = Color.Honeydew; // Hijau muda
                        e.CellStyle.ForeColor = Color.DarkGreen;
                    }
                }
            }
        }

        // ==========================================
        // 3. FITUR DETAIL BARANG (POPUP)
        // ==========================================
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Pastikan kolom ID ada dan valid
                if (dataGridView1.Rows[e.RowIndex].Cells["id_peminjaman"].Value != DBNull.Value)
                {
                    int idPeminjaman = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id_peminjaman"].Value);
                    string noPB = dataGridView1.Rows[e.RowIndex].Cells["No. PB"].Value.ToString();
                    ShowDetailBarang(idPeminjaman, noPB);
                }
            }
        }

        private void ShowDetailBarang(int idPeminjaman, string noPB)
        {
            Form frmDetail = new Form();
            frmDetail.Text = $"Detail Barang - No PB: {noPB}";
            frmDetail.Size = new Size(650, 350);
            frmDetail.StartPosition = FormStartPosition.CenterParent;

            DataGridView gridDetail = new DataGridView();
            gridDetail.Dock = DockStyle.Fill;
            gridDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridDetail.ReadOnly = true;
            gridDetail.AllowUserToAddRows = false;

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            B.kode_barang AS 'Kode',
                            B.nama_barang AS 'Nama Barang',
                            B.merek AS 'Merek',
                            D.unit AS 'Jml Pinjam',
                            CASE WHEN D.status_kembali = 1 THEN 'SUDAH KEMBALI' ELSE 'BELUM KEMBALI' END AS 'Status Barang'
                        FROM tbl_detailpb D
                        JOIN tbl_barang B ON D.id_barang = B.id_barang
                        WHERE D.id_peminjaman = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", idPeminjaman);
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            gridDetail.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal ambil detail: " + ex.Message);
                return;
            }

            frmDetail.Controls.Add(gridDetail);
            frmDetail.ShowDialog();
        }

        // ==========================================
        // 4. CRUD (TAMBAH, EDIT, HAPUS)
        // ==========================================

        private void btn_tambah_Click(object sender, EventArgs e)
        {
            if (IsPlaceholder(txt_no_pb) || IsPlaceholder(txt_id_user))
            {
                MessageBox.Show("Isi data No PB dan ID User!");
                return;
            }

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO tbl_peminjaman 
                        (no_pb, tgl_pinjam, id_user, nama_petugaspeminjam, tujuan) 
                        VALUES (@no_pb, @tgl_pinjam, @id_user, @nama_petugaspeminjam, @tujuan)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@no_pb", txt_no_pb.Text);
                        // Validasi format tanggal: 'YYYY-MM-DD HH:MM:SS'
                        cmd.Parameters.AddWithValue("@tgl_pinjam", txt_tgl_pinjam.Text);
                        cmd.Parameters.AddWithValue("@id_user", txt_id_user.Text);
                        cmd.Parameters.AddWithValue("@nama_petugaspeminjam", txt_nama_petugaspeminjam.Text);
                        cmd.Parameters.AddWithValue("@tujuan", txt_tujuan.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Data berhasil ditambahkan!");
                LoadDataPeminjaman();
                ClearInput();
                SetPlaceholder();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }

        private void btn_edit_data_Click(object sender, EventArgs e)
        {
            if (IsPlaceholder(txt_no_pb)) { MessageBox.Show("Pilih data dulu!"); return; }

            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE tbl_peminjaman SET 
                        tgl_pinjam=@tgl_pinjam, 
                        id_user=@id_user, 
                        nama_petugaspeminjam=@nama_petugaspeminjam,
                        tujuan=@tujuan
                        WHERE no_pb=@no_pb";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@no_pb", txt_no_pb.Text);
                        cmd.Parameters.AddWithValue("@tgl_pinjam", txt_tgl_pinjam.Text);
                        cmd.Parameters.AddWithValue("@id_user", txt_id_user.Text);
                        cmd.Parameters.AddWithValue("@nama_petugaspeminjam", txt_nama_petugaspeminjam.Text);
                        cmd.Parameters.AddWithValue("@tujuan", txt_tujuan.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Data berhasil diupdate!");
                LoadDataPeminjaman();
                ClearInput();
                SetPlaceholder();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            if (IsPlaceholder(txt_no_pb)) { MessageBox.Show("Pilih data terlebih dahulu!"); return; }

            if (MessageBox.Show("Yakin hapus data ini? Detail barang terkait juga akan terhapus.", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = DB.GetConnection())
                    {
                        conn.Open();
                        // Hapus Header (Detail akan terhapus otomatis jika DB di-set ON DELETE CASCADE)
                        // Jika tidak, Anda harus menghapus detailnya dulu manual.
                        string query = "DELETE FROM tbl_peminjaman WHERE no_pb=@no_pb";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@no_pb", txt_no_pb.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Data berhasil dihapus!");
                    LoadDataPeminjaman();
                    ClearInput();
                    SetPlaceholder();
                }
                catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
            }
        }

        // ==========================================
        // 4. NAVIGASI, CARI & RELOAD
        // ==========================================
        private void btn_cari_Click(object sender, EventArgs e)
        {
            string keyword = txt_no_pb.Text.Trim();
            if (IsPlaceholder(txt_no_pb)) keyword = "";

            if (string.IsNullOrEmpty(keyword))
            {
                dtPeminjaman.DefaultView.RowFilter = "";
            }
            else
            {
                // Filter di memori DataTable
                dtPeminjaman.DefaultView.RowFilter = string.Format("[No. PB] LIKE '%{0}%' OR [Peminjam] LIKE '%{0}%'", keyword);
            }
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            LoadDataPeminjaman();
            ClearInput();
            SetPlaceholder();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                RemovePlaceholder(); // Hapus placeholder sebelum isi data

                txt_no_pb.Text = row.Cells["No. PB"].Value.ToString();
                txt_tgl_pinjam.Text = row.Cells["Tgl Pinjam"].Value.ToString();
                txt_id_user.Text = row.Cells["id_user"].Value.ToString(); // Mengambil ID (bukan nama) untuk edit
                txt_nama_petugaspeminjam.Text = row.Cells["Petugas"].Value.ToString();
                txt_tujuan.Text = row.Cells["Tujuan"].Value.ToString();
            }
        }

        private void btn_DataUser_Click(object sender, EventArgs e) { hlm_DataUser p = new hlm_DataUser(); p.Show(); this.Hide(); }
        private void btn_DataBarang_Click(object sender, EventArgs e) { hlm_barang p = new hlm_barang(); p.Show(); this.Hide(); }
        private void btn_DataPeminjam_Click(object sender, EventArgs e) { LoadDataPeminjaman(); }
        private void btn_keluar_Click(object sender, EventArgs e) { hlm_login p = new hlm_login(); p.Show(); this.Close(); }

        // ==========================================
        // 5. PLACEHOLDER LOGIC (SAFE & CLEAN)
        // ==========================================

        private void SetupPlaceholderEvents()
        {
            AddPHEvent(txt_no_pb, "No PB");
            AddPHEvent(txt_tgl_pinjam, "Tanggal Pinjam");
            AddPHEvent(txt_id_user, "ID User");
            AddPHEvent(txt_nama_petugaspeminjam, "Nama Petugas");
            AddPHEvent(txt_tujuan, "Tujuan");
        }

        private void AddPHEvent(TextBox txt, string placeholder)
        {
            txt.Enter += (s, e) => { if (txt.Text == placeholder) { txt.Text = ""; txt.ForeColor = Color.Black; } };
            txt.Leave += (s, e) => { if (string.IsNullOrWhiteSpace(txt.Text)) { txt.Text = placeholder; txt.ForeColor = Color.Gray; } };
        }

        private void SetPlaceholder()
        {
            SetOnePH(txt_no_pb, "No PB");
            SetOnePH(txt_tgl_pinjam, "Tanggal Pinjam");
            SetOnePH(txt_id_user, "ID User");
            SetOnePH(txt_nama_petugaspeminjam, "Nama Petugas");
            SetOnePH(txt_tujuan, "Tujuan");
        }

        private void SetOnePH(TextBox txt, string text)
        {
            if (string.IsNullOrWhiteSpace(txt.Text) || txt.Text == text) { txt.Text = text; txt.ForeColor = Color.Gray; }
        }

        private void RemovePlaceholder()
        {
            txt_no_pb.ForeColor = Color.Black;
            txt_tgl_pinjam.ForeColor = Color.Black;
            txt_id_user.ForeColor = Color.Black;
            txt_nama_petugaspeminjam.ForeColor = Color.Black;
            txt_tujuan.ForeColor = Color.Black;
        }

        private void ClearInput()
        {
            txt_no_pb.Clear(); txt_tgl_pinjam.Clear(); txt_id_user.Clear();
            txt_nama_petugaspeminjam.Clear(); txt_tujuan.Clear();
        }

        private bool IsPlaceholder(TextBox txt)
        {
            return txt.ForeColor == Color.Gray || string.IsNullOrWhiteSpace(txt.Text);
        }

        // Mappings
        private void btn_update_Click(object sender, EventArgs e) => btn_edit_data_Click(sender, e);
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) => DataGridView1_CellClick(sender, e);
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) => DataGridView1_CellClick(sender, e);

        // Empty handlers
        private void oldbtn_hapus_Click(object sender, EventArgs e) { }
        private void txt_no_pb_TextChanged(object sender, EventArgs e) { }
        private void txt_tgl_pinjam_TextChanged(object sender, EventArgs e) { }
        private void txt_id_user_TextChanged(object sender, EventArgs e) { }
        private void txt_nama_petugaspeminjam_TextChanged(object sender, EventArgs e) { }
        private void txt_tujuan_TextChanged(object sender, EventArgs e) { }
        private void dataG(object sender, EventArgs e) { }
    }
}