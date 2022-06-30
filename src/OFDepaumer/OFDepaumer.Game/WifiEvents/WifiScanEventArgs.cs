using System;
using System.Collections.Generic;
using System.Text;
using OFDepaumer.Game.WifiPositioning;

namespace OFDepaumer.Game.WifiEvents
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
