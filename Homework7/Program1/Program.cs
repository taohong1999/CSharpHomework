using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1
{
    public class Program
    {
        static void Main(string[] args)
        {
            OrderService orderService = new OrderService();
            
            bool isContinue = true;
            do
            {
                Console.WriteLine("请输入您需要的服务（添加订单，查询订单，修改订单，删除订单,）");
                string s1 = Console.ReadLine();
                DoService.DoOrder(s1, orderService);
                Console.WriteLine("请问是否继续服务(是或否）：");
                string s2 = Console.ReadLine();
                if (s2 == "是") isContinue = true;
                else if (s2 == "否") isContinue = false;
                else
                {
                    Console.WriteLine("输入的指令有误");
                }
            } while (isContinue == true);
            orderService.Export("order.xml");
            OrderService.Import("order.xml");
        }
    }

    class DoService
    {

        public static void DoOrder(string s, OrderService orderService)
        {
            if (s == "添加订单")
            {
                Console.WriteLine("请按顺序输入客户姓名，订单号（例：张三 123456789（每输入一个信息请按下回车键））：");
                string customer = "";
                string ordernumber = "";
                customer = Console.ReadLine();
                ordernumber = Console.ReadLine();
                Console.WriteLine("该订单有几种商品：");
                string sNum = Console.ReadLine();
                int num = 0;
                try
                {
                    num = Int32.Parse(sNum);
                }
                catch
                {
                    Console.WriteLine("输入的件数存在问题");
                }
                if (num <= 0) { Console.WriteLine("商品件数应该大于0"); }
                else
                {
                    orderService.AddOrder(ordernumber, customer, num);
                }
            }

            else if (s == "查询订单")
            {
                string s1 = "";
                Console.WriteLine("请输入需要查询的订单的一项信息（姓名或订单号）：");
                s1 = Console.ReadLine();
                Console.WriteLine("您的订单信息为：");
                orderService.OutOrder(s1);
                Console.WriteLine("是否查找10000元以上订单");
                s1 = Console.ReadLine();
                if (s1 == "是") orderService.SearchByMoney();
            }

            else if (s == "修改订单")
            {
                Console.WriteLine("请输入需要查询的订单的一项信息（姓名或订单号）：");
                string s1 = "";
                s1 = Console.ReadLine();
                List<Orders> orders = orderService.SearchOrder(s1);
                orderService.OutOrder(s1);
                Console.WriteLine("接下来按照顺序修改");
                foreach (Orders order in orders )
                {
                    if (order != null)
                    {
                        string str;
                        Console.WriteLine("是否修改订单号和客户名称(是或否)");
                        str = Console.ReadLine();
                        if (str == "是")
                        {
                            Console.WriteLine("请按顺序输入新的订单信息（客户姓名，订单号）:");
                            string customer = "";
                            string ordernumber = "";
                            customer = Console.ReadLine();
                            ordernumber = Console.ReadLine();
                            orderService.ChangeOrder(order, customer, ordernumber);
                        }

                        Console.WriteLine("是否增加商品");
                        str = Console.ReadLine();
                        if (str == "是")
                        {
                            bool isContinue = true;
                            do
                            {
                                Console.WriteLine("输入添加的商品的信息（名称，单价，数量）");
                                string str2 = "";
                                str2 = Console.ReadLine();
                                string name = str2;

                                str2 = Console.ReadLine();
                                double price = 0;
                                try { price = double.Parse(str2); }
                                catch { Console.WriteLine("输入单价格式错误"); }

                                str2 = Console.ReadLine();
                                int number = 0;
                                try { number = Int32.Parse(str2); }
                                catch { Console.WriteLine("输入数量格式错误"); }
                                orderService.ChangeGoods(order, true, name, price, number);
                                Console.WriteLine("是否继续增加");
                                str2 = Console.ReadLine();
                                if (str2 == "是") isContinue = true;
                                else if (str2 == "否") isContinue = false;
                                else { Console.WriteLine("输入的指令有误"); }
                            } while (isContinue == true);
                        }

                        Console.WriteLine("是否删除商品");
                        str = Console.ReadLine();
                        if (str == "是")
                        {
                            bool isContinue = true;
                            do
                            {
                                Console.WriteLine("输入删除的商品的名称");
                                string str2 = "";
                                str2 = Console.ReadLine();
                                string name = str2;
                                orderService.ChangeGoods(order, false, name);
                                Console.WriteLine("是否继续删除");
                                str2 = Console.ReadLine();
                                if (str2 == "是") isContinue = true;
                                else if (str2 == "否") isContinue = false;
                                else { Console.WriteLine("输入的指令有误"); }
                            } while (isContinue == true);
                        }
                    }
                }
            }

            else if (s == "删除订单")
            {
                Console.WriteLine("请输入需要删除的订单的一项信息（姓名或订单号）：");
                string s1 = "";
                s1 = Console.ReadLine();
                orderService.DeleteOrder(s1);
            }

            else
            {
                Console.WriteLine("没有该项服务");
            }
        }
    }
}



