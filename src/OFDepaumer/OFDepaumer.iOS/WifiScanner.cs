using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using osu.Framework.iOS;
using Foundation;
using UIKit;
using System.Threading.Tasks;
using OFDepaumer.Game.WifiEvents;
using Xamarin;
using OFDepaumer.Game.WifiPositioning;

namespace OFDepaumer.iOS
{
    public class WifiScanner : IWifiScanner
    {
        public IWifiSignal[] LastWifiScanSignals { get; }

        public WifiScanner()
        {
            LastWifiScanSignals = new IWifiSignal[0];
        }

        public bool TryPerformWifiScan(out IWifiSignal[] scannedSignals)
        {
            scannedSignals = null;
        
            return false;
        }

    }
}
