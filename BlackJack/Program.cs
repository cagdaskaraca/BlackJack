using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;

namespace CardGames
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Oyunu kaydetmek istiyormusunuz? (Y or N)");
            char loadgame = char.ToUpper(char.Parse(Console.ReadLine()));
            GameState savedGame = new GameState();
            bool successfulGameLoad = false;
            if (loadgame == 'Y')
            {
                Console.Write("Yüklenecek dosya adını girin: ");
                string filename = Console.ReadLine();
                savedGame = LoadSavedGame(filename);
                if (savedGame.Player != null)
                    successfulGameLoad = true;
            }
            Console.Title = "Blackjack";
            ScreenOperations.ClearGameScreen();
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < 35; i++)
                Console.Write("-");
            Console.Write("BLACKJACK");
            for (int i = 0; i < 36; i++)
                Console.Write("-");
            if (!successfulGameLoad)
            {
                Console.SetCursorPosition(0, 15);
                Console.Write("Oyuncu Adı giriniz. ");
                string name = Console.ReadLine();
                ScreenOperations.ClearGameLine(0, 15, 49);
                Player player = new Player(name);
                GameState blackjack = new GameState(player);
                while (blackjack.Turn()) ;
                if (blackjack.Player.Funds > 0)
                {
                    Console.WriteLine("Kaydetmek için Kayıt adı giriniz. ");
                    string filename = Console.ReadLine();
                    SaveGameState(filename, blackjack);
                }
            }
            else
            {
                while (savedGame.Turn()) ;
                if (savedGame.Player.Funds > 0)
                {
                    Console.WriteLine("Kaydetmek için Kayıt adı giriniz. ");
                    string filename = Console.ReadLine();
                    SaveGameState(filename, savedGame);
                }
            }
            Console.ReadLine();
        }

        static GameState LoadSavedGame(string filename)
        {
            FileStream fs = null;
            GameState savedGame = new GameState();
            try
            {
                using (fs = new FileStream(filename, FileMode.Open))
                {
                    DataContractSerializer dcsGameState = new DataContractSerializer(typeof(GameState));
                    XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                    savedGame = (GameState)dcsGameState.ReadObject(reader);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                ScreenOperations.ClearGameScreen();
            }
            finally
            {
                fs?.Close();
            }
            return savedGame;
        }

        static void SaveGameState(string filename, GameState savedGame)
        {
            FileStream fs = null;
            try
            {
                using (fs = new FileStream(filename, FileMode.Create))
                {
                    DataContractSerializer dcsGameState = new DataContractSerializer(typeof(GameState));
                    XmlDictionaryWriter xdw = XmlDictionaryWriter.CreateTextWriter(fs, Encoding.UTF8);
                    dcsGameState.WriteObject(xdw, savedGame);
                    xdw.Close();
                }
            }
            finally
            {
                fs?.Close();
            }
        }
    }
}
