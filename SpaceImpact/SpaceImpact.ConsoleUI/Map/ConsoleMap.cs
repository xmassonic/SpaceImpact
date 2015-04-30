using System;
// review VD: непотрібні простори імен
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceImpact.GameEngine;
using System.Globalization;
using System.Threading;

namespace SpaceImpact.ConsoleUI.Map
{
    public class ConsoleMap
    {
        public void DrawFrontier(Game game)
        {
            int w = 2*game.GameSpace.Width;
            int h = 2*game.GameSpace.Height;

            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(0, 0);
            Console.Write("╔");
            Console.SetCursorPosition(1, 0);
            Console.Write(new string('═', w));
            Console.SetCursorPosition(w + 1, 0);
            Console.Write("╗");

            Console.SetCursorPosition(0, h + 1);
            Console.Write("╚");
            Console.SetCursorPosition(1, h + 1);
            Console.Write(new string('═', w));
            Console.SetCursorPosition(w + 1, h + 1);
            Console.Write("╝");

            for (int i = 0; i < h; i++)
            {
                Console.CursorTop = i + 1;
                Console.CursorLeft = 0;
                Console.Write("║");
                Console.CursorLeft = w + 1;
                Console.Write("║");
            }
        }

        public void DrawMap(Game game)
        {
            int w = 2*game.GameSpace.Width;
            int h = 2*game.GameSpace.Height;

            for (int i = 1; i < w - 1; i++)
            {
                Console.CursorTop = 1;
                Console.CursorLeft = i + 1;
                // review VD: тут варто поставити круглі дужки
                if (i%8 == 0)
                {
                    Console.Write(' ');
                }
                Console.Write('*');
            }

            for (int i = 1; i < w - 1; i++)
            {
                Console.CursorTop = h;
                Console.CursorLeft = i + 1;
                // review VD: тут варто поставити круглі дужки
                if (i%8 == 0)
                {
                    Console.Write(' ');
                }
                Console.Write('*');
            }

            Console.SetCursorPosition(3, 2);
            for (int i = 0; i < (w - 1)/8; i++)
            {
                Console.Write("*****   ");
            }

            Console.SetCursorPosition(3, h - 1);
            for (int i = 0; i < (w - 1) / 8; i++)
            {
                Console.Write("*****   ");
            }

            Console.SetCursorPosition(4, 3);
            for (int i = 0; i < (w - 1) / 8; i++)
            {
                Console.Write("***     ");
            }

            Console.SetCursorPosition(4, h - 2);
            for (int i = 0; i < (w - 1) / 8; i++)
            {
                Console.Write("***     ");
            }

            Console.SetCursorPosition(5, 4);
            for (int i = 0; i < (w - 1) / 8; i++)
            {
                Console.Write("*       ");
            }

            //Console.SetCursorPosition(5, h - 3);
            //for (int i = 0; i < (w - 1) / 8; i++)
            //{
            //    Console.Write("*       ");
            //}           
        }
    }
}
