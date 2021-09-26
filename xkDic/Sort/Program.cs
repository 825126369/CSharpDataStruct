using System;
using System.Collections.Generic;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> sortList = new List<int>();
            Random mRandom = new Random();
            for(int i = 0; i <= 100; i++)
            {
                sortList.Add(mRandom.Next(0, 100));
            }

            QuickSort.Sort(sortList);

            for (int i = 0; i < sortList.Count; i++)
            {
                Console.Write("" + sortList[i] + ",");
                if (i % 20 == 0)
                {
                    Console.WriteLine();
                }
            }

            while (true) { }
        }
    }
}
