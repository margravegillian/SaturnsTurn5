using System;

namespace SaturnsTurn5
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SaturnsTurnGame())
                game.Run();
        }
    }
}
