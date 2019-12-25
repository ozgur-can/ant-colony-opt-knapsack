using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColonyOptKnapsack
{
    class Program
    {
        static void Main(string[] args)
        {
            int karincaSayisi = 100;
            int ADIM_SAYISI = 100;
            double ALFA = 0.2;
            double BETA = 0.3;
            double Q = 0.001;
            double PHI = 0.2;

            VeriOkuma veri = new VeriOkuma();
            List<Esya> esyaList = veri.elemanlarListesi("test1.txt");

            KarincaKolonisi karincaKolonisi = new KarincaKolonisi(karincaSayisi, ADIM_SAYISI, esyaList, veri.Kapasite, Q, PHI, ALFA, BETA);
            karincaKolonisi.IlkAtama();
            karincaKolonisi.Optimizasyon();
            Console.ReadKey();
        }
    }
}
