using System.Collections.Generic;
using System.Timers;

namespace SpaceImpact.GameEngine
{
    #region Public Enum

    public enum GameControl
    {
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

    public enum Platform
    {
        Console,
        Winforms
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
        private GameControl _playerAction;
        private List<Enemy> _enemies;
        private GameControl _bossAction;
        private GameStatus _gameStatus;

        #endregion

        #region Game Score delegate & event

        public delegate void GameScore(int score, int pointX, int pointY);

        public delegate void GameExit(int score);

        public delegate void ExitKind(int score, ExitStatus status);

        public delegate void InfoBlock();

        public event GameScore GameScoreUpdate;

        public event ExitKind ConfirmGameExit;

        public event GameExit GameIsOver;

        public event InfoBlock ClearInfoBlock;

        public event InfoBlock ShowCtrlButtons;

        public event InfoBlock GameSpaceClear;

        public event InfoBlock GamePause;

        public event InfoBlock GameResume;

        public event InfoBlock InitBoss;

        public event InfoBlock TimersStart;

        public event InfoBlock TimersStop;

        public event InfoBlock WinForms;

        #endregion

        #region Private Properties

        private int ScorePointY { get; set; }

        private int ScorePointX { get; set; }

        private int HeroHealthPointX { get; set; }

        private int HeroHealthPointY { get; set; }

        private int BossHealthPointX { get; set; }

        private int BossHealthPointY { get; set; }

        private int Score { get; set; }

        public bool BossInited { get; set; }

        #endregion

        #region Private Enum

        private enum GameStatus
        {
            InProgress = 0,
            Paused = 1,
            Stopped = 2,
            GameEnd = 3
        }

        public enum ExitStatus
        {
            WinGame = 0,
            LoseGame = 1,
            StopGame = 2,
            DefaultStatus = 3
        }

        #endregion

        #region Constructor

        public Game(Spaceship hero, Spaceship boss, Laser laser, Enemy enemy, Control control, Space space,
            int scorePointX,
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
                    if (laser.X == enemy.X && laser.Y == enemy.Y ||
                        laser.X + 1 == enemy.X && laser.Y == enemy.Y)
                    {
                        _laser.OnLaserHide(laser.X, laser.Y);
                        _enemy.OnEnemyHide(enemy.X, enemy.Y);
                        enemies.Remove(enemy);
                        lasers.Remove(laser);
                        collisionsCount++;
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
                            OnTimersStop();
                            Exit(ExitStatus.LoseGame);
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
                            _gameStatus = GameStatus.GameEnd;
                             OnTimersStop();
                            Exit(ExitStatus.WinGame);
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
            var laserNearBorder = laser.X + 1 == bounds[1] || laser.X == bounds[0];
            return laserNearBorder;
        }

        #endregion

        #region Game Objects Motion


        public void OnGameObjectsTurn(object sender, ElapsedEventArgs e)
        {
            GameObjectsTurn();
        }

        public void GameObjectsTurn()
        {
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

        public void PlayerMove()
        {
            var bounds = _space.Bounds;
            _playerAction = _control.OnGetAction();
            switch (_playerAction)
            {
                case GameControl.MoveUp:
                    if (_hero.CanMove(0, -1, bounds) && PlayerCanMove)
                    {
                        _hero.Move(0, -1);
                        PlayerCanMove = false;
                    }
                    break;
                case GameControl.MoveDown:
                    if (_hero.CanMove(0, 1, bounds) && PlayerCanMove)
                    {
                        _hero.Move(0, 1);
                        PlayerCanMove = false;
                    }
                    break;
                case GameControl.MoveRight:
                    if (_hero.CanMove(1, 0, bounds) && PlayerCanMove)
                    {
                        _hero.Move(1, 0);
                        PlayerCanMove = false;
                    }
                    break;
                case GameControl.MoveLeft:
                    if (_hero.CanMove(-1, 0, bounds) && PlayerCanMove)
                    {
                        _hero.Move(-1, 0);
                        PlayerCanMove = false;
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
                    _gameStatus = GameStatus.GameEnd;
                    OnTimersStop();
                    Exit(ExitStatus.StopGame);
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
                        Exit(ExitStatus.LoseGame);
                        break;
                    }
                }
                if (EnemyNearBorder(enemy))
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
                        _enemy.OnEnemyHide(enemy.X, enemy.Y);
                        _enemies.Remove(enemy);
                        _gameStatus = GameStatus.Stopped;
                        Exit(ExitStatus.LoseGame);
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

        public void BossShoot()
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

        #region Game Status

        public void StartGame(Platform platform)
        {
            OnShowCtrlButtons();
            _space.OnBordersDraw();
            _space.OnGameSpaceDraw();
            GameObjectsInit();
            _gameStatus = GameStatus.InProgress;
            OnTimersStart();
            ChooseMode(platform);
        }

        private void ChooseMode(Platform platform)
        {
            switch (platform)
            {
                case Platform.Console:
                    GameProcess();
                    break;
            }
        }

        private void ResumeGame()
        {
            OnGameResume();
            _gameStatus = GameStatus.InProgress;
        }

        private void PauseGame()
        {
            OnGamePause();
            _gameStatus = GameStatus.Paused;
        }

        private void Exit(ExitStatus status)
        {
            OnTimersStop();
            OnGameConfirmExit(Score, status);
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
            OninitBoss();
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

        public void GameProcess()
        {
            while (_gameStatus != GameStatus.GameEnd && _hero.IsAlive)
            {
                if (_gameStatus == GameStatus.Paused)
                {
                    if (_control.OnGetAction() == GameControl.ResumeGame)
                    {
                        ResumeGame();
                    }
                }
                if (PlayerCanMove && _gameStatus != GameStatus.Paused)
                {
                    PlayerMove();
                }
            }
            if (_gameStatus == GameStatus.Stopped)
            {
                ExitStatus status = ExitStatus.DefaultStatus;
                if (_hero.Life > 0 && _boss.Life > 0)
                {
                    status = ExitStatus.StopGame;
                }
                if (_hero.Life == 0 && _boss.Life > 0)
                {
                    status = ExitStatus.LoseGame;
                }
                if (_hero.Life > 0 && _boss.Life == 0)
                {
                    status = ExitStatus.WinGame;
                }
                Exit(status);
            }
        }

        
        
        #endregion

        #region Handlers

        public void OnBossShoot(object sender, ElapsedEventArgs e)
        {
            BossShoot();
        }

        public void OnPlayerCanMove(object sender, ElapsedEventArgs e)
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

        private void OnShowCtrlButtons()
        {
            if (ShowCtrlButtons != null)
            {
                ShowCtrlButtons();
            }
        }

        public void OnGamePause()
        {
            if (GamePause != null)
            {
                GamePause();
            }
        }

        public void OnGameResume()
        {
            if (GameResume != null)
            {
                GameResume();
            }
        }

        public void OninitBoss()
        {
            if (InitBoss != null)
            {
               InitBoss();
            }
        }

        public void OnTimersStart()
        {
            if (TimersStart != null)
            {
                TimersStart();
            }
        }

        public void OnTimersStop()
        {
            if (TimersStop != null)
            {
                TimersStop();
            }
        }

        public void OnWinForms()
        {
            if (WinForms != null)
            {
                WinForms();
            }
        }

    #endregion

    }
}
