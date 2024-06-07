using MvcCodeFlowClientManual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Data
{
    public class NewOrders
    {
        public List<Order> Orders { get; set; }

        public NewOrders()
        {
            Orders = new List<Order>();
        }
        public NewOrders(Order order)
        {
            Orders = new List<Order>();
            Orders.Add(order);
            //this.Orders = orders;
        }
        public List<Order> getOrders()
        {
            return Orders;
        }
    }
}