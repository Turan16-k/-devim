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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PaintApp.Models;
using PaintApp.Services;

namespace PaintApp
{
    public partial class AnaForm : Form
    {
        // Programda çizilen tüm şekilleri bu listede tutuyoruz.
        private List<Sekil> _sekiller = new List<Sekil>();
        
        // O an seçili olan şekil (Rengini değiştirmek veya taşımak için)
        private Sekil _seciliSekil;
        
        // Çizilmekte olan geçici şekil
        private Sekil _cizilmekteOlanSekil;
        
        // Fareye ilk tıklandığı yer
        private Point _baslangicNoktasi;
        
        // Şu an hangi şekil tipinin seçili olduğu (Varsayılan Dikdörtgen)
        private string _suAnkiSekilTipi = "Dikdörtgen";
        
        // Seçili renk ve kalınlık ayarları (Koyu tema için kenarlığı beyaz başlatıyorum)
        private Color _kenarlikRengi = Color.White;
        private Color _dolguRengi = Color.Gainsboro;
        private int _kalinlik = 2;
        
        // Kayıt ve yükleme işlemlerini yapan sınıfımız
        private DosyaYonetici _dosyaYonetici = new DosyaYonetici();

        public AnaForm()
        {
            InitializeComponent();
            // Ekranın daha akıcı görünmesi ve titrememesi için bu ayarı açıyoruz.
            this.DoubleBuffered = true;
        }

        // Fareye tıklandığında (MouseDown)
        private void AnaForm_MouseDown(object sender, MouseEventArgs e)
        {
            // Sol tuşa basıldıysa
            if (e.Button == MouseButtons.Left)
            {
                // Tıklanan şekli bul
                foreach (var sekil in _sekiller)
                {
                    if (sekil.IcindeMi(e.X, e.Y))
                    {
                        _seciliSekil = sekil;
                        this.Invalidate(); // Ekranı tazele (Seçili olduğunu göstermek için)
                        return;
                    }
                }

                // Eğer boş yere tıklandıysa: Yeni şekil oluşturmaya başlıyoruz
                _seciliSekil = null;
                _baslangicNoktasi = e.Location;
                _cizilmekteOlanSekil = SekilOlustur(_suAnkiSekilTipi);
                
                // Şeklin ilk özelliklerini atıyoruz
                _cizilmekteOlanSekil.X = e.X;
                _cizilmekteOlanSekil.Y = e.Y;
                _cizilmekteOlanSekil.KenarlikRengi = _kenarlikRengi;
                _cizilmekteOlanSekil.DolguRengi = _dolguRengi;
                _cizilmekteOlanSekil.CizgiKalinligi = _kalinlik;
            }
        }

        // Fare hareket ederken (MouseMove)
        private void AnaForm_MouseMove(object sender, MouseEventArgs e)
        {
            // Sürükleniyorsa yeni şekli boyutlandır
            if (e.Button == MouseButtons.Left && _cizilmekteOlanSekil != null)
            {
                // Şeklin genişliğini ve yüksekliğini fareye göre güncelliyoruz
                _cizilmekteOlanSekil.Genislik = Math.Abs(e.X - _baslangicNoktasi.X);
                _cizilmekteOlanSekil.Yukseklik = Math.Abs(e.Y - _baslangicNoktasi.Y);
                
                // Ters yöne doğru çizim desteği için en küçük X ve Y'yi alıyoruz
                _cizilmekteOlanSekil.X = Math.Min(e.X, _baslangicNoktasi.X);
                _cizilmekteOlanSekil.Y = Math.Min(e.Y, _baslangicNoktasi.Y);
                
                this.Invalidate(); // Çizimin anlık görünebilmesi için ekranı sürekli yeniliyoruz
            }
            // Seçilen bir şekil sürükleniyorsa (taşıma)
            else if (e.Button == MouseButtons.Left && _seciliSekil != null)
            {
                _seciliSekil.X = e.X - _seciliSekil.Genislik / 2;
                _seciliSekil.Y = e.Y - _seciliSekil.Yukseklik / 2;
                this.Invalidate();
            }
        }

        // Fare bırakıldığında (MouseUp)
        private void AnaForm_MouseUp(object sender, MouseEventArgs e)
        {
            // Çizim bitmişse asıl listeye ekle
            if (_cizilmekteOlanSekil != null)
            {
                // Çizimi biten şekli asıl listeye ekliyoruz
                _sekiller.Add(_cizilmekteOlanSekil);
                _seciliSekil = _cizilmekteOlanSekil;
                _cizilmekteOlanSekil = null;
                this.Invalidate();
            }
        }

        // Ekran her tazelendiğinde (Paint) her şeyi baştan çiziyoruz
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            
            // Çizimlerin daha yumuşak görünmesi için AntiAlias açıyoruz
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Arka planı özel Koyu Yeşil tonuyla doldur ve Atö43 desenleri çiz
            using (SolidBrush bgFirca = new SolidBrush(Color.FromArgb(10, 30, 20))) // Çok koyu yeşil
            {
                g.FillRectangle(bgFirca, this.ClientRectangle);
            }

            // Arka plana şeffaf Atö43 filigranları yerleştir
            using (Font f = new Font("Impact", 48, FontStyle.Italic))
            using (SolidBrush trnFirca = new SolidBrush(Color.FromArgb(15, 255, 255, 255))) // %6 şeffaf beyaz
            {
                for (int x = 20; x < this.Width; x += 300)
                {
                    for (int y = 20; y < this.Height; y += 200)
                    {
                        g.DrawString("Atö43", f, trnFirca, x, y);
                    }
                }
            }

            // Önceki şekilleri ekrana çiz
            foreach (var sekil in _sekiller)
            {
                sekil.Ciz(g);
                
                // Seçili olana çerçeve at (vurgu)
                if (sekil == _seciliSekil)
                {
                    using (Pen p = new Pen(Color.Blue, 1))
                    {
                        p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        g.DrawRectangle(p, sekil.X - 2, sekil.Y - 2, sekil.Genislik + 4, sekil.Yukseklik + 4);
                    }
                }
            }

            // Farenin ucundaki geçici şekil
            if (_cizilmekteOlanSekil != null)
                _cizilmekteOlanSekil.Ciz(g);
        }

        // Butonlardan gelen metne göre doğru nesneyi oluşturan yardımcı metod
        private Sekil SekilOlustur(string tip)
        {
            if (tip == "Kare") return new Kare();
            if (tip == "Elips") return new Elips();
            if (tip == "Daire") return new Daire();
            if (tip == "Çember") return new Cember();
            if (tip == "Üçgen") return new Ucgen();
            if (tip == "Doğru") return new Dogru();
            if (tip == "Paralel Kenar") return new ParalelKenar();
            if (tip == "Altıgen") return new Altigen();
            if (tip == "Elmas") return new Elmas();
            return new Dikdortgen();
        }

        // --- SİL BUTONU İŞLEMİ ---
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (_seciliSekil != null)
            {
                _sekiller.Remove(_seciliSekil); // Şekli listeden sil
                _seciliSekil = null; // Seçimi temizle
                this.Invalidate(); // Arayüzü yenile
            }
        }

        // --- BUTON OLAYLARI ---

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Metin Dosyası|*.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                _dosyaYonetici.Kaydet(sfd.FileName, _sekiller);
                MessageBox.Show("Dosya başarıyla kaydedildi.");
            }
        }

        private void btnYukle_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Metin Dosyası|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _sekiller = _dosyaYonetici.Yukle(ofd.FileName);
                _seciliSekil = null;
                this.Invalidate();
                MessageBox.Show("Dosya başarıyla yüklendi.");
            }
        }
    }
}
