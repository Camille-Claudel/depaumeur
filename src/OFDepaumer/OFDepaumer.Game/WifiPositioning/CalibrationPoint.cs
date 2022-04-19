using System;
using System.Collections.Generic;
using System.Text;
using osuTK;

namespace OFDepaumer.Game.WifiPositioning
{
    public class CalibrationPoint : ICalibrationPoint
    {
        public CalibrationPoint(Vector2 position, double[] signals)
        {
            Position = position;
            Signals = signals;
        }
        public Vector2 Position { get; }

        public double[] Signals { get; }
    }
}
