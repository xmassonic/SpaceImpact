using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SpaceImpact.GameEngine
{
    #region Public Enum

    public enum GameControl
    {
        //review VD: явне присвоєння можна було опустити, оскільки елементи перечислення отримають відповідні значенння автоматично
        StartGame = 0,
        EndGame = 1,
        PauseGame = 2,
        MoveLeft = 3,
        MoveRight = 4,
        MoveUp = 5,
        MoveDown = 6,
        SpaceshipShoot = 7,
        ResumeGame = 8,
        DafaultAction = 9
    }

    #endregion

    public class Game
    {
        #region Private Fields

        private Spaceship _hero;
        private Spaceship _boss;
        private Laser _laser;
        private Enemy _enemy;
        private Control _control;
        private Space _space;
        private Timer _objectsTimer;
        private Timer _playerTimer;
        private Timer _bossShootTimer;
        private GameControl _playerAction;
        private List<Enemy> _enemies;
        private GameControl _bossAction;
        private GameStatus _gameStatus;

        #endregion

        #region Delegate & event

        public delegate void GameScore(int score, int pointX, int pointY);

        public delegate void GameExit(int score);

        public delegate void ExitKind(int score, ExitStatus status);

        public delegate void InfoBlock();

        public event GameScore GameScoreUpdate;

        public event ExitKind ConfirmGameExit;

        public event GameExit GameIsOver;

        public event InfoBlock ClearInfoBlock;

        public event InfoBlock ShowCtrlButtons;

        #endregion

        #region Private Properties

        private int ScorePointY { get; set; }

        private int ScorePointX { get; set; }

        private int HeroHealthPointX { get; set; }

        private int HeroHealthPointY { get; set; }

        private int BossHealthPointX { get; set; }

        private int BossHealthPointY { get; set; }

        private int Score { get; set; }

        [DefaultValue(false)]
        private bool BossInited { get; set; }

        #endregion

        #region Private Enum

        private enum GameStatus
        {
            //review VD: явне присвоєння можна було опустити, оскільки елементи перечислення отримають відповідні значенння автоматично
            InProgress = 0,
            Paused = 1,
            Stopped = 2,
            GameEnd = 3
        }

        public enum ExitStatus
        {
            //review VD: явне присвоєння можна було опустити, оскільки елементи перечислення отримають відповідні значенння автоматично
            WinGame = 0,
            LoseGame = 1,
            StopGame = 2,
            DefaultStatus = 3
        }
        #endregion

        #region Constructor

        public Game(Spaceship hero, Spaceship boss, Laser laser, Enemy enemy, Control control, Space space, int scorePointX,
            int scorePointY, int heroHealthPointX, int heroHealthPointY, int bossHealthPointX, int bossHealthPointY)
        {
            _hero = hero;
            _boss = boss;
            _laser = laser;
            _enemy = enemy;
            _control = control;
            _space = space;
            ScorePointX = scorePointX;
            ScorePointY = scorePointY;
            HeroHealthPointX = heroHealthPointX;
            HeroHealthPointY = heroHealthPointY;
            BossHealthPointX = bossHealthPointX;
            BossHealthPointY = bossHealthPointY;
        }

        #endregion

        #region Collisions

        private void LasersCollisions(List<Laser> lasers, List<Enemy> enemies)
        {
            int collisionsCount = 0;
            for (int i = 0; i < lasers.Count; i++)
            {
                var laser = lasers[i];
                for (int j = 0; j < enemies.Count; j++)
                {
                    var enemy = enemies[j];
                    // review VD: умови варто відділити дужками
                    if (laser.X == enemy.X && laser.Y == enemy.Y ||
                        laser.X + 1 == enemy.X && laser.Y == enemy.Y)
                    {
                        _laser.OnLaserHide(laser.X, laser.Y);
                        _enemy.OnEnemyHide(enemy.X, enemy.Y);
                        enemies.Remove(enemy);
                        lasers.Remove(laser);
                        collisionsCount++;
                        //review VD: використання магічного числа
                        Score += 10;
                        OnGameScoreUpdate(Score, ScorePointX, ScorePointY);
                    }
                }
            }
            for (int i = 0; i < collisionsCount; i++)
            {
                if (_enemy.MaxEnemyCount > 0)
                {
                    _enemies.Add(_enemy.CreateEnemy());
                    _enemy.MaxEnemyCount--;
                }
            }
        }

        private void BossCollisions(List<Laser> pLasers, List<Laser> bLasers)
        {
            PlayerCrash(_hero.Model, bLasers);
            BossCrash(_boss.Model, pLasers);
            LaserCrash(pLasers, bLasers);
        }

        private bool EnemyCrashPlayer(Enemy enemy)
        {
            bool crash = false;
            foreach (var h in _hero.Model)
            {
                // review VD: умови варто відділити дужками
                if (enemy.X - 1 == h.X && enemy.Y == h.Y)
                {
                    crash = true;
                }
            }
            return crash;
        }

        private void PlayerCrash(List<SpaceshipFragment> spaceship, List<Laser> bLasers)
        {
            for (int i = 0; i < bLasers.Count; i++)
            {
                var laser = bLasers[i];
                for (int j = 0; j < spaceship.Count; j++)
                {
                    var element = spaceship[j];
                    // review VD: умови варто відділити дужками
                    if (laser.X - 1 == element.X && laser.Y == element.Y ||
                        laser.X == element.X && laser.Y == element.Y)
                    {
                        if (_hero.Life > 1)
                        {
                            _laser.OnLaserHide(laser.X, laser.Y);
                            bLasers.Remove(laser);
                            _hero.Life -= 1;
                            _hero.OnDrawHealth(HeroHealthPointX, HeroHealthPointY, _hero.Life);
                        }
                        if (_hero.Life == 1)
                        {
                            _laser.OnLaserHide(laser.X, laser.Y);
                            bLasers.Remove(laser);
                            _hero.Life -= 1;
                            _hero.OnDrawHealth(HeroHealthPointX, HeroHealthPointY, _hero.Life);
                            _gameStatus = GameStatus.Stopped;
                            StopTimers();
                            ConfirmExit(ExitStatus.LoseGame);
                        }
                    }
                }
            }
        }

        private void BossCrash(List<SpaceshipFragment> spaceship, List<Laser> pLasers)
        {
            for (int i = 0; i < pLasers.Count; i++)
            {
                var laser = pLasers[i];
                for (int index = 0; index < spaceship.Count; index++)
                {
                    var element = spaceship[index];
                    // review VD: умови варто відділити дужками
                    if (laser.X - 1 == element.X && laser.Y == element.Y ||
                        laser.X == element.X && laser.Y == element.Y)
                    {
                        _laser.OnLaserHide(laser.X, laser.Y);
                        pLasers.Remove(laser);
                        if (_boss.Life > 1)
                        {
                            _boss.Life -= 1;
                            _boss.OnDrawHealth(BossHealthPointX, BossHealthPointY, _boss.Life);
                            Score += 20;
                            OnGameScoreUpdate(Score, ScorePointX, ScorePointY);
                        }
                        if (_boss.Life == 1)
                        {
                            _boss.Life -= 1;
                            _boss.OnDrawHealth(BossHealthPointX, BossHealthPointY, _boss.Life);
                            Score += 20;
                            OnGameScoreUpdate(Score, ScorePointX, ScorePointY);
                            _gameStatus = GameStatus.Stopped;
                            StopTimers();
                            ConfirmExit(ExitStatus.WinGame);
                        }
                    }
                }
            }
        }

        private void LaserCrash(List<Laser> pLasers, List<Laser> bLasers)
        {
            for (int i = 0; i < pLasers.Count; i++)
            {
                var pLaser = pLasers[i];
                for (int j = 0; j < bLasers.Count; j++)
                {
                    var bLaser = bLasers[j];
                    // review VD: умови варто відділити дужками
                    if (pLaser.X + 1 == bLaser.X && pLaser.Y == bLaser.Y ||
                        pLaser.X == bLaser.X && pLaser.Y == bLaser.Y)
                    {
                        _laser.OnLaserHide(pLaser.X, pLaser.Y);
                        pLasers.Remove(pLaser);
                        _laser.OnLaserHide(bLaser.X, bLaser.Y);
                        bLasers.Remove(bLaser);
                    }
                }
            }
        }

        private bool LaserNearBorder(Laser laser, List<int> bounds)
        {
            // review VD: умови варто відділити дужками
            var laserNearBorder = laser.X + 1 == bounds[1] || laser.X == bounds[0];
            return laserNearBorder;
        }

        #endregion

        #region Game Objects Motion

        private void GameObjectsTurn(object sender, ElapsedEventArgs e)
        {
            // review VD: умови варто відділити дужками
            if (_enemies.Count == 0 && !BossInited)
            {
                BossInit();
            }
            LaserMove(_hero.Lasers);
            if (_enemies.Count > 0)
            {
                LasersCollisions(_hero.Lasers, _enemies);
                EnemyMove();
            }
            if (BossInited)
            {
                BossCollisions(_hero.Lasers, _boss.Lasers);
                BossMove();
                BossLasersMove(_boss.Lasers);
            }
        }


        private void BossLasersMove(List<Laser> lasers)
        {
            for (int index = 0; index < lasers.Count; index++)
            {
                var laser = lasers[index];
                if (LaserNearBorder(laser, _space.Bounds))
                {
                    _laser.OnLaserHide(laser.X, laser.Y);
                    lasers.Remove(laser);
                }
                if (lasers.Contains(laser))
                {
                    lasers[index] = _laser.Move(laser.X, laser.Y, -1, 0);
                }
            }
        }

        private void PlayerMove()
        {
            var bounds = _space.Bounds;
            _playerAction = _control.OnGetAction();
            switch (_playerAction)
            {
                case GameControl.MoveUp:
                    if (_hero.CanMove(0, -1, bounds))
                    {
                        _hero.Move(0, -1);
                    }
                    break;
                case GameControl.MoveDown:
                    if (_hero.CanMove(0, 1, bounds))
                    {
                        _hero.Move(0, 1);
                    }
                    break;
                case GameControl.MoveRight:
                    if (_hero.CanMove(1, 0, bounds))
                    {
                        _hero.Move(1, 0);
                    }
                    break;
                case GameControl.MoveLeft:
                    if (_hero.CanMove(-1, 0, bounds))
                    {
                        _hero.Move(-1, 0);
                    }
                    break;
                case GameControl.SpaceshipShoot:
                    HeroShoot();
                    break;
                case GameControl.PauseGame:
                    if (_gameStatus == GameStatus.InProgress)
                    {
                        PauseGame();
                    }
                    break;
                case GameControl.ResumeGame:
                    if (_gameStatus == GameStatus.Paused)
                    {
                        ResumeGame();
                    }
                    break;
                case GameControl.EndGame:
                    _gameStatus = GameStatus.Stopped;
                    StopTimers();
                    ConfirmExit(ExitStatus.StopGame);
                    break;
            }
            PlayerCanMove = false;
        }

        private void BossMove()
        {
            if (_bossAction == GameControl.MoveUp && _boss.CanMove(0, -1, _space.Bounds))
            {
                _boss.Move(0, -1);
            }
            if (_bossAction == GameControl.MoveUp && !_boss.CanMove(0, -1, _space.Bounds))
            {
                _bossAction = GameControl.MoveDown;
                _boss.Move(0, 1);
            }
            if (_bossAction == GameControl.MoveDown && _boss.CanMove(0, 1, _space.Bounds))
            {
                _boss.Move(0, 1);
            }
            if (_bossAction == GameControl.MoveDown && !_boss.CanMove(0, 1, _space.Bounds))
            {
                _bossAction = GameControl.MoveUp;
                _boss.Move(0, -1);
            }
        }

        public void EnemyMove()
        {
            for (int index = 0; index < _enemies.Count; index++)
            {
                var enemy = _enemies[index];
                if (EnemyCrashPlayer(enemy))
                {
                    if (_hero.Life > 1)
                    {
                        _hero.Life--;
                        _hero.OnDrawHealth(HeroHealthPointX, HeroHealthPointY, _hero.Life);
                        _enemy.OnEnemyHide(enemy.X, enemy.Y);
                        _enemies.Remove(enemy);
                        if (_enemy.MaxEnemyCount > 0)
                        {
                            _enemies.Add(_enemy.CreateEnemy());
                            _enemy.MaxEnemyCount--;
                        }
                    }
                    else if (_hero.Life == 1)
                    {
                        _hero.Life--;
                        _hero.OnDrawHealth(HeroHealthPointX, HeroHealthPointY, _hero.Life);
                        _gameStatus = GameStatus.Stopped;
                        StopTimers();
                        ConfirmExit(ExitStatus.LoseGame);
                        break;
                    }
                }
                if (EnemyNearBorder(enemy))
                {
                    _hero.Life--;
                    _hero.OnDrawHealth(HeroHealthPointX, HeroHealthPointY, _hero.Life);
                    _enemy.OnEnemyHide(enemy.X, enemy.Y);
                    _enemies.Remove(enemy);
                    if (_enemy.MaxEnemyCount > 0)
                    {
                        _enemies.Add(_enemy.CreateEnemy());
                        _enemy.MaxEnemyCount--;
                    }
                }
                if (_enemies.Contains(enemy))
                {
                    var movedEnemy = _enemy.Move(enemy.X, enemy.Y, -1, 0);
                    _enemies[index] = movedEnemy;
                }
                
            }
        }

        private bool EnemyNearBorder(Enemy enemy)
        {
            bool nearborder = enemy.X - 1 == _space.Bounds[0];
            return nearborder;
        }

        public void LaserMove(List<Laser> lasers)
        {
            for (int index = 0; index < lasers.Count; index++)
            {
                var laser = lasers[index];
                if (LaserNearBorder(laser, _space.Bounds))
                {
                    _laser.OnLaserHide(laser.X, laser.Y);
                    lasers.Remove(laser);
                }
                else
                {
                    lasers[index] = _laser.Move(laser.X, laser.Y, 1, 0);
                }
            }
        }

        private void BossShoot()
        {
            var laser = new Laser(_boss.Model[0].X - 1, _boss.Model[0].Y);
            _boss.Shoot(laser);
            var laser1 = new Laser(_boss.Model[6].X - 1, _boss.Model[6].Y);
            _boss.Shoot(laser1);
        }

        private void HeroShoot()
        {
            var laser = new Laser(_hero.Model[4].X + 2, _hero.Model[4].Y);
            _hero.Shoot(laser);
        }

        #endregion

        #region Timers

        private void InitTimers()
        {
            _playerTimer = new Timer { Interval = 30 };
            _playerTimer.Elapsed += OnPlayerCanMove;

            _bossShootTimer = new Timer { Interval = 800 };
            _bossShootTimer.Elapsed += OnBossShoot;

            _objectsTimer = new Timer { Interval = 50 };
            _objectsTimer.Elapsed += GameObjectsTurn;
            _objectsTimer.Disposed += (sender, args) => BossInited = false;
        }

        private void StartTimers()
        {
            _objectsTimer.Start();
            _playerTimer.Start();
        }

        private void StopTimers()
        {
            _bossShootTimer.Stop();
            _bossShootTimer.Close();
            _objectsTimer.Stop();
            _objectsTimer.Dispose();
            _objectsTimer.EndInit();
            _playerTimer.Stop();
            _playerTimer.Dispose();
        }

        #endregion

        #region Game Status

        public void StartGame()
        {
            OnShowCtrlButtons();
            _space.OnBordersDraw();
            _space.OnGameSpaceDraw();
            GameObjectsInit();
            InitTimers();
            StartTimers();
            _gameStatus = GameStatus.InProgress;
            GameProcess();
        }

        public void EndGame()
        {
            _gameStatus = GameStatus.GameEnd;
        }

        private void RestartGame()
        {
            GameObjectsClear();
            GameObjectsInit();
            InitTimers();
            StartTimers();
        }

        private void ResumeGame()
        {
            _playerTimer.Enabled = true;
            if (BossInited)
            {
                _bossShootTimer.Enabled = true;
            }
            _objectsTimer.Enabled = true;
            _gameStatus = GameStatus.InProgress;
        }

        private void PauseGame()
        {
            _playerTimer.Enabled = false;
            if (BossInited)
            {
                _bossShootTimer.Enabled = false;
            }
            _objectsTimer.Enabled = false;
            _gameStatus = GameStatus.Paused;
        }

        private void ConfirmExit(ExitStatus status)
        {
            OnGameConfirmExit(Score, status);
            var gameAction = _control.OnGetAction();
            // review VD: умови варто відділити дужками
            while (gameAction != GameControl.EndGame && gameAction != GameControl.StartGame)
            {
                gameAction = _control.OnGetAction();
            }
            switch (gameAction)
            {
                case GameControl.EndGame:
                    OnClearInfoBlock();
                    OnGameIsOver(Score);
                    _control.OnGetAction();
                    EndGame();
                    break;
                case GameControl.StartGame:
                    OnClearInfoBlock();
                    RestartGame();
                    break;
            }
        }

        private void OnClearInfoBlock()
        {
            if (ClearInfoBlock != null)
            {
                ClearInfoBlock();
            }
        }

        #endregion

        #region Game Obgjects Create & Destroy

        private void BossInit()
        {
            _boss.InitSpaceship();
            _boss.OnDrawSpaceship(_boss.Model);
            _boss.OnDrawHealth(BossHealthPointX, BossHealthPointY, _boss.Life);
            BossInited = true;
            _bossAction = GameControl.MoveUp;
            _bossShootTimer.Start();
        }
        private void GameObjectsInit()
        {
            _hero.InitSpaceship();
            _hero.OnDrawSpaceship(_hero.Model);
            _hero.OnDrawHealth(HeroHealthPointX, HeroHealthPointY, _hero.Life);
            _enemies = _enemy.InitEnemies();
            foreach (var enemy in _enemies)
            {
                _enemy.OnEnemyDraw(enemy.X, enemy.Y);
            }
            OnGameScoreUpdate(Score, ScorePointX, ScorePointY);
            _gameStatus = GameStatus.InProgress;
        }

        private void GameProcess()
        {
            // review VD: умови варто відділити дужками
            while (_gameStatus != GameStatus.GameEnd && _hero.IsAlive)
            {
                if (_gameStatus == GameStatus.Paused)
                {
                    if (_control.OnGetAction() == GameControl.ResumeGame)
                    {
                        ResumeGame();
                    }
                }
                // review VD: умови варто відділити дужками
                if (PlayerCanMove && _gameStatus != GameStatus.Paused)
                {
                    PlayerMove();
                }   
            }
            if (_gameStatus == GameStatus.Stopped)
            {
                ExitStatus status = ExitStatus.DefaultStatus;
                // review VD: умови варто відділити дужками
                if (_hero.Life > 0 && _boss.Life > 0)
                {
                    status = ExitStatus.StopGame;
                }
                // review VD: умови варто відділити дужками
                if (_hero.Life == 0 && _boss.Life > 0)
                {
                    status = ExitStatus.LoseGame;
                }
                // review VD: умови варто відділити дужками
                if (_hero.Life > 0 && _boss.Life == 0)
                {
                    status = ExitStatus.WinGame;
                }
                ConfirmExit(status);
            }
        }

        private void GameObjectsClear()
        {
            LasersClear();
            SpaceshipsClear();
            EnemiesClear();
            BossInited = false;
            StopTimers();
            
        }

        private void EnemiesClear()
        {
            //review VD: навіщо занулювати значення змінної index на кожному кроці циклу?
            for (int index = 0; index < _enemies.Count; index = 0)
            {
                var enemy = _enemies[index];
                _enemy.OnEnemyHide(enemy.X, enemy.Y);
                _enemies.Remove(enemy);
            }
            _enemy.ResetMaxCount();
        }

        private void SpaceshipsClear()
        {
            _hero.OnHideSpaceship(_hero.Model);
            _hero.Model.Clear();
            _hero.ResetHealth();
            _boss.OnHideSpaceship(_boss.Model);
            _boss.Model.Clear();
            _boss.ResetHealth();
        }

        private void LasersClear()
        {
            //review VD: навіщо занулювати значення змінної index на кожному кроці циклу?
            for (int index = 0; index < _hero.Lasers.Count; index = 0)
            {
                var laser = _hero.Lasers[index];
                _laser.OnLaserHide(laser.X, laser.Y);
                _hero.Lasers.Remove(laser);
            }
            // review VD: цей кусок коду краще було б винести в окремий метод
            //review VD: навіщо занулювати значення змінної index на кожному кроці циклу?
            for (int index = 0; index < _boss.Lasers.Count; index = 0)
            {
                var laser = _boss.Lasers[index];
                _laser.OnLaserHide(laser.X, laser.Y);
                _boss.Lasers.Remove(laser);
            }
        }
        

        #endregion

        #region Handlers

        private void OnBossShoot(object sender, ElapsedEventArgs e)
        {
            BossShoot();
        }

        private void OnPlayerCanMove(object sender, ElapsedEventArgs e)
        {
            PlayerCanMove = true;
        }

        public bool PlayerCanMove { get; set; }

        public void OnGameScoreUpdate(int score, int pointX, int pointY)
        {
            if (GameScoreUpdate != null)
            {
                GameScoreUpdate(score, pointX, pointY);
            }
        }

        public void OnGameConfirmExit(int score, ExitStatus status)
        {
            if (ConfirmGameExit != null)
            {
                ConfirmGameExit(score, status);
            }
        }

        private void OnGameIsOver(int score)
        {
            if (GameIsOver != null)
            {
                GameIsOver(score);
            }
        }

        private void OnShowCtrlButtons()
        {
            if (ShowCtrlButtons != null)
            {
                ShowCtrlButtons();
            }
        }
        
        #endregion
    }
}
