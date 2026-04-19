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
    // Daire sınıfımız. Elips sınıfından miras alıyor çünkü daire aslında çapları eşit bir elipstir.
    public class Daire : Elips
    {
        // Ekrana daire çizdiren metodumuz
        public override void Ciz(Graphics g)
        {
            // Genişlik ve yüksekliğin en küçüğünü çap olarak alıyoruz ki tam daire olsun, yamulmasın
            int cap = Math.Min(Genislik, Yukseklik);
            
            // Kalem çizgi için, fırça ise iç dolgu için kullanılıyor
            using (Pen kalem = new Pen(KenarlikRengi, CizgiKalinligi))
            using (SolidBrush firca = new SolidBrush(DolguRengi))
            {
                // Önce dairenin içini boyuyoruz
                g.FillEllipse(firca, X, Y, cap, cap);
                // Sonra dairenin dış çizgisini çiziyoruz
                g.DrawEllipse(kalem, X, Y, cap, cap);
            }
        }

        // Daireyi texte dönüştürüp dosyamıza kaydetmeye yarayan kısım
        public override string VeriyeDonustur()
        {
            return $"Daire;{X};{Y};{Genislik};{Yukseklik};{KenarlikRengi.ToArgb()};{DolguRengi.ToArgb()};{CizgiKalinligi}";
        }
    }
}
