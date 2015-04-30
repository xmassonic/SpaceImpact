using System;
// review VD: непотрібні простори імен
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceImpact.ConsoleUI.Map;
using SpaceImpact.GameEngine;

namespace SpaceImpact.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            // review VD: тут варто було викликати функцію Start()
            ConsoleControl ctrl = new ConsoleControl(game);
            ctrl.SpaceshipMotion();
        }
    }
}
