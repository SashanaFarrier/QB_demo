using System;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Collections.Generic;
using MvcCodeFlowClientManual.Models;
using QBFC15Lib;


namespace MvcCodeFlowClientManual.Controllers
{
    public class AppController : Controller
    {
        //public static string appId = ConfigurationManager.AppSettings["appId"];
        //public static string appCertificate = ConfigurationManager.AppSettings["appCertificate"];
        //public static string companyFile = ConfigurationManager.AppSettings["companyFile"];

        public IList<Customers> customers = new List<Customers>();

        public static QBSessionManager sessionManager = new QBSessionManager();

        /// <summary>
        /// Use the Index page of App controller to get all endpoints from discovery url
        /// </summary>
        public ActionResult Index()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Session.Clear();
            Session.Abandon();
            Request.GetOwinContext().Authentication.SignOut("Cookies");
            return View();
        }

        /// <summary>
        /// Start Auth flow
        /// </summary>
        public ActionResult InitiateAuth(string submitButton)
        {
            switch (submitButton)
            {
                case "Connect to QuickBooks":
                ConnectToQuickBooks();
                return RedirectToAction("ApiCallService");
                default:
                    return (View());
            }
        }

        public ActionResult ApiCallService()
        {
            if (sessionManager != null)
            {
                try
                {
                   // sessionManager.QBXMLVersionsForSession.ToString();

                    IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);
                    
                    ICustomerQuery customerQuery = requestMsgSet.AppendCustomerQueryRq();

                    IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

                    IResponseList responseList = responseMsgSet.ResponseList;

                    IResponse response = responseList.GetAt(0);

                    if(response.StatusCode == 0)
                    {
                        IResponseType responseType = response.Type;

                        ICustomerRetList customertList = (ICustomerRetList)response.Detail;

                        for (int i = 0; i < customertList.Count; i++)
                        {
                            ICustomerRet customerRet = customertList.GetAt(i);
                            Customers customer = new Customers();
                            customer.FullName = customerRet.FullName.GetValue();
                            customers.Add(customer);
                        }
                    }
                   
                       return View(customers);

                }
                catch (Exception ex)
                {
                    return View("ApiCallService", (object)("QB API call Failed!" + " Error message: " + ex.Message));
                }
            }
            else
            {
                 return View("ApiCallService", (object)"QB API call Failed!");
            }
        }

        public ActionResult Error()
        {
            return View("Error");
        }

        private void ConnectToQuickBooks()
        {   // Connect to QuickBooks Desktop
            sessionManager = new QBSessionManager();
            
            //Hardcoded QB application name on local system. Make changes based on the name of your application 
            sessionManager.OpenConnection("", "Test");
            sessionManager.BeginSession("", ENOpenMode.omDontCare);
        }
    }

}