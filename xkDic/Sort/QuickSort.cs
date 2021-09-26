using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sort
{
    public class QuickSort
    {
        //快速排序
        //1: 随机找到列表中索引临界值，使得列表中左边的数小于等于临界值，右边的数大于等于临界值
        //2；递归
        public static void Sort(List<int> sortList)
        {
            Sort2(sortList, 0, sortList.Count - 1);
        }

        public static void Sort2(List<int> sortList, int nBeginIndex, int nEndIndex)
        {
            if (nBeginIndex < nEndIndex)
            {
                int nCompareIndex = nBeginIndex + (nEndIndex - nBeginIndex) / 2;
                int nCompareValue = sortList[nCompareIndex];

                int nIndex1 = nBeginIndex;
                int nIndex2 = nEndIndex;

                while (nIndex1 < nIndex2)
                {
                    while (nIndex2 > nCompareIndex && sortList[nIndex2] >= nCompareValue)
                    {
                        nIndex2--;
                    }

                    while (nIndex1 < nCompareIndex && sortList[nIndex1] <= nCompareValue)
                    {
                        nIndex1++;
                    }

                    for (int i = nIndex1; i <= nIndex2; i++)
                    {
                        if (i > nCompareIndex && sortList[i] < nCompareValue)
                        {
                            sortList[nCompareIndex] = sortList[i];
                            sortList[i] = nCompareValue;
                            nCompareIndex = i;
                        }
                    }

                    for (int i = nIndex1; i <= nIndex2; i++)
                    {
                        if (i < nCompareIndex && sortList[i] > sortList[nCompareIndex])
                        {
                            sortList[nCompareIndex] = sortList[i];
                            sortList[i] = nCompareValue;
                            nCompareIndex = i;
                        }
                    }
                }

                for (int i = nBeginIndex; i <= nEndIndex; i++)
                {
                    if (i < nCompareIndex)
                    {
                        Debug.Assert(sortList[nCompareIndex] >= sortList[i], sortList[nCompareIndex] + " | " + sortList[i]);
                    }
                    else if (i > nCompareIndex)
                    {
                        Debug.Assert(sortList[nCompareIndex] <= sortList[i], sortList[nCompareIndex] + " | " + sortList[i]);
                    }
                }

                Sort2(sortList, nBeginIndex, nCompareIndex - 1);
                Sort2(sortList, nCompareIndex + 1, nEndIndex);
            }
        }

        public static void Sort1(List<int> sortList, int nBeginIndex, int nEndIndex)
        {
            if (nBeginIndex < nEndIndex)
            {
                int nCompareIndex = nBeginIndex;
                int nCompareValue = sortList[nCompareIndex];
                
                int nIndex1 = nBeginIndex;
                int nIndex2 = nEndIndex;

                while (nIndex1 < nIndex2)
                {
                    while (sortList[nIndex2] >= nCompareValue && nIndex2 > nIndex1)
                    {
                        nIndex2--;
                    }
                    
                    sortList[nIndex1] = sortList[nIndex2];

                    while (sortList[nIndex1] <= nCompareValue && nIndex2 > nIndex1)
                    {
                        nIndex1++;
                    }
                    
                    sortList[nIndex2] = sortList[nIndex1];
                }

                sortList[nIndex1] = nCompareValue;
                nCompareIndex = nIndex2;

                for (int i = nBeginIndex; i <= nEndIndex; i++)
                {
                    if (i < nCompareIndex)
                    {
                        Debug.Assert(sortList[nCompareIndex] >= sortList[i], sortList[nCompareIndex] + " | " + sortList[i]);
                    }
                    else if (i > nCompareIndex)
                    {
                        Debug.Assert(sortList[nCompareIndex] <= sortList[i], sortList[nCompareIndex] + " | " + sortList[i]);
                    }
                }

                Sort1(sortList, nBeginIndex, nCompareIndex - 1);
                Sort1(sortList, nCompareIndex + 1, nEndIndex);
            }
        }

        public static void Sort(List<int> sortList, int nBeginIndex, int nEndIndex)
        {
            if (nBeginIndex < nEndIndex)
            {
                int nCompareIndex = nBeginIndex + (nEndIndex - nBeginIndex) / 2;
                while (true)
                {
                    int nSwitchCount = 0;
                    for (int i = nBeginIndex; i <= nEndIndex; i++)
                    {
                        if (nCompareIndex < i && sortList[nCompareIndex] > sortList[i])
                        {
                            int temp = sortList[nCompareIndex];
                            sortList[nCompareIndex] = sortList[i];
                            sortList[i] = temp;
                            nCompareIndex = i;

                            nSwitchCount++;
                        }
                    }

                    for (int i = nBeginIndex; i <= nEndIndex; i++)
                    {
                        if (nCompareIndex > i && sortList[nCompareIndex] < sortList[i])
                        {
                            int temp = sortList[nCompareIndex];
                            sortList[nCompareIndex] = sortList[i];
                            sortList[i] = temp;
                            nCompareIndex = i;

                            nSwitchCount++;
                        }
                    }

                    if (nSwitchCount == 0)
                    {
                        break;
                    }
                }

                for (int i = nBeginIndex; i <= nEndIndex; i++)
                {
                    if (nCompareIndex > i)
                    {
                        Debug.Assert(sortList[nCompareIndex] >= sortList[i], sortList[nCompareIndex] + " | " + sortList[i]);
                    }
                    else if (nCompareIndex < i)
                    {
                        Debug.Assert(sortList[nCompareIndex] <= sortList[i], sortList[nCompareIndex] + " | " + sortList[i]);
                    }
                }

                Sort(sortList, nBeginIndex, nCompareIndex - 1);
                Sort(sortList, nCompareIndex + 1, nEndIndex);
            }
        }
    }
}
