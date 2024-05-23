using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Models
{
    public class Item
    {
        public string ItemId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}