using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Depaumer.WifiPositioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Depaumer.WifiEvents;

namespace Depaumer
{
    public class Program
    {
        public readonly WifiMain wifi;

        private MainActivity activity;
       
        public Program(MainActivity activity)
        {
            WifiScan.Scanner = new AndroidWifiScanner();

            //ICalibrationSettings settings = CalibrationParser.LoadSettings("somefile.ext");
            //wifi = new WifiMain(settings);
            this.activity = activity;
        }

        public void Run()
        {
            //wifi.Start();
            _ = WifiScan.RunScanTimer(5000);
            WifiScan.OnWifiScanned += OnScannedWifi;
        }

        public void OnScannedWifi(object sender, WifiScanEventArgs args)
        {
            List<string> scannedData = new List<string>(args.RecievedSignals.Length);
            foreach(IWifiSignal signal in args.RecievedSignals)
            {
                scannedData.Add($"{signal.SSID} : {signal.RSSI}");
            }
            
            var adapter = new ArrayAdapter<string>(activity, Android.Resource.Layout.SimpleListItem1, scannedData.ToArray());

            adapter.AddAll(Android.Resource.Layout.ActivityListItem);
            activity.ui.scannedWifis.Adapter = adapter;

        }

    }
}