using osu.Framework;
using osu.Framework.Platform;

namespace OFDepaumer.Game.Tests
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableHost("visual-tests"))
            using (var game = new OFDepaumerTestBrowser())
                host.Run(game);
        }
    }
}
