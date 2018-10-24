using Microsoft.VisualStudio.TestTools.UnitTesting;
using Program1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        [TestMethod()]
        public void AddOrderTest()
        {
            OrderService service = new OrderService();
            List<Orders> orders = new List<Orders>();
            service.AddOrder("123456","Tom",0);
            orders = service.mOrders;
            Assert.IsTrue(orders.Count != 0);
        }

        [TestMethod()]
        public void SearchOrderTest()
        {
            Orders order = new Orders("123456", "Tom");
            List<Orders> orders = new List<Orders>();
            OrderService service = new OrderService();
            service.mOrders.Add(order);
            orders = service.SearchOrder("Tom");
            Assert.IsTrue(orders.Count != 0);
        }

        [TestMethod()]
        public void ChangeOrderTest()
        {
            Orders order = new Orders("123456", "Tom");
            List<Orders> orders = new List<Orders>();
            OrderService service = new OrderService();
            service.mOrders.Add(order);
            orders = service.SearchOrder("Tom");
            foreach (Orders o in orders)
            {
                service.ChangeOrder(o, "123456", "Jack");
            }
            orders = service.SearchOrder("Tom");
            Assert.IsFalse(orders == null);
        }

        [TestMethod()]
        public void ChangeGoodsTest()
        {
            try
            {
                Orders order = new Orders("123456", "Tom");
                List<Orders> orders = new List<Orders>();
                OrderService service = new OrderService();
                OrderDetails details = new OrderDetails("TestThing", 10000, 1);
                service.mOrders.Add(order);
                foreach (Orders o in service.mOrders)
                {
                    service.ChangeGoods(o, false, "TestThing");
                    service.ChangeGoods(o, true, "TestThing2", 1000, 1);
                }
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Assert.IsTrue(false);
            }
        }

        [TestMethod()]
        public void DeleteOrderTest()
        {
            Orders order = new Orders("123456", "Tom");
            List<Orders> orders = new List<Orders>();
            OrderService service = new OrderService();
            service.mOrders.Add(order);
            service.DeleteOrder("Tom");
            orders = service.SearchOrder("Tom");
            Assert.IsNotNull(orders);
        }

        [TestMethod()]
        public void SearchByMoneyTest()
        {
            try
            {
                Orders order = new Orders("123456", "Tom");
                order.money = 15000;
                OrderService service = new OrderService();
                service.mOrders.Add(order);
                service.SearchByMoney();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ExportTest()
        {
            Orders order = new Orders("123456","Tom");
            OrderService service = new OrderService();
            service.mOrders.Add(order);
            FileInfo fs = service.Export();
            Assert.IsTrue(fs.Exists);
        }

        [TestMethod()]
        public void ImportTest()
        {
            Orders order = new Orders("1234567","Tom");
            List<Orders> orders = new List<Orders>();
            OrderService service = new OrderService();
            service.mOrders.Add(order);
            FileInfo fs = service.Export("Test.xml");
            OrderService service2 = OrderService.Import("Test.xml");
            fs.Delete();
            orders = service2.SearchOrder("Tom");
            Assert.IsTrue(orders != null);
        }
    }
}