using System;
using System.Collections.Generic;

namespace Sort
{
    public class SelectionSort
    {
        //选择排序
        public static void Sort(List<int> sortList)
        {
            for (int i = 0; i < sortList.Count; i++)
            {
                int nFindMinIndex = i;
                for (int j = i; j < sortList.Count; j++)
                {
                    if (sortList[j] < sortList[nFindMinIndex])
                    {
                        nFindMinIndex = j;
                    }
                }

                int temp = sortList[i];
                sortList[i] = sortList[nFindMinIndex];
                sortList[nFindMinIndex] = temp;
            }
        }
    }

}
