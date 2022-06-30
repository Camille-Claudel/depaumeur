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

        public WifiMain(ICalibrationSettings settings)
        {
            //  --  Load settings  --
            locator = new Locator(settings);
        }

        /// <summary>
        /// Starts wifi scanning
        /// </summary>
        /// <param name="msScanAttempRate">Defines the time intervals in ms the application will attempt to scan the wifi</param>
        public void Start(int msScanAttempRate)
        {
            WifiScan.OnWifiScanned += OnScan;   // Registering scan
            _ = WifiScan.RunScanTimer(msScanAttempRate);     // Running timer asynchronously
        }

        private void OnScan(object sender, WifiScanEventArgs e)
        {
            // Example scanning code
            Vector2 position = locator.Locate(e.RecievedSignals);
        }

    }
}
