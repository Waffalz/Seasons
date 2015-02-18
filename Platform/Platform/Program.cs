using System;

namespace Platform
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Menu game = new Menu())
            {
                game.Run();
            }
        }
    }
#endif
}

