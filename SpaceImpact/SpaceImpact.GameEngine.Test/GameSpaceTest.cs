using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceImpact.GameEngine.BaseGameElements;

namespace SpaceImpact.GameEngine.Test
{
    [TestClass]
    public class GameSpaceTest
    {
        [TestMethod]
        public void TestConstructor_0()
        {
            BattleSpace battleSpace = new BattleSpace(10, 8);
            Assert.AreEqual(10, battleSpace.Width);
            Assert.AreEqual(8, battleSpace.Height);
            Assert.IsNotNull(battleSpace.GameObjects);
            Assert.AreEqual(0, battleSpace.GameObjects.Count());
        }

        [TestMethod]
        public void TestConstructor_1()
        {
            Space space = new Space(14, 10);
            Assert.AreEqual(14, space.Width);
            Assert.AreEqual(10, space.Height);
            Assert.IsNotNull(space.GameElements);
            Assert.AreEqual(0, space.GameElements.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWidth_Wrong_0()
        {
            BattleSpace battleSpace = new BattleSpace(0, 8);  
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWidth_Wrong__0()
        {
            Space space = new Space(0, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWidth_Wrong_1()
        {
            BattleSpace battleSpace = new BattleSpace(-2, 8);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestWidth_Wrong__1()
        {
           Space space = new Space(-4, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestHeight_Wrong_0()
        {
            BattleSpace battleSpace = new BattleSpace(5, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestHeight_Wrong__0()
        {
            Space space = new Space(8, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestHeight_Wrong_1()
        {
            BattleSpace battleSpace = new BattleSpace(5, -2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestHeight_Wrong__1()
        {
            Space space = new Space(8, -4);
        }

        [TestMethod]
        public void TestAddGameObject_BattleSpaceship()
        {
            BattleSpace battleSpace = new BattleSpace(10, 5);
            IGameObject spaceship = new Spaceship(3, 3, 3);
            battleSpace.AddGameObject(spaceship);

            IGameObject lastGameObject = battleSpace.GameObjects.Last();
            Assert.AreSame(lastGameObject, spaceship);
            Assert.AreSame(battleSpace, spaceship.BattleSpace);
        }

        [TestMethod]
        public void TestAddGameElement_HealthScale()
        {
            Space space = new Space(10, 5);
            IGameElement healthscale = new HealthScale(1, 1, 3);
            space.AddGameElement(healthscale);

            IGameElement lastGameElement = space.GameElements.Last();
            Assert.AreSame(lastGameElement, healthscale);
            Assert.AreSame(space, healthscale.Space);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddGameObject_BattleSpaceship_Wrong_Position()
        {
            BattleSpace battleSpace = new BattleSpace(76, 23);
            IGameObject spaceship = new Spaceship(-1, 11, 3);
            battleSpace.AddGameObject(spaceship);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddGameElement_HealthScale_Wrong_Position()
        {
            Space space = new Space(80, 25);
            IGameElement healthscale = new HealthScale(-1, 1, 3);
            space.AddGameElement(healthscale);
        }

        [TestMethod]
        public void TestCanLocated_BattleSpaceship()
        {
            BattleSpace battleSpace = new BattleSpace(10, 5);
            IGameObject spaceship = new Spaceship(3, 3, 3);
            battleSpace.AddGameObject(spaceship);

            Assert.IsTrue(battleSpace.CanLocated(spaceship, 5, 3));
            Assert.IsTrue(battleSpace.CanLocated(spaceship, 9, 4));
            Assert.IsTrue(battleSpace.CanLocated(spaceship, 2, 2));

            Assert.IsFalse(battleSpace.CanLocated(spaceship, -1, 0));
            Assert.IsFalse(battleSpace.CanLocated(spaceship, 0, -1));
            Assert.IsFalse(battleSpace.CanLocated(spaceship, 10, 0));
            Assert.IsFalse(battleSpace.CanLocated(spaceship, 0, 5));
        }

        [TestMethod]
        public void TestCanLocated_Space()
        {
            Space space = new Space(25, 10);
            IGameElement healthscale = new HealthScale(1, 1, 3);
            space.AddGameElement(healthscale);

            Assert.IsTrue(space.CanLocated(healthscale, 5, 3));
            Assert.IsTrue(space.CanLocated(healthscale, 9, 4));
            Assert.IsTrue(space.CanLocated(healthscale, 2, 2));

            Assert.IsFalse(space.CanLocated(healthscale, -1, 0));
            Assert.IsFalse(space.CanLocated(healthscale, 0, -1));
            Assert.IsFalse(space.CanLocated(healthscale, 10, 0));
            Assert.IsFalse(space.CanLocated(healthscale, 0, 5));
        }

        [TestMethod]
        public void TestCanLocated_BattleSpaceship_Does_Not_Belong_To()
        {
            BattleSpace battleSpace = new BattleSpace(10, 5);
            IGameObject spaceship = new Spaceship(3, 3, 3);
            Assert.IsFalse(battleSpace.CanLocated(spaceship, 0, 5));
        }

        [TestMethod]
        public void TestLife()
        {
            BattleSpace battleSpace = new BattleSpace(10, 5);
            IGameObject spaceship = new Spaceship(3, 3, 3);
            battleSpace.AddGameObject(spaceship);
            Assert.IsTrue(spaceship.Life == 3);
            Assert.IsFalse(spaceship.Life == 0);
        }
    }
}

