using System;
// review VD: непотрібні простори імен
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceImpact.GameEngine
{
    public enum GameStatus
    {
        ReadyToStart,
        InProgress,
        Completed
    }

    public class Game
    {
        #region Private Fields

        private GameStatus _status;
        private Spaceship _spaceship;
        //private Enemy _boss;
        private List<Enemy> _enemies = new List<Enemy>();
        private Space _space;
        private BattleSpace _battleSpace;
        private Random _random = new Random();
        // review VD: потрібно зробити константою
        private int _enemiesCount = 3;

        #endregion

        #region Constructors

        public Game()
        {
            this._status = GameStatus.ReadyToStart;
            this._space = new Space(80, 25);
            this._battleSpace = new BattleSpace(76, 23);
            this._spaceship = new Spaceship(3, 11, 3);
            //this._boss = new Enemy(73, 11, 3);
            for (int i = 0; i < this._enemiesCount; i++)
            {
                this._enemies.Add(new Enemy(_random.Next(50, 73), _random.Next(4, 20), 1));
                this._battleSpace.AddGameObject(this.Enemies[i]);
            }   
            this._battleSpace.AddGameObject(this._spaceship); 
        }

        #endregion

        #region Public Properties

        public GameStatus Status
        {
            get { return this._status; }
            // review VD: завеликий відступ фігурної дужки
            }

        public Space GameSpace
        {
            get { return this._space; }
        }

        public BattleSpace BattleGameSpace
        {
            get { return this._battleSpace; }
        }

        public Spaceship Spaceship
        {
            get { return this._spaceship; }
        }

        //public Enemy Enemy
        //{
        //    get { return this._boss; }
        //}

        public List<Enemy> Enemies
        {
            get { return this._enemies; }
        }

        #endregion

        #region Public Methods

        public void Start()
        {
            #region Validation

            if (this._status != GameStatus.ReadyToStart)
            {
                throw new InvalidOperationException("Only game with status 'ReadyToStart' can be started");
            }

            #endregion

            this._status = GameStatus.InProgress;
        }

        public void Stop()
        {
            #region Validation

            if (this._status != GameStatus.InProgress)
            {
                throw new InvalidOperationException("Only game with status 'InProgress' can be stopped");
            }

            #endregion

            this._status = GameStatus.Completed;
        }

        //public void SpaceshipShoot(Spaceship spaceship)
        //{
        //    var laser = new Laser(spaceship.X + 4, spaceship.Y, 1);
        //    this._battleSpace.AddGameObject(laser);
        //    spaceship.TakeAim(laser);
        //}

        #endregion

        // review VD: навіщо цей регіон?
        #region Helpers

        #endregion
    }
}
