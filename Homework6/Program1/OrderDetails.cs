using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1
{
    //订单中的商品信息
    public class OrderDetails
    {
        public string goodName;
        public double goodPrice;
        public int goodNumber;
        public OrderDetails()
        {
        }
        public OrderDetails(string goodname, double goodprice, int number)
        {
            this.goodName = goodname;
            this.goodPrice = goodprice;
            this.goodNumber = number;
        }
    }
}
