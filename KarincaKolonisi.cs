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
            List<List<double>> secilmeDurumToplam = new List<List<double>>();
            List<List<double>> durumToplam = new List<List<double>>();
            double durum;

            for (int i = 0; i < Karincalar.Count; i++)
            {
                //Console.WriteLine(i);
                // karinca tum esyalari secene dek
                while (Karincalar[i].TabuListesi.Count < Esyalar.Count)
                {
                    double pSum = 0;
                    //List<double> tempPs = new List<double>();
                    Dictionary<int, double> indisVeProportion = new Dictionary<int, double>();
                    Dictionary<int, double> output = new Dictionary<int, double>();

                    foreach (var secilmemis in Karincalar[i].Secilmemis())
                    {
                        //tempPs.Add(secilmemis.Feromon * secilmemis.Fayda);
                        //pSum += tempPs.Last();

                        indisVeProportion.Add(secilmemis.Indis, secilmemis.Feromon * secilmemis.Fayda);
                        pSum += indisVeProportion[secilmemis.Indis];
                    }

                    int a = indisVeProportion.ToList().Count;
                    foreach (var element in indisVeProportion.ToList())
                    {
                        indisVeProportion[element.Key] = element.Value / pSum;
                        Console.WriteLine(indisVeProportion[element.Key] + "\n");
                    }

                    // en buyuk proportion'i olan esyayi o karincanin tabu listesine ekledik
                    int secilecekEsya = indisVeProportion.Keys.Max();
                    Karincalar[i].TabuListesi.Add(secilecekEsya);


                }

                //for (int j = 0; j < Karincalar[i].Secilmemis().Count; j++)
                //{
                //    //ilgili esyanin secilmesi icin hesabi yaptik
                //    durum = Esyalar[j].Feromon * Esyalar[j].Fayda;
                //    Karincalar[i].SecmeDurumu[j] = durum;
                //    //yaptigimiz hesabi o karıncanın durumToplam listesine ekledik
                //    durumToplam[i].Add(durum);
                //    secilmeDurumToplam[i].Add(0);
                //}
            }

            for (int i = 0; i < Karincalar.Count; i++)
            {
                for (int j = 0; j < Karincalar[i].TabuListesi.Count; j++)
                    Console.WriteLine(Karincalar[i].TabuListesi[j]);
                Console.WriteLine();
            }
            ////her karınca icin bir tane durum listesi actik
            //durumToplam.Add(new List<double>());
            ////her karınca icin bir tane secilme durum listesi actik
            //secilmeDurumToplam.Add(new List<double>());

            //foreach (var el in Karincalar[i].Secilmemis())
            //{

            //}

            Console.WriteLine("..");
            //for (int i = 0; i < durumToplam.Count; i++)
            //{
            //    for (int j = 0; j < durumToplam[i].Count; j++)
            //    {
            //        // her esyanin secilme durumunu, durum(kendisi)/durum(toplam)
            //        // ile bulduk
            //        secilmeDurumToplam[i][j] = durumToplam[i][j] / durumToplam[i].Sum();
            //    }
            //}

            //for (int i = 0; i < durumToplam.Count; i++)
            //{
            //    Console.WriteLine("i = " + i);
            //    for (int j = 0; j < durumToplam[i].Count; j++)
            //    {
            //        Console.WriteLine("j = " + j + " * " + Math.Round(secilmeDurumToplam[i][j], 4) + " ");
            //    }
            //    Console.Write("\n");
            //}

        }

        public int IterasyonSayisi { get => iterasyonSayisi; set => iterasyonSayisi = value; }
        internal List<Esya> Esyalar { get => esyalar; set => esyalar = value; }
        internal List<Karinca> Karincalar { get => karincalar; set => karincalar = value; }
        public int KarincaSayisi { get => karincaSayisi; set => karincaSayisi = value; }
    }
}
