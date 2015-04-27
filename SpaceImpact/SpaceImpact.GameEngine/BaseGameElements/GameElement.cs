using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceImpact.GameEngine.BaseGameElements
{
    public class GameElement: IGameElement
    {
        #region Fields

        private Space _space = null;
        private int _x;
        private int _y;
        
        #endregion

        #region Constructors

        public GameElement(int x, int y, int life)
        {
            this._x = x;
            this._y = y;
        }

        #endregion

        #region IGameElement Members

        public Space Space
        {
            get { return this._space; }
            set { this._space = value; }
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

        #endregion
    }
}
