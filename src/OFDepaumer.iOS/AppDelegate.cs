// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using Foundation;
using osu.Framework.iOS;
using OFDepaumer.Game;

namespace OFDepaumer.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : GameAppDelegate
    {
        protected override osu.Framework.Game CreateGame() => new OFDepaumerGame();
    }
}
