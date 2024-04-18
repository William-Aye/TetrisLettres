using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace TetrisLettres
{
	class Dictionnaire
	{
        private int nombre_Mot_Lettre;
        private string langue;
        private string fichier;
        private string[] dicoTrie;

        public Dictionnaire()
        {
            nombre_Mot_Lettre = 0;
            langue = "";
        }

        public Dictionnaire(int nombre_Mot_Lettre, string langue, string fichier)
        {
            this.nombre_Mot_Lettre = nombre_Mot_Lettre;
            this.langue = langue;
            dicoTrie = TriFusion(fichier);
        }

        public int Nombre_Mot_Lettre
        {
            get { return nombre_Mot_Lettre; }
            set { nombre_Mot_Lettre = value; }
        }
        public string Langue
        {
            get { return langue; }
            set { langue = value; }
        }
        public string Fichier
        {
            get { return fichier; }
            set { fichier = value; }
        }
        public string[] DicoTrie
        {
            get { return dicoTrie; }
        }

        /// <summary>
        /// Cette méthode permet de trier le dictionnaire dans l'ordre 
        /// alphabètique par l'appel des deux méthodes suivantes
        /// </summary>
        /// <param name="fichier">Le paramètre corresponds au fichier dans lequel se situe le dictionnaire à trier</param>
        /// <returns>Cela retourne </returns>
        public string[] TriFusion(string fichier)
        {
            return TrieurDiviseurTab(ReadFile(fichier));
        }

        /// <summary>
        /// Cette méthode permet de diviser le tableau en deux parties pour le trie fusion
        /// </summary>
        /// <param name="tab">Le paramètre corresponds au fichier sous forme de tableau</param>
        /// <returns>Cela retour les deux tableaux créé afin d'aller les refusionner dans la méthode suivante</returns>
        private string[] TrieurDiviseurTab(string[] tab)
        {
            if (tab == null)
            {
                return null;
            }
            if (tab.Length == 1) return tab;
            string[] tab1 = new string[tab.Length / 2];
            string[] tab2 = new string[tab.Length - tab1.Length];

            for (int i = 0; i < tab1.Length; i++)
                tab1[i] = tab[i];
            for (int i = tab1.Length; i < tab.Length; i++)
                tab2[i - tab1.Length] = tab[i];

            return TrieurFusionTab(TrieurDiviseurTab(tab1), TrieurDiviseurTab(tab2));
        }

        /// <summary>
        /// Cette méthode permet de refusionner les deux tableaux créé precedement mais dans l'ordre alphabétique
        /// </summary>
        /// <param name="tab1">première partie du tableau final</param>
        /// <param name="tab2">seconde partie du tableau final</param>
        /// <returns>Retourne le tableau trié</returns>
        private string[] TrieurFusionTab(string[] tab1, string[] tab2)
        {
            string[] tab = new string[tab1.Length + tab2.Length];

            int i = 0, j = 0;
            while (i + j < tab.Length)
            {
                if (tab1.Length > i && tab2.Length > j)
                {
                    if (tab1[i].CompareTo(tab2[j]) < 0)
                    {
                        tab[i + j] = tab1[i];
                        i++;
                    }
                    else
                    {
                        tab[i + j] = tab2[j];
                        j++;
                    }
                }
                else
                {
                    if (tab1.Length > i)
                    {
                        tab[i + j] = tab1[i];
                        i++;
                    }
                    else
                    {
                        tab[i + j] = tab2[j];
                        j++;
                    }
                }

            }

            return tab;
        }

        /// <summary>
        /// Methode qui permet de lire un fichier
        /// </summary>
        /// <param name="fichier">Le paramètre corresponds au fichier que l'on veut ouvrir</param>
        /// <returns>la méthode retourne le fichier d'entrer sous forme de tableau de string (non trié)</returns>
        private string[] ReadFile(string fichier)
        {
            StreamReader fichierLecture;
            try
            {
                fichierLecture = new StreamReader(fichier);
            }
            catch
            {
                return null;
            }
            List<string> dicoListe = new List<string>();
            while (fichierLecture.Peek() > 0)
            {
                string[] matInter = fichierLecture.ReadLine().Split(' ');
                foreach (string mot in matInter)
                {
                    dicoListe.Add(mot);
                }
            }
            fichierLecture.Close();
            return dicoListe.ToArray();
        }

        /// <summary>
        /// Cette méthode permet l'affichage de la description d'un dictionnaire (Nombre de mot par lettre et langue).
        /// </summary>
        /// <returns>La méthode retourne le message sous la forme d'une chaîne de caractère</returns>
        public string toString()
        {
            return "Langue : " + langue + "\nNombre de mot par lettre : " + nombre_Mot_Lettre;
        }

        /// <summary>
        /// Cette méthode appel la méthode qui va permettre la recherche si les mots entré par le joueur sont dans 
        /// le dictionnaire sous forme de recherche par "Dichotomie"
        /// </summary>
        /// <param name="mot">Le paramètre corresponds au mot trouvé par le joueur</param>
        /// <param name="tabDico">Le paramètre corresponds au dictionnaire qui va être chercher </param>
        /// <returns>La méthode retourne vrai ou faux en fonction de l'existance du mot dans le dictionnaire</returns>
        public bool RechercheDicho(string[] tabDico, string mot)
        {
            return RechDichoRescursif(tabDico, mot, 0, tabDico.Length, tabDico.Length / 2);
        }
        /// <summary>
        /// Cette méthode permet de vérifier par recherche dictomique si 
        /// le mot trouvé par le joueur est présent dans le dictionnaire
        /// </summary>
        /// <param name="tabDico"></param>
        /// <param name="mot"></param>
        /// <param name="g">valeurs la plus faibles</param>
        /// <param name="d">valeurs la plus grandes</param>
        /// <param name="m">milieu du tableau</param>
        /// Les trois paramètres precedent sont amené à changer tout au long de la recherche dichotomique.
        /// <returns>retourne true si le mot est present, false dans le cas contraire</returns>
        private bool RechDichoRescursif(string[] tabDico, string mot, int g, int d, int m)
        {
            if (g == d)
            {
                if (tabDico[m] == mot) return true;
                else return false;
            }

            if (tabDico[m].CompareTo(mot) > 0) d = m - 1;
            else if (tabDico[m].CompareTo(mot) == 0) return true;
            else g = m + 1;

            m = (g + d) / 2;

            return RechDichoRescursif(tabDico, mot, g, d, m);
        }
    }
}
