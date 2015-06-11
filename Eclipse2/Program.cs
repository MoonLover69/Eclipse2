using System;

namespace Eclipse2Game
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (EclipseGame game = new EclipseGame())
            {
                game.Run();
            }
        }
    }
#endif
}

