﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1
{
    //订单的基本信息
    [Serializable]
    public class Orders
    {
        public string orderNumber { set; get; }
        public string theCustomer { set; get; }
        public string phoneNumber { set; get; }
        private double myMoney = 0;
        public List<OrderDetails> mOrderDetails = new List<OrderDetails>();
        public double money
        {
            set
            {                
            }
            get {return myMoney; }

        }
        public List<OrderDetails> GetDetails
        {
            set { mOrderDetails = value; }
            get { return mOrderDetails; }
        }
        public Orders()
        {
        }
        public Orders(string ordernumber, string thecustomer,string phonenumber)
        {
            this.orderNumber = ordernumber;
            this.theCustomer = thecustomer;
            this.phoneNumber = phonenumber;
        }

        public void AddGood(string name, double price, int number)
        {
            OrderDetails orderdetails = new OrderDetails(name, price, number);
            mOrderDetails.Add(orderdetails);
            myMoney += price * number;
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
            else
            {
                this.money -= Detail.goodPrice;
                mOrderDetails.Remove(Detail);
            }
        }

        //非直接添加时刷新订单总额
        public void ReMoney()
        {
            if (this.mOrderDetails.Count == 0)
            {
                myMoney = 0;
            }
            else
            {
                for (int i = 0; i < mOrderDetails.Count; i++)
                {
                    myMoney += (this.mOrderDetails[i].goodPrice * mOrderDetails[i].goodNumber);
                }
            }
            money = myMoney;
        }
    }
}
