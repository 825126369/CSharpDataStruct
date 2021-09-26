using System;
using System.Collections.Generic;

namespace Sort
{
    public class InsertionSort
    {
        //插入排序：对已有的排序好的列表进行插入排序
        //这里初始以索引0 为已排序好的列表
        public static void Sort(List<int> sortList)
        {
            for (int i = 1; i < sortList.Count; i++)
            {
                if (sortList[i] < sortList[i - 1])
                {
                    for (int j = i; j >= 1; j--)
                    {
                        if (sortList[j] < sortList[j - 1])
                        {
                            int temp = sortList[j];
                            sortList[j] = sortList[j - 1];
                            sortList[j - 1] = temp;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

}
