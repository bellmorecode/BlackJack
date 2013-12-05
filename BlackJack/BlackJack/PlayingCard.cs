using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public sealed class PlayingCard
    {
        static PlayingCard() {
            CardNames[0] = "Ace";
            CardNames[1] = "2";
            CardNames[2] = "3";
            CardNames[3] = "4";
            CardNames[4] = "5";
            CardNames[5] = "6";
            CardNames[6] = "7";
            CardNames[7] = "8";
            CardNames[8] = "9";
            CardNames[9] = "10";
            CardNames[10] = "Jack";
            CardNames[11] = "Queen";
            CardNames[12] = "King";
            
        }

        internal PlayingCard()
        {

        }

        private static string[] CardNames = new string[13];

        static string GetCardName(int cardValue) {
            if (cardValue < 1 || cardValue > 13) throw new ArgumentOutOfRangeException("cardValue", "Should be between 1 and 13 inclusively");
            return CardNames[cardValue - 1];
        }

        public enum Suits { Spade, Heart, Club, Diamond }

        public int CardValue { get; set; }

        public Suits Suit { get; set; }

        public override string ToString()
        {
            return string.Format("{0} of {1}", GetCardName(this.CardValue), Suit);
        }
    }


}
