using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OFDepaumer.Game.WifiPositioning
{
    public static class CalibrationParser
    {

        /*public static ICalibrationSettings LoadSettings(string fileName)
        {
            string jsonText = System.IO.File.ReadAllText(fileName);
            dynamic jsonData = JObject.Parse(jsonText); // listen technically i should create a class for this, but flemme

            string[] macAddresses = jsonData.macAddresses.ToObject<string[]>();
            CalibrationPoint[] calibrationPoints = new CalibrationPoint[jsonData.points.Count];

            for (int i = 0; i < jsonData.points.Count; i++)
            {
                float x = jsonData.points[i].coords[0];
                float y = jsonData.points[i].coords[1];
                double[] RSSs = jsonData.points[i].data.ToObject<double[]>();
                calibrationPoints[i] = new CalibrationPoint(new osuTK.Vector2(x, y), RSSs);
            }

            return new CalibrationSettings(calibrationPoints, macAddresses);
        }*/

    }
}
