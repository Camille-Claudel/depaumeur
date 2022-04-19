using System;
using System.Collections.Generic;
using System.Text;
using Depaumer.WifiPositioning;


namespace Depaumer.WifiEvents
{

    public class WifiScanEventArgs : EventArgs
    {
        public IWifiSignal[] RecievedSignals;

        public WifiScanEventArgs(IWifiSignal[] recievedSignals)
        {
            RecievedSignals = recievedSignals;
        }
    }
}