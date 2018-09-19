 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            int num = 0;
            Console.Write("Please input an int:");
            s = Console.ReadLine();
            num = Int32.Parse(s);
            if(num <= 1)
            {
                Console.Write("The int is wrong");
            }
            else
            {
                int i = 2;
                Console.Write(num + " 的素数因子为 ");
                while(num != 1)
                {
                    if (num % i == 0)
                    {
                        Console.Write(i + " ");
                        num = num / i;
                    }
                    else i++;
                }
            }
        }
    }
}
