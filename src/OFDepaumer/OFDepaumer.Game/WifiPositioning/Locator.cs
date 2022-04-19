using System;
using System.Collections.Generic;
using System.Text;
using osuTK;

namespace OFDepaumer.Game.WifiPositioning
{
    public class Locator : ILocator
    {
        public ICalibrationSettings Settings { get; }

        public Locator(ICalibrationSettings settings)
        {
            Settings = settings;
        }

        private double[] makeVector(IWifiSignal[] signals)
        {
            double[] vector = new double[Settings.WifiPointMacAddresses.Length];

            foreach (IWifiSignal signal in signals)
            {
                int index = Array.IndexOf(Settings.WifiPointMacAddresses, signal.MacAddress);
                if (index != -1)
                    vector[index] = signal.RSS;
            }

            return vector;
        }

        public Vector2 Locate(IWifiSignal[] signals)
        {
            double[] measureVector = makeVector(signals);

            double minDst = double.MaxValue;
            int argmin = 0;
            for (int i = 0; i < Settings.CalibrationPoints.Length; i++)
            {
                double dst = Math.ArrayExtensions.SquaredEuclidianDistance(measureVector, Settings.CalibrationPoints[i].Signals);
                if (dst < minDst)
                {
                    minDst = dst;
                    argmin = i;
                }
            }

            return Settings.CalibrationPoints[argmin].Position;
        }
    }
}
