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
                return null;
            }

            //if(_salesOrderItems.SalesOrderId == null)
            //{
            //    _salesOrderItems.SalesOrderId = Guid.NewGuid().ToString();
            //}
            
            //Items.Add(newItem);

            _salesOrderItems.ItemList.Add(newItem);

            return RedirectToAction("Index", "App");
        }

        [HttpGet]
        public ActionResult UpdateItem(string id)
        {
            //List<Item> items = _salesOrderItems.ItemDictionary.Values.SelectMany(itemList =>  itemList).ToList();
                
               //Item item = items.Find(x => x.Name == id);
            return PartialView("~/Views/Shared/_UpdateItemModal.cshtml");
        }

        [HttpPost]
        public ActionResult UpdateItem(Item item) 
        {
            //List<Item> items = _salesOrderItems.ItemDictionary[ItemCategoryHelper.ParseEnum<ItemCategory>(item.Category)];

            //Item originalItem = items.Find(x => x.Category == item.Category);

            //if (originalItem != null)
            //{
            //    if(item.Quantity == 0)
            //    {
            //        items.Remove(originalItem);
            //    } 
            //    else 
            //    {
            //        if(item.Quantity != originalItem.Quantity)
            //        {
            //            originalItem.Quantity = item.Quantity;
            //        }

            //        if(item.Cost != originalItem.Cost)
            //        {
            //            originalItem.Cost = item.Cost;
            //        }
 
            //    } 

                //originalItem.Quantity = item.Quantity;

           // }
            
            //Item originalItem = 
            
            return RedirectToAction("~/Views/Shared/_UpdateItemModal");
        }

        [HttpDelete]
        public ActionResult DeleteItem(int id)
        {
            return RedirectToAction("Index", "App");
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