using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MIXMO_Albert_de_WATRIGANT
{
    public class Program
    {
        static void Main(string[] args)
        {
            Lettres pioche = CréationPioche(); // la pioche est céée et est un instance de la classe lettres
            //Console.WriteLine(pioche.toString());
            Dictionnaire Dico = CréationDictionnaire(); //le dico est créée est un instance de la classe dictionnaire
            Lettres référence = CréationPioche();  //on cée une seconde pioche auquelle on ne touchera pas, elle servira de point de comparaison
            //Dico.toString();
            Random r = new Random();
            Joueur Nathan = new Joueur("nathan");// création de joueurs
            Joueur Mathieu = new Joueur("mathieu");
            Nathan.Add_lettres(6, pioche, r);//création de mains des joueurs
            Mathieu.Add_lettres(6, pioche, r);
            List<Joueur> participants = new List<Joueur>();
            participants.Add(Mathieu);
            participants.Add(Nathan);
            
            

            while(pioche.Collection.Count()>0)
            {
                foreach(Joueur a in participants)
                {
                    AfficherMatrice(MelangeMatrice(a.MotsCroisés, AffichageGrille())); //affichage de base des grilles des joueurs
                    Console.WriteLine(a.toString());
                    Console.WriteLine(" ");
                    a.Numéro = participants.IndexOf(a)+1;
                    
                }
                Console.WriteLine("Saisissez numéro du joueur : "); //on choisit le joueur qui pose un mot
                int choix = Convert.ToInt32(Console.ReadLine());
                foreach(Joueur a in participants)
                {
                    if(a.Numéro==choix)
                    {
                        Console.WriteLine(a.Nom + ", saisissez un nouveau mot trouvé : ");
                        string mot = Console.ReadLine().ToUpper();
                        while((mot.Length<2)||(Dico.RechDichoRecursif(0,mot,Dico.MotsPossibles.ElementAt(mot.Length-2).Count())==false)||(a.Possession1(mot)==false))//on vérifie la validité du mot
                        {
                            Console.WriteLine(a.Nom + ", saisissez un nouveau mot VALIDE : ");
                            mot = Console.ReadLine().ToUpper();
                        }
                        Console.WriteLine("quelle ligne : ");   //on choisit l'emplacement du mot sur la grille
                        int ligne = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("quelle colonne :");
                        int colonne = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("quel sens : ");
                        string sens = Console.ReadLine().ToUpper();
                        char lettreManquante=a.AjoutGrille(mot, ligne, colonne, sens);
                        if (a.Trouves.Count() > 0)//si un mot est déjà sur la grille, le nouveau mot doit être lié à un ancien
                        {
                            while (lettreManquante == ' ')
                            {
                                Console.WriteLine("votre mot n'est en lien avec aucun autre sur votre grille : ");
                                Console.WriteLine(a.Nom + ", saisissez un nouveau mot trouvé : ");
                                mot = Console.ReadLine().ToUpper();
                                while ((mot.Length < 2) || (Dico.RechDichoRecursif(0, mot, Dico.MotsPossibles.ElementAt(mot.Length - 2).Count()) == false) || (a.Possession1(mot) == false))
                                {
                                    Console.WriteLine(a.Nom + ", saisissez un nouveau mot VALIDE : ");
                                    mot = Console.ReadLine().ToUpper();
                                }
                                Console.WriteLine("quelle ligne : ");
                                ligne = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("quelle colonne :");
                                colonne = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("quel sens : ");
                                sens = Console.ReadLine().ToUpper();
                                lettreManquante = a.AjoutGrille(mot, ligne, colonne, sens);
                            }
                        }

                        a.OteLettre1(mot);//on enlève de la main du joueur les lettres qu'il a utilisées
                        foreach(Joueur player in participants) //chaque joueur tire deux lettres dans la pioche
                        {
                            player.Add_lettres(2, pioche, r);
                        }
                        
                        a.Add(mot,référence);//on ajoute le mot à la liste du joueur, et augmente son score s'il le faut
                        
                        MotsCroises grilleActu = new MotsCroises(a.MotsCroisés);
                        foreach (string element in grilleActu.Vérification())
                        {
                            if (Dico.RechDichoRecursif(0, element, Dico.MotsPossibles.ElementAt(element.Length - 2).Count()) == false)// si des mots ont été cée sur la grille et qu'ils ne sont pas valables, la partie s'arrête
                            {
                                Console.WriteLine("mots pas valables; fin de la partie");
                                break;
                            }



                        }
                    }
                }
                

            }
            

            //char[,] grille = new char[,] { { 'A', ' ', 'H' }, { ' ', 'D', ' ' }, { 'V', 'I', 'Y' } };
            //char[,] grille1 = new char[10, 10];
            //for(int i=0;i<grille1.GetLength(0);i++)
            //{
            //    for(int j=0;j<grille1.GetLength(1);j++)
            //    {
            //        if(j%2==0)
            //        {
            //            grille1[i, j] = 'A';
            //        }
            //        else
            //        {
            //            grille1[i, j] = 'B';
            //        }
            //    }
            //}
            //AfficherMatrice(MelangeMatrice(grille1, AffichageGrille()));
            //MotsCroises Test = new MotsCroises(grille);

            //Console.WriteLine(Test.Vérification().Count());
            //foreach (string z in Test.Vérification())
            //{
            //    Console.WriteLine(z + "; ");
            //}
            Console.ReadKey();
        }
        static string[,] AffichageGrille()
        {
            string[,] matrice = new string[12, 33];
            for(int i=0; i<matrice.GetLength(0);i++)  //on remplit la matrice avec des espaces
            {
                for(int j=0;j<matrice.GetLength(1);j++)
                {
                    matrice[i, j] = " ";
                }
            }
            for (int i = 2; i < 12; i++)  //délimitation des colonnes
            {
                for (int j = 3; j < matrice.GetLength(1);j=j+3)
                {
                    if(i==11)
                    {
                        matrice[i, j - 1] = "|";
                    }
                    else
                    {
                        matrice[i, j] = "|";
                    }
                    
                }
            }
            int nb = 1;
            for(int i=4; i<matrice.GetLength(1);i=i+3) //remplissage premiere ligne
            {
                matrice[0, i] = Convert.ToString(nb);
                nb++;
            }
            nb = 1;
            for (int i = 1; i < matrice.GetLength(1); i++)// remplissage deuxième ligne
            {
                matrice[1, i] = "=";
            }
            for(int i=2;i<12;i++) //remplissage première colonne
            {
                matrice[i, 1] = Convert.ToString(nb);
                nb++;
            }

            return matrice;
        }
        static string [,] MelangeMatrice(char[,] ajout, string[,] matrice)
        {
            int ligne = 0;
            int colonne = 0;
            for (int i = 2; i < 12;i++)
            {
                for(int j=4;j<matrice.GetLength(1);j=j+3)
                {
                    if(i==11)
                    {
                        matrice[i, j-1] = Convert.ToString(ajout[ligne, colonne]);
                        colonne++;
                    }
                    else
                    {
                        matrice[i, j] = Convert.ToString(ajout[ligne, colonne]);
                        colonne++;
                    }
                    
                }
                ligne++;
                colonne = 0;
            }
            return matrice;
        }
        static void AfficherMatrice(string [,] matrice)
        {
            for (int i=0;i<matrice.GetLength(0);i++)
            {
                for(int j=0;j<matrice.GetLength(1);j++)
                {
                    Console.Write(matrice[i, j] );
                }
                Console.WriteLine(" ");
            }
        }
        static Lettres CréationPioche()
        {
            List<Lettre> att = new List<Lettre>();
            string fichier = "Lettre.txt";
            StreamReader fichLect = new StreamReader(fichier);
            char[] sep = new char[1] { ',' };
            string ligne = " ";
            string[] datas = new string[6];
            while (fichLect.Peek() > 0)
            {
                ligne = fichLect.ReadLine();
                datas = ligne.Split(sep);
                string symbole = datas[0];
                int quantité = int.Parse(datas[1]);
                int poids = Convert.ToInt32(datas[2]);
                for (int i = 0; i < quantité; i++)
                {
                    Lettre x = new Lettre(symbole, poids);
                    att.Add(x);
                }
            }
            Lettres pioche = new Lettres(att);
            return pioche;
            
        }
        static Dictionnaire CréationDictionnaire()
        {
            int a;
            List<List<string>> finale = new List<List<string>>();
            string fichier1 = "MotsPossibles1.txt";
            StreamReader fichLect1 = new StreamReader(fichier1);
            string ligne1 = "";
            List<string> datas1 = new List<string>();
            while (fichLect1.Peek() > 0)
            {
                ligne1 = fichLect1.ReadLine();
                string[] ensemble = ligne1.Split(' ');
                if (int.TryParse(ligne1, out a))
                {
                    if (Convert.ToInt32(ensemble[0]) > 2)
                    {
                        finale.Add(datas1);
                        datas1 = new List<string>();
                    }
                }
                else
                {
                    for (int i = 0; i < ensemble.Length; i++)
                    {
                        datas1.Add(ensemble[i]);
                    }

                }
            }
            Dictionnaire total = new Dictionnaire(finale);
            return total;
        }
       
    }
}
