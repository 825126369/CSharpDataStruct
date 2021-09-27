using System;
using System.Collections.Generic;

namespace Sort
{
    public class BucketSort
    {
        //计数排序
        public static void Sort(List<int> sortList)
        {
            CountSort(sortList);
        }

        public static void CountSort(List<int> sortList)
        {
            int[] b = new int[sortList.Count];

            int max = sortList[0];
            int min = sortList[0];

            foreach (int item in sortList)
            {
                if (item > max)
                {
                    max = item;
                }
                else if (item < min)
                {
                    min = item;
                }
            }

            int k = max - min + 1;
            int[] c = new int[k];
            for (int i = 0; i < sortList.Count; i++)
            {
                int nValue = sortList[i] - min;
                c[nValue] += 1;
            }

            for (int i = 1; i < c.Length; i++)
            {
                c[i] += c[i - 1];
            }

            for (int i = sortList.Count - 1; i >= 0; i--)
            {
                int nPosIndex = --c[sortList[i] - min];
                b[nPosIndex] = sortList[i];
            }

            sortList.Clear();
            sortList.AddRange(b);
        }
    }
}
