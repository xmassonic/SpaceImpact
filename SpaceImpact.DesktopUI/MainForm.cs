using System;
using System.Drawing;
using System.Windows.Forms;
using SpaceImpact.GameEngine;

namespace SpaceImpact.DesktopUI
{
    public partial class SpaceImpact : Form
    {
        public static Graphics GameSpace { get; set; }
        public enum Action
        {
            Up,
            Down,
            Left,
            Right,
            Shoot,
            Default
        }

        #region Properties

        public static GameControl UserAction { get; set; }

        private InitObjects Init { get; set; }
        public static Game.ExitStatus Status { get; set; }
        public static string Result { get; set; }

        #endregion

        public SpaceImpact()
        {
            InitializeComponent();
            GameSpace = plGameSpace.CreateGraphics();
        }

        private void UserKeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Up:
                    UserAction = GameControl.MoveUp;
                    break;
                case Keys.Down:
                    UserAction = GameControl.MoveDown;
                    break;
                case Keys.Right:
                    UserAction = GameControl.MoveRight;
                    break;
                case Keys.Left:
                    UserAction = GameControl.MoveLeft;
                    break;
                case Keys.Space:
                    UserAction = GameControl.SpaceshipShoot;
                    break;
                case Keys.P:
                    UserAction = GameControl.PauseGame;
                    break;
                case Keys.R:
                    UserAction = GameControl.ResumeGame;
                    break;
                case Keys.Escape:
                    UserAction = GameControl.EndGame;
                    break;
            }
        }

        private void GameLoad(object sender, EventArgs e)
        {
            Paint += Draw;
            Init = new InitObjects();
            Init.InitAndStart(lblBossLifeValue, lblHeroLifeValue, lblScoreValue);
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Init.Player.OnDrawSpaceship(Init.Player.Model);
        }

        public static void Exit()
        {
            DialogResult result = MessageBox.Show("Do You Want To Restart Game?", Result, MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
            Application.Exit();
        }
    }
}
