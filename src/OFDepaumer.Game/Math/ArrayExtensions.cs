using System;
using System.Collections.Generic;
using System.Text;

namespace OFDepaumer.Game.Math
{
    public static class ArrayExtensions
    {
        public static double SquaredEuclidianDistance(this Array array, Array other)
        {
            double value = 0d;
            double diff;

            for (int i = 0; i < array.Length; i++)
            {
                diff = (double)array.GetValue(i) - (double)other.GetValue(i);
                value += diff * diff;
            }

            return value;
        }
    }
}
