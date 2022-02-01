using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MIXMO_Albert_de_WATRIGANT
{
    public class Dictionnaire
    {
        private List<List<string>> motsPossibles;

        public Dictionnaire(List<List<string>> motsPossibles)
        {
            this.motsPossibles = motsPossibles; 
        }

        public List<List<string>> MotsPossibles => motsPossibles;

        public void toString()
        {
            
            for(int i=0;i<this.motsPossibles.Count();i++)
            {
                Console.WriteLine(i + 2);
                for (int a = 0; a<this.motsPossibles.ElementAt(i).Count();a++)
                {
                    Console.Write(this.motsPossibles.ElementAt(i)[a] + "; ");
                }
                Console.Write("\n");
            }
        }
        public bool RechDichoRecursif(int début, string recherche, int fin)
        {
            
            if(fin<début)
            {
                return false;
            }
            List<string> collection = new List<string>();
            collection=(this.motsPossibles.ElementAt(recherche.Length-2));
            int milieu = (fin+début) / 2;
            string motMilieu = collection.ElementAt(milieu);
            int nb = recherche.CompareTo(motMilieu);
            if(nb==0)
            {
                return true;
            }
            else
            {
                if (nb > 0)
                {
                    return RechDichoRecursif(milieu + 1, recherche, fin);
                }
                else
                {
                    return RechDichoRecursif(début, recherche, milieu - 1);
                }
            }
        }
    }
}
