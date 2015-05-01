using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceImpact.GameEngine.BaseGameElements;

namespace SpaceImpact.GameEngine
{
    /*
     * Review GY: використання даного класу бачу лише в проекті Test.
     * Яке призначення цього класу?
     */
    public class HealthScale: GameElement
    {
        public HealthScale(int x, int y, int life) : base(x, y, life) {}
    }
}
