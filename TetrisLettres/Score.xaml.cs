using System;
using System.Collections.Generic;
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
	/// Logique d'interaction pour Score.xaml
	/// </summary>
	public partial class Score : Page
	{
		public static string motPlayer = "";
		public static bool actua = false;
		private DispatcherTimer timeEvent = new DispatcherTimer();

		public Score()
		{
			InitializeComponent();
			Joueur1TextBox.Text = "\n";
			Joueur2TextBox.Text = "\n";
			timeEvent.Interval = TimeSpan.FromSeconds(1);
			timeEvent.Tick += AffScore;
			timeEvent.Start();
		}

		public static void ActualiserScore(string motJoueur)
		{
			motPlayer = motJoueur;
			actua = true;
		}

		private void AffScore(object sender, EventArgs e)
		{
			if (actua)
			{
				motPlayer = Grille.JoueurActuelle.Nom;

				if (motPlayer == "joueur1")
				{

					if (Joueur.mot_Trouver.Count > 0)
						Joueur1TextBox.Text += "\n" + Grille.JoueurActuelle.Score + " " + Joueur.mot_Trouver[Joueur.mot_Trouver.Count-1];
					Joueur1.Foreground = Brushes.Black;
					Joueur2.Foreground = Brushes.Blue;
				}
				else
				{
					if (Joueur.mot_Trouver.Count > 0)
						Joueur2TextBox.Text += "\n" + Grille.JoueurActuelle.Score + " " + Joueur.mot_Trouver[Joueur.mot_Trouver.Count-1];
					Joueur1.Foreground = Brushes.Blue;
					Joueur2.Foreground = Brushes.Black;
				}
				actua = false;
			}
		}
	}
}
