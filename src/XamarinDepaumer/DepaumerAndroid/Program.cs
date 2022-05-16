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
using Android.Content.Res;
using System.IO;

namespace Depaumer
{
    public class Program
    {
        public readonly WifiMain wifi;

        private MainActivity activity;
       
        public Program(MainActivity activity)
        {
            this.activity = activity;

            // Registering the android scanner
            WifiScan.Scanner = new AndroidWifiScanner();

            // Loading the json settings from Android Assets (App package)
            string settingsJSON;
            AssetManager assets = activity.Assets;
            using (StreamReader sr = new StreamReader(assets.Open("settings.json")))
                settingsJSON = sr.ReadToEnd();

            // Parsing the settings and loading them into a new Wifi instance (instance that will scan wifi and search for current location)
            ICalibrationSettings settings = CalibrationParser.LoadSettingsFromJSON(settingsJSON);
            wifi = new WifiMain(settings);
        }

        public void Run()
        {
            wifi.locator.PositionUpdated += OnLocationUpdated;
            wifi.Start(5000);
        }

        private void OnLocationUpdated(object sender, PositionUpdateArgs args)
        {
            string text = $"Location : {args.Current}, UpdateTime : {wifi.locator.LastUpdateTime}\nPreviously - Location : {args.Previous}, UpdateTime : {args.LastUpdateTime}";

            text += "\n\nScanned following Wifi signals :";

            foreach (IWifiSignal signal in WifiScan.Scanner.LastWifiScanSignals)
                text += $"\nScanned signal - {signal.SSID} : {signal.RSSI}";

            activity.ui.wifiTextView.Text = text; // When the location is updated, we load a new string representing the new location
        }

    }
}