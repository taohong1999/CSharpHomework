using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Program1
{
    public class OrderService
    {
        public List<Orders> mOrders = new List<Orders>();

        public List<Orders> Getorders
        {
            set { mOrders = value; }
            get { return mOrders; }
        }

        //添加订单
        public void AddOrder(string s1, string s2, int num)
        {
            //将订单添加到List里面
            Orders order = new Orders(s1, s2);
            Console.WriteLine("请按顺序输入各种商品的信息（名称，单价，数量）：");
            for (int i = 0; i < num; i++)
            {
                string s = "";
                s = Console.ReadLine();
                string name = s;

                s = Console.ReadLine();
                double price = 0;
                try { price = double.Parse(s); }
                catch { Console.WriteLine("输入单价格式错误"); }

                s = Console.ReadLine();
                int number = 0;
                try { number = Int32.Parse(s); }
                catch { Console.WriteLine("输入数量格式错误"); }
                if (price > 0 && number > 0) { order.AddGood(name, price, number); }
                else { Console.WriteLine("输入的单价和数量不可是负数"); }
            }
            mOrders.Add(order);
        }

        public void AddOrders2(Orders orders)
        {
            mOrders.Add(orders);
        }


        //寻找订单
        public List<Orders> SearchOrder(string s1)
        {
            //foreach (Orders order in mOrders)
            //{
            //    //订单中任意一项信息匹配则输出订单信息
            //    if (order.orderNumber == s1 || order.theCustomer == s1)
            //    {
            //        mOrder = order;
            //    }
            //}
            //if (mOrder == null)
            //{
            //    Console.WriteLine("未找到该订单信息，请确认输入的信息是否正确");
            //    return null;
            //}
            //return mOrder;

            List<Orders> mOrder2 = new List<Orders>();
            int i = 0;
            //使用Linq语句查找
            var mOrder = from o in mOrders where (o.orderNumber == s1 || o.theCustomer == s1) select o;
            foreach(var o in mOrder)
            {
                i++;
                mOrder2.Add(o);
            }
            if(i == 0)
            {
                Console.WriteLine("未找到该订单信息，请确认输入的信息是否正确");
            }

            return mOrder2;
        }

        //输出订单的详细信息
        public void OutOrder(string s)
        {
            List <Orders> orders = SearchOrder(s);
            if (orders != null)
            {
                foreach (Orders order in orders)
                {
                    if (order != null)
                    {
                        Console.WriteLine("订单用户为：" + order.theCustomer);
                        Console.WriteLine("订单号为：" + order.orderNumber);
                        Console.WriteLine("订单商品有：");
                        foreach (OrderDetails details in order.mOrderDetails)
                        {
                            Console.WriteLine("商品名称：" + details.goodName);
                            Console.WriteLine("商品单价：" + details.goodPrice);
                            Console.WriteLine("商品数量：" + details.goodNumber);
                        }
                    }
                }
            }

        }

        //修改订单基本信息
        public void ChangeOrder(Orders order, string s1, string s2)
        {
            try
            {
                order.theCustomer = s1;
                order.orderNumber = s2;
                Console.WriteLine("您的订单号和客户名已修改");
            }
            catch
            {
                Console.WriteLine("发生异常，修改失败,请确认信息是否正确");
            }
        }

        //增删订单中商品
        public void ChangeGoods(Orders order, bool isAdd, string name, double price = 0, int number = 0)
        {
            if (isAdd == true)
            {
                if (price > 0 && number > 0)
                {
                    order.AddGood(name, price, number);
                    Console.WriteLine("增加成功");
                }
                else { Console.WriteLine("输入的单价和数量不可是负数"); }
            }
            else if (isAdd == false)
            {
                order.DelGood(name);
                Console.WriteLine("删除成功");
            }

        }

        //删除订单
        public void DeleteOrder(string s)
        {
            List<Orders> orders = SearchOrder(s);
            if (orders.Count != 0)
            {
                foreach (Orders order in orders)
                {
                    try
                    {
                        mOrders.Remove(order);
                        Console.WriteLine("您的其中一项订单已删除");
                    }
                    catch
                    {
                        Console.WriteLine("发生异常，修改失败");
                    }
                }
            }
        }

        public void DeleteOrder2(Orders order)
        {
            this.mOrders.Remove(order);
        }

        //查询总价钱在一定价钱上的订单
        public void SearchByMoney()
        {
            int i = 0;
            //使用Linq语句查找
            var mOrder = from o in mOrders where o.money >= 10000 select o;
            foreach (var o in mOrder)
            {
                i++;
            }
            if (i == 0)
            {
                Console.WriteLine("未找到该订单信息，请确认输入的信息是否正确");
            }
        }

        //将订单序列化为XML文件
        public FileInfo Export(string xmlfilename = "order.xml")
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(OrderService));
            FileStream fs = new FileStream(xmlfilename, FileMode.Create);
            xmlser.Serialize(fs,this);
            fs.Close();
            return new FileInfo (xmlfilename);
        }

        //从XML文件中载入订单
        public static OrderService Import(string xmlfilename)
        {
            XmlSerializer xmlser = new XmlSerializer(typeof(OrderService));
            try
            {
                StreamReader sr = new StreamReader(xmlfilename);
                OrderService service = (OrderService)xmlser.Deserialize(sr);
                sr.Close();
                return service;
            }
            catch
            {
                Console.WriteLine("文件信息导入失败");
                return null;
            }
           
        }
    }
}
