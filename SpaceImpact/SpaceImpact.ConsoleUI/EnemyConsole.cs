using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceImpact.GameEngine;

namespace SpaceImpact.ConsoleUI
{
    class EnemyConsole
    {
        public void HideEnemy(Game game)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < game.Enemies.Count; i++)
            {
                Console.SetCursorPosition(2 * game.Enemies[i].X + 1, 2 * game.Enemies[i].Y + 1);
                Console.Write("  ");
            }
        }

        public void DrawEnemy(Game game)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < game.Enemies.Count; i++)
            {
                Console.SetCursorPosition(2 * game.Enemies[i].X + 1, 2 * game.Enemies[i].Y + 1);
                Console.Write("<#");
            }
        }
    }
}
