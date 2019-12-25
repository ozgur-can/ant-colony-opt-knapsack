using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColonyOptKnapsack
{
    class KarincaKolonisi
    {
        private double alfa;
        private double beta;
        private double q;
        private double phi;
        private double globalMax = 0;
        private int iterasyonSayisi;
        private int karincaSayisi;
        private int kapasite;
        private List<Esya> esyalar = new List<Esya>();
        List<Karinca> karincalar = new List<Karinca>();
        RastgeleSayi rastgeleSayi = new RastgeleSayi();

        public KarincaKolonisi(int karincaSayisi, int iterasyonSayisi, List<Esya> esyalar, int kapasite, double q, double phi, double alfa, double beta)
        {
            KarincaSayisi = karincaSayisi;
            IterasyonSayisi = iterasyonSayisi;
            Esyalar = esyalar;
            Kapasite = kapasite;
            Q = q;
            Phi = phi;
            Alfa = alfa;
            Beta = beta;

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
            for (int step = 0; step < IterasyonSayisi; step++)
            {
                for (int i = 0; i < Karincalar.Count; i++)
                {
                    // karinca tum esyalari secene dek
                    while (Karincalar[i].CantaDegeri() < Kapasite)
                    {
                        double pToplam = 0;
                        Dictionary<int, double> indisVeProportion = new Dictionary<int, double>();

                        foreach (var secilmemis in Karincalar[i].Secilmemis())
                        {
                            indisVeProportion.Add(secilmemis.Indis, Math.Pow(secilmemis.Feromon, Alfa) * Math.Pow(secilmemis.Fayda, Beta));
                            pToplam += indisVeProportion[secilmemis.Indis];
                        }

                        foreach (var element in indisVeProportion.ToList())
                        {
                            // eski hali => indisVeProportion[element.Key] = element.Value / pToplam
                            indisVeProportion[element.Key] = (1 - (element.Value / pToplam)) / (indisVeProportion.Count - 1);
                        }

                        // rulet ile yeni esyayi o karincanin tabu listesine ekledik
                        int secilecekEsya = RuletIleSecim(indisVeProportion);
                        if (Esyalar[secilecekEsya].Agirlik + Karincalar[i].CantaAgirligi() <= Kapasite)
                            Karincalar[i].TabuListesi.Add(secilecekEsya);
                        else break;
                    }
                }
                FeromonGuncelle();
            }

            Console.WriteLine(GlobalMaxBul());

            for (int i = 0; i < Karincalar.Count; i++)
            {
                //for (int j = 0; j < Karincalar[i].TabuListesi.Count; j++)
                //   Console.WriteLine(Karincalar[i].TabuListesi[j]);
                Console.WriteLine("W = " + Karincalar[i].CantaDegeri() + "\n");
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

        public double GlobalMaxBul()
        {
            for (int i = 0; i < Karincalar.Count; i++)
            {
                double localmax = 0;
                if (localmax < Karincalar[i].CantaDegeri())
                    localmax = Karincalar[i].CantaDegeri();

                if (GlobalMax < localmax)
                    GlobalMax = localmax;
            }

            return GlobalMax;
        }

        public void FeromonGuncelle()
        {
            double G;
            double pheDegisim;
            double pheToplam = 0;

            for (int i = 0; i < Karincalar.Count; i++)
            {
                G = Q / Karincalar[i].CantaAgirligi();
                for (int j = 0; j < Karincalar[i].TabuListesi.Count; j++)
                {
                    int guncellenecekEsya = Karincalar[i].TabuListesi[j];
                    pheToplam += Esyalar[guncellenecekEsya].Agirlik * G;
                }
            }

            //G=Q/karıncanın agırlıgı
            //feromon degişimi= esyanın agırlıgı*G
            //Feromon=(1-phi)*eski feromon+deromon değişimi
            for (int i = 0; i < Karincalar.Count; i++)
            {
                G = Q / Karincalar[i].CantaAgirligi();
                for (int j = 0; j < Karincalar[i].TabuListesi.Count; j++)
                {
                    int guncellenecekEsya = Karincalar[i].TabuListesi[j];
                    pheDegisim = Esyalar[guncellenecekEsya].Agirlik * G / pheToplam;
                    Esyalar[guncellenecekEsya].Feromon = (1 - Phi) * Esyalar[guncellenecekEsya].Feromon + pheDegisim;
                }
            }
        }

        public int IterasyonSayisi { get => iterasyonSayisi; set => iterasyonSayisi = value; }
        internal List<Esya> Esyalar { get => esyalar; set => esyalar = value; }
        internal List<Karinca> Karincalar { get => karincalar; set => karincalar = value; }
        public int KarincaSayisi { get => karincaSayisi; set => karincaSayisi = value; }
        public RastgeleSayi RastgeleSayi { get => rastgeleSayi; set => rastgeleSayi = value; }
        public int Kapasite { get => kapasite; set => kapasite = value; }
        public double GlobalMax { get => globalMax; set => globalMax = value; }
        public double Q { get => q; set => q = value; }
        public double Phi { get => phi; set => phi = value; }
        public double Alfa { get => alfa; set => alfa = value; }
        public double Beta { get => beta; set => beta = value; }
    }
}
