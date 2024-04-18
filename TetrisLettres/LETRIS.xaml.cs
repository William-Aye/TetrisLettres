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
	/// Logique d'interaction pour LETRIS.xaml
	/// </summary>
	public partial class LETRIS : Page
	{
		public LETRIS()
		{
			InitializeComponent();
		}
		private void GenererClicker(object sender, RoutedEventArgs e)
		{
			Grille.DonneesGrille(Convert.ToInt32(tailleGrilleIndex.Text), Convert.ToInt32(TempsIndex.Text));
			Chrono.DonneeChrono(Convert.ToInt32(TempsIndex.Text), Convert.ToInt32(TempsIndexTotal.Text)*60);
			this.NavigationService.Navigate(new Affichage());
		}
		private void RetourClicker(object sender, RoutedEventArgs e)
		{
			this.NavigationService.GoBack();
		}
	}
}
