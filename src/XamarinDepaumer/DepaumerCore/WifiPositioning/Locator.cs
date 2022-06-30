using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Depaumer.Utils;
using System.Linq;

namespace Depaumer.WifiPositioning
{
    public class Locator : ILocator
    {

        public ICalibrationSettings Settings { get; }

        public Vector2? CurrentPosition { get; private set; }

        public DateTime LastUpdateTime { get; private set; }

        public event EventHandler<PositionUpdateArgs> PositionUpdated;

        public Locator(ICalibrationSettings settings)
        {
            Settings = settings;
            LastUpdateTime = DateTime.Now;
        }

        private double[] makeVector(IWifiSignal[] signals)
        {
            // I don't know why this line wouldn't work on android or iphone systems
            // double[] vector = Enumerable.Repeat(-100d, Settings.WifiPointMacAddresses.Length).ToArray();

            double[] vector = new double[Settings.WifiPointMacAddresses.Length];
            for (int i = 0; i < vector.Length; i++) vector[i] = -100d;

            foreach (IWifiSignal signal in signals)
            {
                if (!MacAddressParser.IsMacAddressValid(signal.MacAddress))
                    continue;
                int index = Array.IndexOf(
                    Settings.WifiPointMacAddresses, 
                    MacAddressParser.GetStrippedMacAddress(signal.MacAddress));
                if (index != -1)
                    vector[index] = signal.RSSI;
            }

            return vector;
        }

        public Vector2 Locate(IWifiSignal[] signals)
        {

            // Calculating new position
            double[] measureVector = makeVector(signals);

            double minDst = double.MaxValue;
            int argmin = 0;
            for (int i = 0; i < Settings.CalibrationPoints.Length; i++)
            {
                double dst = Utils.ArrayExtensions.SquaredEuclidianDistance(measureVector, Settings.CalibrationPoints[i].Signals);
                if (dst < minDst)
                {
                    minDst = dst;
                    argmin = i;
                }
            }

            // Firing Event and return of new position
            Vector2 pos = Settings.CalibrationPoints[argmin].Position; // Buffering position

            PositionUpdateArgs args = new PositionUpdateArgs(
                CurrentPosition,    // Previous position
                pos,                // The new calculated distance
                LastUpdateTime);

            CurrentPosition = pos;                              // Updating instance variables
            LastUpdateTime = DateTime.Now;

            PositionUpdated?.Invoke(this, args);

            return pos;
        }
    }
}