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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using PaintApp.Models;

namespace PaintApp.Services
{
    public class DosyaYonetici
    {
        // Şekilleri dosyaya kaydeder
        public void Kaydet(string dosyaYolu, List<Sekil> sekiller)
        {
            // Dosyaya yazılacak satırları tutan liste
            List<string> satirlar = new List<string>();

            // Her şekli dönüştürüp listeye al
            foreach (var sekil in sekiller)
            {
                satirlar.Add(sekil.VeriyeDonustur());
            }

            // Dosyayı oluşturup tüm satırları yazıyoruz
            File.WriteAllLines(dosyaYolu, satirlar);
        }

        // Dosyadan şekilleri okur
        public List<Sekil> Yukle(string dosyaYolu)
        {
            List<Sekil> yuklenenSekiller = new List<Sekil>();

            // Dosya yoksa çıkalım
            if (!File.Exists(dosyaYolu)) return yuklenenSekiller;

            // Dosyayı satır satır okuyoruz
            string[] satirlar = File.ReadAllLines(dosyaYolu);

            // Satırları dön
            foreach (var satir in satirlar)
            {
                // Satırı noktalı virgüllere göre parçalıyoruz
                string[] parcalar = satir.Split(';');
                
                // Eksik veri varsa atla
                if (parcalar.Length < 8) continue;

                string tip = parcalar[0];
                
                // Tipe göre nesne oluştur
                Sekil yeniSekil = tip switch
                {
                    "Dikdortgen" => new Dikdortgen(),
                    "Kare" => new Kare(),
                    "Elips" => new Elips(),
                    "Daire" => new Daire(),
                    "Cember" => new Cember(),
                    "Ucgen" => new Ucgen(),
                    "Dogru" => new Dogru(),
                    "ParalelKenar" => new ParalelKenar(),
                    "Altigen" => new Altigen(),
                    "Elmas" => new Elmas(),
                    _ => null
                };

                // Oluştuysa özellikleri doldur
                if (yeniSekil != null)
                {
                    // Parçaları ilgili özelliklere atıyoruz
                    yeniSekil.X = int.Parse(parcalar[1]);
                    yeniSekil.Y = int.Parse(parcalar[2]);
                    yeniSekil.Genislik = int.Parse(parcalar[3]);
                    yeniSekil.Yukseklik = int.Parse(parcalar[4]);
                    yeniSekil.KenarlikRengi = Color.FromArgb(int.Parse(parcalar[5]));
                    yeniSekil.DolguRengi = Color.FromArgb(int.Parse(parcalar[6]));
                    yeniSekil.CizgiKalinligi = int.Parse(parcalar[7]);

                    yuklenenSekiller.Add(yeniSekil);
                }
            }

            return yuklenenSekiller;
        }
    }
}
