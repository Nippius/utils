using System;

namespace PrimeBench
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(2);
            for (int i = 3; i <= 10000; i++)
            {
                bool prime = true;
                for (int j = 2; j < i; j++)
                {
                    int k = i / j;
                    int l = k * j;
                    if (l == i) prime = false;
                }
                if (prime) Console.WriteLine(i);
            }
        }
    }
}