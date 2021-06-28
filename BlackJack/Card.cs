using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CardGames
{
    [DataContract]
    public struct Card
    {
        [DataMember] //Bir türün üyesine uygulandığında, üyenin bir veri sözleşmesinin parçası olduğunu ve tarafından seri hale getirilme olduğunu belirtir
        private readonly int Card_Blank_Value;
        [DataMember]
        private readonly string suitValue;
        [DataMember]
        private readonly string cardValue;
        [DataMember]
        private readonly ConsoleColor foregroundValue;
        [DataMember]
        private readonly ConsoleColor backgroundValue;
        [DataMember]
        private bool inPlay;
        [DataMember]
        private bool drawn;
        public int CardRawValue => Card_Blank_Value;
        public string SuitValue => suitValue;

        public string CardValue => cardValue;
        public ConsoleColor ForegroundColor => foregroundValue;
        public ConsoleColor BackgroundColor => backgroundValue;
        public bool InPlay
        {
            get => inPlay;
            set => inPlay = value;
        }

        public bool Drawn
        {
            get => drawn;
            set => drawn = value;
        }

        public Card(int cardPassedInValue, string suitPassedInValue)
        {
            inPlay = false;
            drawn = false;
            backgroundValue = ConsoleColor.White;
            if (cardPassedInValue > 0 && cardPassedInValue < 14)
            {
                Card_Blank_Value = cardPassedInValue;
            }
            else
            {
                throw new ArgumentException("Geçersiz Kart Değeri");
            }
            if (suitPassedInValue == "Kupa" || suitPassedInValue == "Karo")
            {
                suitValue = suitPassedInValue;
                foregroundValue = ConsoleColor.Red;
            }
            else if (suitPassedInValue == "Maça" || suitPassedInValue == "Sinek")
            {
                suitValue = suitPassedInValue;
                foregroundValue = ConsoleColor.Black;
            }
            else
            {
                throw new ArgumentException("Geçersiz değer.");
            }
            switch (cardPassedInValue)
            {
                case 1:
                    cardValue = "As";
                    break;
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    cardValue = cardPassedInValue.ToString();
                    break;
                case 11:
                    cardValue = "Vale";
                    break;
                case 12:
                    cardValue = "Kız";
                    break;
                case 13:
                    cardValue = "Papaz";
                    break;
                default:
                    cardValue = "Kart Değil";
                    break;
            }
        }
    }
}
