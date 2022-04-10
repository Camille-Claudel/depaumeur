using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using OFDepaumer.Game.WifiEvents;
using OFDepaumer.Game.WifiPositioning;

namespace OFDepaumer.Game
{
    public class OFDepaumerGame : OFDepaumerGameBase
    {

        private ScreenStack screenStack;
        private MainScreen mainScreen;
        private WifiMain wifi;

        [BackgroundDependencyLoader]
        private void load()
        {
            // Add your top-level game components here.
            // A screen stack and sample screen has been provided for convenience, but you can replace it if you don't want to use screens.
            Child = screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            mainScreen = new MainScreen();
            screenStack.Push(mainScreen);

            wifi = new WifiMain(mainScreen);

        }

        protected override bool OnClick(ClickEvent e)
        {
            bool b = base.OnClick(e);
            //wifi.Start(); Commented because this will literally crash the app
            mainScreen.ChangeText("Ok i lied, it doesn't work yet :wut:, you'll have to debug using Tests for now");
            return b;
        }

    }
}
