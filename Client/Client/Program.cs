namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
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