using System;
using System.Collections.Generic;
using System.Text;
using OFDepaumer.Game.WifiEvents;
using OFDepaumer.Game.WifiPositioning;
using osuTK;

namespace OFDepaumer.Game
{
    public class WifiMain
    {

        private readonly ILocator locator;
        private readonly MainScreen mainScreen;

        public WifiMain(MainScreen screen)
        {
            mainScreen = screen;

            //  --  Load settings  --
            //ICalibrationSettings settings = CalibrationParser.LoadSettings("settingsFile.ext");
            //locator = new Locator(settings);
        }

        public void Start()
        {
            WifiScan.OnWifiScanned += OnScan;   // Registering scan
            _ = WifiScan.RunScanTimer(500);     // Running timer asynchronously
        }

        private void OnScan(object sender, WifiScanEventArgs e)
        {
            // Example scanning code
            Vector2 position = locator.Locate(e.RecievedSignals);

            // Showing coordinates on main screen text
            mainScreen.ChangeText($"x: {position.X}; y: {position.Y}");
        }

    }
}
