namespace Lab_DKV
{
    partial class hlm_pengembalian
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NamaPenerima = new System.Windows.Forms.TextBox();
            this.Tanggal = new System.Windows.Forms.DateTimePicker();
            this.BtnKirim = new System.Windows.Forms.Button();
            this.BtnKembali = new System.Windows.Forms.Button();
            this.BtnPilihSemua = new System.Windows.Forms.Button();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(199, 282);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(884, 237);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // NamaPenerima
            // 
            this.NamaPenerima.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NamaPenerima.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NamaPenerima.Location = new System.Drawing.Point(199, 142);
            this.NamaPenerima.Name = "NamaPenerima";
            this.NamaPenerima.Size = new System.Drawing.Size(288, 30);
            this.NamaPenerima.TabIndex = 2;
            this.NamaPenerima.TextChanged += new System.EventHandler(this.NamaPenerima_TextChanged);
            // 
            // Tanggal
            // 
            this.Tanggal.CalendarMonthBackground = System.Drawing.Color.Transparent;
            this.Tanggal.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tanggal.Location = new System.Drawing.Point(199, 217);
            this.Tanggal.Name = "Tanggal";
            this.Tanggal.Size = new System.Drawing.Size(288, 22);
            this.Tanggal.TabIndex = 3;
            this.Tanggal.ValueChanged += new System.EventHandler(this.Tanggal_ValueChanged);
            // 
            // BtnKirim
            // 
            this.BtnKirim.BackColor = System.Drawing.Color.Transparent;
            this.BtnKirim.FlatAppearance.BorderSize = 0;
            this.BtnKirim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnKirim.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnKirim.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnKirim.Location = new System.Drawing.Point(923, 525);
            this.BtnKirim.Name = "BtnKirim";
            this.BtnKirim.Size = new System.Drawing.Size(160, 36);
            this.BtnKirim.TabIndex = 4;
            this.BtnKirim.Text = "Kirim";
            this.BtnKirim.UseVisualStyleBackColor = false;
            this.BtnKirim.Click += new System.EventHandler(this.Kirim_Click);
            // 
            // BtnKembali
            // 
            this.BtnKembali.BackColor = System.Drawing.Color.Transparent;
            this.BtnKembali.FlatAppearance.BorderSize = 0;
            this.BtnKembali.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnKembali.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnKembali.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnKembali.Location = new System.Drawing.Point(189, 528);
            this.BtnKembali.Name = "BtnKembali";
            this.BtnKembali.Size = new System.Drawing.Size(160, 36);
            this.BtnKembali.TabIndex = 5;
            this.BtnKembali.Text = "Kembali";
            this.BtnKembali.UseVisualStyleBackColor = false;
            this.BtnKembali.Click += new System.EventHandler(this.BtnKembali_Click);
            // 
            // BtnPilihSemua
            // 
            this.BtnPilihSemua.BackColor = System.Drawing.Color.Transparent;
            this.BtnPilihSemua.FlatAppearance.BorderSize = 0;
            this.BtnPilihSemua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPilihSemua.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPilihSemua.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnPilihSemua.Location = new System.Drawing.Point(957, 242);
            this.BtnPilihSemua.Name = "BtnPilihSemua";
            this.BtnPilihSemua.Size = new System.Drawing.Size(139, 37);
            this.BtnPilihSemua.TabIndex = 6;
            this.BtnPilihSemua.Text = "Pilih Semua";
            this.BtnPilihSemua.UseVisualStyleBackColor = false;
            this.BtnPilihSemua.Click += new System.EventHandler(this.Bt_Pilih_Semua_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(280, 314);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(108, 21);
            this.checkedListBox1.TabIndex = 7;
            this.checkedListBox1.Visible = false;
            // 
            // hlm_pengembalian
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lab_DKV.Properties.Resources.hlm_Pengembalian;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.BtnPilihSemua);
            this.Controls.Add(this.BtnKembali);
            this.Controls.Add(this.BtnKirim);
            this.Controls.Add(this.Tanggal);
            this.Controls.Add(this.NamaPenerima);
            this.Controls.Add(this.dataGridView1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "hlm_pengembalian";
            this.Text = "Pengembalian ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox NamaPenerima;
        private System.Windows.Forms.DateTimePicker Tanggal;
        private System.Windows.Forms.Button BtnKirim;
        private System.Windows.Forms.Button BtnKembali;
        private System.Windows.Forms.Button BtnPilihSemua;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}

