using System;
using System.Collections.Generic;

namespace Sort
{
    public class BubbleSort
    {
        //冒泡排序
        //1: 每次确定一个最大值，
        //2:记录相邻元素移动次数，如果为0表示移动完成
        public static void Sort(List<int> sortList)
        {
            int nMaxCompareCount = sortList.Count - 1;
            for (int i = 0; i < nMaxCompareCount; i++)
            {
                int nMoveCount = 0;
                for(int j = 0; j < nMaxCompareCount; j++)
                {
                    if(sortList[j] > sortList[j + 1])
                    {
                        int temp = sortList[j];
                        sortList[j] = sortList[j + 1];
                        sortList[j + 1] = temp;
                        nMoveCount++;
                    }
                }

                if (nMoveCount == 0)
                {
                    break;
                }
            }
        }
    }
}
