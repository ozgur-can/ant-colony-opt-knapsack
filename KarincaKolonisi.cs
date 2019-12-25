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
        private int kapasite;
        private List<Esya> esyalar = new List<Esya>();
        List<Karinca> karincalar = new List<Karinca>();
        RastgeleSayi rastgeleSayi = new RastgeleSayi();

        public KarincaKolonisi(int karincaSayisi, int iterasyonSayisi, List<Esya> esyalar, int kapasite)
        {
            KarincaSayisi = karincaSayisi;
            IterasyonSayisi = iterasyonSayisi;
            Esyalar = esyalar;
            Kapasite = kapasite;
            for (int i = 0; i < karincaSayisi; i++)
                Karincalar.Add(new Karinca(esyalar));

        }

        public void IlkAtama()
        {
            int sayi;

            // karinca ilk esyayi secti
            for (int i = 0; i < Karincalar.Count; i++)
            {
                sayi = RastgeleSayi.Between(0, Esyalar.Count);
                Karincalar[i].TabuListesi.Add(sayi);
            }

        }

        // Karincanin basladigi yerden sonrasi icin
        // Tum esyalarin secilme durumlarini hesaplar

        // Feromon * Fayda / Tum(Feromon*Fayda)' yi hesapliyoruz
        // Esyalarin proportion degerine atayacagiz
        // T feromon, N fayda => değer/ağırlık
        public void Optimizasyon()
        {
            for (int i = 0; i < Karincalar.Count; i++)
            {
                // karinca tum esyalari secene dek
                while (Karincalar[i].CantaAgirligi() < Kapasite)
                {
                    double pToplam = 0;
                    Dictionary<int, double> indisVeProportion = new Dictionary<int, double>();

                    foreach (var secilmemis in Karincalar[i].Secilmemis())
                    {
                        indisVeProportion.Add(secilmemis.Indis, secilmemis.Feromon * secilmemis.Fayda);
                        pToplam += indisVeProportion[secilmemis.Indis];
                    }

                    foreach (var element in indisVeProportion.ToList())
                    {
                        indisVeProportion[element.Key] = element.Value / pToplam;
                    }

                    // rulet ile yeni esyayi o karincanin tabu listesine ekledik
                    int secilecekEsya = RuletIleSecim(indisVeProportion);
                    if (Esyalar[secilecekEsya].Agirlik + Karincalar[i].CantaAgirligi() <= Kapasite)
                        Karincalar[i].TabuListesi.Add(secilecekEsya);
                    else break;
                }
            }

            for (int i = 0; i < Karincalar.Count; i++)
            {
                for (int j = 0; j < Karincalar[i].TabuListesi.Count; j++)
                    Console.WriteLine(Karincalar[i].TabuListesi[j]);
                Console.WriteLine("W = " + Karincalar[i].CantaAgirligi() + "\n");
            }
        }

        // karincanin secebilecegi esyalari rulete atip ruletten hangisini sececegine karar ver
        public int RuletIleSecim(Dictionary<int, double> indisProp)
        {
            // dictionary'deki degerleri artan sirada siraladik
            var siraliIndisProp = indisProp.ToList();
            siraliIndisProp.Sort((x, y) => x.Value.CompareTo(y.Value));

            double toplam = 0;
            Dictionary<int, double> toplamList = new Dictionary<int, double>();

            for (int i = 0; i < siraliIndisProp.Count; i++)
            {
                for (int j = i; j <= i; j++)
                    toplam += siraliIndisProp[j].Value;
                //dictionary'nin indisiyle beraber ekliyoruz
                toplamList.Add(siraliIndisProp[i].Key, toplam);
            }

            // 0 ile 1 arasinda sayi tuttuk
            double sayi = RastgeleSayi.BetweenDouble(0, 2);

            // tutulan sayi, hangi rulet araliginda kaliyorsa o indisi tutuyoruz
            int secilecekEsya = toplamList.Aggregate((x, y) => x.Value < sayi && y.Value > sayi ? y : x).Key;

            return secilecekEsya;
        }

        public int IterasyonSayisi { get => iterasyonSayisi; set => iterasyonSayisi = value; }
        internal List<Esya> Esyalar { get => esyalar; set => esyalar = value; }
        internal List<Karinca> Karincalar { get => karincalar; set => karincalar = value; }
        public int KarincaSayisi { get => karincaSayisi; set => karincaSayisi = value; }
        public RastgeleSayi RastgeleSayi { get => rastgeleSayi; set => rastgeleSayi = value; }
        public int Kapasite { get => kapasite; set => kapasite = value; }
    }
}
