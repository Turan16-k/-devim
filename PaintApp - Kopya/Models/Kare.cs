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
    // Kare sınıfımız. Dikdörtgenle her şeyi aynı olduğu için ondan türettik.
    public class Kare : Dikdortgen
    {
        // Temel farkı çizerken yapıyoruz: kenarların eşit olması lazım.
        public override void Ciz(Graphics g)
        {
            // Kullanıcı fareyi nasıl çekerse çeksin, kare olması için genişlik ve yüksekliğin en küçüğünü baz alıyoruz
            int boyut = Math.Min(Genislik, Yukseklik);

            // Çizim araçlarımızı (kalem ve fırça) nesne olarak oluşturuyoruz
            using (Pen kalem = new Pen(KenarlikRengi, CizgiKalinligi))
            using (SolidBrush firca = new SolidBrush(DolguRengi))
            {
                // Ekrandaki o koordinatlara içini boyayarak bir dikdörtgen (ama kenarları eşit, yani kare) çizdiriyoruz
                g.FillRectangle(firca, X, Y, boyut, boyut);
                g.DrawRectangle(kalem, X, Y, boyut, boyut);
            }
        }

        // Faremizle bu şekli tutmaya çalıştığımızda tetiklenip "içinde misin?" diye sorgulayan metod
        public override bool IcindeMi(int fareX, int fareY)
        {
            int boyut = Math.Min(Genislik, Yukseklik);
            // Sınırların kontrolü (basit bir x, y alanı hesabı)
            return fareX >= X && fareX <= X + boyut &&
                   fareY >= Y && fareY <= Y + boyut;
        }

        // Txt dosyasına yedeğini alırken kullanılacak veriyi metne olarak çeviriyoruz
        public override string VeriyeDonustur()
        {
            return $"Kare;{X};{Y};{Genislik};{Yukseklik};{KenarlikRengi.ToArgb()};{DolguRengi.ToArgb()};{CizgiKalinligi}";
        }
    }
}
