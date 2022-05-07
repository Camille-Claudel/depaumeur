using System;
using System.Collections.Generic;
using System.Text;

namespace Depaumer.WifiPositioning
{
    internal class WifiSignal : IWifiSignal
    {
        public WifiSignal(string macAddress, double rss, string ssid)
        {
            MacAddress = macAddress;
            RSSI = rss;
            SSID = ssid;
        }
        public string MacAddress { get; }
        public double RSSI { get; }
        public string SSID { get; }
    }

}