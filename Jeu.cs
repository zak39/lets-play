using System;
using System.Text; // import le StringBuider

namespace lets_play
{
    public enum Mode { pendu,bescherelle }; // On a cree une variable qui est globale dans toutes les classes et fonctions si
                                            // elles font parties du namespece "lets_play".
                                            // Pourtant on me dit que `Mode` est un type. Pourqoi ? 

    interface Int_Jeu
    {
        void joue(char caractere);
    }

    // public class Classe
    // {

    // }

    public class Orthogenie : Int_Jeu
    /// This class allows to game the "pendu" or "bescherelle".
    {
        // constructeur
        public string solution, mot;
        public Orthogenie (string solution, string mot) { this.solution = ""; this.mot = ""; }
        // public Orthogenie (string solution) { this.solution = System.String.Empty; }

        public int points;
        public Orthogenie (int points) { this.points = 0; }

        public Mode mode;
        public Orthogenie (Mode mode) { this.mode = mode; }


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
                // Console.WriteLine(this.solution[compteur]);
                if ( this.solution[compteur] == caractere )
                {
                    motMystere[compteur] = caractere;
                    this.mot = motMystere.ToString();
                    this.points = this.points + 1;
                }
            }

        }

    }

    // public class Revision : Orthogenie
    // {

    //     public Revision(int points, string prenom): base(points)
    //     { this.prenom = prenom ;}
    // }   

    public class Jeu
    /// This class allows to execute the game with the method "main".
    {

        static void Main(string[] args)
        {
           
            Mode mode;
            Orthogenie monjeu = new Orthogenie("girafe","girafe");
            monjeu.solution = "girafe";
            monjeu.mode = Mode.pendu;
        
            monjeu.motMystere(monjeu.solution);

            Console.WriteLine(monjeu.mode);

            while(monjeu.mot.Contains('*'))
            {
                Console.WriteLine("Saisir une lettre alphanumerique au clavier : ");
                string lettre_keyboard = Console.ReadLine();
                while( lettre_keyboard.Length > 1 )
                {
                    Console.WriteLine("Vous devez saisir qu'UN seul caractère alphanumerique : ");
                    lettre_keyboard = Console.ReadLine();
                }
                char lettre = Convert.ToChar(lettre_keyboard);
                monjeu.joue(lettre);
                Console.WriteLine(monjeu.mot);
            }
            Console.WriteLine("J'ai gagne avec " + Convert.ToString(monjeu.points) + " points !" );
            Console.WriteLine(monjeu.points);

        }

    }

}