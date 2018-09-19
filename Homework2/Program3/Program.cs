using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] num = new int[99];
            for(int i= 0;i < 99;i++)
            {
                num[i] = i + 2;
            }

            for(int j = 2;j <= 100;j++)
            {
                for(int k = 0;k < 99;k++)
                {
                    if (num[k] % j == 0 && num[k] > j) num[k] = 0;
                }
            }

            Console.Write("2-100内的素数有：");
            for(int n = 0;n < 99;n++)
            {
                if (num[n] != 0) Console.Write(num[n] + " ");
            }
        }
    }
}
