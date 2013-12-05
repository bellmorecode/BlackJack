using System;

namespace BlackJack
{
    class Program
    {
        static void Main(string[] args)
        {
            var playagain = string.Empty;
            var game = new BlackJackGame();
            game.Shuffle();
            
            do
            {
                Console.Clear();
                if (game.NeedToShuffle())
                {
                    game.Shuffle();
                    Console.WriteLine("Reshuffling!");
                }
                game.Deal();

                Console.WriteLine();
                Console.Write("Play Again? (y)es or (n)o ");
                playagain = Console.ReadLine();

            } while (playagain == "y");

            Console.WriteLine("Game over!");
            Console.ReadLine();
        }
    }
}
