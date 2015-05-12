using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceImpact.GameEngine
{
    public class Dispatcher
    {
        public delegate void SpaceshipDrawing(List<SpaceshipFragment> hero);

        public delegate void GameObjectDrawing(int pointX, int pointY);

        public delegate void SpaceDrawing();

        public delegate void GameInfo(int pointX, int pointY, int info);
    }
}
