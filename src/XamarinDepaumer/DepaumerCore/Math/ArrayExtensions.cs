using System;
using System.Collections.Generic;
using System.Text;

namespace Depaumer.Math;

public static class ArrayExtensions
{
    public static double SquaredEuclidianDistance(this double[] array, double[] other)
    {
        double value = 0d;
        double diff;

        for (int i = 0; i < array.Length; i++)
        {
            diff = array[i] - other[i];
            value += diff * diff;
        }

        return value;
    }
}
