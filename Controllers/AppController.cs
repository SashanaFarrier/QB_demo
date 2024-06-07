using System;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Collections.Generic;
using MvcCodeFlowClientManual.Models;
using QBFC15Lib;
using MvcCodeFlowClientManual.Config;
using MvcCodeFlowClientManual.Data;


namespace MvcCodeFlowClientManual.Controllers
{
    public class AppController : Controller
    {

        public IList<Customer> customers = new List<Customer>();

        public QBConnection qBConnection = new QBConnection();

        private QBSessionManager sessionManager;
        
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
            return View();
        }
        public ActionResult Error()
        {
            return View("Error");
        }

        [HttpPost]
        public ActionResult ClearOrders()
        {
            List<Order> orders = new List<Order>();
            return View("Index", orders);
        }

    }

}