using SpaceImpact.GameEngine.Base;

namespace SpaceImpact.GameEngine
{
    // review VD: цей клас варто було винести в каталог Base
    public class GameObject : IGameObject
    {
        public Space Space { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}