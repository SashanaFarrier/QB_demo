using MvcCodeFlowClientManual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.ViewModels
{
    public class ItemsViewModel
    {

            public ItemsViewModel()
            {
                Items = new List<Item>(); 
            }

            public List<Item> Items { get; set; }
      

        //public List<Item> ItemDictionary { get; set; }
    }
}