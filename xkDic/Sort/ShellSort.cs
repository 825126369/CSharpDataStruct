using System;
using System.Collections.Generic;

namespace Sort
{
    public class ShellSort
    {
        //希尔插入排序：对已有的排序好的列表进行插入排序
        //这里初始以索引0 为已排序好的列表
        //主要是目的是消除直接插入排序时产生的大量的移动元素操作
        public static void Sort(List<int> sortList)
        {
            int nStepLength = sortList.Count / 2;

            while (nStepLength >= 1)
            {
                for (int n = 0; n < nStepLength; n++)
                {
                    for (int i = 0; i < sortList.Count; i += nStepLength)
                    {
                        for (int j = i; j >= 1; j -= nStepLength)
                        {
                            int nLastNextIndex = j - nStepLength;
                            if (sortList[j] < sortList[nLastNextIndex])
                            {
                                int temp = sortList[j];
                                sortList[j] = sortList[nLastNextIndex];
                                sortList[nLastNextIndex] = temp;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }

                nStepLength /= 2;
            }
        }
    }
}
