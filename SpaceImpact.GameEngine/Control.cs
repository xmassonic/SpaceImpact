using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceImpact.GameEngine
{
    public class Control
    {
        public delegate GameControl Action();

        public event Action GetAction;

        public GameControl OnGetAction()
        {
            if (GetAction != null)
            {
                return GetAction();
            }
            return GameControl.DafaultAction;
        }
    }
}
