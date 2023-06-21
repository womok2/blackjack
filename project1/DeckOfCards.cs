using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project1
{
	class DeckOfCards
	{
        private static Random randomNumbers = new Random();

        private const int NumberOfCards = 52; 
        private Card[] deck = new Card[NumberOfCards];

        public DeckOfCards()
        {
            string[] faces = {"Ace", "Deuce", "Three", "Four", "Five", "Six",
         "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King"};
            string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
            int[] point = { 11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10 };
             
            for (var count = 0; count < deck.Length; ++count)
            {
                deck[count] = new Card(faces[count % 13], suits[count / 13],point[count % 13]);
            }
        }

        public void Shuffle()
        {
            for (var first = 0; first < deck.Length; ++first)
            {
                var second = randomNumbers.Next(NumberOfCards);

                Card temp = deck[first];
                deck[first] = deck[second];
                deck[second] = temp;
            }
        }
        public Card Numbercard(int a)
        {
            return deck[a];
        }

    }
}
