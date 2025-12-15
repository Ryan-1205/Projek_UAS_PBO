using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic; // Wajib untuk List<>
using System.Data;
using System.Drawing; // Wajib untuk Font & Size
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_DKV
{
    public partial class hlm_peminjaman : Form
    {
        private DataTable dtKetersediaan = new DataTable();
        private DataTable dtKeranjang = new DataTable();

        private int currentUserId = 0;
        private string currentUserName = string.Empty;

        private bool isProgrammaticChange = false;

        public hlm_peminjaman()
        {
            InitializeComponent();
            InitGrids();
            HookEvents();
        }

        public hlm_peminjaman(int userId, string userName) : this()
        {
            SetCurrentUser(userId, userName);
        }

        public void SetCurrentUser(int userId, string userName)
        {
            currentUserId = userId;
            currentUserName = userName ?? string.Empty;
        }

        private void HookEvents()
        {
            // --- EVENT HANDLER ---
            // Button Events (Hapus/Komen jika sudah ada di Designer untuk menghindari double click)
            // btnTambah.Click += btnTambah_Click;
            // btnHapus.Click += btnHapus_Click;
            // btnKirim.Click += btnKirim_Click;

            // if (btnBersihkan != null) btnBersihkan.Click += btnBersihkan_Click;
            // if (btnKembali != null) btnKembali.Click += btnKembali_Click;

            // Search/Filter Events (Wajib manual jika tidak ada di Designer)
            txtKodeBarang.TextChanged += InputFilter_TextChanged;
            txtNamaBarang.TextChanged += InputFilter_TextChanged;
            txtMerek.TextChanged += InputFilter_TextChanged;

            // Grid Click Event
            gridKetersediaanBarang.CellClick += gridKetersediaanBarang_CellClick;
        }

        private async void hlm_peminjaman_Load(object sender, EventArgs e)
        {
            if (currentUserId <= 0 && Session.UserId > 0)
            {
                currentUserId = Session.UserId;
                currentUserName = Session.UserName;
            }

            if (currentUserId <= 0)
            {
                MessageBox.Show("User tidak terdeteksi. Fitur peminjaman dikunci.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnTambah.Enabled = false;
                btnKirim.Enabled = false;
            }
            else
            {
                btnTambah.Enabled = true;
            }

            await LoadAllBarangAsync();
            btnHapus.Enabled = false;
        }

        private void InitGrids()
        {
            // 1. SETUP TABEL KETERSEDIAAN
            dtKetersediaan = new DataTable();
            dtKetersediaan.Columns.Add("id_barang", typeof(int));
            dtKetersediaan.Columns.Add("kode_barang", typeof(string));
            dtKetersediaan.Columns.Add("nama_barang", typeof(string));
            dtKetersediaan.Columns.Add("merek", typeof(string));
            dtKetersediaan.Columns.Add("kondisi_barang", typeof(string));
            dtKetersediaan.Columns.Add("jumlah_barang", typeof(int));

            gridKetersediaanBarang.DataSource = dtKetersediaan;

            if (gridKetersediaanBarang.Columns["id_barang"] != null) gridKetersediaanBarang.Columns["id_barang"].Visible = false;
            if (gridKetersediaanBarang.Columns["kondisi_barang"] != null) gridKetersediaanBarang.Columns["kondisi_barang"].Visible = false;
            if (gridKetersediaanBarang.Columns["kode_barang"] != null) gridKetersediaanBarang.Columns["kode_barang"].HeaderText = "Kode";

            if (gridKetersediaanBarang.Columns["nama_barang"] != null)
            {
                gridKetersediaanBarang.Columns["nama_barang"].HeaderText = "Nama Barang";
                gridKetersediaanBarang.Columns["nama_barang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (gridKetersediaanBarang.Columns["merek"] != null) gridKetersediaanBarang.Columns["merek"].HeaderText = "Merek";
            if (gridKetersediaanBarang.Columns["jumlah_barang"] != null) gridKetersediaanBarang.Columns["jumlah_barang"].HeaderText = "Stok";

            gridKetersediaanBarang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridKetersediaanBarang.MultiSelect = false;
            gridKetersediaanBarang.ReadOnly = true;

            // 2. SETUP TABEL KERANJANG
            dtKeranjang = new DataTable();
            dtKeranjang.Columns.Add("id_barang", typeof(int));
            dtKeranjang.Columns.Add("kode_barang", typeof(string));
            dtKeranjang.Columns.Add("nama_barang", typeof(string));
            dtKeranjang.Columns.Add("merek", typeof(string));
            dtKeranjang.Columns.Add("unit", typeof(int));

            gridListPinjamBarang.DataSource = dtKeranjang;

            if (gridListPinjamBarang.Columns["id_barang"] != null) gridListPinjamBarang.Columns["id_barang"].Visible = false;
            if (gridListPinjamBarang.Columns["kode_barang"] != null) gridListPinjamBarang.Columns["kode_barang"].HeaderText = "Kode";

            if (gridListPinjamBarang.Columns["nama_barang"] != null)
            {
                gridListPinjamBarang.Columns["nama_barang"].HeaderText = "Nama Barang";
                gridListPinjamBarang.Columns["nama_barang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (gridListPinjamBarang.Columns["merek"] != null) gridListPinjamBarang.Columns["merek"].HeaderText = "Merek";
            if (gridListPinjamBarang.Columns["unit"] != null) gridListPinjamBarang.Columns["unit"].HeaderText = "Jumlah";

            gridListPinjamBarang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridListPinjamBarang.MultiSelect = false;
        }

        private async Task LoadAllBarangAsync()
        {
            dtKetersediaan.Clear();
            try
            {
                using (MySqlConnection conn = DB.GetConnection())
                {
                    await conn.OpenAsync();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM tbl_barang ORDER BY nama_barang";
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var row = dtKetersediaan.NewRow();
                                row["id_barang"] = reader["id_barang"];
                                row["kode_barang"] = reader["kode_barang"];
                                row["nama_barang"] = reader["nama_barang"];
                                row["merek"] = reader["merek"];
                                row["kondisi_barang"] = reader["kondisi_barang"];
                                row["jumlah_barang"] = reader["jumlah_barang"];
                                dtKetersediaan.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat barang: " + ex.Message);
            }
        }

        private void InputFilter_TextChanged(object sender, EventArgs e)
        {
            if (isProgrammaticChange) return;
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string kode = txtKodeBarang.Text.Trim();
            string nama = txtNamaBarang.Text.Trim();
            string merek = txtMerek.Text.Trim();

            List<string> filters = new List<string>();

            if (!string.IsNullOrEmpty(kode)) filters.Add($"kode_barang LIKE '%{kode}%'");
            if (!string.IsNullOrEmpty(nama)) filters.Add($"nama_barang LIKE '%{nama}%'");
            if (!string.IsNullOrEmpty(merek)) filters.Add($"merek LIKE '%{merek}%'");

            if (filters.Count > 0)
                dtKetersediaan.DefaultView.RowFilter = string.Join(" AND ", filters);
            else
                dtKetersediaan.DefaultView.RowFilter = "";
        }

        private void gridKetersediaanBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && gridKetersediaanBarang.CurrentRow != null)
            {
                isProgrammaticChange = true;
                var row = gridKetersediaanBarang.CurrentRow;
                txtKodeBarang.Text = row.Cells["kode_barang"].Value.ToString();
                txtNamaBarang.Text = row.Cells["nama_barang"].Value.ToString();
                txtMerek.Text = row.Cells["merek"].Value.ToString();
                txtJumlahBarang.Text = "1";
                isProgrammaticChange = false;
            }
        }

        private void btnBersihkan_Click(object sender, EventArgs e)
        {
            isProgrammaticChange = true;
            txtKodeBarang.Clear();
            txtNamaBarang.Clear();
            txtMerek.Clear();
            txtJumlahBarang.Clear();
            dtKetersediaan.DefaultView.RowFilter = "";
            gridKetersediaanBarang.ClearSelection();
            isProgrammaticChange = false;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKodeBarang.Text) || string.IsNullOrEmpty(txtNamaBarang.Text))
            {
                MessageBox.Show("Pilih barang dari tabel terlebih dahulu.");
                return;
            }

            if (gridKetersediaanBarang.CurrentRow == null)
            {
                MessageBox.Show("Silakan klik baris barang di tabel.");
                return;
            }

            int idBarang = Convert.ToInt32(gridKetersediaanBarang.CurrentRow.Cells["id_barang"].Value);
            string kode = txtKodeBarang.Text;
            string nama = txtNamaBarang.Text;
            string merek = txtMerek.Text;
            int stokTersedia = Convert.ToInt32(gridKetersediaanBarang.CurrentRow.Cells["jumlah_barang"].Value);

            string strJumlah = txtJumlahBarang.Text.Trim();
            int jumlahPinjamBaru;

            if (!int.TryParse(strJumlah, out jumlahPinjamBaru))
            {
                MessageBox.Show("Jumlah barang harus berupa angka valid.");
                return;
            }

            if (jumlahPinjamBaru <= 0)
            {
                MessageBox.Show("Jumlah barang minimal 1.");
                return;
            }

            DataRow existingRow = null;
            int jumlahDiKeranjang = 0;

            foreach (DataRow dr in dtKeranjang.Rows)
            {
                if (Convert.ToInt32(dr["id_barang"]) == idBarang)
                {
                    existingRow = dr;
                    jumlahDiKeranjang = Convert.ToInt32(dr["unit"]);
                    break;
                }
            }

            int totalPermintaan = jumlahDiKeranjang + jumlahPinjamBaru;

            if (totalPermintaan > stokTersedia)
            {
                MessageBox.Show($"Stok tidak cukup! Tersedia: {stokTersedia}. \n(Sudah di keranjang: {jumlahDiKeranjang}, Minta tambah: {jumlahPinjamBaru})");
                return;
            }

            if (existingRow != null)
            {
                existingRow["unit"] = totalPermintaan;
            }
            else
            {
                dtKeranjang.Rows.Add(idBarang, kode, nama, merek, jumlahPinjamBaru);
            }

            btnKirim.Enabled = true;
            btnBersihkan_Click(null, null);
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (gridListPinjamBarang.SelectedRows.Count > 0)
            {
                gridListPinjamBarang.Rows.RemoveAt(gridListPinjamBarang.SelectedRows[0].Index);
                btnKirim.Enabled = dtKeranjang.Rows.Count > 0;
            }
        }

        private async void btnKirim_Click(object sender, EventArgs e)
        {
            if (this.currentUserId <= 0)
            {
                if (Session.UserId > 0) this.currentUserId = Session.UserId;
                else
                {
                    MessageBox.Show("Sesi habis. Login ulang.");
                    return;
                }
            }

            if (dtKeranjang.Rows.Count == 0) return;

            string namaPetugas, tujuanPinjam;
            DialogResult drInput = PromptPeminjaman.ShowDialog(out namaPetugas, out tujuanPinjam);

            if (drInput != DialogResult.OK) return;

            if (string.IsNullOrEmpty(namaPetugas) || string.IsNullOrEmpty(tujuanPinjam))
            {
                MessageBox.Show("Nama Petugas dan Tujuan wajib diisi!");
                return;
            }

            using (MySqlConnection conn = DB.GetConnection())
            {
                await conn.OpenAsync();
                using (MySqlTransaction trans = await conn.BeginTransactionAsync())
                {
                    try
                    {
                        string noPB = "PB-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                        DateTime tglPinjam = DateTime.Now;

                        string qHead = @"INSERT INTO tbl_peminjaman (no_pb, tgl_pinjam, id_user, nama_petugaspeminjam, tujuan) 
                                         VALUES (@no, @tgl, @uid, @petugas, @tujuan)";
                        long newId = 0;
                        using (MySqlCommand cmd = new MySqlCommand(qHead, conn, trans))
                        {
                            cmd.Parameters.AddWithValue("@no", noPB);
                            cmd.Parameters.AddWithValue("@tgl", tglPinjam);
                            cmd.Parameters.AddWithValue("@uid", currentUserId);
                            cmd.Parameters.AddWithValue("@petugas", namaPetugas);
                            cmd.Parameters.AddWithValue("@tujuan", tujuanPinjam);
                            await cmd.ExecuteNonQueryAsync();
                            newId = cmd.LastInsertedId;
                        }

                        foreach (DataRow r in dtKeranjang.Rows)
                        {
                            int idBrg = Convert.ToInt32(r["id_barang"]);
                            int unit = Convert.ToInt32(r["unit"]);
                            string ket = "-";

                            string qDet = "INSERT INTO tbl_detailpb (id_peminjaman, id_barang, unit, keterangan) VALUES (@idp, @idb, @u, @k)";
                            using (MySqlCommand cmd = new MySqlCommand(qDet, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@idp", newId);
                                cmd.Parameters.AddWithValue("@idb", idBrg);
                                cmd.Parameters.AddWithValue("@u", unit);
                                cmd.Parameters.AddWithValue("@k", ket);
                                await cmd.ExecuteNonQueryAsync();
                            }

                            string qUpd = "UPDATE tbl_barang SET jumlah_barang = jumlah_barang - @u WHERE id_barang = @idb";
                            using (MySqlCommand cmd = new MySqlCommand(qUpd, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@u", unit);
                                cmd.Parameters.AddWithValue("@idb", idBrg);
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        await trans.CommitAsync();

                        string info = $"PEMINJAMAN BERHASIL!\n\n" +
                                      $"Nama Peminjam: {currentUserName}\n" +
                                      $"Petugas Pengawas: {namaPetugas}\n" +
                                      $"Tanggal: {tglPinjam:dd MMM yyyy HH:mm}\n" +
                                      $"No. PB: {noPB}";

                        // --- POPUP OTOMATIS & AUTO CLOSE & KEMBALI KE LOGIN ---
                        AutoClosingMessageBox.Show(info, "Sukses (Auto Close 10s)", 10000);

                        // Setelah popup tertutup, kembali ke Login
                        hlm_login frmLogin = new hlm_login();
                        frmLogin.Show();

                        // Tutup form peminjaman
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        await trans.RollbackAsync();
                        MessageBox.Show("Transaksi Gagal: " + ex.Message);
                    }
                }
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(
                "Apakah Anda yakin ingin membatalkan peminjaman dan kembali ke menu utama?",
                "Konfirmasi Pembatalan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dr == DialogResult.Yes)
            {
                hlm_siswa frmSiswa = new hlm_siswa(currentUserId, currentUserName);
                frmSiswa.Show();
                this.Close();
            }
        }

        // Placeholder Handlers (Wajib ada jika di Designer masih terhubung)
        private void txtKodeBarang_TextChanged(object sender, EventArgs e) { }
        private void txtNamaBarang_TextChanged(object sender, EventArgs e) { }
        private void txtMerek_TextChanged(object sender, EventArgs e) { }
        private void txtJumlahBarang_TextChanged(object sender, EventArgs e) { }
        private void gridKetersediaanBarang_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void gridListPinjamBarang_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void btnCari_Click(object sender, EventArgs e) { }
    }

    // ==========================================
    // CLASS POPUP INPUT DATA
    // ==========================================
    public static class PromptPeminjaman
    {
        public static DialogResult ShowDialog(out string namaPetugas, out string tujuan)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 280,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Data Peminjaman",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblPetugas = new Label() { Left = 20, Top = 20, Text = "Nama Petugas Pengawas:", Width = 300 };
            TextBox txtPetugas = new TextBox() { Left = 20, Top = 45, Width = 340 };

            Label lblTujuan = new Label() { Left = 20, Top = 80, Text = "Tujuan Peminjaman:", Width = 300 };
            TextBox txtTujuan = new TextBox() { Left = 20, Top = 105, Width = 340, Height = 60, Multiline = true };

            Button btnConfirm = new Button() { Text = "Kirim", Left = 230, Width = 100, Top = 180, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Batal", Left = 120, Width = 100, Top = 180, DialogResult = DialogResult.Cancel };

            btnConfirm.Click += (sender, e) => { prompt.Close(); };
            btnCancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(lblPetugas);
            prompt.Controls.Add(txtPetugas);
            prompt.Controls.Add(lblTujuan);
            prompt.Controls.Add(txtTujuan);
            prompt.Controls.Add(btnConfirm);
            prompt.Controls.Add(btnCancel);
            prompt.AcceptButton = btnConfirm;

            DialogResult result = prompt.ShowDialog();

            namaPetugas = txtPetugas.Text.Trim();
            tujuan = txtTujuan.Text.Trim();

            return result;
        }
    }

    // ==========================================
    // CLASS POPUP AUTO CLOSE
    // ==========================================
    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;

        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }

        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption); // Mencari Window MessageBox
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }

        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
}