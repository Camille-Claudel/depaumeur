using Android.Net.Wifi;
using Depaumer.WifiEvents;
using Depaumer.WifiPositioning;
using System;
using System.Collections.Generic;

namespace Depaumer
{
    public class AndroidWifiScanner : IWifiScanner
    {
        public AndroidWifiScanner()
        {
            LastWifiScanSignals = new AndroidWifiSignal[0];
        }

        public IWifiSignal[] LastWifiScanSignals { get; private set; }

        [Obsolete]
        public bool TryPerformWifiScan(out IWifiSignal[] scannedSignals)
        {
            IWifiSignal[] signals = null; // Default value if scan failed
            bool result = false;
            try
            {
                result = MainActivity.wifiManager.StartScan();

                if (result)
                {
                    IList<ScanResult> scans = MainActivity.wifiManager.ScanResults;
                    signals = new IWifiSignal[scans.Count];
                    for (int i = 0; i < scans.Count; i++)
                    {
                        ScanResult scan = scans[i];
                        signals[i] = new AndroidWifiSignal(scan);
                    }
                }

            }
            finally
            {
                LastWifiScanSignals = signals;
                scannedSignals = signals;
            }

            return result;
        }
    }
}