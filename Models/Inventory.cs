using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Models
{
    public class Inventory
    {
        public string ItemId { get; set; }
        public string Name { get; set; }
        public List<object> SubItems { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }

        public Inventory(string name, string desc, int quantity, List<object> items)
        {

            //this.ItemId = id;
            this.Name = name;
            this.SubItems = items;
            this.Description = desc;
            this.Quantity = quantity;
        }
    }
}