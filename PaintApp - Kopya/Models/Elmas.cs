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
    public class Elmas : Sekil
    {
        public override void Ciz(Graphics g)
        {
            using (Pen kalem = new Pen(KenarlikRengi, CizgiKalinligi))
            using (SolidBrush firca = new SolidBrush(DolguRengi))
            {
                // Baklava/Elmas dilimi koordinatları (4 tepe noktası)
                Point[] koseler = {
                    new Point(X + Genislik / 2, Y),
                    new Point(X + Genislik, Y + Yukseklik / 2),
                    new Point(X + Genislik / 2, Y + Yukseklik),
                    new Point(X, Y + Yukseklik / 2)
                };

                g.FillPolygon(firca, koseler);
                g.DrawPolygon(kalem, koseler);
            }
        }

        public override bool IcindeMi(int fareX, int fareY)
        {
            return fareX >= X && fareX <= X + Genislik &&
                   fareY >= Y && fareY <= Y + Yukseklik;
        }

        public override string VeriyeDonustur()
        {
            return $"Elmas;{X};{Y};{Genislik};{Yukseklik};{KenarlikRengi.ToArgb()};{DolguRengi.ToArgb()};{CizgiKalinligi}";
        }
    }
}
