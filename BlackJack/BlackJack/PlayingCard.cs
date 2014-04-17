using System;

namespace BlackJack
{
    public sealed class PlayingCard
    {
        static PlayingCard() 
        {
            CardNames[0] = "A";
            CardNames[1] = "2";
            CardNames[2] = "3";
            CardNames[3] = "4";
            CardNames[4] = "5";
            CardNames[5] = "6";
            CardNames[6] = "7";
            CardNames[7] = "8";
            CardNames[8] = "9";
            CardNames[9] = "10";
            CardNames[10] = "J";
            CardNames[11] = "Q";
            CardNames[12] = "K";
        }

        internal PlayingCard() { }

        private static string[] CardNames = new string[13];

        private static string GetCardName(int cardValue) 
        {
            if (cardValue < 1 || cardValue > 13) throw new ArgumentOutOfRangeException("cardValue", "Should be between 1 and 13 inclusively");
            return CardNames[cardValue - 1];
        }

        private static string GetSuitSymbol(Suits s)
        {
            switch (s)
            {
                case Suits.Spade: return "♠";
                case Suits.Heart: return "♥";
                case Suits.Club: return "♣";
                case Suits.Diamond: return "♦";
                default:
                    return "unknown";
            }
        }

        public enum Suits { Spade, Heart, Club, Diamond }

        public int CardValue { get; set; }

        public Suits Suit { get; set; }

        public override string ToString()
        {
            return string.Format("{0}{1}", GetCardName(this.CardValue), GetSuitSymbol(Suit));
        }
    }
}
