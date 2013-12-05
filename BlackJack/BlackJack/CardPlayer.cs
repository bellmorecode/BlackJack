using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class CardPlayer
    {
        public CardPlayer()
        {
            this.BidLimit = 21;
            this.Hand = new List<PlayingCard>();
        }

        public int BidLimit { get; set; }

        public List<PlayingCard> Hand { get; set; }

        public int CurrentScore
        {
            get {
                // score adjustments
                var score = Hand.Select(q => q.CardValue)
                                .Select(q => q > 10 ? 10 : q)
                                .Select(q => q == 1 ? 11 : q).Sum();

                // TODO: Come back to this. 
                //if (score > 21 && Hand.Any()) {
                //
                //}

                return score;
            }
        }

        public bool DoneTakingCards { get; set; }

        public void DiscardHand()
        {
            this.Hand.Clear();
            this.DoneTakingCards = false;
        }

        public bool HasBlackJack
        {
            get
            {
                if (this.Hand.Count != 2) return false;
                
                var card1 = this.Hand[0];
                var card2 = this.Hand[1];

                return (card1.CardValue == 1 && card2.CardValue >= 10) ||
                       (card2.CardValue == 1 && card1.CardValue >= 10);
            }
        }

        public override string ToString()
        {
            return string.Join(", ", this.Hand.Select(q => q.ToString()));
        }
    }
}
