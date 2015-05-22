using System;
using System.Collections.Generic;
using SpaceImpact.GameEngine.Base;

namespace SpaceImpact.GameEngine
{
    public class Enemy: GameObject
    {
        private readonly Random _random = new Random();

        //review VD: для чого потрібна ця змінна, якщо нижче оголошена така ж property?
        private readonly int _maxEnemyCount;
        public int MaxEnemyCount { get; set; }

        public int StartEnemyCount { get; set; }

        public List<int> Bounds { get; set; }

        public event Dispatcher.GameObjectDrawing EnemyHide;

        public event Dispatcher.GameObjectDrawing EnemyDraw;

        public void OnEnemyHide(int pointX, int pointY)
        {
            if (EnemyHide != null)
            {
                EnemyHide(pointX, pointY);
            }
        }

        public void OnEnemyDraw(int pointX, int pointY)
        {
            if (EnemyDraw != null)
            {
                EnemyDraw(pointX, pointY);
            }
        }

        public Enemy(int maxEnemyCount, int startEnemyCount, List<int> bounds)
        {
            MaxEnemyCount = maxEnemyCount;
            StartEnemyCount = startEnemyCount;
            //review VD: лишнє присвоєння
            _maxEnemyCount = MaxEnemyCount;
            Bounds = bounds;
        }

        private Enemy(int pointX, int pointY)
        {
            X = pointX;
            Y = pointY;
        }
        public List<Enemy> InitEnemies()
        {
            var listEnemy = new List<Enemy>();
            for (int i = 0; i < StartEnemyCount; i++)
            {
                listEnemy.Add(CreateEnemy());
            }
            return listEnemy;
        }

        public Enemy CreateEnemy()
        {
            return new Enemy(Bounds[1] - 2, _random.Next(Bounds[2] + 2, Bounds[3] - 2));
        }

        public Enemy Move(int pointX, int pointY, int changePointX, int changePointY)
        {
            var enemy = new Enemy(pointX, pointY);
            OnEnemyHide(enemy.X, enemy.Y);
            enemy.X += changePointX;
            enemy.Y += changePointY;
            OnEnemyDraw(enemy.X, enemy.Y);
            return enemy;
        }

        public bool CanMove(int changePointX, int changePointY, List<int> bounds)
        {
            bool canMove = true;
            if (bounds.Count == 4)
            {
                if (!((X + changePointX > bounds[0]) && (X + changePointX < bounds[1])
                      && (Y + changePointY > bounds[2]) && (Y + changePointY < bounds[3])))
                {
                    //review VD: тут можна було одразу повернути результат false
                    canMove = false;
                }

            }

            else
            {
                canMove = false;
            }
            return canMove;
        }

        public void ResetMaxCount()
        {
            MaxEnemyCount = _maxEnemyCount;
        }
    }
}
