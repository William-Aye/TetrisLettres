namespace TetrisLettres
{
	class Joueur
	{
		private string nom;
		public static List<string> mot_Trouver = new List<string>();
		private int score;

		public Joueur()
		{
			nom = "";
			score = 0;
		}
		/// <summary>
		/// Définition du constructeur paramétré prenant tous les attributs en paramètre
		/// </summary>
		/// <param name="nom">nom du joueur</param>
		/// <param name="mot_Trouver">Liste des mots trouver par le joueur</param>
		/// <param name="scores">Scores de jeu (calculer en fonction des mots trouver par le joueur)</param>
		public Joueur(string nom, List<string> mot_Trouver, int scores)
		{
			this.nom = nom;
			mot_Trouver = mot_Trouver;
			this.score = scores;
		}

		public string Nom
		{
			get { return nom; }
			set { nom = value; }
		}
		public List<string> Mot_Trouver
		{
			get { return mot_Trouver; }
			set { mot_Trouver = value; }
		}
		public int Score
		{
			get { return score; }
			set { score = value; }
		}

		/// <summary>
		/// Cette méthode permet de rajouter le mot trouver par le joueur s'il est bon dans la liste des mots trouvé.
		/// </summary>
		/// <param name="mot">Ce paramètre corresponds au mot trouvé par le joueur</param>
		public void Add_Mot(string mot)
		{
			if(mot != null || mot != "")
			{
				mot_Trouver.Add(mot);
			}
		}
		/// <summary>
		/// Cette méthode permet l'affichage de la description d'un joueur (nom, score et mot trouvé).
		/// </summary>
		/// <returns>La méthode retourne le message sous forme de chaîne de caractère</returns>
		public string toString()
		{
			string message = "Le joueur : " + nom + "\n A le score suivant :" + score + "en trouvant les mots suivant : ";
			foreach (string mot in mot_Trouver)
			{
				message += mot + "\n";
			}
			return message;
		}
		/// <summary>
		/// Cette méthode permet dans le cas où le joueur à trouvé un mot d'ajouter à sont score, le score du mot trouvé.
		/// </summary>
		/// <param name="val">Le paramètre correspond à la valeur du score que lui a apporter le mot trouvé</param>
		public void Add_Score(int val)
		{
			if(val > 0)
			{
				score += val;
			}
		}
		/// <summary>
		/// Cette méthode permet de vérifier si le nouveau mot trouvé par le joueur 
		/// n'a pas déjà était trouver et est donc déjà dans la liste de mot trouvé.
		/// </summary>
		/// <param name="mot">Le paramètre correspond au mot trouver par le joueur</param>
		/// <returns>La méthode retourne vrai ou faux en fonction de l'existance du mot dans la liste</returns>
		public bool Contient(string mot)
		{
			bool res = false;
			foreach (string word in Mot_Trouver)
			{
				if (mot == word)
				{
					res = true;
				}
			}
			return res;
		}
	}
}
