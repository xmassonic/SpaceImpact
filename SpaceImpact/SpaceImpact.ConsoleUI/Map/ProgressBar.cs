using System;
using System.Threading;

namespace SpaceImpact.ConsoleUI.Map
{
    public class ProgressBar
    {
        public static void Print(string message, int delay)
        {
            Thread.Sleep(delay);
            Console.WriteLine(message);
        }

        public static void ShowProgress()
        {
            Console.CursorVisible = false;
            Console.BufferHeight = 53;
            Console.BufferWidth = 163;
            Console.WindowHeight = 53;
            Console.WindowWidth = 163;

            Console.WriteLine("Loading... ");
            Print("Loading game...", 100);

            int pos = Console.CursorTop;
            Console.SetCursorPosition(11, 0);
            Console.Write("OK");
            Console.SetCursorPosition(0, pos);

            var rand = new Random();

            for (int i = 0; i <= 100; i++)
            {
                if (i < 25)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (i < 50)
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                else if (i < 75)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                else if (i < 100)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                string pct = string.Format("{0,-30} {1,3}%", new string((char)0x2592, i * 30 / 100), i);
                Console.CursorLeft = 0;
                Console.Write(pct);
                Thread.Sleep(rand.Next(0, 10));
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press Enter to start");
            Thread.Sleep(1000);
            Console.ResetColor();
        }
    }
}
