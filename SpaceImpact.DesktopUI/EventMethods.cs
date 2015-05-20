using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using SpaceImpact.GameEngine;

namespace SpaceImpact.DesktopUI
{
    public class EventMethods
    {
        private static readonly string _projectPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        private static readonly string _framesPath = String.Concat(_projectPath, @"\Frames\");
        private readonly Image _enemy = Image.FromFile(String.Concat(_framesPath, "EnemyModel.png"));
        private readonly Image _hero = Image.FromFile(String.Concat(_framesPath, "HeroModel.png"));
        private readonly Image _boss = Image.FromFile(String.Concat(_framesPath, "BossModel.png"));
        private readonly int _coordinateMultiplier = Int32.Parse(ConfigurationManager.AppSettings["CoordinateMultiplier"]);

        public GameControl OnGetAction()
        {
            var action = SpaceImpact.UserAction;
            SpaceImpact.UserAction = GameControl.DafaultAction;
            return action;
        }

        public void OnEnemyHide(int pointX, int pointY)
        {
            SpaceImpact.GameSpace.FillRectangle(Brushes.Black, pointX * _coordinateMultiplier, pointY * _coordinateMultiplier, 32, 15);
        }

        public void OnEnemyDraw(int pointX, int pointY)
        {
            SpaceImpact.GameSpace.DrawImage(_enemy, new Point(pointX * _coordinateMultiplier, pointY * _coordinateMultiplier));
        }

        
        public void OnLaserHide(int pointX, int pointY)
        {
            SpaceImpact.GameSpace.FillRectangle(Brushes.Black, pointX * _coordinateMultiplier, pointY * _coordinateMultiplier, 8, 1);
        }

        public void OnLaserDraw(int pointX, int pointY)
        {
            SpaceImpact.GameSpace.FillRectangle(Brushes.Red, pointX * _coordinateMultiplier, pointY * _coordinateMultiplier, 8, 1);
        }

        public void OnHideSpaceship(List<SpaceshipFragment> hero)
        {
            SpaceImpact.GameSpace.FillRectangle(Brushes.Black, new Rectangle(hero[0].X * _coordinateMultiplier, hero[0].Y * _coordinateMultiplier, 32, 32));
        }

        public void OnDrawHero(List<SpaceshipFragment> hero)
        {
            SpaceImpact.GameSpace.DrawImage(_hero, new Point(hero[0].X*_coordinateMultiplier, hero[0].Y*_coordinateMultiplier));
        }

        public void OnDrawBoss(List<SpaceshipFragment> boss)
        {
            SpaceImpact.GameSpace.DrawImage(_boss, new Point(boss[0].X * _coordinateMultiplier, boss[0].Y * _coordinateMultiplier));
        } 
    }
}