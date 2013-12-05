using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public sealed class BlackJackGame
    {
        private Random r = new Random();

        public BlackJackGame()
        {
            this.Cards = new Queue<PlayingCard>();
            this.Dealer = new CardDealer();

            // NOTE: only one player in this game.
            this.Players = new List<CardPlayer> { new CardPlayer() };
        }
        public CardPlayer Dealer { get; set; }

        public IEnumerable<CardPlayer> Players { get; set; }

        public Queue<PlayingCard> Cards { get; set; }

        public void Shuffle(int numberOfDecks = 1)
        {
            var firstShuffle = new List<PlayingCard>();
            for (int q = 0; q < numberOfDecks; q++)
            {
                var deck = new DeckOfCards();
                deck.Shuffle();
                while (deck.HasCardsLeft())
                    firstShuffle.Add(deck.GetNext());
            }

            // second shuffle (for multi-deck games)
            int limit = firstShuffle.Count;

            while (firstShuffle.Count > 0)
            {
                var index = r.Next(0, limit - 1);
                var c = firstShuffle[index];
                Cards.Enqueue(c);
                firstShuffle.Remove(c);
                limit = firstShuffle.Count;
            }
        }
        
        public void Deal()
        {
            // deal two cards
            for (var round = 1; round <= 2; round++)
            {
                foreach (var player in this.Players)
                {
                    player.Hand.Add(this.Cards.Dequeue());
                }
                this.Dealer.Hand.Add(this.Cards.Dequeue());
            }

            PrintGameState();

            foreach (var player in this.Players)
            {
                // TODO: take turn    
            }
            

            //Console.Clear();
        }

        private void PrintGameState()
        {
            Console.WriteLine("Dealer - Score: {0}", this.Dealer.CurrentScore);
            foreach (var card in this.Dealer.Hand)
                Console.WriteLine("{0}", card);

            Console.WriteLine();

            var playersArray = this.Players.ToArray();
            for (var w = 0; w < playersArray.Length; w++)
            {
                Console.WriteLine("Player #{0} - Score: {1}", (w + 1), playersArray[w].CurrentScore);
                foreach (var card in playersArray[w].Hand)
                    Console.WriteLine("{0}", card);

                Console.WriteLine();
            }
        }

        public bool NeedToShuffle()
        {
            // HACK: this will break.  need to check mid-deal
            // or set some threshhold, but that would mean that certain cards wond get dealt

            //return this.Cards.Any(); // UPDATED!!
            return this.Cards.Count < 10;
        }
    }
}
