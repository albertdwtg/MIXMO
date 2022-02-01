using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIXMO_Albert_de_WATRIGANT
{
    public class Joueur
    {
        private string nom;
        private int score;
        private List<Lettre> perso;
        private List<string> trouves = new List<string>();
        private char[,] motsCroisés;
        private int numéro;

        public Joueur(string nom)
        {
            this.nom = nom;
            this.score = 0;
            this.perso = new List<Lettre>();
            char[,] creation = new char[10, 10];
            for(int i=0; i<10;i++)
            {
                for(int j=0;j<10;j++)
                {
                    creation[i, j] = ' ';
                }
            }
            this.motsCroisés = creation;
            this.numéro = 0;
        }
        public List<Lettre> Perso => perso;
        public char[,] MotsCroisés => motsCroisés;
        public int Numéro
        {
            get
            {
                return this.numéro;
            }
            set
            {
                this.numéro = value;

            }
        }
        public string Nom => nom;
        public List<string> Trouves
        {
            get
            {
                return this.trouves;
            }
            set
            {
                this.trouves = value;
            }
        }

        public bool Add_lettres(int nb, Lettres pioche, Random r)//méthode permettant d'ajouter des lettres dans la main du joueur, tirées depuis la pioche aléatoirement
        {
            int j = pioche.Collection.Count;
            bool ajout = false;
            
            for (int i = 0; i < nb; i++)
            {
                int hasard=r.Next(0, j); //le r prend une nouvelle valeur aléatoire à tour de la boucle
                j--;  //on décrémente j car on retire des éléments de la pioche, donc sa taille diminue
                this.perso.Add(pioche.Collection[hasard]);
                pioche.Collection.RemoveAt(hasard);
                ajout = true;
            }
            return ajout;
        }
        public string toString()//méthode d'affichage des caractéristiques du joueur
        {
            string réponse = nom + "; " + "score : " + score + "; " + "main du joueur : ";
            foreach(Lettre l in this.perso)
            {
                réponse = réponse + l.Symbole + " ";
            }
            return réponse;
        }
        public char  AjoutGrille(string mot, int ordonnées, int abscisses, string choix) //ajoute un mot sur la grille selon la position et le sens indiqués
        {
            char lettreManquante = ' '; //si ce caractère est toujours vide à la fin de la fonction, c'est que le mot ajouté n'a croisé aucun autre mot sur la grille
            int ligne = ordonnées - 1;
            int colonne = abscisses - 1;
            int index1 = 0;
            int index2 = 0;
            int index3 = 0;
            char[] tab = new char[mot.Length];
            foreach(char c in mot) //décomposition de la chaine en caractères
            {
                tab[index1] = c;
                index1++;
            }

            while (choix != "H" && choix != "V") //si l'utilisateur n'a pas rentré un bon paramètre
            {
                Console.WriteLine("Choisissez V si vous le voulez à la verticale, sinon H: ");
                choix = Console.ReadLine().ToUpper();
            }
            if (choix=="H") //cas d'un mot à l'horizontal
            {
                while(this.motsCroisés.GetLength(1)<colonne+mot.Length)//si le mot va sortir des limites de la grille
                {
                    Console.WriteLine("le mot ne rentre pas, choisissez un numero de colonne plus petit : ");
                    colonne = Convert.ToInt32(Console.ReadLine())-1;
                }
                for (int i=colonne;i<colonne+mot.Length;i++)
                {
                    if(this.motsCroisés[ligne,i]==' ')
                    {
                        this.motsCroisés[ligne, i] = tab[index2];
                        index2++;
                    }
                    
                    else if(this.motsCroisés[ligne, i] == tab[index2])
                    {
                        lettreManquante = this.motsCroisés[ligne, i];
                    }
                    
                }
            }
            if (choix == "V")// cas des mots à la verticale
            {
                while (this.motsCroisés.GetLength(0) < ligne + mot.Length)//si le mot va sortir des limites de la grille
                {
                    Console.WriteLine("le mot ne rentre pas, choisissez un numero de ligne plus petit : ");
                    ligne = Convert.ToInt32(Console.ReadLine())-1;
                }
                for(int j=ligne;j<ligne+mot.Length;j++)
                {
                    if(this.motsCroisés[j,colonne]==' ')
                    {
                        this.motsCroisés[j, colonne] = tab[index3];
                        index3++;
                    }
                    if(this.motsCroisés[j,colonne]==mot[j-ligne])
                    {
                        this.motsCroisés[j, colonne] = mot[j - ligne];
                        lettreManquante = this.motsCroisés[j, colonne];
                    }
                    else if(this.motsCroisés[j, colonne] == tab[index3])
                    {
                        lettreManquante = this.motsCroisés[j, colonne];
                    }
                    
                }
            }
            if(this.trouves.Count()>0)//si le mot a été écrit n'importe où sur la grille, on l'efface de la grille (valable que s'il y avait déjà un mot sur la grille
            {
                if ((lettreManquante == ' ') && (choix == "H"))
                {
                    for (int i = colonne; i < colonne + mot.Length; i++)
                    {
                        this.motsCroisés[ligne, i] = ' ';
                    }
                }
                if ((lettreManquante == ' ') && (choix == "V"))
                {
                    for (int i = ligne; i < ligne + mot.Length; i++)
                    {
                        this.motsCroisés[i, colonne] = ' ';
                    }
                }
            }
            return lettreManquante;
            
        }
        
        public void Add(string mot,Lettres référence)//ajoute un mot à la liste du joueur et calcule son score en comparant avec le poids de chaque lettre de la pioche de référence
        {
            this.trouves.Add(mot);
            if (mot.Length >= 5)
            {
                this.score = this.score + mot.Length;
            }
            foreach (char c in mot)
            {
                foreach (Lettre l in référence.Collection)
                {
                    string conversion = Convert.ToString(c);
                    if (conversion == l.Symbole)
                    {
                        this.score = this.score + l.Poids;
                    }
                }
            }
        }
        
        public void OteLettre(string mot)//retire les lettres de la main du joueur en fonction du mot qu'il vient de poser
        {
            
            List<Lettre> copie =new List<Lettre>();  // on crée une copie de la main du joueur afin d'éviter les erreurs dûes au changement de taille de liste
            foreach(Lettre a in this.perso)
            {
                copie.Add(a);
            }
            
            foreach(Lettre l in copie)
            {
                foreach(char c in mot)
                {
                    string conversion = Convert.ToString(c);
                    if(conversion==l.Symbole)
                    {
                        this.perso.Remove(l);
                        
                    }
                }
            }
            
            
        }
        

        
        public bool Possession(string mot)//permet de dire si le joueur peut créer un mot avec les lettres qu'il a 
        {
            bool réponse = true;
            string chaine = "";
            int compteurManquant = 0;
            foreach(Lettre l in this.perso)
            {
                chaine = chaine + l.Symbole;
            }
            for(int i=0;i<mot.Length;i++)
            {
                if (chaine.Contains(mot[i])==false)
                {

                    compteurManquant++;
                }
            }
            if(compteurManquant>1)//il peut y avoir un false car peut etre une lettre est déja sur le plateau
            {
                réponse = false;
            }
            return réponse;
        }
        
        
    }
}
