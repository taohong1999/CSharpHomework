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

namespace Program2
{
    public partial class Form2 : Form
    {


        Orders orders = new Orders();
        Form1 form1;
        public Form2(Form1 form)
        {
            InitializeComponent();

            this.label1.Text = "添加订单";
            this.button1.Text = "添加";
            this.label2.Text = "客户姓名 ";
            this.label3.Text = "订单号";
            bindingSource1.DataSource = orders;
            this.form1 = form;
        }

        public Form2(Form1 form, Orders order)
        {
            InitializeComponent();

            this.label1.Text = "修改订单";
            this.button1.Text = "确认修改";
            this.label2.Text = "客户姓名 ";
            this.label3.Text = "订单号";

            this.orders = order;
            this.textBox1.Text = order.theCustomer;
            this.textBox2.Text = order.orderNumber;
            bindingSource1.DataSource = orders;
            this.form1 = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str1 = textBox1.Text;
            string str2 = textBox2.Text;
            if (this.label1.Text == "添加订单")
            {
               
                if (str1 == "" || str2 == "")
                {
                    MessageBox.Show("信息未输入完整");
                }
                else
                {
                    orders.theCustomer = str1;
                    orders.orderNumber = str2;
                    orders.ReMoney();
                    form1.service.AddOrders2(orders);
                }
            }

            else if(this.label1.Text == "修改订单")
            {
                orders.theCustomer = str1;
                orders.orderNumber = str2;
                orders.ReMoney();
                //bindingSource1.DataSource = orders;               
            }
            form1.ReOrders();
            this.Close();
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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
