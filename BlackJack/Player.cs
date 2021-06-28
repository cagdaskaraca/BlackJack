using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    [DataContract]
    class Player : CardPlayer
    {
        [DataMember]
        private int CariFon = 100;
        private int Bahis = 0;
        private int GercekFon;
        private int GercekBahis;
        [DataMember]
        private string name;

        public Player(string name) : base(name)
        {
            this.name = name;
        }

        public int Funds
        {
            get { return CariFon; }
            set { CariFon = value; }
        }

        public int Bet
        {
            get { return Bahis; }
            set { Bahis = value; }
        }

        public int OriginalFunds
        {
            get { return GercekFon; }
            set { GercekFon = value; }
        }

        public int OriginalBet
        {
            get { return GercekBahis; }
            set { GercekBahis = value; }
        }

        public void PlayerBet()
        {
            ScreenOperations.ClearGameLine(0, 15, 43);
            string betText = ($"Bakiyeniz: ${Funds}. Bahis tutarı: ");
            Console.Write(betText, Funds);
            OriginalFunds = Funds; 
            int cursorPosition = betText.Length;
            bool possibleBet = false;
            bool formatexception = false;
            while (!possibleBet)
            {
                try
                {
                    Bet = Int32.Parse(Console.ReadLine());
                    formatexception = false;
                }
                catch (FormatException)
                {
                    formatexception = true;
                    ScreenOperations.ClearGameLine(0, 16, 45);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Yalnızca Rakam girişi yapınız.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ScreenOperations.ClearGameLine(cursorPosition, 15, 23);
                }
                catch (OverflowException)
                {
                    formatexception = true;
                    ScreenOperations.ClearGameLine(0, 16, 45);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Yeterli bakiyeniz bulunmamaktadır!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ScreenOperations.ClearGameLine(cursorPosition, 15, 23);
                }
                if (Bet > Funds && !formatexception)
                {
                    Console.SetCursorPosition(0, 16);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Yeterli bakiyeniz bulunmamaktadır!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ScreenOperations.ClearGameLine(cursorPosition, 15, 11);
                }
                else if (Bet < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Negatif bakiye girişi yapılamaz.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ScreenOperations.ClearGameLine(cursorPosition, 15, 25);
                }
                else if (!formatexception)
                {
                    Console.SetCursorPosition(0, 16);
                    Console.WriteLine("----------------------");
                    possibleBet = true;
                }
            }
            OriginalBet = Bet;
            Funds -= Bet;
            ScreenOperations.DebugView(ToString());
        }

        public void Win()
        {
            ScreenOperations.ClearGameLine(0, 15, 39);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kazandınız. Bakiyeniz: ${0}", Bet * 2);
            Funds += Bet * 2;
            Console.Write("                                 ");
            Console.SetCursorPosition(0, 16);
            Console.WriteLine("Kalan: ${0}", Funds);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Lost()
        {
            ScreenOperations.ClearGameLine(0, 15, 39);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Kaybettiniz Bakiyeniz: ${0}", Bet);
            Console.Write("                                 ");
            Console.SetCursorPosition(0, 16);
            Console.WriteLine("Kalan: ${0}", Funds);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void Tie()
        {
            ScreenOperations.ClearGameLine(0, 15, 39);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Bahsi geri al");
            Console.Write("                                    ");
            Console.SetCursorPosition(0, 16);
            Funds += Bet;
            Console.WriteLine("Kalan: ${0}", Funds);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public int LostTheGame()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 20);
            Console.WriteLine("Bakiye bitti. Kaybettin! ");
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("...Tekrar Oynamak için Oyunu tekrar çalıştırın.");
            return 2;
        }

        // For debugging...
        public override string ToString()
        {
            return "Oyuncu Ismi: " + Name + "\tKalan: " + Funds + "\tBakiye: " + Bet + "\tBahis: " + OriginalBet + "\tToplam: " + OriginalFunds;
        }
    }
}
