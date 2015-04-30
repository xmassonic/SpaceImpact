using System;
// review VD: непотрібні простори імен
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceImpact.GameEngine;

namespace SpaceImpact.ConsoleUI
{
    class SpaceshipConsole
    {
        public void HideSpaceship(Game game)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(2 * game.Spaceship.X + 1, 2 * game.Spaceship.Y + 1);
            Console.Write("   ");
            Console.SetCursorPosition(2 * game.Spaceship.X + 1, 2 * game.Spaceship.Y + 2);
            Console.Write("   ");
            Console.SetCursorPosition(2 * game.Spaceship.X + 1, 2 * game.Spaceship.Y + 3);
            Console.Write("   ");
        }

        public void DrawSpaceship(Game game)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2 * game.Spaceship.X + 1, 2 * game.Spaceship.Y + 1);
            Console.Write("(x\\");
            Console.SetCursorPosition(2 * game.Spaceship.X + 1, 2 * game.Spaceship.Y + 2);
            Console.Write("=@>");
            Console.SetCursorPosition(2 * game.Spaceship.X + 1, 2 * game.Spaceship.Y + 3);
            Console.Write("(x/");
        }
    }
}
