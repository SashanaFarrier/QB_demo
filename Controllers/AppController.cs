using System;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Collections.Generic;
using MvcCodeFlowClientManual.Models;
using QBFC15Lib;
using MvcCodeFlowClientManual.Config;
using MvcCodeFlowClientManual.Data;
using System.Linq;
using MvcCodeFlowClientManual.Services;
using System.Threading.Tasks;



namespace MvcCodeFlowClientManual.Controllers
{
    public class AppController : Controller
    {
        ItemService ItemService = new ItemService();
        //private SalesOrder _salesOrderItems;
        static SalesOrder _salesOrderItems = new SalesOrder();
        public IList<Customer> customers = new List<Customer>();

        public QBConnection qBConnection = new QBConnection();
        public CreateSalesOrderService createSalesOrderService = new CreateSalesOrderService();

        private QBSessionManager sessionManager;

        //SalesOrder salesOrder  = new SalesOrder();
        //public AppController(SalesOrder salesorderitems)
        //{
        //    _salesOrderItems = salesorderitems;
        //}

      

        public IList<Customer> ApiCallService()
        {
            if (qBConnection.getSessionManager() != null)
            {
                try
                {

                    sessionManager = qBConnection.getSessionManager();

                    IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);

                    ICustomerQuery customerQuery = requestMsgSet.AppendCustomerQueryRq();

                    IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

                    IResponseList responseList = responseMsgSet.ResponseList;

                    IResponse response = responseList.GetAt(0);

                    if (response.StatusCode == 0)
                    {
                        IResponseType responseType = response.Type;

                        ICustomerRetList customertList = (ICustomerRetList)response.Detail;

                        for (int i = 0; i < customertList.Count; i++)
                        {
                            ICustomerRet customerRet = customertList.GetAt(i);
                            string customerName = customerRet.FullName.GetValue();

                            var customer = new Customer()
                            {
                                FullName = customerName
                            };

                            customers.Add(customer);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            return customers;
        }
        public ActionResult Index()
        {
            //List<Item> items = new List<Item>();
            
            if (_salesOrderItems != null)
            {
                //foreach(KeyValuePair<string, List<Item>> entry in _salesOrderItems.ItemDictionary)
                //{
                //    foreach(Item item in entry.Value)
                //    {
                //        items.Add(item);
                //    }
                //}
                //Dictionary<string, List<Item>> orders = 
                return View(_salesOrderItems);
            }


            return View();
           
        }

        [HttpPost]
        public ActionResult AddItem(Item newItem)
        {
            //ItemCategory itemCategory = ItemCategoryHelper.ParseEnum<ItemCategory>(newItem.Category);

            //List<Item> Items = _salesOrderItems.ItemDictionary.ContainsKey(itemCategory) ? _salesOrderItems.ItemDictionary[itemCategory] : new List<Item>();

            if (!ModelState.IsValid)
            {
                //return a message to user
                return RedirectToAction("Index", "App");
            }

            //if(_salesOrderItems.SalesOrderId == null)
            //{
            //    _salesOrderItems.SalesOrderId = Guid.NewGuid().ToString();
            //}

            //Items.Add(newItem);

            //string id = ItemService.GetItems().Values.SelectMany(item => item).ToList().Find(x => x.Name == newItem.Name).ItemId;
            //newItem.ItemId = id;

            _salesOrderItems.ItemList.Add(newItem);

            return RedirectToAction("Index", "App");
        }

        [HttpGet]
        public JsonResult UpdateItem(string id)
        {
            Item item = _salesOrderItems.ItemList.Find(x => x.ItemId == id);
           // List<Item> items = _salesOrderItems.ItemDictionary.Values.SelectMany(itemList =>  itemList).ToList();
                
               //Item item = items.Find(x => x.Name == id);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateItem(Item item) 
{
            Item updatedItem = _salesOrderItems.ItemList.Find(x =>  x.Name == item.Name);

            if (updatedItem != null)
            {
                if (item.Quantity == 0)
                {
                    _salesOrderItems.ItemList.Remove(updatedItem);
                }
                else
                {
                    if (item.Quantity != updatedItem.Quantity)
                    {
                        updatedItem.Quantity = item.Quantity;
                    }

                    if (item.Amount != updatedItem.Amount)
                    {
                        updatedItem.Amount = item.Amount;
                    }

                    if(item.Description != updatedItem.Description)
                    {
                        updatedItem.Description = item.Description;
                    }

                }

            }

            return RedirectToAction("Index", "App");
        }

        [HttpPost]
        public JsonResult DeleteItem(string id)
        {
            //var item = Orders.Find(i => i.OrderId == id);
            var item = _salesOrderItems.ItemList.Find(x => x.ItemId == id);
            if (item == null)
            {
                return Json(new { message = "Item not found" }, JsonRequestBehavior.AllowGet);
            }
            //Orders.Remove(item);
            _salesOrderItems.ItemList.Remove(item);
            return Json(new { data = _salesOrderItems.ItemList, deletedItem = item.ItemId });
        }

        [HttpPost]
        public async Task<ActionResult> Submit(SalesOrder salesOrder)
        {
            //salesOrder.ItemDictionary = _salesOrderItems.ItemDictionary;

            if (salesOrder != null && salesOrder.ItemList.Count > 0)
            {
                createSalesOrderService.CreateSalesOrder(salesOrder);
            }


            _salesOrderItems.ItemList = new List<Item>();

            return RedirectToAction("Index", "App");
        }

        public ActionResult Error()
        {
            return View("Error");
        }

    }

}