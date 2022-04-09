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

        public Vector2 Locate(IWifiSignal[] signals)
        {
            throw new NotImplementedException();
        }
    }
}
