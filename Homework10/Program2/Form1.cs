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

                List<OrderDetails> details = new List<OrderDetails>() {
                new OrderDetails("apple2", 10.0, 20),
                new OrderDetails("egg3", 2.0, 100)
                };
                Orders order = new Orders("20181129001", "jack", "13811111111", details);
                order.ReMoney();

                service.AddOrders2(order);
                List<Orders> orders = service.mOrders;
                List<Orders> orders2 = service.mOrders;

            }
        }

        //查询订单
        private void button1_Click_1(object sender, EventArgs e)
        {
            //bindingSerives.DataSource = service;
            mservice.mOrders.Clear();
            string str1 = textBox1.Text;
            string str2 = comboBox1.Text;
            if (str2 == null || str1 == null)
            {
                MessageBox.Show("信息未输入");
                return;
            }
            else
            {
                switch (comboBox1.SelectedIndex)
                {

                    case 1:
                        bindingSerives.DataSource =
                          service.SearchOrder(textBox1.Text);
                        break;
                    case 2:
                        bindingSerives.DataSource =
                          service.SearchOrder(textBox1.Text);
                        break;
                    default:
                        bindingSerives.DataSource =
                          service.mOrders;
                        break;
                }
                bindingSerives.ResetBindings(false);
                bindingOrders.ResetBindings(false);
            }
        }

        //查看订单明细
        private void button2_Click(object sender, EventArgs e)
        {
            Orders orders = new Orders();
            string str1 = label2.Text;
            string str2 = label3.Text;
            List<Orders> morders = service.mOrders;
            for (int i = 0; i < morders.Count; i++)
            {
                if (morders[i].orderNumber == str1 && morders[i].theCustomer == str2)
                {
                    orders = morders[i];
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
            using (var db = new OrderDB())
            {
                if (bindingSerives.Current != null)
                {
                    Orders order = (Orders)bindingSerives.Current;
                    service.DeleteOrder(order.orderNumber,order.theCustomer);
                }
            }
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
