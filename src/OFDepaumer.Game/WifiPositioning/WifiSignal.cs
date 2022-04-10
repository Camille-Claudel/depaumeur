using System;
using System.Collections.Generic;
using System.Text;

namespace OFDepaumer.Game.WifiPositioning
{
    internal class WifiSignal : IWifiSignal
    {
        public WifiSignal(string macAddress, double rss, string ssid)
        {
            MacAddress = macAddress;
            RSS = rss;
            SSID = ssid;
        }
        public string MacAddress { get; }
        public double RSS { get; }
        public string SSID { get; }
    }
}
