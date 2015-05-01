using System;
using System.Timers;
using SpaceImpact.ConsoleUI.Map;
using SpaceImpact.GameEngine;
using Timer = System.Timers.Timer;

namespace SpaceImpact.ConsoleUI
{
    class ConsoleControl
    {
        // Review GY: поле не повинно бути публічним
        public Game _game;

        public ConsoleControl(Game game)
        {
            this._game = game;
        }

        public void EnemyMove(Enemy enemy)
        {
            var ec = new EnemyConsole();
            if (enemy.CanMotion(-1, 0))
            {
                ec.HideEnemy(_game);
                enemy.Motion(-1, 0);
                ec.DrawEnemy(_game);
            }
            else
            {
                ec.HideEnemy(_game);
                _game.Enemies.Remove(enemy);
            }
        }

        public void LaserMove(Laser laser)
        {
            var lc = new LaserConsole();
            if (laser.CanMotion(1, 0))
            {
                lc.HideLaserSpaceship(_game);
                laser.Motion(1, 0);
                lc.DrawLaserSpaceship(_game);
            }
            else
            {
                _game.Spaceship._lasers.Remove(laser);
                lc.HideLaserSpaceship(_game);
            }
        }

        public void OnLaserMotion(Object o, ElapsedEventArgs e)
        {
            foreach (var laser in _game.Spaceship._lasers)
            {
                LaserMove(laser);
            }
        }

        public void OnEnemyMotion(Object o, ElapsedEventArgs e)
        {
            /*
             * Review GY: видалення елементів з колекції, по котрій проходить foreach, 
             * призводить до виключної ситуації InvalidOperationException
             */
            foreach (var enemy in _game.Enemies)
            {
                EnemyMove(enemy);
            }
        }

        public void SpaceshipMotion()
        {
            var enemyTimer = new Timer {Interval = 200};
            enemyTimer.Elapsed += OnEnemyMotion;

            var laserTimer = new Timer { Interval = 100 };
            laserTimer.Elapsed += OnLaserMotion;

            var cmap = new ConsoleMap();
            var sc = new SpaceshipConsole();
            ProgressBar.ShowProgress();
            /*
             *Review GY: консоль варто чистити після натиснення клавіші Enter.
             *В іншому випадку користувача не встигає прочитати ваше повідомлення про умову старту гри.
             */
            Console.Clear();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                /*
                 * Review GY: в процесі гри при натисненні клавіші Enter вилітає ексепшин InvalidOperationException.
                 * Дана поведінка створює незручності користувачу.
                 */
                if (key.Key == ConsoleKey.Enter)
                {
                    _game.Start();
                    cmap.DrawFrontier(_game);
                    cmap.DrawMap(_game);
                    sc.DrawSpaceship(_game);
                    enemyTimer.Enabled = true;
                    //laserTimer.Enabled = true;
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (_game.Spaceship.CanMotion(-1, 0))
                    {
                        sc.HideSpaceship(_game);
                        _game.Spaceship.Motion(-1, 0);
                        sc.DrawSpaceship(_game);
                    }
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (_game.Spaceship.CanMotion(1, 0))
                    {
                        sc.HideSpaceship(_game);
                        _game.Spaceship.Motion(1, 0);
                        sc.DrawSpaceship(_game);
                    }
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    if (_game.Spaceship.CanMotion(0, -1))
                    {
                        sc.HideSpaceship(_game);
                        _game.Spaceship.Motion(0, -1);
                        sc.DrawSpaceship(_game);
                    }
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    if (_game.Spaceship.CanMotion(0, 1))
                    {
                        sc.HideSpaceship(_game);
                        _game.Spaceship.Motion(0, 1);
                        sc.DrawSpaceship(_game);
                    }
                }
                else if (key.Key==ConsoleKey.Spacebar)
                {
                    // Review GY: повинна бути реалізована можливість робити посріли
                    //_game.SpaceshipShoot(_game.Spaceship);
                }
                else if (key.Key == ConsoleKey.Q || key.Key == ConsoleKey.Escape)
                {
                    laserTimer.Enabled = false;
                    enemyTimer.Enabled = false;
                    _game.Stop();
                    break;
                }
            }
            Console.SetCursorPosition(0, 2 + 2 * _game.GameSpace.Height);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
