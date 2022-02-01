using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MIXMO_Albert_de_WATRIGANT
{
    public class Lettres
    {
        
        private List<Lettre> collection = new List<Lettre>();


        public Lettres(List<Lettre> collection)
        {
            this.collection = collection;
        }
        public List<Lettre> Collection
        {
            get
            {
                return this.collection;
            }
            set
            {
                this.collection = value;
            }
        }
        public string toString()
        {
            string réponse="";
            foreach (Lettre l in collection)
            {
                réponse = réponse+l.toString() + "; ";
            }
            return réponse;
        }

       
        

        
    }
}