using System;

namespace SpaceImpact.ConsoleUI
{
    public class ConsoleGame
    {
        public static void Main()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            InitGameObjects init = new InitGameObjects();
            init.ShowProgress();
            init.InitAndStart();
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Console.WriteLine("MyHandler caught : " + e.Message);
            Console.WriteLine("Runtime terminating: {0}", args.IsTerminating);
        }
    }
}