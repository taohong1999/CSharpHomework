using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program2
{
    //订单的基本信息
    class Orders
    {
        internal string orderNumber;
        internal string theCustomer;
        public List<OrderDetails> mOrderDetails = new List<OrderDetails>();
        public Orders(string ordernumber, string thecustomer)
        {
            this.orderNumber = ordernumber;
            this.theCustomer = thecustomer;
        }

        public void AddGood(string name, double price, int number)
        {
            OrderDetails orderdetails = new OrderDetails(name, price, number);
            mOrderDetails.Add(orderdetails);
        }

        public void DelGood(string name)
        {
            OrderDetails Detail = null;
            foreach (OrderDetails details in mOrderDetails)
            {
                if (details.goodName == name)
                {
                    Detail = details;
                }
            }
            if (Detail == null) { Console.WriteLine("未找到该商品"); }
            else { mOrderDetails.Remove(Detail); }
        }
    }
}
