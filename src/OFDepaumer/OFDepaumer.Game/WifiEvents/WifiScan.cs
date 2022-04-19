using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OFDepaumer.Game.WifiPositioning;

namespace OFDepaumer.Game.WifiEvents
{
    public static class WifiScan
    {

        /// <summary>
        /// Fired every time the application fires a wifi scan
        /// </summary>
        public static event EventHandler<WifiScanEventArgs> OnWifiScanned;

        public static IWifiScanner Scanner;

        private static bool interruptScan;
        private static bool running;
        private static bool currentlyScanning = false;

        /// <summary>
        /// Runs the scanner every `msAttemptRate` ms, if it was successfully performed the OnWifiScanned event will be fired
        /// </summary>
        /// <param name="msAttemptRate"></param>
        /// <returns></returns>
        public static async Task RunScanTimer(int msAttemptRate)
        {
            if (running) throw new Exception("A wifi scanner is already running, cannot run another timer.");

            running = true;
            interruptScan = false;

            while (!interruptScan)
            {
                currentlyScanning = true;
                if (Scanner.TryPerformWifiScan(out IWifiSignal[] signals))
                    OnWifiScanned?.Invoke(Scanner, new WifiScanEventArgs(signals));
                currentlyScanning = false;

                await Task.Delay(msAttemptRate);
            }
        }

        /// <summary>
        /// Stops the scanning timer, if a scan is already running will not stop immediately
        /// </summary>
        public static void InterruptScan()
        {
            interruptScan = true;
        }

        /// <summary>
        /// After executing `InterruptScan` method, this will block execution till the scan is finished
        /// </summary>
        /// <param name="msTimeout">If -1 never timesout, </param>
        public static void WaitForScanInterrupt(int msTimeout = -1, int msRate = 10)
        {
            int totalTime = 0;
            while (currentlyScanning)
            {
                Thread.Sleep(msRate);
                totalTime += msRate;
                if (msTimeout >= 0 && totalTime >= msTimeout)
                    throw new TimeoutException($"The scan didn't stop after waiting for {msTimeout}ms");
            }
        }

    }
}
