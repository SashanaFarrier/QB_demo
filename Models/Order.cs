using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string CustomerJob {  get; set; }
        public string ItemName { get; set; }
        public string ItemNumber { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
       // public double SubTotal { get; set; }
        public string Tax { get; set; }
       // public double Total { get; set; }
    }

}