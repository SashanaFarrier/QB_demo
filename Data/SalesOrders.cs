using MvcCodeFlowClientManual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCodeFlowClientManual.Data
{
    public interface ISalesOrdersList
    {
        List<SalesOrder> GetItems();
        void AddItem(SalesOrder order);
    }

    public class SalesOrders: ISalesOrdersList
    {
        private static List<SalesOrder> orders = new List<SalesOrder>();
        public List<SalesOrder> GetItems()
        {
            return orders;
        }

        public void AddItem(SalesOrder newOrder)
        {
            orders.Add(newOrder);
        }
    }
}
