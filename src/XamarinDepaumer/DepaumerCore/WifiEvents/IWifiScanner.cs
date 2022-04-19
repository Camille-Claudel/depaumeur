using System;
using System.Collections.Generic;
using System.Text;
using Depaumer.WifiPositioning;

namespace Depaumer.WifiEvents;

public interface IWifiScanner
{

    public IWifiSignal[] LastWifiScanSignals { get; }

    public bool TryPerformWifiScan(out IWifiSignal[] scannedSignals);
}
