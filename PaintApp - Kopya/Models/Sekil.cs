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
    // Bu sınıf tüm şekillerin atasıdır (Kalıtım/Inheritance).
    // Ortak özellikleri (Konum, Renk, Kalınlık) burada topluyoruz.
    public abstract class Sekil
    {
        // Şeklin sol üst köşesinin X ve Y koordinatları
        public int X { get; set; }
        public int Y { get; set; }

        // Şeklin boyutları
        public int Genislik { get; set; }
        public int Yukseklik { get; set; }

        // Görsel özellikler
        public Color KenarlikRengi { get; set; }
        public Color DolguRengi { get; set; }
        public int CizgiKalinligi { get; set; }

        // Şekli ekrana çizecek metod. Her şekil bunu kendine göre dolduracak (Polymorphism).
        public abstract void Ciz(Graphics g);

        // Bir noktanın şeklin içinde olup olmadığını kontrol eder (Seçme işlemi için).
        public abstract bool IcindeMi(int fareX, int fareY);

        // Şekli dosyaya kaydederken kullanacağımız veriyi string olarak döndürür.
        public abstract string VeriyeDonustur();
    }
}
