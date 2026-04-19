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
    public class Dikdortgen : Sekil
    {
        public override void Ciz(Graphics g)
        {
            // Kalem ve fırça nesneleri oluşturuluyor
            using (Pen kalem = new Pen(KenarlikRengi, CizgiKalinligi))
            using (SolidBrush firca = new SolidBrush(DolguRengi))
            {
                // Önce içi boyanıyor, sonra kenarları çiziliyor
                g.FillRectangle(firca, X, Y, Genislik, Yukseklik);
                g.DrawRectangle(kalem, X, Y, Genislik, Yukseklik);
            }
        }

        public override bool IcindeMi(int fareX, int fareY)
        {
            // Fare koordinatları dikdörtgenin sınırları içinde mi?
            return fareX >= X && fareX <= X + Genislik &&
                   fareY >= Y && fareY <= Y + Yukseklik;
        }

        public override string VeriyeDonustur()
        {
            // Verileri yan yana yazıp araya noktalı virgül koyuyoruz (Basit Kayıt Formatı)
            return $"Dikdortgen;{X};{Y};{Genislik};{Yukseklik};{KenarlikRengi.ToArgb()};{DolguRengi.ToArgb()};{CizgiKalinligi}";
        }
    }
}
