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
            double n1 = 0;
            double n2 = 0;
            double theAdd;
            Console.Write("Please input an int: ");
            s = Console.ReadLine();
            n1 = double.Parse(s);
            Console.Write("Please input another int: ");
            s = Console.ReadLine();
            n2 = double.Parse(s);
            theAdd = n1 * n2;
            Console.WriteLine("The product of two ints is : " + theAdd);
        }
    }
}
