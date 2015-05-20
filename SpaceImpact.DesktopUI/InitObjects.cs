using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using SpaceImpact.GameEngine;
using Control = SpaceImpact.GameEngine.Control;


namespace SpaceImpact.DesktopUI
{
    public class InitObjects
    {
        private Timer _objectsTimer;
        private Timer _playerTimer;
        private Timer _bossShootTimer;
        private Timer _playerCanMoveTimer;
        private EventMethods _info;
        private Label BossLife { get; set; }
        private Label HeroLife { get; set; }
        private Label ScoreValue { get; set; }

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

        public Game Game { get; set; }

        public Spaceship Player { get; set; }

        public void InitAndStart(Label lblBossLifeValue, Label lblHeroLifeValue, Label lblScoreValue)
        {
            BossLife = lblBossLifeValue;
            HeroLife = lblHeroLifeValue;
            ScoreValue = lblScoreValue;

            _info = new EventMethods();

            Control control = new Control();
            control.GetAction += _info.OnGetAction;
            var bounds = new List<int> { _minXBound, _maxXBound, _minYBound, _maxYBound };

            Enemy enemy = new Enemy(_maxEnemyCount, _startEnemyCount, bounds);
            enemy.EnemyHide += _info.OnEnemyHide;
            enemy.EnemyDraw += _info.OnEnemyDraw;

            Laser laser = new Laser();
            laser.LaserHide += _info.OnLaserHide;
            laser.LaserDraw += _info.OnLaserDraw;

            Space space = new Space(_minPointX, _minPointY, _maxPointX, _maxPointY, 1, 1, 1);
            
            Player = new Spaceship(_playerLife, _playerStartPointX, _playerStartPointY);
            Player.DrawHealth += HeroDrawHealth;
            Player.HideSpaceship += _info.OnHideSpaceship;
            Player.DrawSpaceship += _info.OnDrawHero;

            Spaceship boss = new Spaceship(_bossLife, _bossStartPointX, _bossStartPointY);
            boss.DrawHealth += BossDrawHealth;
            boss.HideSpaceship += _info.OnHideSpaceship;
            boss.DrawSpaceship += _info.OnDrawBoss;

            Game = new Game(Player, boss, laser, enemy, control, space, 0, 0,
                0, 0, 0, 0);
            Game.GamePause += PauseGame;
            Game.GameResume += ResumeGame;
            Game.InitBoss += InitBoss;
            Game.TimersStart += TimersStart;
            Game.TimersStop += TimersStop;
            Game.GameScoreUpdate += ScoreUpdate;
            Game.ConfirmGameExit += Exit;
            InitTimers();
            Game.StartGame(Platform.Winforms);
        }

        
        private void Exit(int score, Game.ExitStatus status)
        {
            SpaceImpact.Status = status;
            SetResult();
            SpaceImpact.Exit();
        }

        private void SetResult()
        {
            switch (SpaceImpact.Status)
            {
                case Game.ExitStatus.WinGame:
                    SpaceImpact.Result = "You Win";
                    break;
                case Game.ExitStatus.LoseGame:
                    SpaceImpact.Result = "You Lose";
                    break;
                case Game.ExitStatus.StopGame:
                    SpaceImpact.Result = "You Stop Game";
                    break;
            }
        }

        private void BossDrawHealth(int pointx, int pointy, int info)
        {
            BossLife.Text = String.Empty;
            char lifePoint = Convert.ToChar(9829);
            string lifePart = null;
            for (int i = 0; i < info; i++)
            {
                lifePart = string.Concat(lifePart, lifePoint.ToString());
            }
            BossLife.Text = lifePart;
        }

        private void ScoreUpdate(int score, int pointx, int pointy)
        {
            ScoreValue.Text = score.ToString();
        }

        private void HeroDrawHealth(int pointx, int pointy, int info)
        {
            HeroLife.Text = String.Empty;
            char lifePoint = Convert.ToChar(9829);
            string lifePart = null;
            for (int i = 0; i < info; i++)
            {
                lifePart = string.Concat(lifePart, lifePoint.ToString());
            }
            HeroLife.Text = lifePart;
        }

        public void InitTimers()
        {
            _playerTimer = new Timer { Interval = 30 };
            _playerTimer.Tick += PlayerMove;

            _bossShootTimer = new Timer { Interval = 250 };
            _bossShootTimer.Tick += CanShootMove;

            _objectsTimer = new Timer { Interval = 35 };
            _objectsTimer.Tick += ObjectsMove;

            _playerCanMoveTimer = new Timer{Interval = 20};
            _playerCanMoveTimer.Tick += PlayerCanMove;
        }

        private void PlayerCanMove(object sender, EventArgs e)
        {
            Game.PlayerCanMove = true;
        }

        private void ObjectsMove(object sender, EventArgs e)
        {
            Game.GameObjectsTurn();
        }

        private void CanShootMove(object sender, EventArgs e)
        {
            Game.BossShoot();
        }

        private void PlayerMove(object sender, EventArgs e)
        {
            Game.PlayerMove();
        }

        public void TimersStart()
        {
            _objectsTimer.Start();
            _playerCanMoveTimer.Start();
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
            _playerCanMoveTimer.Enabled = false;
            if (Game.BossInited)
            {
                _bossShootTimer.Enabled = false;
            }
            _objectsTimer.Enabled = false;
        }

        public void ResumeGame()
        {
            _playerCanMoveTimer.Enabled = true;
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

        public void RestartGame()
        {
            InitAndStart(BossLife, HeroLife, ScoreValue);
        }
    }
}