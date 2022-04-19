using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Depaumer.WifiEvents;
using Depaumer.WifiPositioning;

namespace Depaumer
{
    public class WifiMain
    {

        public readonly ILocator locator;

        public WifiMain()
        {
            //  --  Load settings  --
            ICalibrationSettings settings = CalibrationParser.LoadSettings("settingsFile.ext");
            locator = new Locator(settings);
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
        }

    }
}
