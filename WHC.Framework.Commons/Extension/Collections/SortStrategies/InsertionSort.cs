using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHC.Framework.Commons.SortStrategies
{
    internal class InsertionSort
    {
        internal static void Sort(ref IList<int> numbers)
        {
            int i, j;
            int index;
            for (i = 1; i < numbers.Count; i++)
            {
                index = numbers[i];
                j = i;
                while ((j > 0) && (numbers[j - 1] > index))
                {
                    numbers[j] = numbers[j - 1];
                    j = j - 1;
                }
                numbers[j] = index;
            }
        }
        internal static void Sort(ref IList<long> numbers)
        {
            int i, j;
            long index;
            for (i = 1; i < numbers.Count; i++)
            {
                index = numbers[i];
                j = i;
                while ((j > 0) && (numbers[j - 1] > index))
                {
                    numbers[j] = numbers[j - 1];
                    j = j - 1;
                }
                numbers[j] = index;
            }
        }
        internal static void Sort(ref IList<decimal> numbers)
        {
            int i, j;
            decimal index;
            for (i = 1; i < numbers.Count; i++)
            {
                index = numbers[i];
                j = i;
                while ((j > 0) && (numbers[j - 1] > index))
                {
                    numbers[j] = numbers[j - 1];
                    j = j - 1;
                }
                numbers[j] = index;
            }
        }
        internal static void Sort(ref IList<float> numbers)
        {
            int i, j;
            float index;
            for (i = 1; i < numbers.Count; i++)
            {
                index = numbers[i];
                j = i;
                while ((j > 0) && (numbers[j - 1] > index))
                {
                    numbers[j] = numbers[j - 1];
                    j = j - 1;
                }
                numbers[j] = index;
            }
        }
        internal static void Sort(ref IList<double> numbers)
        {
            int i, j;
            double index;
            for (i = 1; i < numbers.Count; i++)
            {
                index = numbers[i];
                j = i;
                while ((j > 0) && (numbers[j - 1] > index))
                {
                    numbers[j] = numbers[j - 1];
                    j = j - 1;
                }
                numbers[j] = index;
            }
        }
    }
}
