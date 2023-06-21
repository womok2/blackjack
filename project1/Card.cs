using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1
{
	class Card
	{
		private string Face { get; }
		private string Suit { get; }

		public int Point { get; }


		public Card(string face, string suit, int point)
		{
			Face = face; 
			Suit = suit;
			Point = point;
		}
		public override string ToString() => $"{Face} of {Suit},점수:{Point}";


	
	}
}
