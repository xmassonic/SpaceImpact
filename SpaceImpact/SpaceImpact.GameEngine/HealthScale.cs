using System;
// review VD: непотрібні простори імен
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceImpact.GameEngine.BaseGameElements;

namespace SpaceImpact.GameEngine
{
    // цей клас ніде не використовується
    public class HealthScale: GameElement
    {
        public HealthScale(int x, int y, int life) : base(x, y, life) {}
    }
}
