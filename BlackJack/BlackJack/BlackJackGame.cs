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

        public bool NeedToShuffle()
        {
            // HACK: this will break.  need to check mid-deal
            // or set some threshhold, but that would mean that certain cards wond get dealt

            //return this.Cards.Any(); // UPDATED!!
            return this.Cards.Count < 10;
        }

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

        private void PrintGameState()
        {
            Console.Clear();
            var go = IsGameOver();

            CardDealer d = this.Dealer as CardDealer;
            if (d.IsHandOpen)
            {
                Console.WriteLine("Dealer - Score: {0}", d.CurrentScore);
            }
            else
            {
                Console.WriteLine("Dealer");
            }

            
            if (this.Dealer.HasBlackJack) Console.WriteLine("BLACKJACK!");

            Console.WriteLine("{0}", this.Dealer);
            Console.WriteLine();

            var playersArray = this.Players.ToArray();
            for (var w = 0; w < playersArray.Length; w++)
            {
                Console.WriteLine("Player #{0} - Score: {1}", (w + 1), playersArray[w].CurrentScore);
                if (playersArray[w].HasBlackJack) Console.WriteLine("BLACKJACK!");
                Console.WriteLine("{0}", playersArray[w]);

                Console.WriteLine();
            }

            if (go)
            {
                Console.WriteLine(GameResultMessage);
            }
        }

        public string GameResultMessage { get; set; }

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

            if (!IsGameOver())
            {
                foreach (var player in this.Players)
                {
                    do
                    {
                        Console.WriteLine("Your move... ");
                        Console.Write("(H)it  (S)tay ");
                        var choice = Console.ReadLine();
                        if (choice.ToUpper() == "H")
                        {
                            player.Hand.Add(this.Cards.Dequeue());
                            if (player.CurrentScore > 21) player.DoneTakingCards = true;
                        }
                        else
                        {
                            player.DoneTakingCards = true;
                        }

                        PrintGameState();

                    } while (!player.DoneTakingCards);
                }

                var go = this.IsGameOver();

                if (!go)
                {
                    if (this.Dealer.MustTakeCards())
                    {
                        while (this.Dealer.MustTakeCards())
                        {
                            this.Dealer.Hand.Add(this.Cards.Dequeue());
                            if (this.Dealer.CurrentScore >= 21) this.Dealer.DoneTakingCards = true;

                            PrintGameState();
                        }
                        if (IsGameOver()) PrintGameState();
                    }
                    else
                    {
                        PrintGameState();
                        // just print and end
                    }
                    
                }
                else
                {
                    this.Dealer.DoneTakingCards = true;
                    PrintGameState();
                }
            }

            foreach (var player in this.Players)
            {
                player.DiscardHand();
            }
            Dealer.DiscardHand();
        }

        private bool IsGameOver()
        {
            this.GameResultMessage = string.Empty;

            if (this.Dealer.HasBlackJack)
            {
                GameResultMessage = "Dealer has BlackJack. You Suck!";
                return true;
            }

            if (this.Dealer.HasBlackJack)
            {
                GameResultMessage = "Player has BlackJack. Winner, Winner!";
                return true;
            }

            // HACK: this is for a 1 player game, revisit for multi-player game
            if (this.Players.Any(q => q.CurrentScore > 21))
            {
                GameResultMessage = "Player Busts! Lame!";
                return true;
            }

            if (this.Dealer.CurrentScore > 21)
            {
                GameResultMessage = "Dealer Busts! Winner, Winner!";
                return true;
            }

            if (this.Dealer.DoneTakingCards && this.Players.All(q => q.DoneTakingCards))
            {
                //HACK: for one player scenarios only
                var player = this.Players.First();
                if (this.Dealer.CurrentScore == player.CurrentScore)
                {
                    GameResultMessage = "PUSH!";
                }
                else
                {
                    if (this.Dealer.CurrentScore > player.CurrentScore)
                    {
                        GameResultMessage = "Dealer Wins!";
                    }
                    else
                    {
                        GameResultMessage = "Player Wins!";
                    }
                }
                return true;
            }

            return false;
        }

        
    }
}
