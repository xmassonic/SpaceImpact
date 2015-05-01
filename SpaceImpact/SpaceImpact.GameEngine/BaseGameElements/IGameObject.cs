using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceImpact.GameEngine.BaseGameElements
{
    /*
     * Review GY: не потрібно перевизначати властивості Space, X, Y, оскільки вони описані в батьківському інтерфейсі.
     * Після видалення цих властивостей проект залишився в робочому стані.
     */
    public interface IGameObject: IGameElement
    {
        //new Space Space { get; set; }
        BattleSpace BattleSpace { get; set; }
        //new int X { get; }
        //new int Y { get; }
        int Life { get; }
        int IsAlive();
    }
}
