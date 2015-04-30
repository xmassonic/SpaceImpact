using System;
// review VD: непотрібний простір імен System.Collection.Generic
using System.Collections.Generic;
// review VD: непотрібний простір імен System.LINQ
using System.Linq;
// review VD: непотрібний простір імен System.Text
using System.Text;
// review VD: непотрібний простір імен System.Threading.Tasks
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
