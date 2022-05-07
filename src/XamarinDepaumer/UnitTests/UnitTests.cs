using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using Depaumer.WifiPositioning;

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
    }
}