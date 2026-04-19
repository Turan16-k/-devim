/****************************************************************************
** SAKARYA ÜNİVERSİTESİ
** BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
** BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
** NESNEYE DAYALI PROGRAMLAMA DERSİ
** 2025-2026 BAHAR DÖNEMİ
**
** ÖDEV NUMARASI..........: 1
** ÖĞRENCİ ADI............: Abdurrahman Turan Özcan
** ÖĞRENCİ NUMARASI.......: B251210043
** ÖĞRENCİ GRUBU..........: B Grubu
****************************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace PaintApp
{
    partial class AnaForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlSol;
        private Panel pnlAlt;
        private Label lblBaslik;
        private NumericUpDown numKalinlik;
        private Button btnRenkKenar;
        private Button btnRenkDolgu;
        private Label lblBilgi;
        private Label lblOgrenciBilgi;
        private Button btnSil;

        private void InitializeComponent()
        {
            this.pnlSol = new Panel();
            this.pnlAlt = new Panel();
            this.lblBaslik = new Label();
            this.numKalinlik = new NumericUpDown();
            this.btnRenkKenar = new Button();
            this.btnRenkDolgu = new Button();
            this.lblBilgi = new Label();
            this.lblOgrenciBilgi = new Label();
            this.btnSil = new Button();

            // Ana Form Ayarları
            this.Text = "SAÜ Paint - Basit Vektörel Çizim Uygulaması";
            this.Size = new Size(1100, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // Sol Panel (Şekil Butonları)
            this.pnlSol.Dock = DockStyle.Left;
            this.pnlSol.Width = 160;
            this.pnlSol.BackColor = Color.FromArgb(20, 40, 25); // Koyu yeşil yan panel
            this.pnlSol.ForeColor = Color.White;

            this.lblBaslik.Text = "ŞEKİLLER";
            this.lblBaslik.Font = new Font("Arial", 12, FontStyle.Bold);
            this.lblBaslik.ForeColor = Color.White;
            this.lblBaslik.Location = new Point(10, 20);
            this.lblBaslik.Size = new Size(140, 30);
            this.pnlSol.Controls.Add(this.lblBaslik);

            string[] butonlar = { "Dikdörtgen", "Kare", "Elips", "Daire", "Çember", "Üçgen", "Doğru", "Paralel Kenar", "Altıgen", "Elmas" };
            int yPos = 60;
            foreach (string ad in butonlar)
            {
                Button btn = new Button();
                btn.Text = ad;
                btn.Location = new Point(10, yPos);
                btn.Size = new Size(140, 45);
                btn.FlatStyle = FlatStyle.Flat;
                btn.BackColor = Color.FromArgb(30, 60, 40); // Koyu yeşil buton
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = Color.SeaGreen;
                btn.Click += (s, e) => { _suAnkiSekilTipi = ad; _seciliSekil = null; this.Invalidate(); };
                this.pnlSol.Controls.Add(btn);
                yPos += 55;
            }

            // Alt Panel (Ayarlar ve Dosya İşlemleri)
            this.pnlAlt.Dock = DockStyle.Bottom;
            this.pnlAlt.Height = 100;
            this.pnlAlt.BackColor = Color.FromArgb(15, 30, 20); // Koyu yeşil alt panel

            // Renk ve Kalınlık Kontrolleri
            this.btnRenkKenar.Text = "Kenarlık";
            this.btnRenkKenar.Location = new Point(20, 30);
            this.btnRenkKenar.Size = new Size(100, 40);
            this.btnRenkKenar.BackColor = Color.White;
            this.btnRenkKenar.ForeColor = Color.Black;
            this.btnRenkKenar.Click += (s, e) => {
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK) {
                    _kenarlikRengi = cd.Color;
                    this.btnRenkKenar.BackColor = cd.Color;
                    if (_seciliSekil != null) _seciliSekil.KenarlikRengi = cd.Color;
                    this.Invalidate();
                }
            };

            this.btnRenkDolgu.Text = "Dolgu";
            this.btnRenkDolgu.Location = new Point(130, 30);
            this.btnRenkDolgu.Size = new Size(100, 40);
            this.btnRenkDolgu.BackColor = Color.Gainsboro;
            this.btnRenkDolgu.Click += (s, e) => {
                ColorDialog cd = new ColorDialog();
                if (cd.ShowDialog() == DialogResult.OK) {
                    _dolguRengi = cd.Color;
                    this.btnRenkDolgu.BackColor = cd.Color;
                    if (_seciliSekil != null) _seciliSekil.DolguRengi = cd.Color;
                    this.Invalidate();
                }
            };

            this.numKalinlik.Location = new Point(250, 40);
            this.numKalinlik.Size = new Size(60, 30);
            this.numKalinlik.Value = 2;
            this.numKalinlik.Minimum = 1;
            this.numKalinlik.Maximum = 20;
            this.numKalinlik.ValueChanged += (s, e) => {
                _kalinlik = (int)this.numKalinlik.Value;
                if (_seciliSekil != null) _seciliSekil.CizgiKalinligi = _kalinlik;
                this.Invalidate();
            };

            // Kaydet ve Yükle Butonları
            Button btnKaydet = new Button { Text = "KAYDET", Location = new Point(400, 30), Size = new Size(100, 40), BackColor = Color.LightGreen };
            btnKaydet.Click += btnKaydet_Click;
            
            Button btnYukle = new Button { Text = "YÜKLE", Location = new Point(510, 30), Size = new Size(100, 40), BackColor = Color.LightBlue };
            btnYukle.Click += btnYukle_Click;

            // Sil Butonu
            this.btnSil.Text = "SİL";
            this.btnSil.Location = new Point(620, 30);
            this.btnSil.Size = new Size(100, 40);
            this.btnSil.BackColor = Color.IndianRed;
            this.btnSil.ForeColor = Color.White;
            this.btnSil.Click += btnSil_Click;

            // Öğrenci Bilgisi
            this.lblOgrenciBilgi.Text = "Ad Soyad: Abdurrahman Turan Özcan\nNo: B251210043 (B Grubu)\nOkul: Sakarya Üni. - Bilgisayar Müh.";
            this.lblOgrenciBilgi.Location = new Point(740, 20);
            this.lblOgrenciBilgi.Size = new Size(300, 60);
            this.lblOgrenciBilgi.Font = new Font("Arial", 10, FontStyle.Italic);
            this.lblOgrenciBilgi.ForeColor = Color.CadetBlue;

            this.pnlAlt.Controls.AddRange(new Control[] { this.btnRenkKenar, this.btnRenkDolgu, this.numKalinlik, btnKaydet, btnYukle, this.btnSil, this.lblOgrenciBilgi });

            // Form Olaylarını Bağla
            this.MouseDown += AnaForm_MouseDown;
            this.MouseMove += AnaForm_MouseMove;
            this.MouseUp += AnaForm_MouseUp;

            this.Controls.Add(this.pnlSol);
            this.Controls.Add(this.pnlAlt);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
    }
}
