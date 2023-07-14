namespace Client
{
    internal class Program
    {
        public static int SCREEN_WIDTH = 120;
        public static int SCREEN_HEIGHT = 40;
        
        static void Main(string[] args)
        {
            Console.SetWindowSize(SCREEN_WIDTH, SCREEN_HEIGHT);

            TextRPG game = new TextRPG();
            game.Start();

            while (true)
            {
                game.Update();
                Thread.Sleep(250);
            }
        }
    }
}
