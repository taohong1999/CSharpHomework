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
using System.IO;

namespace Program2
{
    public partial class Form1 : Form
    {
        internal OrderService service = new OrderService();
        private OrderService mservice = new OrderService();  //用于储存符合条件的order
        public Form1()
        {
            InitializeComponent();

            AddService();
            bindingSerives.DataSource = service;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddService()
        {
            if (File.Exists(@"orders.xml"))
            {
                service = OrderService.Import(@"orders.xml");
            }
            else
            {
                Orders order1 = new Orders("20181115001","Tom","13811111111");
                order1.AddGood("Computer", 5000, 1);
                Orders order2 = new Orders("20181115002","Jack","15822222222");
                order2.AddGood("HUAWEI honor10", 3000, 1);
                Orders order3 = new Orders("20181115003","Fremont","18900000000");
                order3.AddGood("iWatch", 2000, 1);
                service.AddOrders2(order1);
                service.AddOrders2(order2);
                service.AddOrders2(order3);
            }
        }

        //查询订单
        private void button1_Click_1(object sender, EventArgs e)
        {
            //bindingSerives.DataSource = service;
            mservice.mOrders.Clear();
            Orders order;
            string str1 = textBox1.Text;
            string str2 = comboBox1.Text;
            bool isSeach = false;
            if (str2 == null || str1 == null)
            {
                MessageBox.Show("信息未输入");
                return;
            }
            else if (str2 == "订单号")
            {
                for (int i = 0; i < service.mOrders.Count; i++)
                {
                    order = service.mOrders[i];
                    if (order.orderNumber == str1)
                    {
                        mservice.AddOrders2(order);
                        isSeach = true;
                    }
                }
                if (isSeach == false) MessageBox.Show("未找到订单");
            }

            else if (str2 == "姓名")
            {
                for (int i = 0; i < service.mOrders.Count; i++)
                {
                    order = service.mOrders[i];
                    if (order.theCustomer == str1)
                    {
                        mservice.AddOrders2(order);
                        isSeach = true;
                    }
                }
                if (isSeach == false) MessageBox.Show("未找到订单");
            }
            else MessageBox.Show("未找到订单");
            bindingSerives.DataSource = mservice;
        }

        //查看订单明细
        private void button2_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            string str1 = label2.Text;
            string str2 = label3.Text;
            for (int i = 0; i < service.mOrders.Count; i++)
            {
                if (service.mOrders[i].orderNumber == str1 && service.mOrders[i].theCustomer == str2)
                {
                    orders = service.mOrders[i];
                    break;
                }
            }
            bindingOrders.DataSource = orders;
        }

        //添加订单
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(this);
            form2.Show();
        }

        //删除订单
        private void button4_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            string str1 = label2.Text;
            string str2 = label3.Text;
            for (int i = 0; i < service.mOrders.Count; i++)
            {
                if (service.mOrders[i].orderNumber == str1 && service.mOrders[i].theCustomer == str2)
                {
                    orders = service.mOrders[i];
                    break;
                }
            }
            service.DeleteOrder2(orders);
            BindingSource bs = new BindingSource();
            bs.DataSource = service;
            bindingSerives.DataSource = bs;
        }

        //修改订单
        private void button5_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            string str1 = label2.Text;
            string str2 = label3.Text;
            for (int i = 0; i < service.mOrders.Count; i++)
            {
                if (service.mOrders[i].orderNumber == str1 && service.mOrders[i].theCustomer == str2)
                {
                    orders = service.mOrders[i];
                    break;
                }
            }
            Form2 form2 = new Form2(this, orders);
            form2.Show();
        }

        //将订单信息保存到XML文件中
        private void button6_Click(object sender, EventArgs e)
        {
            service.Export();
            service.ExportHtml();
            MessageBox.Show("订单已保存成功");
        }

        public void ReOrders()
        {
            BindingSource bs = new BindingSource();
            bs.DataSource = service;
            bindingSerives.DataSource = bs;
        }


        //多余无用但无法删除
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void bindingSerives_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void bindingOrders_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

 
    }
}
