using MvcCodeFlowClientManual.Models;
using MvcCodeFlowClientManual.Services;
using MvcCodeFlowClientManual.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcCodeFlowClientManual.Controllers
{

    public class ItemController : Controller
    {
        ItemService ItemService = new ItemService();
        //ItemsViewModel ItemsViewModel { get; set; }
        public ActionResult Index()
        {
            //var items = GetItems();
            return View();
        }
        
        public Item GetItemByName(string name)
        {
            return null;
        }
        public Item GetItemById(string id)
        {
            return null;
        }
        public IList<Item> GetItems()
        {
            var items = ItemService.GetItems();
            ItemsViewModel viewModel = new ItemsViewModel();
            //foreach (var item in items)
            //{
            //    viewModel.Items.Add(item);
            //}
            return (items); 
        }

        [HttpPost]
        public string CreateItem(Item item)
        {
            return null;
        }

        [HttpPut]
        public string UpdateItem(Item item)
        {
            return null;
        }

        [HttpDelete]
        public string DeleteItem(Item item)
        {
            return null;
        }
    }
}