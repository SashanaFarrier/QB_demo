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
        public object SubItem { get; set; }
        public List<object> SubItems { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string Tax { get; set; }
        public Item(string id, string name, string desc, List<object> items)
        {
            
            this.ItemId = id;
            this.Name = name;
            this.SubItems = items;
            this.Description = desc;
            //this.Quantity = quantity;
        }

        public Item()
        {
            
        }
    }
}