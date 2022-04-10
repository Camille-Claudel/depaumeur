using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Screens;
using osuTK.Graphics;
using OFDepaumer.Game.WifiPositioning;

namespace OFDepaumer.Game
{
    public class MainScreen : Screen
    {
        [BackgroundDependencyLoader]
        private void load()
        {

            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Violet,
                    RelativeSizeAxes = Axes.Both,
                },
                new SpriteText
                {
                    Y = 20,
                    Text = "Main Screen",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 40)
                },
                new SpinningBox
                {
                    Anchor = Anchor.Centre,
                }
            };
        }

        public void ChangeText(string newText)
        {
            (InternalChildren[1] as SpriteText).Text = newText;
        }

    }
}
