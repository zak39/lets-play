using System;
using System.Text; // import le StringBuider
using System.Collections.Generic;   // pour manipuler les List
using System.IO;   // pour importer un fichier
using System.Linq;   // pour utiliser des listes

namespace lets_play
{
    public enum Mode { pendu,bescherelle }; // On a cree une variable qui est globale dans toutes les classes et fonctions si
                                            // elles font parties du namespece "lets_play".
                                            // Pourtant on me dit que `Mode` est un type. Pourqoi ? 

    interface Int_Jeu
    {
        void joue(char caractere);
    }

    //  A decomenter
    public class Classe
    {
        // Constructeur
        public Classe() { }

        public List<Orthogenie> list_Orthogenie = new List<Orthogenie>()
        {
        };

        public List<Revision> list_Revision = new List<Revision>()
        {

        };

        private List<Orthogenie> list_Orthogenie_pub;

        public Classe(List<Orthogenie> list_Orthogenie)
        {
            this.list_Orthogenie_pub = list_Orthogenie;
        }

        public List<Revision> classement;
        public Classe(List<Revision> list_revision, List<Revision> classement)
        {
            this.list_Revision = list_revision;
            this.classement = classement;
        }

        /*public Classe (List<String> classement) 
        {
            this.classement = new List<String>();
            this.list_Orthogenie_pub = new List<Orthogenie>();
        }*/



        public List<Orthogenie> List_Orthogenie_pub
        {
            get => this.list_Orthogenie_pub;
            set => this.list_Orthogenie_pub = value;
        }

        public List<Revision> Classement
        {
            get => this.classement;
            set => this.classement = value;
        }

        public List<Revision> List_Revision
        {
            get => this.list_Revision;
            set => this.list_Revision = value;
        }

        public void Classer()
        {
            //var result01 = this.list_Revision.OrderByDescending(a => a.note).ThenBy(a => a.prenom);
            var result01 = this.list_Revision.OrderByDescending(a => a.note);       // Source : https://www.codeproject.com/Tips/761275/How-to-Sort-a-List
            this.classement = new List<Revision>();
            foreach (Revision element_revision in result01)
            {
                this.classement.Add(element_revision);
            }

            /*foreach (Revision element in this.classement)
            {
                Console.WriteLine("Prenom : "+element.prenom);
                Console.WriteLine("Score : "+element.note);
                Console.WriteLine("--");
            }*/

        }

        /*public void Charge(string pathRelative="./file.txt")*/ // For Linux
        // public void Charge(string pathRelative="E:\\docs\\code\\csharp\\01_pendu-bescherelle\\lets-play\\file.txt")
        public void Charge(string pathRelative="./file.txt") // For Linux
        {
           /* string readText = File.ReadAllText(pathRelative);
            List<string> listReadText = readText.Split(',').ToList();
            Console.WriteLine(readText);
            Console.WriteLine(listReadText);*/

            using (var reader = new StreamReader(@pathRelative)) // https://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array
            {
                List<string> listPernom = new List<string>();
                List<string> listSolution = new List<string>();
                List<string> listMot = new List<string>();
                List<int> listScore = new List<int>();

                while( !reader.EndOfStream )
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    listPernom.Add(values[0]);
                    listSolution.Add(values[1]);
                    listMot.Add(values[2]);
                    listScore.Add(int.Parse(values[3]));
                }

                int lenPrenom = listPernom.Count;
                int lenSolution = listSolution.Count;
                int lenMot = listMot.Count;
                int lenScore = listScore.Count;

                if ( lenSolution == lenMot )
                {
                    for( int i = 0 ; i <= lenMot-1 ; i++ )
                    {
                        Orthogenie new_orthogenie = new Orthogenie();
                        new_orthogenie.solution = listSolution[i];
                        new_orthogenie.mot = listMot[i];
                        new_orthogenie.points = listScore[i];

                        Revision new_revision = new Revision();
                        new_revision.prenom = listPernom[i];
                        new_revision.note = listScore[i];

                        //this.list_Orthogenie_pub.Add(new_object); // Error -> System.NullReferenceException : 'Object reference not set to an instance of an object.'
                        this.list_Orthogenie.Add(new_orthogenie); // Error -> System.NullReferenceException : 'Object reference not set to an instance of an object.'
                        this.list_Revision.Add(new_revision); // Error -> System.NullReferenceException : 'Object reference not set to an instance of an object.'
                    }
                }
            }
        }

    }

    public class Orthogenie : Int_Jeu
    /// This class allows to game the "pendu" or "bescherelle".
    {
        // constructeur
        public string solution, mot;
        public Orthogenie () {  }   // Constructeur vide
        public Orthogenie (string solution, string mot) { this.solution = ""; this.mot = ""; }
        // public Orthogenie (string solution) { this.solution = System.String.Empty; }

        public int points;
        public Orthogenie (int points) { this.points = 0; }

        public Mode mode;
        public Orthogenie (Mode mode) { this.mode = Mode.pendu; }


        // getters and setters
        public string Solution
        {
            get => this.solution;
            set => this.solution = value;
        }

        public int Points
        {
            get => this.points;
            set => this.points = value;
        }

        public string Mot
        {
            get => this.mot;
            set => this.mot = value;
        }


        // Methodes
        public void motMystere( string monMot )
        /// Initialize the word mystery with stars and it will print to end user.
        {
            int compteur;

            for ( compteur = 0; compteur <= this.solution.Length-1; compteur++)
            {
                this.mot = this.mot + "*";
            }
            
        }

        public void joue(char caractere)
        /// This method allows to execute the "pendu" or "bescherelle" game. 
        {
            // Jeu du pendu
            int compteur;
            StringBuilder motMystere = new StringBuilder(this.mot);
            
            for( compteur = 0; compteur <= this.solution.Length-1; compteur++ )
            {
                if ( this.mode == Mode.pendu )
                {
                    if ( this.solution[compteur] == caractere )
                    {
                        motMystere[compteur] = caractere;
                        this.mot = motMystere.ToString();
                        this.points = this.points + 1;
                    }
                }
                if ( this.mode == Mode.bescherelle )
                {
                    
                    // Console.WriteLine("-------");
                    // Console.WriteLine(this.solution[compteur]);
                    // Console.WriteLine(this.mot[compteur]);
                    // Console.WriteLine(motMystere[compteur]);
                    // Console.WriteLine(caractere);

                    // 'g' == 'g' && 'g' =! '*'
                    // 'g' == 'i' && '*' =! '*'
                    // 1 && 1
                    // if ( this.solution[compteur] == caractere && this.mot[compteur] != '*' )
                 
                    if ( this.mot[compteur] == '*' )
                    {
                        if (this.solution[compteur] == caractere )
                        {
                            motMystere[compteur] = caractere;
                            this.mot = motMystere.ToString();
                            this.points = this.points + 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

            }

        }

    }

    public class Revision : Orthogenie
    {

        // constructeur
        public string solution, mot;
        public Revision() { }
        public Revision(string solution, string mot)
        : base(solution, mot)
        {
        }

        public int points, note;
        public Revision(int points, int note)
        :base(points)
        {
        }
        
        public string prenom;
        public Revision (string prenom) { this.prenom = "unknown"; }

        public string Prenom
        { 
            get => this.prenom; 
            set => this.prenom = value;
        }


        public int Note
        {
            get => this.note;
            set => this.note = value;
        }
    }   

    public class Jeu
    /// This class allows to execute the game with the method "main".
    {

        static void Main(string[] args)
        {
           
            // Mode mode;
            Orthogenie monjeu = new Orthogenie();
            Revision joueur = new Revision("unknown");
        
            while ( joueur.prenom == "unknown" ){
                Console.WriteLine("Quel est votre prenom ? Autre que 'unknown'");
                joueur.prenom = Console.ReadLine();
            }
               

            Console.WriteLine("Merci de saisir soit 'pendu' soit 'bescherelle' sans la cote !");
            
            string mon_mode_jeu = null;

            // string mon_mode_jeu = null;
            // while( mon_mode_jeu != "pendu" || mon_mode_jeu != "bescherelle")
            while( String.IsNullOrEmpty(mon_mode_jeu) )
            {
                mon_mode_jeu = Console.ReadLine();

                if ( mon_mode_jeu == "pendu" )
                {
                    monjeu.mode = Mode.pendu;       
                    //joueur.mode = Mode.pendu;       

                }
                else if ( mon_mode_jeu == "bescherelle" )
                {
                    monjeu.mode = Mode.bescherelle;
                    //joueur.mode = Mode.bescherelle;       
                }
                else
                {
                    Console.WriteLine("Le mode de jeu par defaut est donc le Pendu !");
                    monjeu.mode = Mode.pendu;
                    //joueur.mode = Mode.pendu;       
                    // Console.WriteLine("Il y a un probleme dans le programme !");
                }

            }

            monjeu.solution = "girafe";
            //joueur.solution = "elephant";

            //Console.WriteLine("solution : " + joueur.solution);
            //Console.WriteLine("solution : " + joueur.mode);
            //string pause = Console.ReadLine();
            //joueur.motMystere(joueur.solution);
            monjeu.motMystere(monjeu.solution);

            Console.WriteLine(monjeu.mode);
            //Console.WriteLine(joueur.mode);

            int nbDeCoup = monjeu.solution.Length;
            //int nbDeCoup = joueur.solution.Length;

            // monjeu
            while (monjeu.mot.Contains('*') && nbDeCoup != 0)
            {
                Console.WriteLine("Saisir une lettre alphanumerique au clavier : ");
                string lettre_keyboard = Console.ReadLine();
                while (lettre_keyboard.Length > 1)
                {
                    Console.WriteLine("Vous devez saisir qu'UN seul caractère alphanumerique : ");
                    lettre_keyboard = Console.ReadLine();
                }
                char lettre = Convert.ToChar(lettre_keyboard);
                monjeu.joue(lettre);
                Console.WriteLine(monjeu.mot);
                nbDeCoup -= 1;
                Console.WriteLine(Convert.ToString(nbDeCoup));
            }

            // joueur
            /*while (joueur.mot.Contains('*') && nbDeCoup != 0)
            {
                Console.WriteLine("Saisir une lettre alphanumerique au clavier : ");
                string lettre_keyboard = Console.ReadLine();
                while (lettre_keyboard.Length > 1)
                {
                    Console.WriteLine("Vous devez saisir qu'UN seul caractère alphanumerique : ");
                    lettre_keyboard = Console.ReadLine();
                }
                char lettre = Convert.ToChar(lettre_keyboard);
                joueur.joue(lettre);
                Console.WriteLine(joueur.mot);
                nbDeCoup -= 1;
                Console.WriteLine(Convert.ToString(nbDeCoup));
            }*/

            joueur.note = monjeu.points;

            // A decomenter
            Console.WriteLine("Le joueur.euse " + joueur.prenom + " a gagne.e avec " + Convert.ToString(joueur.note) + " points !");
            /*Console.WriteLine(monjeu.points);*/

            Classe classer = new Classe();
            classer.Charge();
            classer.Classer();

            // foreach(Revision score in classer.classement)
            // {
            //     Console.WriteLine(score.prenom + " - " + score.note);
            // }
        }

    }

}