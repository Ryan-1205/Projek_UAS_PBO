using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic; // Wajib untuk List<>
using System.Data;
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

        // Flag agar saat klik tabel, fitur search tidak mereset hasil filter
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
            this.Load += hlm_peminjaman_Load;

            // Button Events
            btnTambah.Click += btnTambah_Click;
            btnHapus.Click += btnHapus_Click;
            btnKirim.Click += btnKirim_Click;

            // Search/Filter Events (Saat mengetik)
            txtKodeBarang.TextChanged += InputFilter_TextChanged;
            txtNamaBarang.TextChanged += InputFilter_TextChanged;
            txtMerek.TextChanged += InputFilter_TextChanged;

            // Grid Click Event (Mengisi textbox saat tabel diklik)
            gridKetersediaanBarang.CellClick += gridKetersediaanBarang_CellClick;
        }

        private async void hlm_peminjaman_Load(object sender, EventArgs e)
        {
            if (currentUserId <= 0)
            {
                if (Session.UserId > 0)
                {
                    currentUserId = Session.UserId;
                    currentUserName = Session.UserName;
                }
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
            // =======================================================
            // 1. SETUP TABEL KETERSEDIAAN (KIRI)
            // =======================================================

            // Kita tetap buat kolom lengkap di DataTable agar data ID tersimpan di memori
            dtKetersediaan = new DataTable();
            dtKetersediaan.Columns.Add("id_barang", typeof(int));       // Perlu untuk logic (Hidden)
            dtKetersediaan.Columns.Add("kode_barang", typeof(string));  // Tampil
            dtKetersediaan.Columns.Add("nama_barang", typeof(string));  // Tampil
            dtKetersediaan.Columns.Add("merek", typeof(string));        // Tampil
            dtKetersediaan.Columns.Add("kondisi_barang", typeof(string)); // Ada di DB tapi tidak ditampilkan
            dtKetersediaan.Columns.Add("jumlah_barang", typeof(int));   // Tampil

            gridKetersediaanBarang.DataSource = dtKetersediaan;

            // --- PENGATURAN TAMPILAN GRID ---

            // Sembunyikan kolom yang tidak diminta user
            // id_barang wajib ada untuk coding, tapi disembunyikan dari mata user
            if (gridKetersediaanBarang.Columns["id_barang"] != null)
                gridKetersediaanBarang.Columns["id_barang"].Visible = false;

            // kondisi_barang disembunyikan sesuai request
            if (gridKetersediaanBarang.Columns["kondisi_barang"] != null)
                gridKetersediaanBarang.Columns["kondisi_barang"].Visible = false;

            // Rapikan Judul Header agar lebih enak dibaca
            if (gridKetersediaanBarang.Columns["kode_barang"] != null)
                gridKetersediaanBarang.Columns["kode_barang"].HeaderText = "Kode";

            if (gridKetersediaanBarang.Columns["nama_barang"] != null)
            {
                gridKetersediaanBarang.Columns["nama_barang"].HeaderText = "Nama Barang";
                gridKetersediaanBarang.Columns["nama_barang"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Agar lebar otomatis
            }

            if (gridKetersediaanBarang.Columns["merek"] != null)
                gridKetersediaanBarang.Columns["merek"].HeaderText = "Merek";

            if (gridKetersediaanBarang.Columns["jumlah_barang"] != null)
                gridKetersediaanBarang.Columns["jumlah_barang"].HeaderText = "Stok";

            // Setting standar grid
            gridKetersediaanBarang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridKetersediaanBarang.MultiSelect = false;
            gridKetersediaanBarang.ReadOnly = true;


            // =======================================================
            // 2. SETUP TABEL KERANJANG (KANAN)
            // =======================================================
            dtKeranjang = new DataTable();
            dtKeranjang.Columns.Add("id_barang", typeof(int));
            dtKeranjang.Columns.Add("kode_barang", typeof(string));
            dtKeranjang.Columns.Add("nama_barang", typeof(string));
            dtKeranjang.Columns.Add("merek", typeof(string));
            dtKeranjang.Columns.Add("unit", typeof(int));
            dtKeranjang.Columns.Add("keterangan", typeof(string));

            gridListPinjamBarang.DataSource = dtKeranjang;

            // Sembunyikan ID Barang juga di keranjang agar rapi
            if (gridListPinjamBarang.Columns["id_barang"] != null)
                gridListPinjamBarang.Columns["id_barang"].Visible = false;

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

        // ==========================================
        // LOGIC FILTER / PENCARIAN (GABUNGAN)
        // ==========================================
        private void InputFilter_TextChanged(object sender, EventArgs e)
        {
            // Jika perubahan text terjadi karena program (klik tabel), jangan filter dulu
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
            {
                // Menggunakan AND: Hasil harus cocok dengan SEMUA input
                dtKetersediaan.DefaultView.RowFilter = string.Join(" AND ", filters);
            }
            else
            {
                dtKetersediaan.DefaultView.RowFilter = ""; // Reset
            }
        }

        // ==========================================
        // KLIK TABEL -> ISI TEXTBOX
        // ==========================================
        private void gridKetersediaanBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && gridKetersediaanBarang.CurrentRow != null)
            {
                // Aktifkan flag agar TextChanged tidak memicu filter saat kita mengisi textbox
                isProgrammaticChange = true;

                var row = gridKetersediaanBarang.CurrentRow;
                txtKodeBarang.Text = row.Cells["kode_barang"].Value.ToString();
                txtNamaBarang.Text = row.Cells["nama_barang"].Value.ToString();
                txtMerek.Text = row.Cells["merek"].Value.ToString();

                // Reset jumlah ke 1
                txtJumlahBarang.Text = "1";

                isProgrammaticChange = false; // Matikan flag
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // 1. Ambil Data
            // Prioritas ambil dari Grid yang sedang dipilih agar ID Barang akurat
            if (gridKetersediaanBarang.CurrentRow == null && string.IsNullOrEmpty(txtKodeBarang.Text))
            {
                MessageBox.Show("Pilih barang dari tabel terlebih dahulu.");
                return;
            }

            // Pastikan row terpilih (bisa jadi user mengetik tapi filter membuat row hilang)
            if (gridKetersediaanBarang.CurrentRow == null)
            {
                MessageBox.Show("Barang tidak ditemukan di tabel. Klik baris di tabel untuk memilih.");
                return;
            }

            int idBarang = Convert.ToInt32(gridKetersediaanBarang.CurrentRow.Cells["id_barang"].Value);
            string kode = gridKetersediaanBarang.CurrentRow.Cells["kode_barang"].Value.ToString();
            string nama = gridKetersediaanBarang.CurrentRow.Cells["nama_barang"].Value.ToString();
            string merek = gridKetersediaanBarang.CurrentRow.Cells["merek"].Value.ToString();
            int stokTersedia = Convert.ToInt32(gridKetersediaanBarang.CurrentRow.Cells["jumlah_barang"].Value);

            // 2. Validasi Jumlah
            if (!int.TryParse(txtJumlahBarang.Text, out int jumlahPinjam) || jumlahPinjam <= 0)
            {
                MessageBox.Show("Masukkan jumlah barang (angka) minimal 1.");
                return;
            }

            if (jumlahPinjam > stokTersedia)
            {
                MessageBox.Show($"Stok tidak cukup! Tersedia: {stokTersedia}");
                return;
            }

            // 3. Cek Duplikat
            foreach (DataRow dr in dtKeranjang.Rows)
            {
                if (Convert.ToInt32(dr["id_barang"]) == idBarang)
                {
                    MessageBox.Show("Barang ini sudah ada di keranjang.");
                    return;
                }
            }

            // 4. Tambah ke Keranjang
            dtKeranjang.Rows.Add(idBarang, kode, nama, merek, jumlahPinjam, "-");
            btnKirim.Enabled = true;

            // 5. Bersihkan Input (Siap cari barang lain)
            isProgrammaticChange = true;
            txtKodeBarang.Clear();
            txtNamaBarang.Clear();
            txtMerek.Clear();
            txtJumlahBarang.Clear();
            gridKetersediaanBarang.ClearSelection();
            dtKetersediaan.DefaultView.RowFilter = ""; // Reset Search
            isProgrammaticChange = false;
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (gridListPinjamBarang.SelectedRows.Count > 0)
            {
                gridListPinjamBarang.Rows.RemoveAt(gridListPinjamBarang.SelectedRows[0].Index);
                btnKirim.Enabled = dtKeranjang.Rows.Count > 0;
            }
        }

        // ==========================================
        // PROSES KIRIM (DENGAN POPUP INPUT)
        // ==========================================
        private async void btnKirim_Click(object sender, EventArgs e)
        {
            // Validasi User
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

            // 1. Munculkan Popup Input (Petugas & Tujuan)
            string namaPetugas, tujuanPinjam;
            DialogResult drInput = PromptPeminjaman.ShowDialog(out namaPetugas, out tujuanPinjam);

            if (drInput != DialogResult.OK) return; // Jika User Cancel

            if (string.IsNullOrEmpty(namaPetugas) || string.IsNullOrEmpty(tujuanPinjam))
            {
                MessageBox.Show("Nama Petugas dan Tujuan wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Eksekusi Database
            using (MySqlConnection conn = DB.GetConnection())
            {
                await conn.OpenAsync();
                using (MySqlTransaction trans = await conn.BeginTransactionAsync())
                {
                    try
                    {
                        // A. Insert Header
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

                        // B. Insert Detail & Update Stok
                        foreach (DataRow r in dtKeranjang.Rows)
                        {
                            int idBrg = Convert.ToInt32(r["id_barang"]);
                            int unit = Convert.ToInt32(r["unit"]);
                            string ket = r["keterangan"].ToString();

                            // Detail
                            string qDet = "INSERT INTO tbl_detailpb (id_peminjaman, id_barang, unit, keterangan) VALUES (@idp, @idb, @u, @k)";
                            using (MySqlCommand cmd = new MySqlCommand(qDet, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@idp", newId);
                                cmd.Parameters.AddWithValue("@idb", idBrg);
                                cmd.Parameters.AddWithValue("@u", unit);
                                cmd.Parameters.AddWithValue("@k", ket);
                                await cmd.ExecuteNonQueryAsync();
                            }

                            // Update Stok
                            string qUpd = "UPDATE tbl_barang SET jumlah_barang = jumlah_barang - @u WHERE id_barang = @idb";
                            using (MySqlCommand cmd = new MySqlCommand(qUpd, conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@u", unit);
                                cmd.Parameters.AddWithValue("@idb", idBrg);
                                await cmd.ExecuteNonQueryAsync();
                            }
                        }

                        await trans.CommitAsync();

                        // 3. Popup Sukses (Detail)
                        string info = $"PEMINJAMAN BERHASIL!\n\n" +
                                      $"Nama Peminjam: {currentUserName}\n" +
                                      $"Petugas Pengawas: {namaPetugas}\n" +
                                      $"Tanggal: {tglPinjam:dd MMM yyyy HH:mm}\n" +
                                      $"No. PB: {noPB}";

                        MessageBox.Show(info, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reset
                        dtKeranjang.Clear();
                        btnKirim.Enabled = false;
                        await LoadAllBarangAsync();
                    }
                    catch (Exception ex)
                    {
                        await trans.RollbackAsync();
                        MessageBox.Show("Transaksi Gagal: " + ex.Message);
                    }
                }
            }
        }

        // Placeholder TextChanged (Sudah ditangani di HookEvents -> InputFilter_TextChanged)
        private void txtKodeBarang_TextChanged(object sender, EventArgs e) { }
        private void txtNamaBarang_TextChanged(object sender, EventArgs e) { }
        private void txtMerek_TextChanged(object sender, EventArgs e) { }
        private void txtJumlahBarang_TextChanged(object sender, EventArgs e) { }

        // Grid Content Click (Sudah diganti dengan CellClick di HookEvents)
        private void gridKetersediaanBarang_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void gridListPinjamBarang_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // Button Cari (Tidak dipakai lagi karena sudah Live Search, tapi dibiarkan agar tidak error)
        private void btnCari_Click(object sender, EventArgs e) { }
    }

    // ==========================================
    // CLASS TAMBAHAN: POPUP INPUT CUSTOM
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
}