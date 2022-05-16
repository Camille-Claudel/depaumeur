using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using Depaumer.WifiPositioning;
using Depaumer.Utils;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestCalibrationPoint()
        {
            Vector2 pos = new Vector2(0, 0);
            double[] signals = { 0, 0 };
            CalibrationPoint cp = new CalibrationPoint(pos, signals);
            Assert.AreEqual(pos, cp.Position);
            Assert.AreEqual(signals, cp.Signals);
        }

        [TestMethod]
        public void TestCalibrationSettings()
        {
            string[] macAddresses = { "a", "b", "c" };
            CalibrationPoint[] calPts =
            {
                new CalibrationPoint(new Vector2(0, 0), new double[]{-50, -70, -70}),
                new CalibrationPoint(new Vector2(0, 1), new double[]{-70, -50, -90}),
                new CalibrationPoint(new Vector2(1, 1), new double[]{-90, -70, -70}),
                new CalibrationPoint(new Vector2(1, 0), new double[]{-70, -90, -50})
            };

            CalibrationSettings cs = new CalibrationSettings(calPts, macAddresses);
            Assert.AreEqual(cs.CalibrationPoints, calPts);
            Assert.AreEqual(cs.WifiPointMacAddresses, macAddresses);
        }

        [TestMethod]
        public void TestWifiSignal()
        {
            WifiSignal ws = new WifiSignal("a:b:c", -100, "banana_With special @characte&s @!=*#&$+-");
            Assert.AreEqual(ws.MacAddress, "a:b:c");
            Assert.AreEqual(ws.RSSI, -100);
            Assert.AreEqual(ws.SSID, "banana_With special @characte&s @!=*#&$+-");
        }

        [TestMethod]
        public void TestSquaredDst()
        {
            double[] v1 = { 0, 1, 3, 5 };
            double[] v2 = { 0, 1, 3, 5 };
            Assert.AreEqual(ArrayExtensions.SquaredEuclidianDistance(v1, v2), 0);
            Assert.AreEqual(ArrayExtensions.SquaredEuclidianDistance(v2, v1), 0);

            double[] v3 = { 1, 1, 1, 1 };
            double[] v4 = { 1, 1, 3, 3 };
            Assert.AreEqual(ArrayExtensions.SquaredEuclidianDistance(v3, v4), 8);
            Assert.AreEqual(ArrayExtensions.SquaredEuclidianDistance(v4, v3), 8);

            double[] v5 = new double[1000000];
            double[] v6 = new double[1000000];
            for (int i = 0; i < v5.Length; i++)
            {
                v5[i] = i / 23.5;
                v6[i] = v5[i] + 1;
            }
            Assert.AreEqual(ArrayExtensions.SquaredEuclidianDistance(v5, v6), v5.Length);
        }

        [TestMethod]
        public void TestLocate()
        {
            string[] macAddresses = { "a", "b", "c" };
            CalibrationPoint[] calPts =
            {
                new CalibrationPoint(new Vector2(0, 0), new double[]{-50, -70, -70}),
                new CalibrationPoint(new Vector2(0, 1), new double[]{-70, -50, -90}),
                new CalibrationPoint(new Vector2(1, 1), new double[]{-90, -70, -70}),
                new CalibrationPoint(new Vector2(1, 0), new double[]{-70, -90, -50})
            };
            CalibrationSettings cs = new CalibrationSettings(calPts, macAddresses);
            
            Locator locator = new Locator(cs);
            Assert.AreEqual(locator.Settings, cs);

            WifiSignal[] ws =
            {
                new WifiSignal("c", -85, "banana"),
                new WifiSignal("a", -75, "banana"),
                new WifiSignal("d", -100, "glory to the soviet union"),
                new WifiSignal("b", -55, "another banana")
            };

            Assert.AreEqual(locator.Locate(ws), new Vector2(0, 1));
        }
    }
}