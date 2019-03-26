using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingGame;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameTests
{
    [TestClass]
    public class GameTests
    {
        Game game;
        [TestMethod]
        public void ZeroScore()
        {
            game = new Game(new Frame());
            game.ScoreString = "0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0";
            game.LoadFrames();
            Assert.AreEqual(0, game.TotalScore);
        }
        [TestMethod]
        public void SinglePointScore()
        {
            game = new Game(new Frame());
            game.ScoreString = "1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1" ;
            game.LoadFrames();
            Assert.AreEqual(20, game.TotalScore);
        }
        [TestMethod]
        public void SpareScore()
        {
            game = new Game(new Frame());
            game.ScoreString = "5, 5, 10, 10, 5, 5, 10, 5, 3";
            game.LoadFrames();
            Assert.AreEqual(111, game.TotalScore);
        }
        [TestMethod]
        public void RandomTestScore()
        {
            game = new Game(new Frame());
            game.ScoreString = "1, 4, 4, 5, 6, 4, 5, 5, 10, 0, 1, 7, 3, 6, 4, 10, 2, 8, 6";
            game.LoadFrames();
            Assert.AreEqual(133, game.TotalScore);
        }
        [TestMethod]
        public void PerfectStrikeScore()
        {
            game = new Game(new Frame());
            game.ScoreString = "10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10";
            game.LoadFrames();
            Assert.AreEqual(300, game.TotalScore);
        }
        [TestMethod]
        public void PerfectSpareScore()
        {
            game = new Game(new Frame());
            game.ScoreString = "5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5";
            game.LoadFrames();
            Assert.AreEqual(150, game.TotalScore);
        }

        [TestMethod]
        public void Sparein10thFrame()
        {
            game = new Game(new Frame());
            game.ScoreString = "4, 6, 6, 2, 4, 2, 8, 1, 10, 10, 7, 2, 9, 0, 0, 5, 6, 4, 7";
            game.LoadFrames();
            Assert.AreEqual(125, game.TotalScore);
        }
        [TestMethod]
        public void SingleStrike()
        {
            game = new Game(new Frame());
            game.ScoreString = "4, 6, 6, 2, 4, 2, 8, 1, 10, 6, 2, 7, 2, 9, 0, 0, 5, 6, 3";
            game.LoadFrames();
            Assert.AreEqual(97, game.TotalScore);
        }
        [TestMethod]
        public void DoubleStrike()
        {
            game = new Game(new Frame());
            game.ScoreString = "4, 6, 6, 2, 4, 2, 8, 1, 10, 10, 7, 2, 9, 0, 0, 5, 6, 3";
            game.LoadFrames();
            Assert.AreEqual(117, game.TotalScore);
        }

    }
}
