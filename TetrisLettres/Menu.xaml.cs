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
using System.Windows.Shapes;

namespace TetrisLettres
{
	/// <summary>
	/// Logique d'interaction pour Menu.xaml
	/// </summary>
	public partial class Menu : Page
	{
		public Menu()
		{
			InitializeComponent();
			this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth);
			this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight);
			MenuDepart.Background = new ImageBrush(new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}FondMenu.png", UriKind.RelativeOrAbsolute)));
		}
		private void MenuClick(object sender, RoutedEventArgs e)
		{
			Button inter = (Button)sender;
			switch ((string)inter.Content)
			{
				case "Jouer": this.NavigationService.Navigate(new Uri("LETRIS.xaml", UriKind.Relative)); break;
				case "Option": this.NavigationService.Navigate(new Uri("Option.xaml", UriKind.Relative)); break;
				case "Quitter": Environment.Exit(1); break;
			}

		}
	}
}
