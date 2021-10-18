using System;
using System.Diagnostics;

namespace DigetLink
{
    class Program
    {
        static void Main(string[] args)
        {
            Random mRandom = new Random();
            for (int i = 0; i < 100000; i++)
            {
                uint Input1 = (uint)mRandom.Next();
                uint Input2 = (uint)mRandom.Next();

                //uint Input1 = uint.MaxValue;
                //uint Input2 = 2;

                uint result1 = IntegerAdd(Input1, Input2);
                Console.WriteLine("result1: " + result1);
            }
        }

        static uint IntegerAdd(uint A, uint B)
        {
            IntegerLinkedList result = new IntegerLinkedList(A) + new IntegerLinkedList(B);
            uint result1 = result.ToInteger();
            Debug.Assert(result1 == A + B, result1 + " | " + A + " + " + B + " | " + (A + B));
            return result1;
        }

    }
}
