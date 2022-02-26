using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WHC.Framework.Commons.SortStrategies
{
    /// <summary>
    /// TODO: Fix Error.
    /// </summary>
    public class ShellSort
    {
        internal static void Sort(ref IList<int> numbers)
        {
            int i, j, increment;
            int temp;
            increment = 3;
            while (increment > 0)
            {
                for (i = 0; i < numbers.Count - 1; i++)
                {
                    j = i;
                    temp = numbers[i];
                    while ((j >= increment) && (numbers[j - increment] > temp))
                    {
                        numbers[j] = numbers[j - increment];
                        j = j - increment;
                    }
                    numbers[j] = temp;
                }
                if (increment / 2 != 0)
                    increment = increment / 2;
                else if (increment == 1)
                    increment = 0;
                else
                    increment = 1;
            }
        }
        internal static void Sort(ref IList<decimal> numbers)
        {
            int i, j, increment;
            decimal temp;
            increment = 3;
            while (increment > 0)
            {
                for (i = 0; i < numbers.Count - 1; i++)
                {
                    j = i;
                    temp = numbers[i];
                    while ((j >= increment) && (numbers[j - increment] > temp))
                    {
                        numbers[j] = numbers[j - increment];
                        j = j - increment;
                    }
                    numbers[j] = temp;
                }
                if (increment / 2 != 0)
                    increment = increment / 2;
                else if (increment == 1)
                    increment = 0;
                else
                    increment = 1;
            }
        }
        internal static void Sort(ref IList<long> numbers)
        {
            int i, j, increment;
            long temp;
            increment = 3;
            while (increment > 0)
            {
                for (i = 0; i < numbers.Count - 1; i++)
                {
                    j = i;
                    temp = numbers[i];
                    while ((j >= increment) && (numbers[j - increment] > temp))
                    {
                        numbers[j] = numbers[j - increment];
                        j = j - increment;
                    }
                    numbers[j] = temp;
                }
                if (increment / 2 != 0)
                    increment = increment / 2;
                else if (increment == 1)
                    increment = 0;
                else
                    increment = 1;
            }
        }
        internal static void Sort(ref IList<float> numbers)
        {
            int i, j, increment;
            float temp;
            increment = 3;
            while (increment > 0)
            {
                for (i = 0; i < numbers.Count - 1; i++)
                {
                    j = i;
                    temp = numbers[i];
                    while ((j >= increment) && (numbers[j - increment] > temp))
                    {
                        numbers[j] = numbers[j - increment];
                        j = j - increment;
                    }
                    numbers[j] = temp;
                }
                if (increment / 2 != 0)
                    increment = increment / 2;
                else if (increment == 1)
                    increment = 0;
                else
                    increment = 1;
            }
        }
        internal static void Sort(ref IList<double> numbers)
        {
            int i, j, increment;
            double temp;
            increment = 3;
            while (increment > 0)
            {
                for (i = 0; i < numbers.Count - 1; i++)
                {
                    j = i;
                    temp = numbers[i];
                    while ((j >= increment) && (numbers[j - increment] > temp))
                    {
                        numbers[j] = numbers[j - increment];
                        j = j - increment;
                    }
                    numbers[j] = temp;
                }
                if (increment / 2 != 0)
                    increment = increment / 2;
                else if (increment == 1)
                    increment = 0;
                else
                    increment = 1;
            }
        }
    }
}
