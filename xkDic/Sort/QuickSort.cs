using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Sort
{
    public class QuickSort
    {
        //冒泡排序
        //1: 每次确定一个最大值，
        //2:记录相邻元素移动次数，如果为0表示移动完成
        public static void Sort(List<int> sortList)
        {
            Sort(sortList, 0, sortList.Count - 1);
        }

        public static void Sort(List<int> sortList, int nBeginIndex, int nEndIndex)
        {
            if(nBeginIndex >= nEndIndex)
            {

            }
            else
            {
                int nCompareIndex = nBeginIndex;
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

                    if(nSwitchCount == 0)
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
