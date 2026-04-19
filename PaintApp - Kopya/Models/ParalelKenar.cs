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
    public class ParalelKenar : Sekil
    {
        public override void Ciz(Graphics g)
        {
            using (Pen kalem = new Pen(KenarlikRengi, CizgiKalinligi))
            using (SolidBrush firca = new SolidBrush(DolguRengi))
            {
                int kayma = Genislik / 4; // Paralel kenar eğimi için kayma miktarı

                Point[] koseler = {
                    new Point(X + kayma, Y),                  // Sol üst
                    new Point(X + Genislik, Y),               // Sağ üst
                    new Point(X + Genislik - kayma, Y + Yukseklik), // Sağ alt
                    new Point(X, Y + Yukseklik)               // Sol alt
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
            return $"ParalelKenar;{X};{Y};{Genislik};{Yukseklik};{KenarlikRengi.ToArgb()};{DolguRengi.ToArgb()};{CizgiKalinligi}";
        }
    }
}
