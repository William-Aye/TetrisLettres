using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace TetrisLettres
{
	/// <summary>
	/// Logique d'interaction pour MotDevine.xaml
	/// </summary>
	public partial class MotDevine : Page
	{
		private static int chrono;
		private static string newLettre = "";

		public MotDevine()
		{
			InitializeComponent();

			//AffMot.Text = newLettre;
		}

		private void OnKeyDownHandler(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
                Grille.ChangementJoueur();
				Chrono.RestartChronoBonneValeur();
                Grille.DonneeMotJoueur(AffMot.Text);
				AffMot.Text = "";
			}
		}

		public static void DonneeChrono(int temps)
		{
			chrono = temps;
		}

		/* Pas effectif pour l'instant
		public static void LettreEnvoie(string recu)
		{
			if(recu != null && recu != "")
				newLettre = recu;
		}
		*/

	}
}
