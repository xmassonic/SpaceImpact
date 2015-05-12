using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceImpact.GameEngine;

namespace SpaceImpact.ConsoleUI
{
    class LaserConsole
    {
        public void HideLaserSpaceship(Game game)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < game.Spaceship._lasers.Count; i++)
            {
                Console.SetCursorPosition(2 * game.Spaceship._lasers[i].X + 1, 2 * game.Spaceship._lasers[i].Y + 1);
                Console.Write(" ");
            }
        }

        public void DrawLaserSpaceship(Game game)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < game.Spaceship._lasers.Count; i++)
            {
                Console.SetCursorPosition(2 * game.Spaceship._lasers[i].X + 1, 2 * game.Spaceship._lasers[i].Y + 1);
                Console.Write("-");
            }
        }
    }
}
