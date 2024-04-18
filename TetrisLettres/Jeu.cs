using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisLettres
{
	class Jeu
	{
		private Dictionnaire dictionnaire_Jeu;
		private Plateau plateau_Jeu;
		private List<Joueur> liste_Joueur;

		public Jeu()
		{
			dictionnaire_Jeu = null;
			plateau_Jeu = null;
			liste_Joueur = null;
		}
		public Jeu(Dictionnaire dictionnaire_Jeu, Plateau plateau_Jeu, List<Joueur> liste_Joueur)
		{
			this.dictionnaire_Jeu = dictionnaire_Jeu;
			this.plateau_Jeu = plateau_Jeu;
			this.liste_Joueur = liste_Joueur;
		}
		public Dictionnaire Dictionnaire_Jeu
		{
			get { return dictionnaire_Jeu; }
			set { dictionnaire_Jeu = value; }
		}
		public Plateau Plateau_Jeu
		{
			get { return plateau_Jeu; }
			set { plateau_Jeu = value; }
		}
		public List<Joueur> Liste_Joueur
		{
			get { return liste_Joueur; }
			set { liste_Joueur = value; }
		}


	}
}
