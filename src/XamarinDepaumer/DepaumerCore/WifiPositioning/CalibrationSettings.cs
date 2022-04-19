using System;
using System.Collections.Generic;
using System.Text;

namespace Depaumer.WifiPositioning
{
    internal class CalibrationSettings : ICalibrationSettings
    {
        public CalibrationSettings(ICalibrationPoint[] calibrationPoints, string[] wifiPointMacAddresses)
        {
            CalibrationPoints = calibrationPoints;
            WifiPointMacAddresses = wifiPointMacAddresses;
        }
        public ICalibrationPoint[] CalibrationPoints { get; }
        public string[] WifiPointMacAddresses { get; }
    }
}