using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIXMO_Albert_de_WATRIGANT
{
    public class Lettre
    {
        private string symbole;
        private int poids;

        public Lettre(string symbole,int poids)
        {
            this.poids = poids;
            this.symbole = symbole;
        }
        public string Symbole
        {
            get
            {
                return this.symbole;
            }
            set
            {
                this.symbole = value;
            }
        }
        public int Poids
        {
            get
            {
                return this.poids;
            }
            set
            {
                this.poids = value;
            }
        }

        public string toString()
        {
            string réponse= symbole + " " + poids;
            return réponse;
        }
    }
}
