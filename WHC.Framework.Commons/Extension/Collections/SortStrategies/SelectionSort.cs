using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHC.Framework.Commons.SortStrategies
{
    internal class SelectionSort
    {
        internal static void Sort(ref IList<int> numbers)
        {
            int i, j;
            int min, temp;
            for (i = 0; i < numbers.Count - 1; i++)
            {
                min = i;
                for (j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[j] < numbers[min])
                        min = j;
                }
                temp = numbers[i];
                numbers[i] = numbers[min];
                numbers[min] = temp;
            }
        }
        internal static void Sort(ref IList<long> numbers)
        {
            int i, j, min;
            long temp;
            for (i = 0; i < numbers.Count - 1; i++)
            {
                min = i;
                for (j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[j] < numbers[min])
                        min = j;
                }
                temp = numbers[i];
                numbers[i] = numbers[min];
                numbers[min] = temp;
            }
        }
        internal static void Sort(ref IList<decimal> numbers)
        {
            int i, j, min;
            decimal temp;
            for (i = 0; i < numbers.Count - 1; i++)
            {
                min = i;
                for (j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[j] < numbers[min])
                        min = j;
                }
                temp = numbers[i];
                numbers[i] = numbers[min];
                numbers[min] = temp;
            }
        }
        internal static void Sort(ref IList<float> numbers)
        {
            int i, j,min; 
            float temp;
            for (i = 0; i < numbers.Count - 1; i++)
            {
                min = i;
                for (j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[j] < numbers[min])
                        min = j;
                }
                temp = numbers[i];
                numbers[i] = numbers[min];
                numbers[min] = temp;
            }
        }
        internal static void Sort(ref IList<double> numbers)
        {
            int i, j, min;
            double temp;
            for (i = 0; i < numbers.Count - 1; i++)
            {
                min = i;
                for (j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[j] < numbers[min])
                        min = j;
                }
                temp = numbers[i];
                numbers[i] = numbers[min];
                numbers[min] = temp;
            }
        }
    }
}
