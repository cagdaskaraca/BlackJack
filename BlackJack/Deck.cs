using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    [DataContract]
    class Deck
    {
        [DataMember]
        private Card[] Cards;
        [DataMember]
        private int cardcount = 52;

        public Deck()
        {
            string suitName = null;
            Cards = new Card[52];
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        suitName = "Kupa";
                        break;
                    case 1:
                        suitName = "Maça";
                        break;
                    case 2:
                        suitName = "Sinek";
                        break;
                    case 3:
                        suitName = "Karo";
                        break;
                }
                for (int j = 0; j < 13; j++)
                {
                    Cards[(i * 13) + j] = new Card(j + 1, suitName);
                }
            }
        }

        public Card DrawCard()
        {
            Random x = new Random();
            bool successfuldraw = false;
            while (!successfuldraw)
            {
                int triedcard = x.Next(52);
                if (!Cards[triedcard].InPlay && !Cards[triedcard].Drawn)
                {
                    Cards[triedcard].Drawn = true;
                    Cards[triedcard].InPlay = true;
                    returntodeck();
                    successfuldraw = true;
                    return Cards[triedcard];
                }

            }
            return new Card();
        }
        private void returntodeck()
        {
            cardcount--;
            if (cardcount == 0)
            {
                for (int i = 0; i < Cards.Length; i++)
                    Cards[i].Drawn = false;
                cardcount = 52;
            }
        }

        public void CardsNoLongerInPlay()
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                if (Cards[i].InPlay)
                    Cards[i].InPlay = false;
            }
        }
    }
}
