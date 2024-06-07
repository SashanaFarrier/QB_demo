﻿
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

        public async Task<JsonResult> GetItems()
        {
            var items = await ItemService.GetItems();
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