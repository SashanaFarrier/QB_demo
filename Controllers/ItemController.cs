
using MvcCodeFlowClientManual.Models;
using MvcCodeFlowClientManual.Services;
using MvcCodeFlowClientManual.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace MvcCodeFlowClientManual.Controllers
{

    public class ItemController : Controller
    {
        ItemService ItemService = new ItemService();
        private static List<Item> Items = new List<Item>();
        public ActionResult Index()
        {
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
        //public ActionResult GetItems(string submitButton)
        //{
        //    var viewModel = new List<Item>();

        //    switch(submitButton)
        //    {
        //        case "Connect to QuickBooks":
        //            foreach(var i in ItemService.GetItems()) 
        //            {
        //                viewModel.Add(i);
        //            }
        //            //submitButton = "Close QuickBooks";
        //            break;
        //    }

        //    return View("Index",viewModel); 
        //}

        public ActionResult GetItems()
        {
            return Json(Items, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateItem(Item item)
        {
            if(ModelState.IsValid)
            {
                var itemFound = ItemService.GetItems().First(i => i.Name.ToLower() == item.Name.ToLower()) ?? null;
                if (itemFound != null)
                {
                    Item newItem = new Item
                    {
                        ItemId = itemFound.ItemId,
                        Name = item.Name,
                        Description = item.Description,
                    };
                    Items.Add(newItem);
                    
                }
            }
            return RedirectToAction("Index", "App");
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