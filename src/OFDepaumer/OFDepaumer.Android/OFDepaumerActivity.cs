using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Views;
using osu.Framework;
using osu.Framework.Android;
using Debug = System.Diagnostics.Debug;
using Uri = Android.Net.Uri;
using OFDepaumer.Game;

namespace OFDepaumer.Android
{
    [Activity(ConfigurationChanges = DEFAULT_CONFIG_CHANGES, Exported = true, LaunchMode = DEFAULT_LAUNCH_MODE, MainLauncher = true)]
    public class OFDepaumerActivity : AndroidGameActivity
    {

        private osu.Framework.Game game;

        protected override osu.Framework.Game CreateGame() => game = new OFDepaumerGameBase();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

    }
}
