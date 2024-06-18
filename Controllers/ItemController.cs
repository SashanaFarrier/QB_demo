
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddItem(Item newItem)
        {
            Item item = new Item();
            item.ItemId = newItem.ItemId;
            item.Name = newItem.Name;
            item.Description = newItem.Description;
            item.Category = newItem.Category;
            item.Quantity = newItem.Quantity;
            item.Tax = newItem.Tax;
            return RedirectToAction("Index", "App");
        }

        public Item GetItemById(string id)
        {
            return null;
        }

        public JsonResult GetItems()
        {
            var items =  ItemService.GetItems();
            return Json(items, JsonRequestBehavior.AllowGet);
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