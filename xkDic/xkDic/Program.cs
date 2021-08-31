using System;

namespace xkDic
{
    class Program
    {
        static void Main(string[] args)
        {
            xkDic<int, string> mDic = new xkDic<int, string>();
            mDic.Add(1000, "AAA");
            string AAA = "100";
            if (mDic.FindValue(1000, out AAA))
            {
                Console.WriteLine("AAA: " + AAA);
            }

            Console.WriteLine("Hello World!");
        }
    }
}
