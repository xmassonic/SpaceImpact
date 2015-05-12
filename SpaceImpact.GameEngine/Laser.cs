using System.Collections.Generic;
using SpaceImpact.GameEngine.Base;

namespace SpaceImpact.GameEngine
{
    public class Laser : GameObject
    {
        public event Dispatcher.GameObjectDrawing LaserHide;
        public event Dispatcher.GameObjectDrawing LaserDraw;

        public void OnLaserHide(int pointX, int pointY)
        {
            if (LaserHide != null)
            {
                LaserHide(pointX, pointY);
            }
        }

        public void OnLaserDraw(int pointX, int pointY)
        {
            if (LaserDraw != null)
            {
                LaserDraw(pointX, pointY);
            }
        }

        public Laser Move(int pointX, int pointY, int changePointX, int changePointY)
        {
            OnLaserHide(pointX, pointY);
            pointX += changePointX;
            OnLaserDraw(pointX, pointY);
            return new Laser(pointX, pointY);
        }

        public Laser() { }
        public Laser(int pointX, int pointY)
        {
            X = pointX;
            Y = pointY;
        }
    }
}
