using System;
// review VD: непотрібні простори імен
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceImpact.GameEngine.BaseGameElements
{
    public interface IGameElement
    {
        Space Space { get; set; }
        int X { get; }
        int Y { get; } 
    }
}
