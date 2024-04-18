using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TetrisLettres
{
	class Plateau
	{
		private char car;
		private int score;

		public Plateau()
		{
			car = '*';
			score = 0;
		}

		public Plateau(char cara)
		{
			car = cara;
			score = 0;
		}

		public Plateau(char cara, int score)
		{
			car = cara;
			this.score = score;
		}

		public char Cara
		{ get { return car; } set { car = value; } }

		public int Score
		{ get { return score; } set { score = value; } }
	}
}
