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

namespace PaintApp.Models
{
    public class Elips : Sekil
    {
        public override void Ciz(Graphics g)
        {
            using (Pen kalem = new Pen(KenarlikRengi, CizgiKalinligi))
            using (SolidBrush firca = new SolidBrush(DolguRengi))
            {
                // Elips çizimi için Graphics kütüphanesini kullanıyoruz
                g.FillEllipse(firca, X, Y, Genislik, Yukseklik);
                g.DrawEllipse(kalem, X, Y, Genislik, Yukseklik);
            }
        }

        public override bool IcindeMi(int fareX, int fareY)
        {
            // Basitlik adına, elipsin çevreleyen kutusuna (bounding box) göre kontrol yapıyoruz.
            // Bu bir öğrenci ödevi için yeterli ve anlaşılır bir yaklaşımdır.
            return fareX >= X && fareX <= X + Genislik &&
                   fareY >= Y && fareY <= Y + Yukseklik;
        }

        public override string VeriyeDonustur()
        {
            return $"Elips;{X};{Y};{Genislik};{Yukseklik};{KenarlikRengi.ToArgb()};{DolguRengi.ToArgb()};{CizgiKalinligi}";
        }
    }
}
