using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public class DeckOfCards
    {
        private PlayingCard[] OrderedDeck = new PlayingCard[52];

        private Queue<PlayingCard> ShuffledDeck = new Queue<PlayingCard>();

        private static Random r = new Random();

        public DeckOfCards()
        {
            FillDeck();
        }

        private void FillDeck()
        {
            var pos = 0;
            foreach (var suit in new [] { PlayingCard.Suits.Spade, PlayingCard.Suits.Heart, PlayingCard.Suits.Club, PlayingCard.Suits.Diamond })
            {
                for (var card = 1; card < 14; card++)
                {
                    OrderedDeck[pos++] = new PlayingCard { CardValue = card, Suit = suit };
                }
            }
        }

        public void Shuffle()
        {
            var burnDown = new List<PlayingCard>(OrderedDeck);
            int limit = burnDown.Count;

            while (burnDown.Count > 0)
            {
                var index = r.Next(0, limit - 1);
                var c = burnDown[index];
                ShuffledDeck.Enqueue(c);
                burnDown.Remove(c);
                limit = burnDown.Count;
            }
        }

        public bool HasCardsLeft()
        {
            return ShuffledDeck.Any();
        }

        public PlayingCard GetNext()
        {
            return ShuffledDeck.Dequeue();
        }
    }
}
