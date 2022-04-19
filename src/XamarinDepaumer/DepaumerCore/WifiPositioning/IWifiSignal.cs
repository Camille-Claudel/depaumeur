using System;
using System.Collections.Generic;
using System.Text;

namespace Depaumer.WifiPositioning;

public interface IWifiSignal
{
    /// <summary>
    /// The mac address defining the wifi point
    /// </summary>
    public string MacAddress { get; }
    /// <summary>
    /// The signal strength
    /// </summary>
    public double RSS { get; }
    /// <summary>
    /// The wifi ID
    /// </summary>
    public string SSID { get; }

}
