using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpaceImpact.GameEngine.Test
{
    [TestClass]
    public class GameTest
    {
        #region Game Lifecycle

        [TestMethod]
        public void TestUsualLifecycle()
        {
            Game game = new Game();
            Assert.AreEqual(GameStatus.ReadyToStart, game.Status);
            game.Start();
            Assert.AreEqual(GameStatus.InProgress, game.Status);
            game.Stop();
            Assert.AreEqual(GameStatus.Completed, game.Status);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestStart_WrongStatus_1()
        {
            Game game = new Game();
            game.Start();
            game.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestStart_WrongStatus_2()
        {
            Game game = new Game();
            game.Start();
            game.Stop();
            game.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestStop_WrongStatus_1()
        {
            Game game = new Game();
            game.Stop();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestStop_WrongStatus_2()
        {
            Game game = new Game();
            game.Start();
            game.Stop();
            game.Stop();
        }


        #endregion

        #region GameSpace

        [TestMethod]
        public void TestGameSpaceCreation()
        {
            Game game = new Game();
            Assert.IsNotNull(game.GameSpace);
        }

        [TestMethod]
        public void TestGameSpaceSize()
        {
            Game game = new Game();
            Assert.AreEqual(80, game.GameSpace.Width);
            Assert.AreEqual(25, game.GameSpace.Height);
        }

        [TestMethod]
        public void TestBattleGameSpaceSize()
        {
            Game game = new Game();
            Assert.AreEqual(76, game.BattleGameSpace.Width);
            Assert.AreEqual(23, game.BattleGameSpace.Height);
        }

        #endregion

        #region Player

        [TestMethod]
        public void TestPlayerCreation()
        {
            Game game = new Game();
            Assert.IsNotNull(game.Spaceship);
            Assert.AreSame(game.BattleGameSpace, game.Spaceship.BattleSpace);
            Assert.IsTrue(game.BattleGameSpace.GameObjects.Any(r => r == game.Spaceship));
        }

        #endregion
    }
}
