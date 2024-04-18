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

namespace TetrisLettres
{
	/// <summary>
	/// Logique d'interaction pour Affichage.xaml
	/// </summary>
	public partial class Affichage : Page
	{
		public Affichage()
		{
			InitializeComponent();
			this.MainAffichage.Navigate(new Uri("Fond.xaml", UriKind.Relative));
			this.GrilleAffichage.Navigate(new Uri("Grille.xaml", UriKind.Relative));
			this.ChronoAffichage.Source = new Uri("Chrono.xaml", UriKind.Relative);
			this.MotDevine.Navigate(new Uri("MotDevine.xaml", UriKind.Relative));
			this.ScoreAffichage.Navigate(new Uri("Score.xaml", UriKind.Relative));
			this.FinAffichage.Navigate(new Uri("Fin.xaml",UriKind.Relative));
		}
	}
}
