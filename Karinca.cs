using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColonyOptKnapsack
{
    class Karinca
    {
        private int agirlik;
        private int deger;
        private int indis;
        List<Esya> tabuListesi= new List<Esya>();

        public Karinca()
        {

        }
        public int Agirlik { get => agirlik; set => agirlik = value; }
        public int Deger { get => deger; set => deger = value; }
        public int Indis { get => indis; set => indis = value; }
        internal List<Esya> TabuListesi { get => tabuListesi; set => tabuListesi = value; }
    }
}
