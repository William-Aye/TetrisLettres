using System;
using System.Collections.Generic;
using System.Diagnostics;
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
	/// Logique d'interaction pour Chrono.xaml
	/// </summary>
	public partial class Chrono : Page
	{
		private static int chrono;
		private static int temps;
		private static int tempsParTour;
		public static int tempsRestantJeu;
        DispatcherTimer timer = new DispatcherTimer();

        public Chrono()
		{
			InitializeComponent();

			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += ChronoMetre;
			timer.Start();
		}

		void ChronoMetre(object sender, EventArgs e)
		{
			if(tempsRestantJeu == 0 || Grille.grilleVide)
			{
                timer.Stop();
			}
			AffChrono.Text = chrono.ToString();
			AffChronoGlobal.Text = tempsRestantJeu.ToString();
			temps++;
			if(chrono == 0)
			{
				Grille.ChangementJoueur();
				chrono = tempsParTour;
			}
			chrono--;
			tempsRestantJeu--;
			MotDevine.DonneeChrono(chrono);
		}

		public static void ResetChrono(bool resetChrono)
		{
			if (resetChrono)
			{
				temps = 0;
				resetChrono = false;
			}
		}
		public static void RestartChronoBonneValeur()
		{
			chrono = tempsParTour;
        }

		public static void DonneeChrono(int temps, int tempsRestant)
		{
            tempsParTour = temps;
			tempsRestantJeu = tempsRestant;
		}
	}
}
