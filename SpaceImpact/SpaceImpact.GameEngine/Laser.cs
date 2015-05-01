using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceImpact.GameEngine.BaseGameElements;

namespace SpaceImpact.GameEngine
{
    public class Laser: GameObject, IMotion
    {
        /*
         * Review GY: для чого даному класу потрібна властивість life?
         */
        public Laser(int x, int y, int life) : base(x, y, life) { }

        /*
         * Review GY: цей метод продубльований у всіх класах, похідних від GameObject.
         * Рекомендую перенести цей метод в базовий клас
         */
        public void Motion(int dx, int dy)
        {
            if (this.CanMotion(dx, dy) == false)
            {
                throw new ArgumentException("Laser cannot be moved to the specified positon");
            }

            this.X += dx;
            this.Y += dy;
        }

        /*
         * Review GY: цей метод продубльований у всіх класах, похідних від GameObject.
         * Рекомендую перенести цей метод в базовий клас
         */
        public bool CanMotion(int dx, int dy)
        {
            int newX = this.X + dx;
            int newY = this.Y + dy;
            if (this.BattleSpace != null)
            {
                return this.BattleSpace.CanLocated(this, newX, newY);
            }
            else
            {
                return false;
            }
        }

        /*
         * Review GY: цей метод продубльований у всіх похідних класах.
         * Рекомендую зробити його віртуальним і вразі потреби перевизначати його в похідних класах.
         */
        public new int IsAlive()
        {
            return (this.Life > 0 ? 1 : 0);
        }
    }
}
