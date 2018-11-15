using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Program1
{
    [Serializable]
    public class OrderService
    {
        public List<Orders> mOrders = new List<Orders>();

        public List<Orders> Getorders
        {
            set { mOrders = value; }
            get { return mOrders; }
        }

        //添加订单
        public void AddOrder(string s1, string s2,string s3, int num)
        {
            //将订单添加到List里面
            Orders order = new Orders(s1, s2,s3);
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
        public void ChangeOrder(Orders order, string s1, string s2,string s3)
        {
            try
            {
                order.theCustomer = s1;
                order.orderNumber = s2;
                order.phoneNumber = s3;
                Console.WriteLine("您的订单信息已修改");
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

        //输出HTML文件
        public void ExportHtml()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@".\order.xml");

                XPathNavigator nav = doc.CreateNavigator();
                nav.MoveToRoot();

                XslCompiledTransform xt = new XslCompiledTransform();
                xt.Load(@"..\..\x.xslt");

                FileStream outfile = File.OpenWrite("order.html");

                XmlTextWriter writer = new XmlTextWriter(outfile,System.Text.Encoding.UTF8);

                xt.Transform(nav, null, writer);
                outfile.Close();
            }
            catch(XmlException e)
            {
                Console.WriteLine("XML Exception:" + e.ToString());
            }
            catch(XsltException e)
            {
                Console.WriteLine("Xslt Exception:" + e.ToString());
            }
        }

        //检验所有订单号是否符合格式
        public bool CheckOrder()
        {
            string number = "20[0-9]{2}0[1-9][0-2][0-9][0-9]{3}";
            Regex rx1 = new Regex(number);
            string number2 = "20[0-9]{2}1[0-2][0-2][0-9][0-9]{3}";
            Regex rx2 = new Regex(number2);
            string number3 = "20[0-9]{2}0[1-9]3[0-1][0-9]{3}";
            Regex rx3 = new Regex(number3);
            string number4 = "20[0-9]{2}1[0-2]3[0-1][0-9]{3}";
            Regex rx4 = new Regex(number4);

            foreach (Orders order in mOrders)
            {
                Match m1 = rx1.Match(order.orderNumber);
                Match m2 = rx2.Match(order.orderNumber);
                Match m3 = rx3.Match(order.orderNumber);
                Match m4 = rx4.Match(order.orderNumber);
                if (!(m1.Success || m2.Success || m3.Success || m4.Success)) return false;
            }

            return true;
        }

        //检验所有电话号码格式是否正确
        public bool CheckPhone()
        {
            string number = "1[0-9]{2}[0-9]{4}[0-9]{4}";
            Regex rx = new Regex(number);
            foreach (Orders order in mOrders)
            {
                Match m = rx.Match(order.phoneNumber);
                if (!m.Success) return false;
            }
            return true;
        }
    }
}
