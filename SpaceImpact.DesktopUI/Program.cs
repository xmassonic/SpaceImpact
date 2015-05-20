using System;
using System.Windows.Forms;

namespace SpaceImpact.DesktopUI
{
    static class Program
    {
        public static bool Play { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MenuForm());
            if (Play)
            {
                Application.Run(new SpaceImpact());
            }
        }
    }
}
