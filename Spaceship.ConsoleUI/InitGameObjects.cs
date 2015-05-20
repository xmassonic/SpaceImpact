using System;
using System.Collections.Generic;
using SpaceImpact.GameEngine;
using System.Configuration;
using System.Threading;
using Timer = System.Timers.Timer;

namespace SpaceImpact.ConsoleUI
{
    class InitGameObjects
    {
        private Timer _objectsTimer;
        private Timer _playerTimer;
        private Timer _bossShootTimer;
        private SpaceshipEventMethods info;
        private readonly int _maxEnemyCount = Int32.Parse(ConfigurationManager.AppSettings["MaxEnemyCount"]);
        private readonly int _startEnemyCount = Int32.Parse(ConfigurationManager.AppSettings["StartEnemyCount"]);
        private readonly int _minXBound = Int32.Parse(ConfigurationManager.AppSettings["MinXBound"]);
        private readonly int _maxXBound = Int32.Parse(ConfigurationManager.AppSettings["MaxXBound"]);
        private readonly int _minYBound = Int32.Parse(ConfigurationManager.AppSettings["MinYBound"]);
        private readonly int _maxYBound = Int32.Parse(ConfigurationManager.AppSettings["MaxYBound"]);
        private readonly int _minPointX = Int32.Parse(ConfigurationManager.AppSettings["MinPointX"]);
        private readonly int _minPointY = Int32.Parse(ConfigurationManager.AppSettings["MinPointY"]);
        private readonly int _maxPointX = Int32.Parse(ConfigurationManager.AppSettings["MaxPointX"]);
        private readonly int _maxPointY = Int32.Parse(ConfigurationManager.AppSettings["MaxPointY"]);
        private readonly int _playerLife = Int32.Parse(ConfigurationManager.AppSettings["PlayerLife"]);
        private readonly int _playerStartPointX = Int32.Parse(ConfigurationManager.AppSettings["PlayerStartPointX"]);
        private readonly int _playerStartPointY = Int32.Parse(ConfigurationManager.AppSettings["PlayerStartPointY"]);
        private readonly int _bossLife = Int32.Parse(ConfigurationManager.AppSettings["BossLife"]);
        private readonly int _bossStartPointX = Int32.Parse(ConfigurationManager.AppSettings["BossStartPointX"]);
        private readonly int _bossStartPointY = Int32.Parse(ConfigurationManager.AppSettings["BossStartPointY"]);
        private readonly int _scoreValueX = Int32.Parse(ConfigurationManager.AppSettings["ScoreValueX"]);
        private readonly int _scoreValueY = Int32.Parse(ConfigurationManager.AppSettings["ScoreValueY"]);
        private readonly int _playerHealthX = Int32.Parse(ConfigurationManager.AppSettings["PlayerHealthX"]);
        private readonly int _playerHealthY = Int32.Parse(ConfigurationManager.AppSettings["PlayerHealthY"]);
        private readonly int _bossHealthX = Int32.Parse(ConfigurationManager.AppSettings["BossHealthX"]);
        private readonly int _bossHealthY = Int32.Parse(ConfigurationManager.AppSettings["BossHealthY"]);
        public Game Game { get; set; }

        public void InitAndStart()
        {
            
            info = new SpaceshipEventMethods();
            
            Control control = new Control();
            control.GetAction += info.OnGetAction;
            var bounds = new List<int>{_minXBound, _maxXBound, _minYBound, _maxYBound};
            
            Enemy enemy = new Enemy(_maxEnemyCount, _startEnemyCount, bounds);
            enemy.EnemyHide += info.OnEnemyHide;
            enemy.EnemyDraw += info.OnEnemyDraw;
            
            Laser laser = new Laser();
            laser.LaserHide += info.OnLaserHide;
            laser.LaserDraw += info.OnLaserDraw;

            Space space = new Space(_minPointX, _minPointY,_maxPointX, _maxPointY, _minXBound, _minYBound, _minYBound + 2);
            space.BordersDraw += info.OnBordersDraw;
            space.GameSpaceDraw += info.OnGameSpaceDraw;

            Spaceship player = new Spaceship(_playerLife,_playerStartPointX, _playerStartPointY);
            player.DrawHealth += info.OnDrawHealth;
            player.HideSpaceship += info.OnHideSpaceship;
            player.DrawSpaceship += info.OnDrawHero;

            Spaceship boss = new Spaceship(_bossLife, _bossStartPointX, _bossStartPointY);
            boss.DrawHealth += info.OnDrawHealth;
            boss.HideSpaceship += info.OnHideSpaceship;
            boss.DrawSpaceship += info.OnDrawBoss;

            Game = new Game(player, boss, laser, enemy, control, space, _scoreValueX, _scoreValueY,
                _playerHealthX, _playerHealthY, _bossHealthX, _bossHealthY);
            Game.GameScoreUpdate += info.OnGameScoreUpdate;
            Game.ConfirmGameExit += info.OnConfirmGameExit;
            Game.ClearInfoBlock += info.OnClearInfoBlock;
            Game.ShowCtrlButtons += info.OnShowCtrlButtons;
            Game.GameSpaceClear += info.OnGameSpaceClear;
            Game.GamePause += PauseGame;
            Game.GameResume += ResumeGame;
            Game.InitBoss += InitBoss;
            Game.TimersStart += TimersStart;
            Game.TimersStop += TimersStop;
            InitTimers();
            Game.StartGame(Platform.Console);
            ConfirmExit();
        }
        public void ShowProgress()
        {
            Console.CursorVisible = false;
            Console.BufferHeight = _maxPointY + 3;
            Console.BufferWidth = _maxPointX + 3;
            Console.WindowHeight = _maxPointY + 3;
            Console.WindowWidth = _maxPointX + 3;
            Console.WriteLine("Loading... ");
            Print("Loading game...", 100);

            int pos = Console.CursorTop;
            Console.SetCursorPosition(11, 0);
            Console.Write("OK");
            Console.SetCursorPosition(0, pos);

            var rand = new Random();

            for (int i = 0; i <= 100; i++)
            {
                if (i < 25)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (i < 50)
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                else if (i < 75)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                else if (i < 100)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else
                    Console.ForegroundColor = ConsoleColor.Green;

                string pct = string.Format("{0,-30} {1,3}%", new string((char)0x2592, i * 30 / 100), i);
                Console.CursorLeft = 0;
                Console.Write(pct);
                Thread.Sleep(rand.Next(0, 20));
            }
            Console.Clear();
        }

        private void Print(string message, int delay)
        {
            Thread.Sleep(delay);
            Console.WriteLine(message);
        }

        public void InitTimers()
        {
            _playerTimer = new Timer { Interval = 30 };
            _playerTimer.Elapsed += Game.OnPlayerCanMove;

            _bossShootTimer = new Timer { Interval = 250 };
            _bossShootTimer.Elapsed += Game.OnBossShoot;

            _objectsTimer = new Timer { Interval = 30 };
            _objectsTimer.Elapsed += Game.OnGameObjectsTurn;
        }

        public void TimersStart()
        {
            _objectsTimer.Start();
            _playerTimer.Start();
        }
        public void TimersStop()
        {
            _bossShootTimer.Stop();
            _objectsTimer.Stop();
            _playerTimer.Stop();
        }

        public void PauseGame()
        {
            _playerTimer.Enabled = false;
            if (Game.BossInited)
            {
                _bossShootTimer.Enabled = false;
            }
            _objectsTimer.Enabled = false;
        }

        public void ResumeGame()
        {
            _playerTimer.Enabled = true;
            if (Game.BossInited)
            {
                _bossShootTimer.Enabled = true;
            }
            _objectsTimer.Enabled = true;
        }

        public void InitBoss()
        {
            _bossShootTimer.Start();
        }

        public void ConfirmExit()
        {
            ClearInfoBlock();
            info.OnRestartGame();
            var gameAction = info.OnGetAction();
            while (gameAction != GameControl.EndGame && gameAction != GameControl.StartGame)
            {
                gameAction = info.OnGetAction();
            }
            switch (gameAction)
            {
                case GameControl.EndGame:
                    ClearInfoBlock();
                    break;
                case GameControl.StartGame:
                    info.OnGameSpaceClear();
                    RestartGame();
                    break;
            }
        }

        private void ClearInfoBlock()
        {
            info.OnClearInfoBlock();
        }

        public void RestartGame()
        {
            InitAndStart();
        }
    }
}
