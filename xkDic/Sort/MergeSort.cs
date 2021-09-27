using System;
using System.Collections.Generic;

namespace Sort
{
    public class MergeSort
    {
        //归并排序
        public static void Sort(List<int> sortList)
        {
            Sort(sortList, 0, sortList.Count - 1);
        }

        public static void Sort(List<int> sortList, int nBeginIndex, int nEndIndex)
        {
            if (nBeginIndex < nEndIndex)
            {
                int nMiddleIndex = (nBeginIndex + nEndIndex) / 2;
                Sort(sortList, nBeginIndex, nMiddleIndex);
                Sort(sortList, nMiddleIndex + 1, nEndIndex);
                MergeMethid(sortList, nBeginIndex, nMiddleIndex, nEndIndex);
            }
        }

        private static void MergeMethid(List<int> sortList, int nBeginIndex, int nMiddleIndex, int nEndIndex)
        {
            int[] t = new int[nEndIndex - nBeginIndex + 1];
            int m = nBeginIndex, n = nMiddleIndex + 1, k = 0;
            while (n <= nEndIndex && m <= nMiddleIndex)
            {
                if (sortList[m] > sortList[n])
                {
                    t[k++] = sortList[n++];
                }
                else
                {
                    t[k++] = sortList[m++];
                }
            }

            while (n <= nEndIndex)
            {
                t[k++] = sortList[n++];
            }

            while (m <= nMiddleIndex)
            {
                t[k++] = sortList[m++];
            }

            for (k = 0, m = nBeginIndex; m <= nEndIndex; k++, m++)
            {
                sortList[m] = t[k];
            }
        }

    }
}
