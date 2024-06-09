
using MvcCodeFlowClientManual.Data;
using MvcCodeFlowClientManual.Models;
using MvcCodeFlowClientManual.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcCodeFlowClientManual.Controllers
{
    public class SalesOrderController : Controller
    {
        private readonly ISalesOrdersList _salesOrders;
        ItemService ItemService = new ItemService();
        //private static List<SalesOrder> Orders = new List<SalesOrder>();

        public SalesOrderController(ISalesOrdersList salesOrders)
        {
            _salesOrders = salesOrders;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSalesOrder(SalesOrder item)
        {

            if (ModelState.IsValid)
            {
                var items = await ItemService.GetItems();
                var itemFound = items.First(i => i.Name == item.ItemName);

                if (itemFound != null)
                {
                    SalesOrder newItem = new SalesOrder
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
                    
                    //Orders.Add(newItem);
                    _salesOrders.AddOrder(newItem);
                   
                  
                }
            }
            //return View("~/Views/App/Index.cshtml", Orders);
            return RedirectToAction("Index", "App");
        }
        public JsonResult GetItemOrderById(string id)
        {
            //var item = Orders.Find(i => i.OrderId.ToString() == id);
            var item = _salesOrders.GetOrders().Find(x => x.OrderId == id);
                return Json(item, JsonRequestBehavior.AllowGet);
            
        }

        //Displays popup
        public ActionResult EditOrder(string id)
        {
            if(id == null)
            {
                return PartialView("~/Views/Shared/_AddItemModal.cshtml");
            }

            //var item = Orders.Find(x => x.OrderId == id);
            var item = _salesOrders.GetOrders().Find(x => x.OrderId == id);

            if(item == null)
            {
                return PartialView("~/Views/Shared/_AddItemModal.cshtml");
            }

            return PartialView("~/Views/Shared/_AddItemModal.cshtml", item);
        }

        [HttpPost]
        public ActionResult UpdateOrder(SalesOrder item)
        {

            if (ModelState.IsValid)
            {
                //var prevOrder = Orders.Find(i => i.OrderId == item.OrderId);
                var prevOrder = _salesOrders.GetOrders().Find(x => x.OrderId == item.OrderId);
                if (prevOrder == null)
                {
                    return null;
                    //return RedirectToAction("Index", "App");
                    //return View("~/Views/App/Index.cshtml");
                }

                SalesOrder updatedOrder = new SalesOrder();
                updatedOrder.OrderId = prevOrder.OrderId;
                updatedOrder.TransactionDate = prevOrder.TransactionDate;
                updatedOrder.ItemName = item.ItemName;
                updatedOrder.ItemNumber = item.ItemNumber;
                updatedOrder.Description = item.Description;
                updatedOrder.Quantity = item.Quantity;
                updatedOrder.Rate = item.Rate;
                updatedOrder.Amount = item.Amount;

                //Orders.Remove(prevOrder);
                //Orders.Add(updatedOrder);

                _salesOrders.GetOrders().Remove(prevOrder);
                _salesOrders.AddOrder(updatedOrder);
                return RedirectToAction("Index", "App");
            }

            //return View("~/Views/App/Index.cshtml", updatedOrderList);
            return RedirectToAction("Index", "App");
        }

        [HttpPost]
        public JsonResult DeleteItem(string id)
        {
            //var item = Orders.Find(i => i.OrderId == id);
            var item = _salesOrders.GetOrders().Find(x => x.OrderId == id);
            if (item == null)
            {
                return Json(new { message = "Order not found" }, JsonRequestBehavior.AllowGet);
            }
            //Orders.Remove(item);
            _salesOrders.GetOrders().Remove(item);
            return Json(new { data = _salesOrders.GetOrders(), deleteItem = item.OrderId });
        }

    }

}