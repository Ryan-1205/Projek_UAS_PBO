namespace Lab_DKV
{
    partial class hlm_siswa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hlm_siswa));
            this.btnKeluar = new System.Windows.Forms.Button();
            this.btnPinjamBarang = new System.Windows.Forms.Button();
            this.btnKembalikanBarang = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnKeluar
            // 
            resources.ApplyResources(this.btnKeluar, "btnKeluar");
            this.btnKeluar.BackColor = System.Drawing.Color.Transparent;
            this.btnKeluar.FlatAppearance.BorderSize = 0;
            this.btnKeluar.Name = "btnKeluar";
            this.btnKeluar.UseVisualStyleBackColor = false;
            // 
            // btnPinjamBarang
            // 
            resources.ApplyResources(this.btnPinjamBarang, "btnPinjamBarang");
            this.btnPinjamBarang.BackColor = System.Drawing.Color.Transparent;
            this.btnPinjamBarang.FlatAppearance.BorderSize = 0;
            this.btnPinjamBarang.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnPinjamBarang.Name = "btnPinjamBarang";
            this.btnPinjamBarang.UseVisualStyleBackColor = false;
            this.btnPinjamBarang.Click += new System.EventHandler(this.btnPinjamBarang_Click);
            // 
            // btnKembalikanBarang
            // 
            resources.ApplyResources(this.btnKembalikanBarang, "btnKembalikanBarang");
            this.btnKembalikanBarang.BackColor = System.Drawing.Color.Transparent;
            this.btnKembalikanBarang.FlatAppearance.BorderSize = 0;
            this.btnKembalikanBarang.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnKembalikanBarang.Name = "btnKembalikanBarang";
            this.btnKembalikanBarang.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // hlm_siswa
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Lab_DKV.Properties.Resources.hlm_siswa;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnKembalikanBarang);
            this.Controls.Add(this.btnPinjamBarang);
            this.Controls.Add(this.btnKeluar);
            this.MaximizeBox = false;
            this.Name = "hlm_siswa";
            this.Load += new System.EventHandler(this.hlm_siswa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKeluar;
        private System.Windows.Forms.Button btnPinjamBarang;
        private System.Windows.Forms.Button btnKembalikanBarang;
        private System.Windows.Forms.Label label1;
    }
}