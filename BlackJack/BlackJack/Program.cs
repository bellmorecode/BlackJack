using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (game.NeedToShuffle()) game.Shuffle();
                game.Deal();

                Console.WriteLine();
                Console.Write("Play Again? (y)es or (n)o ");
                playagain = Console.ReadLine();

            } while (playagain == "y");

            Console.WriteLine("Done! Game over!");
            Console.ReadLine();
        }
    }
}
