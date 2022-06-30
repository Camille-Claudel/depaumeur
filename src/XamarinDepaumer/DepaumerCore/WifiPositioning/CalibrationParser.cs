using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Depaumer.WifiPositioning
{
    public static class CalibrationParser
    {

        public const string binaryKey = "CalibrationSettings-1.0\n";
        public const string binaryMacAddressLabel = "MacAddresses\n";
        public const string binaryCalibrationLabel = "CalibrationPoints\n";


        public static ICalibrationSettings LoadSettingsFromJSON(string jsonSettingsString)
        {
            dynamic jsonData = JObject.Parse(jsonSettingsString); // listen technically i should create a class for this, but flemme

            string[] macAddresses = jsonData.macAddresses.ToObject<string[]>();
            CalibrationPoint[] calibrationPoints = new CalibrationPoint[jsonData.points.Count];

            for (int i = 0; i < jsonData.points.Count; i++)
            {
                float x = jsonData.points[i].coords[0];
                float y = jsonData.points[i].coords[1];
                double[] RSSs = jsonData.points[i].data.ToObject<double[]>();
                calibrationPoints[i] = new CalibrationPoint(new Vector2(x, y), RSSs);
            }

            return new CalibrationSettings(calibrationPoints, macAddresses);
        }

        public static ICalibrationSettings LoadSettingsFromBinary(byte[] bytes)
        {
            VerifyKeyIntegrity(bytes, binaryKey, 0);
            VerifyKeyIntegrity(bytes, binaryMacAddressLabel, binaryKey.Length);

            int offset = binaryKey.Length + binaryMacAddressLabel.Length;

            int MASize = BitConverter.ToInt32(bytes, offset);
            offset += 4;
            int MACount = MASize / 6;

            string[] macAddresses = new string[MACount];

            for (int i = 0; i < MACount; i++)
            {
                macAddresses[i] = BytesToMacAddress(bytes, offset);
                offset += 6;
            }

            VerifyKeyIntegrity(bytes, binaryCalibrationLabel, offset);
            offset += binaryCalibrationLabel.Length;

            int RSSISize = BitConverter.ToInt32(bytes, offset);
            int TotalRSSISize = BitConverter.ToInt32(bytes, offset + 4);
            offset += 8;

            int RSSICount = TotalRSSISize / RSSISize;

            ICalibrationPoint[] calibrationPoints = new ICalibrationPoint[RSSICount];
            for (int i = 0; i < RSSICount; i++)
            {
                calibrationPoints[i] = BytesToCalibrationPoint(bytes, offset, RSSISize);
                offset += RSSISize;
            }

            return new CalibrationSettings(calibrationPoints, macAddresses);
        }

        private static ICalibrationPoint BytesToCalibrationPoint(byte[] bytes, int start, int size)
        {
            Vector2 v = new Vector2(BitConverter.ToSingle(bytes, start), BitConverter.ToSingle(bytes, start + 4));
            start += 8;
            size -= 8;

            double[] RSSIs = new double[size / 8];
            for (int i = 0; i < size / 8; i++)
            {
                RSSIs[i] = BitConverter.ToDouble(bytes, start + i * 8);
            }

            return new CalibrationPoint(v, RSSIs);
        }


        private static string BytesToMacAddress(byte[] bytes, int start)
        {
            Span<char> chars = new char[6];
            for (int i = 0; i < 6; i++)
                chars[i] = (char) bytes[start + i];
            return chars.ToString();
        }

        private static void VerifyKeyIntegrity(byte[] bytes, string key, int start)
        {
            byte b;
            int i = 0;
            do
            {
                b = bytes[start + i];
                if (b != key[i])
                    throw new Exception("Wrong bytes format exception");
                i++;
            } while (b != '\n');
        }

    }
}
