using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Models
{
    public class Order
    {
        public DateTime Date { get; set; }
        public string CustomerJob {  get; set; }
        public string ItemName { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public double Rate { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
    }

}