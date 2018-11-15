using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Program1;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Program2
{
    public partial class Form2 : Form
    {
        string ordernumber = "";
        Orders orders = new Orders();
        Form1 form1;
        public Form2(Form1 form)
        {
            InitializeComponent();

            this.form1 = form;
            CreateNumber();
            this.label1.Text = "添加订单";
            this.button1.Text = "添加";
            this.label2.Text = "客户姓名 ";
            this.label3.Text = "订单号";
            this.label6.Text = "电话号码";
            this.textBox2.Text = ordernumber;
            bindingSource1.DataSource = orders;

        }

        public Form2(Form1 form, Orders order)
        {
            InitializeComponent();

            this.label1.Text = "修改订单";
            this.button1.Text = "确认修改";
            this.label2.Text = "客户姓名 ";
            this.label3.Text = "订单号";
            this.label6.Text = "电话号码";

            this.orders = order;
            this.textBox1.Text = order.theCustomer;
            this.textBox2.Text = order.orderNumber;
            this.textBox3.Text = order.phoneNumber;
            bindingSource1.DataSource = orders;
            this.form1 = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str1 = textBox1.Text;
            string str2 = textBox2.Text;
            string str3 = textBox3.Text;
            bool check1 = CheckPhone(str3);
            
            if (this.label1.Text == "添加订单")
            {
                bool check2 = CheckOrder(str2);
                if (str1 == "" || str2 == "")
                {
                    MessageBox.Show("信息未输入完整");
                }
                else if (check1 == false)
                {
                    MessageBox.Show("输入的号码格式有误");
                }
                else if (check2 == false)
                {
                    MessageBox.Show("订单号格式出错");
                }
                else
                {
                    orders.theCustomer = str1;
                    orders.orderNumber = str2;
                    orders.phoneNumber = str3;
                    orders.ReMoney();
                    form1.service.AddOrders2(orders);
                    this.Close();
                }
            }

            else if (this.label1.Text == "修改订单")
            {
                bool check2 = CheckOrder(str2,orders);
                if (check1 == false)
                {
                    MessageBox.Show("输入的号码格式有误");
                }
                else if (check2 == false)
                {
                    MessageBox.Show("订单号格式出错");
                }
                else
                {
                    orders.theCustomer = str1;
                    orders.orderNumber = str2;
                    orders.phoneNumber = str3;
                    orders.ReMoney();
                    this.Close();
                }
                //bindingSource1.DataSource = orders; 

            }
            form1.ReOrders();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OrderDetails details = null;
            string str = label5.Text;
            for (int i = 0; i < orders.mOrderDetails.Count; i++)
            {
                if (orders.mOrderDetails[i].goodName == str)
                {
                    details = orders.mOrderDetails[i];
                    break;
                }
            }
            orders.mOrderDetails.Remove(details);
            BindingSource bs = new BindingSource();
            bs.DataSource = orders;
            bindingSource1.DataSource = bs;
        }

        //自动生成订单号
        private void CreateNumber()
        {
            int n1 = form1.service.mOrders.Count;
            string s1 = form1.service.mOrders[n1 - 1].orderNumber;
            string s2 = s1.Substring(0, 8);
            string s3 = s1.Substring(8, 3);
            int n2 = Int32.Parse(s3);

            DateTime datenow = DateTime.Now;
            string year = datenow.Year.ToString();
            string month = datenow.Month.ToString();
            string day = datenow.Day.ToString();
            if (month.Length < 2) month = '0' + month;
            if (day.Length < 2) day = '0' + day;
            string ymd = year + month + day;

            if (ymd == s2)
            {
                n2++;
                if (n2 < 10) s3 = "00" + n2.ToString();
                else if (n2 < 100) s3 = '0' + n2.ToString();
                else s3 = n2.ToString();
                ordernumber = ymd + s3;
            }

            if (ymd != s2)
            {
                ordernumber = ymd + "001";
            }

        }
        //检验电话号码的格式是否正确
        private bool CheckPhone(string str)
        {
            string number = "1[0-9]{2}[0-9]{4}[0-9]{4}";
            Regex rx = new Regex(number);
            Match m = rx.Match(str);
            if (m.Success) return true;
            else return false;
        }

        //检验订单号
        public bool CheckOrder(string str,Orders order = null)
        {
            string number = "20[0-9]{2}0[1-9][0-2][0-9][0-9]{3}";
            Regex rx1 = new Regex(number);
            string number2 = "20[0-9]{2}1[0-2][0-2][0-9][0-9]{3}";
            Regex rx2 = new Regex(number2);
            string number3 = "20[0-9]{2}0[1-9]3[0-1][0-9]{3}";
            Regex rx3 = new Regex(number3);
            string number4 = "20[0-9]{2}1[0-2]3[0-1][0-9]{3}";
            Regex rx4 = new Regex(number4);

            Match m1 = rx1.Match(str);
            Match m2 = rx2.Match(str);
            Match m3 = rx3.Match(str);
            Match m4 = rx4.Match(str);
            if (!(m1.Success || m2.Success || m3.Success || m4.Success)) return false;
            else
            {
                foreach (Orders o in form1.service.mOrders)
                {
                    if (o == order) continue;
                    if (str == o.orderNumber) return false;
                }
            }
            return true;
        }

        //退出当前界面
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //无用但无法删除
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }


    }
}
