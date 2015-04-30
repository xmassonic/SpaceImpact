using System;
// review VD: непотрібні простори імен
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceImpact.GameEngine.BaseGameElements
{
    public class GameObject: IGameObject
    {
        #region Fields

        // review VD: не бачу необхідності оголошувати ці поля, якщо використовуються відподні проперті
        private Space _space = null;
        private BattleSpace _battleSpace = null;
        private int _x;
        private int _y;
        private int _life;
        
        #endregion

        #region Constructors

        public GameObject(int x, int y, int life)
        {
            this._x = x;
            this._y = y;
            this._life = life;
        }

        #endregion

        #region IGameObject Members

        public Space Space
        {
            get { return this._space; }
            set { this._space = value; }
        }

        public BattleSpace BattleSpace
        {
            get { return this._battleSpace; }
            set { this._battleSpace = value; }
        }

        public int X
        {
            get { return this._x; }
            protected set { this._x = value; }
        }

        public int Y
        {
            get { return this._y; }
            protected set { this._y = value; }
        }

        public int Life
        {
            get { return this._life; }
            protected set { this._life = value; }
        }

        public int IsAlive()
        {
            return (this._life > 0 ? 1 : 0);
        }

        #endregion
    }
}
