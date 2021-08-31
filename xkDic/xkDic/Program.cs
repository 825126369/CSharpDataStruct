using System;
using System.Collections.Generic;

namespace xkDic
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Random mRandom = new Random();
            xkDic<int, string> mDic = new xkDic<int, string>();

            for(int i = 0; i < 1000000; i++)
            {
                mDic.Add(i, "AAA" + i);
                mDic.Remove(i);
            }

            for (int i = 0; i < 1000000; i++)
            {
                if (mRandom.Next(1, 3) == 1)
                {
                    mDic.Add(i, "AAA" + i);
                }
            }

            string AAA = "100";
            if (mDic.FindValue(1000, out AAA))
            {
                Console.WriteLine("AAA: " + AAA);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
