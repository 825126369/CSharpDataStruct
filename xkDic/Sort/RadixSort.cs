using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 基数排序
class RadixSort
{
    public static void Sort(List<int> arr)
    {
        int iMaxLength = GetMaxLength(arr);
        Sort(arr, iMaxLength);
    }

    //排序
    private static void Sort(List<int> arr, int iMaxLength)
    {
        List<int>[] listArr = new List<int>[10];
        for (int i = 0; i < listArr.Length; i++)
        {
            listArr[i] = new List<int>();
        }

        for (int i = 1; i <= iMaxLength; i++) //一共执行iMaxLength次，iMaxLength是元素的最大位数。
        {
            foreach (int number in arr)//分桶
            {
                int nDiJiWeiValue = GetDiJiWeiValue(number, i);
                if (nDiJiWeiValue >= 0)
                {
                    listArr[nDiJiWeiValue].Add(number);
                }
            }

            arr.Clear();
            for (int j = 0; j < listArr.Length; j++) //将十个桶里的数据重新排列，压入list
            {
                foreach (int number in listArr[j])
                {
                    arr.Add(number);
                }
                listArr[j].Clear();
            }
        }
    }

    //得到最大元素的位数
    private static int GetMaxLength(List<int> arr)
    {
        int nMaxWeiShu = 0;

        foreach (int i in arr)//遍历得到最大值
        {
            int nTemp = i;
            int nWeiShu = 0;
            while (nTemp > 0)
            {
                nTemp /= 10;
                nWeiShu++;
            }

            if(nMaxWeiShu < nWeiShu)
            {
                nMaxWeiShu = nWeiShu;
            }
        }

        return nMaxWeiShu; //这样获得最大元素的位数是不是有点投机取巧了...
    }

    private static int GetDiJiWeiValue(int value, int nDiJiWei)
    {
        int nTemp = value;

        int nWeiShu = 0;
        while (nTemp > 0 && nWeiShu < nDiJiWei - 1)
        {
            nTemp /= 10;
            nWeiShu++;
        }
        
        if (nDiJiWei - 1 == nWeiShu)
        {
            nTemp %= 10;
            return nTemp;
        }
        else
        {
            return -1;
        }
    }
}