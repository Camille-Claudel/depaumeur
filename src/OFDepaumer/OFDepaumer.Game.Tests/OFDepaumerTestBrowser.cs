using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace OFDepaumer.Game.Tests
{
    public class OFDepaumerTestBrowser : OFDepaumerGameBase
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddRange(new Drawable[]
            {
                new TestBrowser("OFDepaumer"),
                new CursorContainer()
            });
        }

        public override void SetHost(GameHost host)
        {
            base.SetHost(host);
            host.Window.CursorState |= CursorState.Hidden;
        }
    }
}
