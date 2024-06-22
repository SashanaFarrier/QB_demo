using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Models
{
    public class Item
    {
        public string ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        //public object SubItem { get; set; }
        //public List<object> SubItems { get; set; }
        public string Description { get; set; }

        //[Required]
        public string Category { get; set; }
        public int Quantity { get; set; }

        [Required]
        public double Amount { get; set; }
        public double Rate { get; set; }
        public string Tax { get; set; }

        public Item(string id, string name, string desc)
        {
            
            this.ItemId = id;
            this.Name = name;
            //this.SubItems = items;
            this.Description = desc;
            //this.Quantity = quantity;
        }

        public Item()
        {
            
        }
    }
}