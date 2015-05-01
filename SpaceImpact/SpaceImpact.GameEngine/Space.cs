using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceImpact.GameEngine.BaseGameElements;

namespace SpaceImpact.GameEngine
{
    public class Space
    {
        #region Fields

        private List<IGameElement> _gameElements = new List<IGameElement>();

        #endregion

        #region Constructors

        public Space(int width, int height)
        {
            #region Check

            if (width <= 0)
            {
                throw new ArgumentException("Width can not be <= 0");
            }
            if (height <= 0)
            {
                throw new ArgumentException("Height can not be <= 0");
            }

            #endregion

            this.Width = width;
            this.Height = height;
        }

        #endregion

        #region Public Properties

        public int Width { get; private set; }
        public int Height { get; private set; }

        public IEnumerable<IGameElement> GameElements
        {
            get { return this._gameElements; }
        }

        #endregion

        #region Public Methods

        public void AddGameElement(IGameElement gameElement)
        {
            if (this.IsCorrectPosition(gameElement, gameElement.X, gameElement.Y) == false)
            {
                throw new ArgumentException("A game element cannot be added to the Space due to incorrect coordinates");
            }
            this._gameElements.Add(gameElement);
            gameElement.Space = this;
        }

        public bool CanLocated(IGameElement gameElement, int newX, int newY)
        {
            if (this._gameElements.Any(r => r == gameElement) == false)
            {
                return false;
            }

            return IsCorrectPosition(gameElement, newX, newY);
        }

        #endregion

        #region Helpers

        private bool IsCorrectPosition(IGameElement gameElement, int newX, int newY)
        {
            if (gameElement is Spaceship)
            {
                return ((newX >= 0) && (newX < this.Width) && (newY >= 0) && (newY < this.Height));
            }
            else
            {
                return false;
            }
        }

        #endregion
    }

    /*
    * Review GY: функціонал цього класу дуже схожий на функціонал батьківського класу.
    * Рекомендую зменшити дублювання коду.
    * Яке призначення класу BattleSpace?
    */
    public class BattleSpace: Space
    {
        #region Fields

        private List<IGameObject> _gameObjects = new List<IGameObject>();

        #endregion

        #region Public Properties

        public IEnumerable<IGameObject> GameObjects
        {
            get { return this._gameObjects; }
        }

        #endregion

        #region Public Methods

        public BattleSpace(int width, int height) : base(width, height) { }

        public void AddGameObject(IGameObject gameObject)
        {
            if (this.IsCorrectPosition(gameObject, gameObject.X, gameObject.Y) == false)
            {
                throw new ArgumentException("A game object cannot be added to the Space due to incorrect coordinates");
            }
            this._gameObjects.Add(gameObject);
            gameObject.BattleSpace = this;
        }

        public bool CanLocated(IGameObject gameObject, int newX, int newY)
        {
            if (this._gameObjects.Any(r => r == gameObject) == false)
            {
                return false;
            }

            return IsCorrectPosition(gameObject, newX, newY);
        }

        #endregion

        #region Helpers

        private bool IsCorrectPosition(IGameObject gameObject, int newX, int newY)
        {
            if (gameObject is Spaceship || gameObject is Enemy)
            {
                return ((newX >= 2) && (newX < this.Width) && (newY >= 2) && (newY < this.Height));
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
