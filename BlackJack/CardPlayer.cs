using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CardGames
{
    [DataContract]
    [KnownType(typeof(Player))]
    [KnownType(typeof(Dealer))]
    abstract class CardPlayer
    {
        [DataMember]
        private int Toplam = 0;
        [DataMember]
        private bool YuksekAS = false;
        [DataMember]
        private int KartDurumu;
        [DataMember]
        private string name;
        [DataMember]
        private int cardRowPosition = 0;

        public CardPlayer(string name)
        {
            this.name = name;
        }

        public int Sum
        {
            get { return Toplam; }
            set { Toplam = value; }
        }

        public bool HighAce
        {
            get { return YuksekAS; }
            set { YuksekAS = value; }
        }

        public int ColumnPosition
        {
            get { return KartDurumu; }
            set { KartDurumu = value; }
        }

        public int CardRowPosition
        {
            get { return cardRowPosition; }
            set { cardRowPosition = value; }
        }

        public string Name
        {
            get { return name; }
        }
        public virtual string GetInitialCards(Deck current)
        {
            Card firstCard = current.DrawCard();
            if (firstCard.CardValue == "Ace")
            {
                Toplam += 11;
                YuksekAS = true;
            }
            else if (firstCard.CardValue == "Jack" || firstCard.CardValue == "Queen" || firstCard.CardValue == "King")
            {
                Toplam += 10;
            }
            else
            {
                Toplam += firstCard.CardRawValue;
            }
            Console.SetCursorPosition(ColumnPosition, 2);
            Console.BackgroundColor = firstCard.BackgroundColor;
            Console.ForegroundColor = firstCard.ForegroundColor;
            Console.Write("{0} of {1}", firstCard.CardValue, firstCard.SuitValue);
            Card secondCard = current.DrawCard();
            Console.BackgroundColor = secondCard.BackgroundColor;
            Console.ForegroundColor = secondCard.ForegroundColor;
            if (secondCard.CardValue == "Ace" && !YuksekAS)
            {
                Toplam += 11;
                YuksekAS = true;
            }
            else if (secondCard.CardValue == "Jack" || secondCard.CardValue == "Queen" || secondCard.CardValue == "King")
            {
                Toplam += 10;
            }
            else
            {
                Toplam += secondCard.CardRawValue;
            }
            string secondcardstring = $"{secondCard.CardValue} of {secondCard.SuitValue}";
            return secondcardstring;
        }

        public virtual void GetCard(Deck current)
        {
            bool normaldraw = true;
            Card currentCard = current.DrawCard();
            if (currentCard.CardValue == "Ace" && YuksekAS == false && Toplam < 11)
            {
                Toplam += 11;
                YuksekAS = true;
                normaldraw = false;
            }
            Console.ForegroundColor = currentCard.ForegroundColor;
            Console.BackgroundColor = currentCard.BackgroundColor;
            Console.SetCursorPosition(ColumnPosition, 4 + CardRowPosition);
            Console.Write("{0} of {1}", currentCard.CardValue, currentCard.SuitValue, this.name);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            CardRowPosition++;
            if (normaldraw && !(currentCard.CardRawValue > 10))
                Toplam += currentCard.CardRawValue;
            else if (normaldraw)
            {
                Toplam += 10;
            }
            if (Toplam > 21 && YuksekAS == true)
            {
                Toplam -= 10;
                YuksekAS = false;
            }
        }
    }
}
