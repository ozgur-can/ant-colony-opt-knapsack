using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColonyOptKnapsack
{
    class KarincaKolonisi
    {
        private int iterasyonSayisi;
        private int karincaSayisi;
        private List<Esya> esyalar = new List<Esya>();
        List<Karinca> karincalar = new List<Karinca>();

        public KarincaKolonisi(int karincaSayisi, int iterasyonSayisi, List<Esya> esyalar)
        {
            KarincaSayisi = karincaSayisi;
            IterasyonSayisi = iterasyonSayisi;
            Esyalar = esyalar;
            for (int i = 0; i < karincaSayisi; i++)
                Karincalar.Add(new Karinca(esyalar));

        }

        public void IlkAtama()
        {
            RastgeleSayi rastgeleSayi = new RastgeleSayi();
            int sayi;

            // karinca ilk esyayi secti
            for (int i = 0; i < Karincalar.Count; i++)
            {
                sayi = rastgeleSayi.Between(0, Esyalar.Count - 1);
                Karincalar[i].TabuListesi.Add(sayi);
            }

        }

        public void EsyaAta()
        {
            int sayac = 0;
            // rulet tekerlegi ile diger esyalar atanacak

            do
            {


            } while (sayac < IterasyonSayisi);

        }

        public int SecilmeOrani() // Proportion
        {
            return 0;
        }

        // Karincanin basladigi yerden sonrasi icin
        // Tum esyalarin secilme durumlarini hesaplar

        // Feromon * Fayda / Tum(Feromon*Fayda)' yi hesapliyoruz
        // Esyalarin proportion degerine atayacagiz
        // T feromon, N fayda => değer/ağırlık
        public void SecilmeDurumuHesapla()
        {
            for (int i = 0; i < Karincalar.Count; i++)
            {
                //Console.WriteLine(i);
                // karinca tum esyalari secene dek
                while (Karincalar[i].TabuListesi.Count < Esyalar.Count)
                {
                    double pSum = 0;
                    Dictionary<int, double> indisVeProportion = new Dictionary<int, double>();

                    foreach (var secilmemis in Karincalar[i].Secilmemis())
                    {
                        indisVeProportion.Add(secilmemis.Indis, secilmemis.Feromon * secilmemis.Fayda);
                        pSum += indisVeProportion[secilmemis.Indis];
                    }

                    foreach (var element in indisVeProportion.ToList())
                    {
                        indisVeProportion[element.Key] = element.Value / pSum;
                        Console.WriteLine(indisVeProportion[element.Key] + "\n");
                    }

                    // en buyuk proportion'i olan esyayi o karincanin tabu listesine ekledik
                    int secilecekEsya = indisVeProportion.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                    Karincalar[i].TabuListesi.Add(secilecekEsya);
                }
            }

            for (int i = 0; i < Karincalar.Count; i++)
            {
                for (int j = 0; j < Karincalar[i].TabuListesi.Count; j++)
                    Console.WriteLine(Karincalar[i].TabuListesi[j]);
                Console.WriteLine();
            }
        }

        public int IterasyonSayisi { get => iterasyonSayisi; set => iterasyonSayisi = value; }
        internal List<Esya> Esyalar { get => esyalar; set => esyalar = value; }
        internal List<Karinca> Karincalar { get => karincalar; set => karincalar = value; }
        public int KarincaSayisi { get => karincaSayisi; set => karincaSayisi = value; }
    }
}
