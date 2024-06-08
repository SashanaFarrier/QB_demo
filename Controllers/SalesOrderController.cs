
using MvcCodeFlowClientManual.Data;
using MvcCodeFlowClientManual.Models;
using MvcCodeFlowClientManual.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcCodeFlowClientManual.Controllers
{
    public class SalesOrderController : Controller
    {
        ItemService ItemService = new ItemService();
      
        private static List<Order> Orders = new List<Order>();
        private static List<Order> updatedOrderList = new List<Order>();
       
        [HttpPost]
        public async Task<ActionResult> CreateSalesOrder(Order item)
        {

            if (ModelState.IsValid)
            {
                var items = await ItemService.GetItems();
                var itemFound = items.First(i => i.Name == item.ItemName);

                if (itemFound != null)
                {

                    Order newItem = new Order
                    {
                        OrderId = Guid.NewGuid().ToString(),
                        TransactionDate = DateTime.Now,
                        ItemName = itemFound.Name,
                        ItemNumber = itemFound.ItemId,
                        Description = item.Description,
                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        Amount = item.Amount,
                        //Tax = "Tax"
                    };
                    Orders.Add(newItem);
                 
                    
                }
            }
            return View("~/Views/App/Index.cshtml", Orders);
            //return RedirectToAction("Index", "App");
        }
        public JsonResult GetItemOrderById(string id)
        {
            var item = Orders.Find(i => i.OrderId.ToString() == id);
            
                return Json(item, JsonRequestBehavior.AllowGet);
            
        }

        //Displays popup
        public ActionResult EditOrder(string id)
        {
            if(id == null)
            {
                return PartialView("~/Views/Shared/_AddItemModal.cshtml");
            }

            var item = Orders.Find(x => x.OrderId == id);

            if(item == null)
            {
                return PartialView("~/Views/Shared/_AddItemModal.cshtml");
            }

            return PartialView("~/Views/Shared/_AddItemModal.cshtml", item);
        }

        [HttpPost]
        public ActionResult UpdateOrder(Order item)
        {
            updatedOrderList.Clear();

            if (ModelState.IsValid)
            {
                var prevOrder = Orders.Find(i => i.OrderId == item.OrderId);
                if (prevOrder == null)
                {
                    return RedirectToAction("Index", "App");
                    //return View("~/Views/App/Index.cshtml");
                }

                Order updatedOrder = new Order();
                updatedOrder.OrderId = prevOrder.OrderId;
                updatedOrder.TransactionDate = prevOrder.TransactionDate;
                updatedOrder.ItemName = item.ItemName;
                updatedOrder.ItemNumber = item.ItemNumber;
                updatedOrder.Description = item.Description;
                updatedOrder.Quantity = item.Quantity;
                updatedOrder.Rate = item.Rate;
                updatedOrder.Amount = item.Amount;


                updatedOrderList.Add(updatedOrder);

                foreach (var order in Orders)
                {
                    if (order.OrderId != prevOrder.OrderId)
                    {
                        updatedOrderList.Add(order);
                    }
                }

                 return View("~/Views/App/Index.cshtml", updatedOrderList);

            }

            //Orders = updatedOrderList;
            //return View("~/Views/App/Index.cshtml", updatedOrderList);
            return RedirectToAction("Index", "App");
        }

        [HttpPost]
        public JsonResult DeleteItem(string id)
        {
            var item = Orders.Find(i => i.OrderId == id);
            if (item == null)
            {
                return Json(new { message = "Order not found" }, JsonRequestBehavior.AllowGet);
            }

            foreach (var order in Orders)
            {
                if (order.OrderId != item.OrderId)
                {
                    updatedOrderList.Add(order);
                }
            }
            return Json(new { data = updatedOrderList, deleteItem = item.OrderId });
        }
    }
}