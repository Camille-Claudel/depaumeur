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
    }
}