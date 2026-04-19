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
    public class Dogru : Sekil
    {
        public override void Ciz(Graphics g)
        {
            using (Pen kalem = new Pen(KenarlikRengi, CizgiKalinligi))
            {
                // Bir noktadan diğerine çizgi çekiyoruz
                g.DrawLine(kalem, X, Y, X + Genislik, Y + Yukseklik);
            }
        }

        public override bool IcindeMi(int fareX, int fareY)
        {
            // Çizgi için basit çevreleyen kutu kontrolü
            return fareX >= X - 5 && fareX <= X + Genislik + 5 &&
                   fareY >= Y - 5 && fareY <= Y + Yukseklik + 5;
        }

        public override string VeriyeDonustur()
        {
            return $"Dogru;{X};{Y};{Genislik};{Yukseklik};{KenarlikRengi.ToArgb()};{DolguRengi.ToArgb()};{CizgiKalinligi}";
        }
    }
}
