using System;
using System.Collections.Generic;
using System.Text;
using osuTK;

namespace OFDepaumer.Game.WifiPositioning
{
    public interface ICalibrationPoint
    {

        Vector2 Position { get; }
        double[] Signals { get; }

    }
}
