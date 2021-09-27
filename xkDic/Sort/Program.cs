using System;
using System.Collections.Generic;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            int nArrayCount = 10;
            List<int> sortList = new List<int>();
            Random mRandom = new Random();
            for(int i = 0; i <= nArrayCount; i++)
            {
                sortList.Add(mRandom.Next(0, nArrayCount));
                if (nArrayCount < 100)
                {
                    Console.Write("" + sortList[i] + ",");
                }
            }

            Console.WriteLine();

            JiShuSort.Sort(sortList);

            for (int i = 0; i < sortList.Count; i++)
            {
                Console.Write("" + sortList[i] + ",");
                if (i % 20 == 0 && i > 0)
                {
                    Console.WriteLine();
                }
            }

            while (true) { }
        }
    }
}
