using System;
using System.Collections.Generic;

namespace Sort
{
    public class HeapSort
    {
        //堆排序
        //              0
        //            /   \
        //           1     2
        //          / \    / \
        //         3   4   5  6
        //        / \  
        //       7   8
        //
        //堆排序的基本思想是：将待排序序列构造成一个大顶堆，此时，整个序列的最大值就是堆顶的根节点。
        //将其与末尾元素进行交换，此时末尾就为最大值。
        //然后将剩余n-1个元素重新构造成一个堆，这样会得到n个元素的次小值。
        //如此反复执行，便能得到一个有序序列了
        public static void Sort(List<int> sortList)
        {
            //1.构建大顶堆
            for (int i = sortList.Count / 2 - 1; i >= 0; i--)
            {
                //从第一个非叶子结点从下至上，从右至左调整结构
                max_heapify(sortList, i, sortList.Count - 1);
            }

            // 上述逻辑，建堆结束
            // 下面，开始排序逻辑
            for (int i = sortList.Count - 1; i > 0; i--)
            {
                swap(sortList, 0, i); ;//将堆顶元素与末尾元素进行交换
                max_heapify(sortList, 0, i - 1);
            }
        }

        static void swap(List<int> sortList, int nIndex1, int nIndex2)
        {
            int temp = sortList[nIndex1];
            sortList[nIndex1] = sortList[nIndex2];
            sortList[nIndex2] = temp;
        }

        static void max_heapify(List<int> sortList, int nBeginIndex, int nEndIndex)
        {
            //建立父节点指标和子节点指标
            int nDadIndex = nBeginIndex;
            int nLeftSonIndex = nDadIndex * 2 + 1;
            int nMaxSonIndex = nLeftSonIndex;

            while (nMaxSonIndex <= nEndIndex)  //若子节点指标在范围内才做比较
            {
                if (nMaxSonIndex + 1 <= nEndIndex && sortList[nMaxSonIndex] < sortList[nMaxSonIndex + 1]) //先比较两个子节点大小，选择最大的
                {
                    nMaxSonIndex++;
                }

                if (sortList[nDadIndex] < sortList[nMaxSonIndex]) //如果父节点大于子节点代表调整完毕，直接跳出函数
                {
                    swap(sortList, nDadIndex, nMaxSonIndex);
                    nDadIndex = nMaxSonIndex;
                    nMaxSonIndex = nDadIndex * 2 + 1;
                }
                else
                {
                    break;
                }
            }
        }

    }
}
