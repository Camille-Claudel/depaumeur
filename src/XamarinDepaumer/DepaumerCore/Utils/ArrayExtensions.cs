using System;
using System.Collections.Generic;
using System.Text;

namespace Depaumer.Utils
{
    public static class ArrayExtensions
    {
        public static double SquaredEuclidianDistance(this double[] array, double[] other)
        {
            //NB : If we have performance problems (Because this method is used a lot), we can use registers in an unsafe context, with the help of pointers

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
}