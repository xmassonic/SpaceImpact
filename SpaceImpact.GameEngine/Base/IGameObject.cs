using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceImpact.GameEngine.Base
{
    public interface IGameObject
    {
        Space Space { get; set; }
        int X { get; set; }
        int Y { get; set; }
    }
}
