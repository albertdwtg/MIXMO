using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MIXMO_Albert_de_WATRIGANT
{
    public class MotsCroises
    {
        private char[,] grille;

        public MotsCroises(char [,] grille)
        {
            this.grille = grille;

        }

        public char[,] Grille
        {
            get
            {
                return this.grille;
            }
            set
            {
                this.grille = value;
            }
        }

        public List<string> Vérification()
        {
            
            List<string> ensemble = new List<string>();
            string mot = "";
            for (int i=0; i<grille.GetLength(0);i++)  //cas des mots à l'horizontal
            {
                
                for (int j=0;j<grille.GetLength(1);j++)
                {
                    
                    
                    if ((grille[i, j] == ' '))
                    {
                        if(mot.Length>=2)
                        {
                            ensemble.Add(mot);
                            mot = "";
                        }
                        else
                        {
                            mot = "";
                        }
                        
                    }
                    else if(grille[i,j]!=' ')
                    {
                        mot = mot + grille[i, j];
                    }
                    if ((j == grille.GetLength(1) - 1) && (mot.Length >= 2))
                    {
                        ensemble.Add(mot);
                        mot = "";
                    }

                }
            }
            for (int a = 0; a < grille.GetLength(1); a++)
            {
                for (int b = 0; b < grille.GetLength(0); b++)
                {
                    if (grille[b, a] == ' ')
                    {
                        if(mot.Length>=2)
                        {
                            ensemble.Add(mot);
                            mot = "";
                        }
                        else
                        {
                            mot = "";
                        }
                        
                    }
                    else if (grille[b, a] != ' ')
                    {
                        mot = mot + grille[b, a];
                    }
                    if ((mot.Length >= 2) && (b == grille.GetLength(0) - 1))
                    {

                        ensemble.Add(mot);
                        mot = "";
                    }
                }

            }
            return ensemble;
        }


        public bool Valable(int tour)
        {
            List<string> ensemble = new List<string>();
            string mot = "";
            bool reponse = true;
            List<List<int[]>> repertoire = new List<List<int[]>>();
            List<int[]> posMot = new List<int[]>();
            int[] tab = new int[2];
            if (tour > 1)
            {
                for (int i = 0; i < grille.GetLength(0); i++)  //cas des mots à l'horizontal
                {

                    for (int j = 0; j < grille.GetLength(1); j++)
                    {


                        if ((grille[i, j] == ' '))
                        {
                            if (mot.Length >= 2)
                            {
                                ensemble.Add(mot);
                                repertoire.Add(posMot);
                                posMot = new List<int[]>();
                                mot = "";
                            }
                            else
                            {
                                mot = "";
                            }

                        }
                        else if (grille[i, j] != ' ')
                        {
                            mot = mot + grille[i, j];
                            tab[0] = i;
                            tab[1] = j;
                            posMot.Add(tab);
                            tab = new int[2];
                        }
                        if ((j == grille.GetLength(1) - 1) && (mot.Length >= 2))
                        {
                            ensemble.Add(mot);
                            repertoire.Add(posMot);
                            posMot = new List<int[]>();
                            mot = "";
                        }

                    }



                }
                for (int a = 0; a < grille.GetLength(1); a++)
                {
                    for (int b = 0; b < grille.GetLength(0); b++)
                    {
                        if (grille[b, a] == ' ')
                        {
                            if (mot.Length >= 2)
                            {
                                ensemble.Add(mot);
                                repertoire.Add(posMot);
                                posMot = new List<int[]>();
                                mot = "";
                            }
                            else
                            {
                                mot = "";
                            }

                        }
                        else if (grille[b, a] != ' ')
                        {
                            mot = mot + grille[b, a];
                            tab[0] = b;
                            tab[1] = a;
                            posMot.Add(tab);
                            tab = new int[2];
                        }
                        if ((mot.Length >= 2) && (b == grille.GetLength(0) - 1))
                        {
                            ensemble.Add(mot);
                            repertoire.Add(posMot);
                            posMot = new List<int[]>();
                            mot = "";
                        }
                    }

                }
            }
            foreach (List<int[]> contenu in repertoire)
            {
                foreach (int[] indexChar in contenu)
                {
                    int[] comparer = indexChar;

                }
            }
            return reponse;

        }
    }
}
