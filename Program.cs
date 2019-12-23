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
            int karincaSayisi = 5;
            int ADIM_SAYISI = 10;
            double ALFA = 1;
            double BETA = 1;

            VeriOkuma veri = new VeriOkuma();
            List<Esya> elemanList = veri.elemanlarListesi("test0.txt");

            KarincaKolonisi karincaKolonisi = new KarincaKolonisi(karincaSayisi, ADIM_SAYISI, elemanList);
            karincaKolonisi.IlkAtama();
            karincaKolonisi.SecilmeDurumuHesapla();
            //karincaKolonisi.EsyaAta();
            Console.ReadKey();
        }
    }
}
