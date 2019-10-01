using System;
using System.Text; // import le StringBuider

namespace lets_play
{
    public enum Mode { pendu,bescherelle };

    interface IJeu
    {
        void joue(string caractere);
    }

    // public class Revision
    // {

    // }   

    // public class Classe
    // {

    // }

    public class Orthogenie
    {
        // constructeur
        public string solution, mot;
        public Orthogenie (string solution, string mot) { this.solution = ""; this.mot = ""; }
        // public Orthogenie (string solution) { this.solution = System.String.Empty; }

        public int points;
        public Orthogenie (int points) { this.points = 0; }

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
        {
            int compteur;

            for ( compteur = 0; compteur <= this.solution.Length-1; compteur++)
            {
                this.mot = this.mot + "*";
            }
            
        }

        public void joue(char caractere)
        {
            // Jeu du pendu
            int compteur;
            StringBuilder motMystere = new StringBuilder(this.mot);
            Console.WriteLine(this.solution.Length);

            for( compteur = 0; compteur <= this.solution.Length-1; compteur++ )
            {
                // Console.WriteLine(this.solution[compteur]);
                if ( this.solution[compteur] == caractere )
                {
                    motMystere[compteur] = caractere;
                    this.mot = motMystere.ToString();
                }
                else{
                    points = points + 1;
                }
            }

        }

    }

    public class Jeu
    {

        // // constructeur
        // public int point;
        // public Orthogenie (int point) { this.point = 0; }

        // public int Point
        // {
        //     get => this.point;
        //     set => this.point = value;
        // }


        static void Main(string[] args)
        {
           
            // Mode m;
            // m = Mode.pendu;
            
            Orthogenie monjeu = new Orthogenie("girafe","girafe");
            monjeu.solution = "girafe";
            // Console.WriteLine(monjeu.solution);
            // Console.WriteLine(monjeu.points);
            monjeu.motMystere(monjeu.solution);
            // Console.WriteLine(monjeu.mot);
            // //--
            // monjeu.joue('f');
            // Console.WriteLine(monjeu.mot);
            // Console.WriteLine(monjeu.points);
            // //--
            // monjeu.joue('o');
            // Console.WriteLine(monjeu.mot);
            // Console.WriteLine(monjeu.points);
            // //--
            // monjeu.joue('a');
            // Console.WriteLine(monjeu.mot);
            // Console.WriteLine(monjeu.points);

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
                Console.WriteLine(monjeu.points);

            }

        }

    }

}