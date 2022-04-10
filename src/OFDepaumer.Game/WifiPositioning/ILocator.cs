using System;
using System.Collections.Generic;
using System.Text;
using osuTK;
using OFDepaumer.Game.Math;

namespace OFDepaumer.Game.WifiPositioning
{
    public interface ILocator
    {
        public ICalibrationSettings Settings { get; }

        /// <summary>
        /// Returns the coordinates of the person according to the calibrations and the percieved wifi signals
        /// </summary>
        /// <param name="signals"></param>
        /// <returns></returns>
        public Vector2 Locate(IWifiSignal[] signals);

    }
}
