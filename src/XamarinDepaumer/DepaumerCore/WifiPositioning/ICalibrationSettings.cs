using System;
using System.Collections.Generic;
using System.Text;

namespace Depaumer.WifiPositioning
{
    public interface ICalibrationSettings
    {

        public ICalibrationPoint[] CalibrationPoints { get; }
        string[] WifiPointMacAddresses { get; }

    }
}