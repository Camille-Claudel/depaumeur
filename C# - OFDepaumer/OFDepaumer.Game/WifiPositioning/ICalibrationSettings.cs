using System;
using System.Collections.Generic;
using System.Text;
using osu.Framework;
using osuTK;

namespace OFDepaumer.Game.WifiPositioning
{
    public interface ICalibrationSettings
    {

        public ICalibrationPoint[] CalibrationPoints { get; }
        List<string> WifiPointMacAddresses { get; }
        
    }
}
