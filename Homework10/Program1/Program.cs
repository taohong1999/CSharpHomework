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
            OrderDetails detail1 = new OrderDetails("iphone", 5000, 1);
            OrderDetails detail2 = new OrderDetails("ipad", 10000, 1);
            List<OrderDetails> details = new List<OrderDetails> { detail1, detail2 };
            Orders order1 = new Orders("20181129001", "Jack", "13811111111", details);
            Orders order2 = new Orders("20181129001", "Tom", "13811111111", details);

            orderService.AddOrders2(order1);

            orderService.ChangeOrder(order2);


            List<Orders> orders = orderService.SearchOrder("Tom");
            
            orders.ForEach(o => Console.WriteLine($"{o.orderNumber},{o.theCustomer}"));


        }
    }
}



