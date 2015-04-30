using System;
// review VD: непотрібні простори імен
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceImpact.GameEngine.BaseGameElements
{
    public interface IGameObject: IGameElement
    {
        // review VD: навіщо модифікатор new?
        new Space Space { get; set; }
        BattleSpace BattleSpace { get; set; }
        // review VD: навіщо оператор new?
        new int X { get; }
        // review VD: навіщо модифікатор new?
        new int Y { get; }
        int Life { get; }
        int IsAlive();
    }
}
