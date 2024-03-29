﻿using System;
using System.Collections.Generic;

namespace Sort
{
    public class BucketSort
    {
        //计数排序
        public static void Sort(List<int> sortList)
        {
            BucketSortReCursion2(sortList, 1);
        }

        public static void BucketSortReCursion2(List<int> sortList, int bucketSize)
        {
            //求最值
            int max = sortList[0], min = sortList[0];
            foreach (var item in sortList)
            {
                max = item > max ? item : max;
                min = item < min ? item : min;
            }

            //初新组(桶)
            //初始化桶数量
            int bucketCount = (max - min) / bucketSize + 1;
            var buckets = new List<List<int>>(bucketCount);
            for (int i = 0; i < bucketCount; i++)
            {
                buckets.Add(new List<int>());
            }

            //正填充
            for (int i = 0; i < sortList.Count; i++)
            {
                //找到对应的桶
                int index = (sortList[i] - min) / bucketSize;
                buckets[index].Add(sortList[i]);
            }

            //反填充
            List<int> arrayResult = new List<int>();
            for (int i = 0; i < bucketCount; i++)
            {
                //当桶大小为1时，将桶的数据全部放入结果集
                if (bucketSize == 1)
                {
                    foreach (var item in buckets[i])
                    {
                        arrayResult.Add(item);
                    }
                }
                else
                {
                    if (bucketCount == 1)
                    {
                        bucketSize--;
                    }

                    //递归调用
                    List<int> temp = new List<int>(buckets[i]);
                    BucketSortReCursion2(temp, bucketSize);
                    foreach (var item in temp)
                    {
                        arrayResult.Add(item);
                    }
                }
            }
            
            sortList.Clear();
            sortList.AddRange(arrayResult);
        }
    }
}
