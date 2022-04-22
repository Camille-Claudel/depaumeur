using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Depaumer.WifiPositioning
{
    public interface ILocator
    {
        public ICalibrationSettings Settings { get; }

        public event EventHandler<PositionUpdateArgs> PositionUpdated;
        public Vector2? CurrentPosition { get; }
        public DateTime LastUpdateTime { get; }

        /// <summary>
        /// Returns the coordinates of the person according to the calibrations and the percieved wifi signals
        /// </summary>
        /// <param name="signals"></param>
        /// <returns></returns>
        public Vector2 Locate(IWifiSignal[] signals);

    }
}