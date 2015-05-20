using SpaceImpact.GameEngine.Base;

namespace SpaceImpact.GameEngine
{
    public class GameObject : IGameObject
    {
        public Space Space { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}