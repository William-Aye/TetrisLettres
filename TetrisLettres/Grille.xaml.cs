using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TetrisLettres
{
    /// <summary>
    /// Logique d'interaction pour Grille.xaml
    /// </summary>
    public partial class Grille : Page
    {
        private string mot = "";
        public static string motJoueur = "";
        private int colonneMat, ligneMat;
        private static int tailleGrille, temps = 0;
        private Plateau[,] plat = null;
        private static Joueur joueurActuelle;
        private static Jeu jeuPlateau;
		private static Dictionnaire dico = new Dictionnaire(0, "Français", "Data//Mots_Français.txt");
		private static List<Joueur> joueurListe = new List<Joueur>();
        public static bool grilleVide;
        private DispatcherTimer timeEvent = new DispatcherTimer();

        internal static Joueur JoueurActuelle { get { return joueurActuelle; } set { joueurActuelle = value; } }

        public Grille()
        {
            InitializeComponent();

            plat = new 
                
                
                Plateau[tailleGrille, tailleGrille];
            RemplissagePlat("Test1");

            GenererGrille();
            joueurListe.Add(new Joueur("joueur1", new List<string>(), 0));
            joueurListe.Add(new Joueur("joueur2", new List<string>(), 0));
            jeuPlateau = new Jeu(dico, null, joueurListe);
            timeEvent.Interval = TimeSpan.FromMilliseconds(500);
            timeEvent.Tick += GrilleTimerEvent;
            timeEvent.Start();
        }


        private void GrilleTimerEvent(object sender, EventArgs e)
        {
            if (Chrono.tempsRestantJeu == 0 || grilleVide)
            {
                timeEvent.Stop();

            }
            if (motJoueur != "" && motJoueur != null)
            {
				Queue<int[]> coordMot = new Queue<int[]>();
                if (dico.RechercheDicho(dico.DicoTrie, motJoueur))
                    coordMot = RechercheMot2(motJoueur);
                if (coordMot.Count > 0)
                {
                    DisparitionLettre(coordMot);
					Chrono.ResetChrono(true);
				}

				ChuteLettres();
                //RemplacerLettres();
                motJoueur = "";

                MAJGrille();
            }
        }

        private bool VerificationFin()
        {
            int comptLettre = 0;
			for (int j = 0; j < tailleGrille; j++)
				if (plat[tailleGrille-1, j].Cara == '*')
					comptLettre++;

            return (comptLettre == tailleGrille-1);
		}

		#region Recherche
        /// <summary>
        /// Permet la recherche du mot sur la grille
        /// </summary>
        /// <param name="motJoueur">Mot entré par le joueur</param>
        /// <returns>retourne les coordonnées du mot</returns>
		private Queue<int[]> RechercheMot1(string motJoueur)
		{
			int comptLettre = 0;
			int[] save = { tailleGrille - 1, 0 };
			Queue<int[]> coordMot = new Queue<int[]>();

			for (int j = 0; j < tailleGrille; j++)
				if (plat[save[0], j].Cara == motJoueur[0])
				{
					save[1] = j;
					coordMot.Enqueue(save);
				}

			Queue<int[]> saveCoord = new Queue<int[]>();
			Queue<int[]> tempoCoord = new Queue<int[]>();
			while (coordMot.Count > 0 && comptLettre < motJoueur.Length)
			{
				comptLettre = 1;
				save = coordMot.Dequeue();
				tempoCoord = new Queue<int[]>();
				tempoCoord.Enqueue(save);

				bool abandonRecherche = false;
				while (!abandonRecherche)
				{
					bool lettreTrouve = false;

					for (int i = -1; i <= 1; i++)
						for (int j = -1; j <= 1; j++)
						{
							int tempI = save[0] + i;
							int tempJ = save[1] + j;

							if (tempI >= 0
								&& tempJ >= 0
								&& tempI <= tailleGrille - 1
								&& tempJ <= tailleGrille - 1
								&& comptLettre < motJoueur.Length)
							{
								if (plat[tempI, tempJ].Cara == motJoueur[comptLettre])
								{
									save = new int[2];
									save[0] = tempI;
									save[1] = tempJ;
									tempoCoord.Enqueue(save);

									lettreTrouve = true;
									comptLettre++;
								}
							}
							else if (i == 1 && j == 1 && !lettreTrouve)
								abandonRecherche = true;
						}
				}
				if (tempoCoord.Count > saveCoord.Count)
					saveCoord = tempoCoord;
			}
			if (saveCoord.Count != motJoueur.Length)
				saveCoord = new Queue<int[]>();

			return saveCoord;
		}

        /// <summary>
        /// Permet la recherche du mot sur la grille
        /// </summary>
        /// <param name="motJoueur">Mot entré par le joueur</param>
        /// <returns>retourne les coordonnées du mot</returns>
		private Queue<int[]> RechercheMot2(string motJoueur)
		{
			int comptLettre = 0;
			Queue<int[]> coordLettre = new Queue<int[]>();
			int[] save = { tailleGrille - 1, 0 };
			for (int j = 0; j < tailleGrille; j++)
			{
				if (plat[tailleGrille - 1, j].Cara == motJoueur[0])
				{
					save[1] = j;
					coordLettre.Enqueue(save);
				}
			}
			string[] retour = null;
			foreach (int[] saveCoord in coordLettre)
			{
				retour = Recurrence(saveCoord[0], saveCoord[1], motJoueur, 1, [save[0] + "," + save[1]]);
				if (retour != null)
					break;
			}
			Queue<int[]> envoyer = new Queue<int[]>();
			if (retour != null)
			{
				foreach (string a in retour)
				{
					string[] b = a.Split(',');
					envoyer.Enqueue([Convert.ToInt32(b[0]), Convert.ToInt32(b[1])]);
				}
			}
			return envoyer;
		}

		private string[] Recurrence(int iLettre, int jLettre, string mot, int index, string[] coordLettre)
		{
			if (index == mot.Length)
			{
				return coordLettre;
			}
			string[] coordLettreAvancee = new string[coordLettre.Length + 1];
			for (int i = 0; i < coordLettre.Length; i++)
			{
				coordLettreAvancee[i] = coordLettre[i];
			}
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					int tempI = iLettre + i;
					int tempJ = jLettre + j;
					try
					{
						if (plat[tempI, tempJ].Cara == mot[index]
							&& !coordLettreAvancee.Contains(tempI + "," + tempJ))
						{
							coordLettreAvancee[coordLettreAvancee.Length - 1] = tempI + "," + tempJ;
							return Recurrence(tempI, tempJ, mot, ++index, coordLettreAvancee);
						}
					}
					catch { }
				}
			}
			return null;
		}
		#endregion

		#region RetirerLettre
        /// <summary>
        /// Cette méthode permet de retirer les lettres des mots déjà trouvé par le joueur
        /// </summary>
        /// <param name="coordMot">Le paramètre corresponds au coordonnées des lettres à supprimer</param>
		private void DisparitionLettre(Queue<int[]> coordMot)
        {
            while (coordMot.Count > 0)
            {
                int[] save = coordMot.Dequeue();
                plat[save[0], save[1]].Cara = '*';
                joueurActuelle.Score += plat[save[0], save[1]].Score;
			}
        }

        private void ChuteLettres()
        {
            for (int i = 0; i < tailleGrille; i++)
            {
                Stack<Plateau> ligne = new Stack<Plateau>();

                for (int j = 0; j < tailleGrille; j++)
                    if (plat[j, i].Cara != '*')
                        ligne.Push(plat[j, i]);

                for (int j = tailleGrille - 1; j >= 0; j--)
                {
                    if (ligne.Count > 0)
                        plat[j, i] = ligne.Pop();
                    else
                        plat[j, i] = new Plateau();
                }
            }
            grilleVide = VerificationFin();
        }
		#endregion
        /// <summary>
        /// Cette méthode permet de générer la grille de lettres aléatoire.
        /// </summary>
		private void GenererGrille()
        {
            for (int i = 0; i < tailleGrille; i++)
            {
                RowDefinition row = new RowDefinition();
                ColumnDefinition column = new ColumnDefinition();
                grid.RowDefinitions.Add(row);
                grid.ColumnDefinitions.Add(column);
            }

            MAJGrille();
        }
        /// <summary>
        /// Cette méthode permet de mettre à jour la grille à chaque mouvement des joueurs
        /// </summary>
        private void MAJGrille()
        {
            for (int i = 0; i < tailleGrille; i++)
                for (int j = 0; j < tailleGrille; j++)
                {
                    Button BoutonGrille = new Button();
                    //BoutonGrille.BorderBrush = Brushes.Transparent;
                    //BoutonGrille.Background = Brushes.Transparent;
                    BoutonGrille.Name = $"Bouton{i}_{j}";
                    BoutonGrille.FontSize = 50;
                    BoutonGrille.Content = plat[i, j].Cara;
                    BoutonGrille.MouseEnter += SourisRentrer;
                    BoutonGrille.Click += BoutonCliquer;
                    Grid.SetRow(BoutonGrille, i);
                    Grid.SetColumn(BoutonGrille, j);
                    grid.Children.Add(BoutonGrille);
                }
        }

		#region InitialisationGrille
		private void RemplissagePlat(string choix)
        {
            switch (choix)
            {
                case "Normal": UtilisationTest(EcritureLettres()); break;
                case "Test1": UtilisationTest(choix); break;
                case "Test3": UtilisationTest(choix); break;
            }
        }

        private void UtilisationTest(string choix)
        {
            Queue<string[]> tempo = LectureDataTxt(choix);

            int tailleGrille = tempo.Count;
            for (int i = 0; i < tailleGrille; i++)
            {
                string[] ligneCara = tempo.Dequeue();
                for (int j = 0; j < tailleGrille; j++)
                {
					char cara = Convert.ToChar(ligneCara[j].ToUpper());
					plat[i, j] = new Plateau(cara, ScoreLettre(cara));
				}
			}
        }

        private string EcritureLettres()
        {
            Queue<string[]> tempo = LectureDataTxt("Lettre");

            int nbLettres = tempo.Count - 1;
            Random rnd = new Random();
            string[] ligneCara = tempo.Dequeue();
            int totalProba = Convert.ToInt32(ligneCara[1]);
            char[,] grille = new char[tailleGrille, tailleGrille];

            for (int i = 0; i < tailleGrille; i++)
                for (int j = 0; j < tailleGrille; j++)
                {
                    int choixLettre = rnd.Next(totalProba);

                    char cara = '*';
                    int somme = 0;
                    bool trouve = false;
                    for (int n = 0; n < nbLettres && !trouve; n++)
                    {
                        somme += Convert.ToInt32(ligneCara[1]);
                        ligneCara = tempo.Dequeue();
                        if (choixLettre <= somme)
                        {
                            cara = Convert.ToChar(ligneCara[0]);
                            trouve = true;
                        }
                        tempo.Enqueue(ligneCara);
                    }
                    grille[i, j] = cara;
                }

            ToFile(grille);
            return "PlateauAlea";
		}

		/// <summary>
		/// Cette méthode permet l'enregistrement de la grille dans un fichier texte.
		/// </summary>
		/// <param name="grille">Grille de jeu</param>
		private void ToFile(char[,] grille)
		{
			StreamWriter file = new StreamWriter("Data//PlateauAlea.txt", false);
			for (int i = 0; i < grille.GetLength(0); i++)
			{
				for (int j = 0; j < grille.GetLength(1); j++)
					file.Write(grille[i, j] + ";");
				file.WriteLine();
			}
			file.Close();
		}

		#region InitialisationLettre
		/// <summary>
		/// Cette méthode permet de trouver dans le fichier le point de chaque lettre du mot.
		/// </summary>
		/// <param name="lettre">envoie la lettre</param>
		/// <returns>retourne le score total du mot</returns>
		private int ScoreLettre(char lettre)
		{
			Queue<string[]> tempo = LectureDataTxt("Lettre");

			int score = 0;
			int nbLettres = tempo.Count - 1;
			string[] ligneCara = tempo.Dequeue();

			for (int n = 0; n < nbLettres; n++)
			{
				ligneCara = tempo.Dequeue();
				if (Convert.ToChar(ligneCara[0]) == lettre)
					score = Convert.ToInt32(ligneCara[2]);
			}
			return score;
		}

		private Queue<string[]> LectureDataTxt(string fichier)
		{
			StreamReader lecture = new StreamReader($"Data//{fichier}.txt");
			string[] recu;
			Queue<string[]> tempo = new Queue<string[]>();

			while (lecture.Peek() > 0)
			{
				recu = lecture.ReadLine().Split(";");
				tempo.Enqueue(recu);
			}
			lecture.Close();

			return tempo;
		}
		#endregion

		#endregion

		#region Bouton

		private void BoutonCliquer(object sender, RoutedEventArgs e)
        {
            Button inter = sender as Button;
            mot += inter.Content;
        }

        private void SourisRentrer(object sender, System.EventArgs e)
        {
            var test = (Button)sender;
            string[] position = test.Name.Split("_");
            colonneMat = Convert.ToInt32(position[1]);
            ligneMat = Convert.ToInt32(position[0].Substring(6));
        }
		#endregion

		#region DonneesExterieures
		public static void DonneesGrille(int tailleGrilleIndex, int tempsIndex)
        {
            if (tailleGrille == 0
                && temps == 0)
            {
                if (tempsIndex > 0)
                {
                    tailleGrille = tailleGrilleIndex;
                    temps = tempsIndex;
                }
                else
                    tailleGrille = tailleGrilleIndex;
            }
        }

        public static void DonneeMotJoueur(string motRecu)
        {
            if (jeuPlateau.Dictionnaire_Jeu.RechercheDicho(jeuPlateau.Dictionnaire_Jeu.DicoTrie, motRecu.ToUpper())
                && !Joueur.mot_Trouver.Contains(motRecu))
            {
                JoueurActuelle.Mot_Trouver.Add(motRecu);
                motJoueur = motRecu.ToUpper();
				Score.ActualiserScore(motJoueur);
			}
		}

        public static void ChangementJoueur()
        {
            foreach(Joueur a in joueurListe)
                if(a != joueurActuelle)
                {
                    joueurActuelle = a;
                    break;
                }    
        }
		#endregion
	}
}
