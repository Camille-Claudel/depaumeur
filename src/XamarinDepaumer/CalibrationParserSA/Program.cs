using Depaumer.WifiPositioning;
using System.Text;
using System.Linq;
using System.Diagnostics;
using CalibrationParserSA;

namespace Depaumer.Utils;

public class Program
{
    public static void Main(string[] args)
    {
        // Very unsafe code - For one time use only

        // Read from JSON
        Stopwatch watch = new Stopwatch();
        watch.Start();
        ICalibrationSettings settings = CalibrationParser.LoadSettingsFromJSON(File.ReadAllText("settings.json"));
        watch.Stop();
        Console.WriteLine($"Read json in {watch.ElapsedMilliseconds}ms");

        Console.WriteLine("Simple Convert, Test mode or verify settings ? (c/t/v)");

        ConsoleKey key = Console.ReadKey(true).Key;
        if (key == ConsoleKey.C)
        {
            File.WriteAllBytes("settings.bin", GetBytes(settings));
        }
        else if (key == ConsoleKey.T)
        {
            File.WriteAllBytes("testSettings.bin", GetBytes(settings));
            watch.Restart();
            ICalibrationSettings binarySettings = CalibrationParser.LoadSettingsFromBinary(File.ReadAllBytes("testSettings.bin"));
            watch.Stop();
            Console.WriteLine($"Read binary in {watch.ElapsedMilliseconds}ms");
        }
        else if (key == ConsoleKey.V)
        {
            File.WriteAllBytes("settings.bin", GetBytes(settings));
            byte[] bytes;
            using (BinaryReader reader = new BinaryReader(File.OpenRead("settings.bin")))
                bytes = reader.ProgressiveReadAllBytes();
            ICalibrationSettings binaryReadSettings = CalibrationParser.LoadSettingsFromBinary(bytes);
            
            for (int i = 0; i < binaryReadSettings.CalibrationPoints.Length; i++)
            {
                ICalibrationPoint bp = binaryReadSettings.CalibrationPoints[i];
                ICalibrationPoint jp = settings.CalibrationPoints[i];
                if (!IsEqual(bp, jp))
                {
                    throw new Exception("PSPSPS");
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid key - Quitting");
        }
        Console.WriteLine("END");
    }

    private static byte[] GetBytes(ICalibrationSettings settings)
    {
        byte[] key = GetBytes("CalibrationSettings-1.0\n");

        int MASize = 6 * settings.WifiPointMacAddresses.Length;
        byte[] MacAddressesTag = GetBytes("MacAddresses\n");
        byte[] MacAddressesSize = GetBytes(MASize);

        int RSSISize = 8 + 8 * settings.CalibrationPoints[0].Signals.Length;
        int TotalRSSISize = RSSISize * settings.CalibrationPoints.Length;
        byte[] CalibrationPointsLabel = GetBytes("CalibrationPoints\n");
        byte[] CalibrationPointSize = GetBytes(RSSISize);
        byte[] CalibrationPointsSize = GetBytes(TotalRSSISize);

        Console.WriteLine($"MA:{MASize}, RS:{RSSISize}, TRS:{TotalRSSISize}");

        byte[] BinaryContent = new byte[
            key.Length
            + MacAddressesTag.Length + 4 + MASize
            + CalibrationPointsLabel.Length + 8 + TotalRSSISize
            ];

        int offset = 0;
        key.CopyTo(BinaryContent, offset);
        offset += key.Length;

        MacAddressesTag.CopyTo(BinaryContent, offset);
        offset += MacAddressesTag.Length;

        MacAddressesSize.CopyTo(BinaryContent, offset);
        offset += MacAddressesSize.Length;

        for (int i = 0; i < settings.WifiPointMacAddresses.Length; i++)
        {
            GetBytes(settings.WifiPointMacAddresses[i]).CopyTo(BinaryContent, offset);
            offset += 6;
        }

        CalibrationPointsLabel.CopyTo(BinaryContent, offset);
        offset += CalibrationPointsLabel.Length;

        CalibrationPointSize.CopyTo(BinaryContent, offset);
        offset += CalibrationPointSize.Length;

        CalibrationPointsSize.CopyTo(BinaryContent, offset);
        offset += CalibrationPointsSize.Length;

        for (int i = 0; i < settings.CalibrationPoints.Length; i++)
        {
            GetBytes(settings.CalibrationPoints[i], RSSISize).CopyTo(BinaryContent, offset);
            offset += RSSISize;
        }

        return BinaryContent;

    }

    private static bool IsEqual(ICalibrationPoint A, ICalibrationPoint B)
    {
        if (!((A.Position.X == B.Position.X) && (A.Position.Y == B.Position.Y)))
            return false;
        for (int i = 0; i < A.Signals.Length; i++)
        {
            double sa = A.Signals[i], sb = B.Signals[i];
            if (!(sa == sb))
                return false;
        }
        return true;
    }

    private static byte[] GetBytes(string s)
    {
        return Encoding.UTF8.GetBytes(s);
    }

    private static byte[] GetBytes(int i)
    {
        return BitConverter.GetBytes(i);
    }

    private static byte[] GetBytes(ICalibrationPoint cp, int cpSize)
    {
        byte[] bytes = new byte[cpSize];

        GetBytes(cp.Position.X).CopyTo(bytes, 0);
        GetBytes(cp.Position.Y).CopyTo(bytes, 4);

        for (int i = 0; i < cp.Signals.Count(); i++)
        {
            GetBytes(cp.Signals[i]).CopyTo(bytes, 8 + i * 8);
        }

        return bytes;
    }

    private static byte[] GetBytes(float f)
    {
        return BitConverter.GetBytes(f);
    }

    private static byte[] GetBytes(double d)
    {
        return BitConverter.GetBytes(d);
    }

}