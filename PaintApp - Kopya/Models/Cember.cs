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
    public class Cember : Daire
    {
        // Çember, içi boş bir dairedir.
        public override void Ciz(Graphics g)
        {
            int cap = Math.Min(Genislik, Yukseklik);
            using (Pen kalem = new Pen(KenarlikRengi, CizgiKalinligi))
            {
                // Sadece kalemle kenarlık çiziyoruz, dolgu (Fill) yapmıyoruz.
                g.DrawEllipse(kalem, X, Y, cap, cap);
            }
        }

        public override string VeriyeDonustur()
        {
            return $"Cember;{X};{Y};{Genislik};{Yukseklik};{KenarlikRengi.ToArgb()};{DolguRengi.ToArgb()};{CizgiKalinligi}";
        }
    }
}
