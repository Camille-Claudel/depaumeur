using osu.Framework.Platform;
using osu.Framework;
using OFDepaumer.Game;

namespace OFDepaumer.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost(@"OFDepaumer"))
            using (osu.Framework.Game game = new OFDepaumerGame())
                host.Run(game);
        }
    }
}
