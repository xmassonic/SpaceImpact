using System.Collections.Generic;
using SpaceImpact.GameEngine.Base;

namespace SpaceImpact.GameEngine
{
    public sealed class Spaceship : GameObject, IMovable, ILife
    {
        private int _lifeCount;
        public event Dispatcher.SpaceshipDrawing HideSpaceship;

        public event Dispatcher.SpaceshipDrawing DrawSpaceship;

        public event Dispatcher.GameInfo DrawHealth;

        public int StartPointX { get; private set; }
        public int StartPointY { get; private set; }

        public void OnHideSpaceship(List<SpaceshipFragment> hero)
        {
            if (HideSpaceship != null)
            {
                HideSpaceship(hero);
            }
        }

        public void OnDrawSpaceship(List<SpaceshipFragment> hero)
        {
            if (DrawSpaceship != null)
            {
                DrawSpaceship(hero);
            }
        }

        public void OnDrawHealth(int pointX, int pointY, int health)
        {
            if (DrawHealth != null)
            {
                DrawHealth(pointX, pointY, health);
            }
        }

        public void Move(int changePointX, int changePointY)
        {
            OnHideSpaceship(Model);
            foreach (var h in Model)
            {
                h.X += changePointX;
                h.Y += changePointY;
            }
            OnDrawSpaceship(Model);
        }

        public bool CanMove(int changePointX, int changePointY, List<int> bounds)
        {
            bool canMove = true;
            if (bounds.Count == 4)
            {
                foreach (var h in Model)
                {
                    if (!((h.X + changePointX > bounds[0]) && (h.X + changePointX < bounds[1])
                          && (h.Y + changePointY > bounds[2]) && (h.Y + changePointY < bounds[3])))
                    {
                        canMove = false;
                    }

                }
            }
            else
            {
                canMove = false;
            }
            return canMove;
        }

        public int Life { get; set; }

        public List<SpaceshipFragment> Model { get; set; }

        public List<Laser> Lasers { get; set; }

        public bool IsAlive
        {
            get { return (Life > 0);}
        }

        public Spaceship(int lifeCount, int pointX, int pointY)
        {
            Life = lifeCount;
            _lifeCount = Life;
            StartPointX = pointX;
            StartPointY = pointY;
            Model = new List<SpaceshipFragment>();
            Lasers = new List<Laser>();
        }

        public List<SpaceshipFragment> InitSpaceship()
        {
            Model.Add(new SpaceshipFragment(StartPointX, StartPointY));
            Model.Add(new SpaceshipFragment(StartPointX + 1, StartPointY));
            Model.Add(new SpaceshipFragment(StartPointX + 2, StartPointY));
            Model.Add(new SpaceshipFragment(StartPointX, StartPointY + 1));
            Model.Add(new SpaceshipFragment(StartPointX + 1, StartPointY + 1));
            Model.Add(new SpaceshipFragment(StartPointX + 2, StartPointY + 1));
            Model.Add(new SpaceshipFragment(StartPointX, StartPointY + 2));
            Model.Add(new SpaceshipFragment(StartPointX + 1, StartPointY + 2));
            Model.Add(new SpaceshipFragment(StartPointX + 2, StartPointY + 2));
            return Model;
        }

        public void Shoot(Laser laser)
        {
            Lasers.Add(laser);
        }

        public void RemoveLaser(Laser laser)
        {
            if (Lasers.Contains(laser))
            {
                Lasers.Remove(laser);
            }
        }

        public void ResetHealth()
        {
            Life = _lifeCount;
        }
    }
}
