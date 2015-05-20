using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceImpact.GameEngine.Base
{
    public interface ILife
    {
        int Life { get; set; }
        bool IsAlive { get; }
    }
}
