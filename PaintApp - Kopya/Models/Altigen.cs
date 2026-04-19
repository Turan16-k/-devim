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

using System.Drawing;

namespace PaintApp.Models
{
    // Sekil ana sınıfından (inheritance) türeyen Altıgen sınıfımız.
    public class Altigen : Sekil
    {
        // Ekrana altıgeni çizecek metod. Sekil sınıfındaki metodu (override) eziyoruz.
        public override void Ciz(Graphics g)
        {
            // using blogu sayesinde kalem işi bitince RAM'den atılır, bellek sızdırmaz
            using (Pen kalem = new Pen(KenarlikRengi, CizgiKalinligi))
            using (SolidBrush firca = new SolidBrush(DolguRengi))
            {
                // Altıgenin 6 köşesi için noktaları matematiksel olarak hesaplıyoruz
                int ceyrekG = Genislik / 4;
                int yariY = Yukseklik / 2;

                Point[] koseler = {
                    new Point(X + ceyrekG, Y),
                    new Point(X + 3 * ceyrekG, Y),
                    new Point(X + Genislik, Y + yariY),
                    new Point(X + 3 * ceyrekG, Y + Yukseklik),
                    new Point(X + ceyrekG, Y + Yukseklik),
                    new Point(X, Y + yariY)
                };

                // Önce içini dolduruyoruz
                g.FillPolygon(firca, koseler);
                // Sonra kenar çizgisini çekiyoruz
                g.DrawPolygon(kalem, koseler);
            }
        }

        // Fare ile tıkladığımızda, tıkladığımız noktanın altıgenin içinde olup olmadığına bakar
        // Basit tutmak için şimdilik sınırları (dikdörtgen alanı) baz aldım
        public override bool IcindeMi(int fareX, int fareY)
        {
            return fareX >= X && fareX <= X + Genislik &&
                   fareY >= Y && fareY <= Y + Yukseklik;
        }

        // Dosyaya kaydederken şeklin özelliklerini arasına noktalı virgül koyarak texte çevirir
        public override string VeriyeDonustur()
        {
            return $"Altigen;{X};{Y};{Genislik};{Yukseklik};{KenarlikRengi.ToArgb()};{DolguRengi.ToArgb()};{CizgiKalinligi}";
        }
    }
}
