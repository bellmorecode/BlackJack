using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    public sealed class CardDealer : CardPlayer
    {
        public CardDealer() : base()
        {
            this.BidLimit = 17;
        }
    }
}
