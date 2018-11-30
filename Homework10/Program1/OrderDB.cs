using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program1
{
    public class OrderDB : DbContext
    {
        public OrderDB() : base("OrderDataBase") { }

        public DbSet<Orders> Order { get; set; }
        public DbSet<OrderDetails> Details { get; set; }
    }
}
