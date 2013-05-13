namespace DungeonCrawl
{
#if WINDOWS || XBOX
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
// ReSharper disable UnusedParameter.Local
        private static void Main(string[] args)
// ReSharper restore UnusedParameter.Local
        {
            using (var game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}