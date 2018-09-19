using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program2
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            int length = 0;
            Console.Write("Please input the array length:");
            s = Console.ReadLine();
            length = Int32.Parse(s);
            int[] num = new int[length];
            Console.Write("Please input the number in array:");
            for(int i = 0; i < length;i++)
            {
                s = Console.ReadLine();
                num[i] = Int32.Parse(s);
            }

            //输出最大值
            int MAX = num[0];
            for(int j = 0;j < length; j++)
            {
                if (num[j] > MAX) MAX = num[j];
            }
            Console.Write("数组最大值为：" + MAX +"\n");

            //输出最小值
            int MIN = num[0];
            for (int j = 0; j < length; j++)
            {
                if (num[j] < MIN) MIN = num[j];
            }
            Console.Write("数组最小值为：" + MIN + "\n");

            //求和
            double sum = 0;
            for (int j = 0; j < length; j++)
            {
                sum += num[j];
            }
            Console.Write("数组的和为：" + sum + "\n");

            //求平均值
            double average = 0;
            average = sum / length;
            Console.Write("数组的平均值为：" + average + "\n");
        }
    }
}
