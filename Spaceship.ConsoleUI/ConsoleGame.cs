namespace SpaceImpact.ConsoleUI
{
    public class ConsoleGame
    {
        public static void Main()
        {
            InitGameObjects init = new InitGameObjects();
            init.ShowProgress();
            init.InitAndStart();
        }
    }
}