using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Depaumer.WifiPositioning
{
    public interface ICalibrationPoint
    {

        Vector2 Position { get; }
        double[] Signals { get; }

    }
}