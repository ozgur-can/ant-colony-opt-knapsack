using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntColonyOptKnapsack
{
    class Esya
    {
        private double agirlik;
        private double deger;
        private int indis;
        private double feromon;
        private double fayda;
        private double proportion;

        public Esya(int agirlik, int deger, int indis)
        {
            Agirlik = agirlik;
            Deger = deger;
            Indis = indis;
            Feromon = 1;
            Fayda = Deger / Agirlik;
        }

        public double Agirlik { get => agirlik; set => agirlik = value; }
        public double Deger { get => deger; set => deger = value; }
        public int Indis { get => indis; set => indis = value; }
        public double Feromon { get => feromon; set => feromon = value; }
        public double Fayda { get => fayda; set => fayda = value; }
        public double Proportion { get => proportion; set => proportion = value; }
    }
}
