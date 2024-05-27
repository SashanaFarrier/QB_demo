using MvcCodeFlowClientManual.Config;
using MvcCodeFlowClientManual.Models;
using QBFC15Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Services
{
    public class CustomerJobService
    {
        public QBConnection qBConnection = new QBConnection();
        private QBSessionManager sessionManager;

        public IList<CustomerJob> CustomerJobs = new List<CustomerJob>();

        public IList<CustomerJob> GetCustomerJobs()
        {

            if (qBConnection.getSessionManager() != null)
            {
                try
                {
                    sessionManager = qBConnection.getSessionManager();

                    IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);

                    ISalesOrderQuery orderQuery = requestMsgSet.AppendSalesOrderQueryRq();

                    IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

                    IResponseList responseList = responseMsgSet.ResponseList;

                    IResponse response = responseList.GetAt(0);

                    if (response.StatusCode == 0)
                    {
                        IResponseType responseType = response.Type;

                        ISalesOrderRetList salesOrderList = (ISalesOrderRetList)response.Detail;

                        //To access the IORSalesOrderLineRet obj where desc, quantity and rate are stored.
                        //IORSalesOrderLineRetList oRSalesOrderLineRetList = salesOrderList.GetAt(0).ORSalesOrderLineRetList;

                        string prevCustomer = string.Empty;
                        List<string> locations = new List<string>();
                        Dictionary<string, List<string>> finalLocation = new Dictionary<string, List<string>>();
                        CustomerJob Job = null;
                        for (int i = 0; i < salesOrderList.Count; i++)
                        {
                            ISalesOrderRet salesOrderRet = salesOrderList.GetAt(i);

                            IQBBaseRef customerRef = salesOrderRet.CustomerRef;

                            //string customerListId = salesOrderRet.CustomerRef.ListID.GetValue();
                            //string customer = salesOrderRet.CustomerRef.FullName.GetValue();

                            string customerListId = customerRef.ListID.GetValue();
                            string customer = customerRef.FullName.GetValue();

                            string[] customerJob = customer.Split(':');
                            string customerName = customerJob[0];
                            string location = customerJob[1];

                            //Adding all locations for current customer
                            if(prevCustomer == customerName) 
                            {
                                locations.Add(location);
                            }
                            else
                            {

                                finalLocation.Add(customerName,locations);
                                Job = new CustomerJob(customerListId, customerName, finalLocation);
                                locations = new List<string>();

                            }
                           

                            //locations.Add(location);
                            
                            if(Job != null)
                            {
                                CustomerJobs.Add(Job);
                                Job = null;
                            }
                           

                            //CustomerJobs.Add();
                        }


                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

            return (CustomerJobs);
        }
    }
}