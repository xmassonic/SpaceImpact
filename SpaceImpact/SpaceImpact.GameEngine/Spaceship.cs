using System;
// review VD: непотрібні простори імен
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceImpact.GameEngine.BaseGameElements;

namespace SpaceImpact.GameEngine
{
    public class Spaceship: GameObject, IMotion
    {
        // review VD: поле не повинно бути відкритим
        public List<Laser> _lasers = new List<Laser>();

        public Spaceship(int x, int y, int life) : base(x, y, life) { }

        public void Motion(int dx, int dy)
        {
            if (this.CanMotion(dx, dy) == false)
            {
                throw new ArgumentException("Spaceship cannot be moved to the specified positon");
            }

            this.X += dx;
            this.Y += dy;
        }

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

        // review VD: навіщо модифікатор new?
        public new int IsAlive()
        {
            return (this.Life > 0 ? 1 : 0);
        }

        public List<Laser> Lasers
        {
            get { return this._lasers; }
        }

        //public void TakeAim(Laser laser)
        //{
        //    _lasers.Add(laser);
        //}

    }
}
