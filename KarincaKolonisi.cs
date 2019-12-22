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
                Karincalar.Add(new Karinca());

        }

        public void EsyaAta()
        {
            RastgeleSayi rastgeleSayi = new RastgeleSayi();

            for (int i = 0; i < IterasyonSayisi; i++)
            {
                 //Proportion fonksiyonu burada yazilcak
               
                // karinca ilk esyayi secti
                int sayi = rastgeleSayi.Between(0, Esyalar.Count - 1);
                Karincalar[i].TabuListesi.Add(Esyalar[sayi]);

                for (int j = 0; j < Karincalar.Count; j++)
                {
                    // rulet tekerlegi ile diger esyayi atanacak
                }
            }

            for (int j = 0; j < IterasyonSayisi; j++)
                Console.WriteLine(j + " " + Karincalar[j].TabuListesi[0].Indis);

        }

        // Feromon * Fayda / Tum(Feromon*Fayda)' yi hesapliyoruz
        // Esyalarin proportion degerine atayacagiz
        // T feromon, N fayda => değer/ağırlık
        public int Proportion() 
        {
            return 0;
        }

        public int IterasyonSayisi { get => iterasyonSayisi; set => iterasyonSayisi = value; }
        internal List<Esya> Esyalar { get => esyalar; set => esyalar = value; }
        internal List<Karinca> Karincalar { get => karincalar; set => karincalar = value; }
        public int KarincaSayisi { get => karincaSayisi; set => karincaSayisi = value; }
    }
}
