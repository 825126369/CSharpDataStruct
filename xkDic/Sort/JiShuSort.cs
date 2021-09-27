using System;
using System.Collections.Generic;

namespace Sort
{
    public class JiShuSort
    {
        //计数排序：对已有的排序好的列表进行插入排序
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
            List<int> c = new List<int>();
            for (int i = 0; i < sortList.Count; i++)
            {
                c[sortList[i] - min] += 1;
            }
            for (int i = 1; i < c.Count; i++)
            {
                c[i] = c[i] + c[i - 1];
            }
            for (int i = sortList.Count - 1; i >= 0; i--)
            {
                b[--c[sortList[i] - min]] = sortList[i];
            }

            sortList.Clear();
            sortList.AddRange(b);
        }
    }
}
