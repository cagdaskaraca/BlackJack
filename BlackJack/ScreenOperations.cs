using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames
{
    static class ScreenOperations
    {
        public static void ClearGameScreen()
        {
            Console.SetCursorPosition(0, 2);
            for (int i = 0; i <= 20; i++)
            {
                int numspaces = Console.BufferWidth;
                for (int j = 0; j < numspaces; j++)
                {
                    Console.Write(" ");
                }
            }
        }

        public static void ClearGameLine(int xPosition, int yPosition, int numOfSpaces)
        {
            Console.SetCursorPosition(xPosition, yPosition);
            for (int i = 0; i < numOfSpaces; i++)
            {
                Console.Write(" ");
            }
            Console.SetCursorPosition(xPosition, yPosition);
        }

        public static void DebugView(string PlayerString)
        {
            int currentCursorPositionX = Console.CursorLeft;
            int currentCursorPositionY = Console.CursorTop;
            Console.SetCursorPosition(0, 25);
            ClearGameLine(0, 25, Console.BufferWidth);
            Console.WriteLine(PlayerString);
            Console.SetCursorPosition(currentCursorPositionX, currentCursorPositionY);
        }
    }
}
