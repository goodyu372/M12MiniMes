using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHC.Framework.Commons.SortStrategies
{
    internal class BubbleSort
    {
        internal static void Sort(ref IList<int> numbers)
        {
            int i, j, temp;
            for (i = numbers.Count - 1; i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    if (numbers[j - 1] > numbers[j])
                    {
                        temp = numbers[j - 1];
                        numbers[j - 1] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
        }
        internal static void Sort(ref IList<long> numbers)
        {
            int i, j;
            long temp;
            for (i = numbers.Count -1; i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    if (numbers[j - 1] > numbers[j])
                    {
                        temp = numbers[j - 1];
                        numbers[j - 1] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
        }
        internal static void Sort(ref IList<decimal> numbers)
        {
            int i, j;
            decimal temp;
            for (i = numbers.Count - 1; i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    if (numbers[j - 1] > numbers[j])
                    {
                        temp = numbers[j - 1];
                        numbers[j - 1] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
        }
        internal static void Sort(ref IList<float> numbers)
        {
            int i, j;
            float temp;
            for (i = numbers.Count - 1; i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    if (numbers[j - 1] > numbers[j])
                    {
                        temp = numbers[j - 1];
                        numbers[j - 1] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
        }
        internal static void Sort(ref IList<double> numbers)
        {
            int i, j;
            double temp;
            for (i = numbers.Count - 1; i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    if (numbers[j - 1] > numbers[j])
                    {
                        temp = numbers[j - 1];
                        numbers[j - 1] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
        }
    }
}
