
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
        CreateSalesOrderService createSalesOrderService = new CreateSalesOrderService();
        ItemService ItemService = new ItemService();
      
        private static List<Order> Orders = new List<Order>();
        private static List<Order> updatedOrderList = new List<Order>();
        //public ActionResult Index()
        //{


        //    return View("~/Views/App/Index.cshtml", Orders);
        //}

        //public JsonResult GetSalesOrders()
        //{
        //    return Json(Orders, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public async Task<ActionResult> CreateSalesOrder(Order item)
        {
            //var matchItem = ItemService.GetItems().Equals(item.ItemName);
            if (ModelState.IsValid)
            {
                var items = await ItemService.GetItems();
                var itemFound = items.First(i => i.Name.ToLower() == item.ItemName.ToLower());

                if (itemFound != null)
                {

                    Order newItem = new Order
                    {
                        OrderId = Guid.NewGuid(),
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

        
        public ActionResult EditOrder(string id)
        {
            if(id == null)
            {
                return PartialView("~/Views/Shared/_AddItemModal.cshtml");
            }

            var item = Orders.Find(x => x.OrderId.ToString() == id);

            if(item == null)
            {
                return PartialView("~/Views/Shared/_AddItemModal.cshtml");
            }

            return PartialView("~/Views/Shared/_AddItemModal.cshtml", item);
        }

        [HttpPost]
        public ActionResult UpdateOrder(Order item)
        {
            var prevOrder = Orders.Find(i => i.OrderId == item.OrderId);
            
            if(prevOrder == null) 
            {
               return View("~/Views/App/Index.cshtml");
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
            //Orders = updatedOrderList;
            return View("~/Views/App/Index.cshtml", updatedOrderList);

        }
    }
}