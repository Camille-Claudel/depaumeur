using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Depaumer.WifiPositioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Depaumer
{
    public class AndroidWifiSignal : IWifiSignal
    {
        public string MacAddress { get; }

        public double RSSI { get; }

        public string SSID { get; }

        public AndroidWifiSignal(string macAddress, double rss, string ssid)
        {
            MacAddress = macAddress;
            RSSI = rss;
            SSID = ssid;
        }

        public AndroidWifiSignal(ScanResult scan)
        {
            MacAddress = scan.Bssid;
            SSID = scan.Ssid;
            RSSI = scan.Level;
        }

    }
}