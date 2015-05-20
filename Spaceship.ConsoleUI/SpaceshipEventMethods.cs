using System;
using System.Collections.Generic;
using System.Configuration;
using SpaceImpact.GameEngine;

namespace SpaceImpact.ConsoleUI
{
    public class SpaceshipEventMethods
    {
        private readonly int _minPointX = Int32.Parse(ConfigurationManager.AppSettings["MinPointX"]);
        private readonly int _minPointY = Int32.Parse(ConfigurationManager.AppSettings["MinPointY"]);
        private readonly int _maxPointX = Int32.Parse(ConfigurationManager.AppSettings["MaxPointX"]);
        private readonly int _maxPointY = Int32.Parse(ConfigurationManager.AppSettings["MaxPointY"]);
        private readonly int _scoreX = Int32.Parse(ConfigurationManager.AppSettings["ScoreX"]);
        private readonly int _scoreY = Int32.Parse(ConfigurationManager.AppSettings["ScoreY"]);
        private readonly int _healthX = Int32.Parse(ConfigurationManager.AppSettings["HealthX"]);
        private readonly int _healthY = Int32.Parse(ConfigurationManager.AppSettings["HealthY"]);
        private readonly int _resultX = Int32.Parse(ConfigurationManager.AppSettings["ResultX"]);
        private readonly int _resultY = Int32.Parse(ConfigurationManager.AppSettings["ResultY"]);

        public GameControl OnGetAction()
        {
            GameControl action = GameControl.DafaultAction;
            var keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    action = GameControl.MoveUp;
                    break;
                case ConsoleKey.DownArrow:
                    action = GameControl.MoveDown;
                    break;
                case ConsoleKey.LeftArrow:
                    action = GameControl.MoveLeft;
                    break;
                case ConsoleKey.RightArrow:
                    action = GameControl.MoveRight;
                    break;
                case ConsoleKey.Spacebar:
                    action = GameControl.SpaceshipShoot;
                    break;
                case ConsoleKey.Escape:
                    action = GameControl.EndGame;
                    break;
                case ConsoleKey.Enter:
                    action = GameControl.StartGame;
                    break;
                case ConsoleKey.P:
                    action = GameControl.PauseGame;
                    break;
                case ConsoleKey.R:
                    action = GameControl.ResumeGame;
                    break;
            }
            return action;
        }

        public void OnEnemyHide(int pointX, int pointY)
        {
            ObjectsDrawing.Draw(pointX, pointY, "  ");
        }

        public void OnEnemyDraw(int pointX, int pointY)
        {
            ObjectsDrawing.Draw(pointX, pointY, "<#");
        }

        public void OnGameScoreUpdate(int score, int pointX, int pointY)
        {
            ObjectsDrawing.Draw(pointX, pointY, score.ToString());
        }

        public void OnConfirmGameExit(int score, Game.ExitStatus status)
        {
            string gameStatus = null;
            switch (status)
            {
                case Game.ExitStatus.StopGame:
                    gameStatus = "Game Is Stoped. ";
                    break;
                case Game.ExitStatus.WinGame:
                    gameStatus = "You Win. ";
                    break;
                case Game.ExitStatus.LoseGame:
                    gameStatus = "You Lose. ";
                    break;
            }
            Console.SetCursorPosition(_resultX + 1, _resultY + 1);
            Console.Write("{0}Your Score Is: {1}", gameStatus, score);
            Console.SetCursorPosition(_resultX + 1, _resultY + 2);
            Console.Write(@"Press Any Key");
        }

        public void OnRestartGame()
        {
            Console.SetCursorPosition(_resultX + 1, _resultY + 1);
            Console.Write("For Exit Game Press Escape, For Restart Game Press Enter");
        }

        public void OnGameExit()
        {
            Console.SetCursorPosition(_resultX + 1, _resultY + 1);
            Console.Write("Game Is Over.");
            Console.SetCursorPosition(_resultX + 1, _resultY + 2);
            Console.Write("Press Eny Key To Exit.");
        }

        public void OnClearInfoBlock()
        {
            Console.SetCursorPosition(_resultX + 1, _resultY + 1);
            Console.Write(new string(' ', _maxPointX));
            Console.SetCursorPosition(_resultX + 1, _resultY + 2);
            Console.Write(new string(' ', _maxPointX));
        }

        public void OnGameSpaceClear()
        {
            Console.Clear();
        }

        public void OnShowCtrlButtons()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\tFor Movement - Press Arrow Keys\n");
            Console.WriteLine("\tFor Fire - Press Spacebar Key\n");
            Console.WriteLine("\tFor Pause Game - Press Key P\n");
            Console.WriteLine("\tFor Unpause Game - Press Key R\n");
            Console.WriteLine("\tFor End Game - Press Escape Key\n");
            Console.WriteLine("\tFor Start Game - Press Enter Key\n");
            var keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                keyInfo = Console.ReadKey(true);
            }
            Console.Clear();
        }

        public void OnLaserHide(int pointX, int pointY)
        {
            ObjectsDrawing.Draw(pointX, pointY, " ");
        }

        public void OnLaserDraw(int pointX, int pointY)
        {
            ObjectsDrawing.Draw(pointX, pointY, "-");
        }

        public void OnBordersDraw()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("╔");
            Console.SetCursorPosition(1, 0);
            Console.Write(new string('═', _maxPointX));
            Console.SetCursorPosition(_maxPointX + 1, 0);
            Console.Write("╗");

            Console.SetCursorPosition(0, _maxPointY - 1);
            Console.Write("╚");
            Console.SetCursorPosition(1, _maxPointY - 1);
            Console.Write(new string('═', _maxPointX));
            Console.SetCursorPosition(_maxPointX + 1, _maxPointY - 1);
            Console.Write("╝");

            for (int i = 0; i < _maxPointY - 2; i++)
            {
                Console.CursorTop = i + 1;
                Console.CursorLeft = 0;
                Console.Write("║");
                Console.CursorLeft = _maxPointX + 1;
                Console.Write("║");
            }
        }

        public void OnGameSpaceDraw()
        {
            for (int i = _minPointX + 1; i < _maxPointX - 1; i++)
            {
                Console.CursorTop = _minPointY + 1;
                Console.CursorLeft = i + 1;
                if (i%8 == 0)
                {
                    Console.Write(' ');
                }
                Console.Write('*');
            }

            for (int i = _minPointX + 1; i < _maxPointX - 1; i++)
            {
                Console.CursorTop = _maxPointY - 2;
                Console.CursorLeft = i + 1;
                if (i%8 == 0)
                {
                    Console.Write(' ');
                }
                Console.Write('*');
            }

            Console.SetCursorPosition(_minPointX + 3, _minPointY + 2);
            for (int i = _minPointX; i < (_maxPointX - 1)/8; i++)
            {
                Console.Write("*****   ");
            }

            Console.SetCursorPosition(_minPointX + 3, _maxPointY - 3);
            for (int i = _minPointX; i < (_maxPointX - 1)/8; i++)
            {
                Console.Write("*****   ");
            }

            Console.SetCursorPosition(_minPointX + 4, _minPointY + 3);
            for (int i = _minPointX; i < (_maxPointX - 1)/8; i++)
            {
                Console.Write("***     ");
            }

            Console.SetCursorPosition(_minPointX + 4, _maxPointY - 4);
            for (int i = 0; i < (_maxPointX - 1)/8; i++)
            {
                Console.Write("***     ");
            }

            Console.SetCursorPosition(_minPointX + 5, _minPointY + 4);
            for (int i = _minPointX; i < (_maxPointX - 1)/8; i++)
            {
                Console.Write("*       ");
            }

            Console.SetCursorPosition(_minPointX + 5, _maxPointY - 5);
            for (int i = _minPointX; i < (_maxPointX - 1)/8; i++)
            {
                Console.Write("*       ");
            }
            Console.SetCursorPosition(_healthX, _healthY);
            Console.Write("Health");

            Console.SetCursorPosition(_scoreX, _scoreY);
            Console.Write("Score:");
        }

        public void OnHideSpaceship(List<SpaceshipFragment> hero)
        {
            foreach (var element in hero)
            {
                ObjectsDrawing.Draw(element.X, element.Y, " ");
            }
        }

        public void OnDrawHero(List<SpaceshipFragment> hero)
        {
            ObjectsDrawing.Draw(hero[0].X, hero[0].Y, "(");
            ObjectsDrawing.Draw(hero[1].X, hero[1].Y, "x");
            ObjectsDrawing.Draw(hero[2].X, hero[2].Y, "\\");
            ObjectsDrawing.Draw(hero[3].X, hero[3].Y, "=");
            ObjectsDrawing.Draw(hero[4].X, hero[4].Y, "@");
            ObjectsDrawing.Draw(hero[5].X, hero[5].Y, ">");
            ObjectsDrawing.Draw(hero[6].X, hero[6].Y, "(");
            ObjectsDrawing.Draw(hero[7].X, hero[7].Y, "x");
            ObjectsDrawing.Draw(hero[8].X, hero[8].Y, "/");
        }

        public void OnDrawHealth(int pointX, int pointY, int health)
        {
            char healthPoint = Convert.ToChar(9829);
            for (int i = 0; i < health + 1; i++)
            {
                ObjectsDrawing.Draw(pointX + i, pointY, " ");
            }
            for (int i = 0; i < health; i++)
            {
                ObjectsDrawing.Draw(pointX + i, pointY, healthPoint.ToString());
            }
        }
        public void OnDrawBoss(List<SpaceshipFragment> boss)
        {
            ObjectsDrawing.Draw(boss[0].X, boss[0].Y, "<");
            ObjectsDrawing.Draw(boss[1].X, boss[1].Y, "-");
            ObjectsDrawing.Draw(boss[2].X, boss[2].Y, "/");
            ObjectsDrawing.Draw(boss[3].X, boss[3].Y, "|");
            ObjectsDrawing.Draw(boss[4].X, boss[4].Y, "#");
            ObjectsDrawing.Draw(boss[5].X, boss[5].Y, "C");
            ObjectsDrawing.Draw(boss[6].X, boss[6].Y, "<");
            ObjectsDrawing.Draw(boss[7].X, boss[7].Y, "-");
            ObjectsDrawing.Draw(boss[8].X, boss[8].Y, "\\");
        }
    }
}