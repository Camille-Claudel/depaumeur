using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Depaumer.WifiPositioning;

public class Locator : ILocator
{

    public ICalibrationSettings Settings { get; }

    public Vector2? CurrentPosition { get; private set; }

    public TimeOnly LastUpdateTime { get; private set; }

    public event EventHandler<IPositionUpdateArgs>? PositionUpdated;

    public Locator(ICalibrationSettings settings)
    {
        Settings = settings;
        LastUpdateTime = new TimeOnly(DateTime.Now.Ticks);
    }

    private double[] makeVector(IWifiSignal[] signals)
    {
        double[] vector = new double[Settings.WifiPointMacAddresses.Length];

        foreach (IWifiSignal signal in signals)
        {
            int index = Array.IndexOf(Settings.WifiPointMacAddresses, signal.MacAddress);
            if (index != -1)
                vector[index] = signal.RSS;
        }

        return vector;
    }

    public Vector2 Locate(IWifiSignal[] signals)
    {

        // Calculating new position
        double[] measureVector = makeVector(signals);

        double minDst = double.MaxValue;
        int argmin = 0;
        for (int i = 0; i < Settings.CalibrationPoints.Length; i++)
        {
            double dst = Utils.ArrayExtensions.SquaredEuclidianDistance(measureVector, Settings.CalibrationPoints[i].Signals);
            if (dst < minDst)
            {
                minDst = dst;
                argmin = i;
            }
        }

        // Firing Event and return of new position
        Vector2 pos = Settings.CalibrationPoints[argmin].Position; // Buffering position

        IPositionUpdateArgs args = new PositionUpdateArgs(
            CurrentPosition,    // Previous position
            pos,                // The new calculated distance
            LastUpdateTime);
        
        CurrentPosition = pos;                              // Updating instance variables
        LastUpdateTime = new TimeOnly(DateTime.Now.Ticks);

        PositionUpdated?.Invoke(this, args);

        return pos;
    }
}
