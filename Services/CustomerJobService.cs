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

        List<string> locations = new List<string>();

        public IList<CustomerJob> GetCustomerJobs()
        {

            if (qBConnection.getSessionManager() != null)
            {
                try
                {
                    sessionManager = qBConnection.getSessionManager();

                    IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);

                    ICustomerQuery orderQuery = requestMsgSet.AppendCustomerQueryRq();

                    IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

                    IResponseList responseList = responseMsgSet.ResponseList;

                    IResponse response = responseList.GetAt(0);

                    if (response.StatusCode == 0)
                    {
                        IResponseType responseType = response.Type;

                        ICustomerRetList customerQueryList = (ICustomerRetList)response.Detail;

                        string prevCustomer = string.Empty;
                       
                        for (int i = 0; i < customerQueryList.Count; i++)
                        {
                            string customer = customerQueryList.GetAt(i).FullName.GetValue();

                            //string currentCustomer = customer;
                            
                            if(prevCustomer == "" && !customer.Contains(':'))
                            {
                                prevCustomer = customer;
                            }

                            if(customer.Contains(':'))
                            {
                                string[] customerJob = customer.Split(':');
                                string customerName = customerJob[0];
                                string location = customerJob[1];

                                if(prevCustomer == customerName)
                                {
                                    locations.Add(location);
                                    //prevCustomer = customerName;
                                }
                                
                                if (i <= customerQueryList.Count - 1 )
                                {
                                    string customer2 = customerQueryList.GetAt(i + 1).FullName.GetValue();
                                    if (!customer2.Contains(':'))
                                    {
                                       CustomerJobs.Add(new CustomerJob(customerName, locations));
                                        locations = new List<string>();
                                        prevCustomer = customer2;

                                    }
                                }
                            }
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