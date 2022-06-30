using System;
using System.Collections.Generic;
using System.Text;
using OFDepaumer.Game.WifiPositioning;

namespace OFDepaumer.Game.WifiEvents
{
    public interface IWifiScanner
    {

        public IWifiSignal[] LastWifiScanSignals { get; }

        public bool TryPerformWifiScan(out IWifiSignal[] scannedSignals);
    }
}
