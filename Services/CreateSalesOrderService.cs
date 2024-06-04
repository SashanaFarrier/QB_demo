using Intuit.Ipp.Data;
using MvcCodeFlowClientManual.Config;
using QBFC15Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static System.Collections.Specialized.BitVector32;

namespace MvcCodeFlowClientManual.Services
{
    public class CreateSalesOrderService
    {
        public QBConnection qBConnection = new QBConnection();
        public CustomerJobService customerJob = new CustomerJobService();

        private bool sessionBegun = false;
        private bool connectionOpen = false;
        private QBSessionManager sessionManager;
        public async void CreateSalesOrder()
        {
                if (qBConnection.getSessionManager() != null)
                {
                    try
                    {
                        sessionManager = new QBSessionManager();
                        IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);
                        requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

                        string customer = string.Empty;
                        string listID = string.Empty;
                        ISalesOrderAdd salesOrderAddRq = requestMsgSet.AppendSalesOrderAddRq();

                        var getCustomers =  await customerJob.GetCustomerJobs();
                    
                        //var getCustomer = new List<Customer>()
                        foreach (var c in getCustomers)
                        {
                            if (c.Name == "Baker, Chris")
                            {
                                //customer = c.Name;
                                listID = c.CustomerListID;
                                break;
                            }

                        }
                         
                        salesOrderAddRq.defMacro.SetValue("IQBStringType");
                        salesOrderAddRq.CustomerRef.ListID.SetValue(listID);
                        //salesOrderAddRq.CustomerRef.FullName.SetValue(customer);
                        
                        //Set field value for TxnDate
                        //salesOrderAddRq.TxnDate.SetValue(DateTime.Parse("05/30/2024"));
                        
                        salesOrderAddRq.DueDate.SetValue(DateTime.Parse("06/01/2024"));
                        
                        //salesOrderAddRq.ShipDate.SetValue(DateTime.Parse("06/01/2024"));
                       
                        salesOrderAddRq.Memo.SetValue("Test if showing up in QB");
                       
                    
                    IORSalesOrderLineAdd ORSalesOrderLineAddListElement170 = salesOrderAddRq.ORSalesOrderLineAddList.Append();

                    string ORSalesOrderLineAddListElementType171 = "SalesOrderLineAdd";
                    if (ORSalesOrderLineAddListElementType171 == "SalesOrderLineAdd")
                    {
                        //Set field value for ListID
                        //listid for cabinets - F0000-933272656
                        //ORSalesOrderLineAddListElement170.SalesOrderLineAdd.ItemRef.ListID.SetValue("F0000-933272656");
                        ORSalesOrderLineAddListElement170.SalesOrderLineAdd.ItemRef.FullName.SetValue("Reimb Group");
                        ORSalesOrderLineAddListElement170.SalesOrderLineAdd.Quantity.SetValue(1);
                        //ORSalesOrderLineAddListElement170.SalesOrderLineAdd.Amount.SetValue(2000);
                        ORSalesOrderLineAddListElement170.SalesOrderLineAdd.ORRatePriceLevel.Rate.SetValue(12000);
                        //listid for tax
                        //ORSalesOrderLineAddListElement170.SalesOrderLineAdd.SalesTaxCodeRef.ListID.SetValue("10000-999022286");
                        ORSalesOrderLineAddListElement170.SalesOrderLineAdd.SalesTaxCodeRef.FullName.SetValue("Non");


                    }
                    //if (ORSalesOrderLineAddListElementType171 == "SalesOrderLineGroupAdd")
                    //{
                       
                    //    ORSalesOrderLineAddListElement170.SalesOrderLineGroupAdd.ItemGroupRef.ListID.SetValue("100000-933272656");
                       
                    //    ORSalesOrderLineAddListElement170.SalesOrderLineGroupAdd.Quantity.SetValue(1);
                       
                    //}

                    
                    
                    // Connect to QB and begin session
                    sessionManager.OpenConnection("", "Test");
                    connectionOpen = true;
                    sessionManager.BeginSession("", ENOpenMode.omDontCare);
                    sessionBegun = true;
                    //connectionOpen = true;
                    //sessionBegun = true;

                    // Send the request to QuickBooks
                    //IResponseList responseList1 = (IResponseList)sessionManager.DoRequests(requestMsgSet);
                    IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);
                        sessionManager.EndSession();
                        sessionBegun = false;
                        sessionManager.CloseConnection();
                        connectionOpen = false;

                    IResponseList responseList = responseMsgSet.ResponseList;

                    //IResponse response = responseList.GetAt(0);

                    if (responseList.Count == 1)
                    {
                        IResponse response = responseList.GetAt(0);
                        IResponseType responseType = response.Type;

                        int statusCode = response.StatusCode;
                        string statusSeverity = response.StatusSeverity;
                        string retStatusMesage = response.StatusMessage;
                        Console.WriteLine("Sales Order created successfully! " + $"status code = {statusCode}, status severity = {statusSeverity}, status message = {retStatusMesage} ");
                        
                        
                    } else
                        {
                            Console.WriteLine($"Error creating Sales Order:");
                        }
                       
                }
                catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }


                }
        }
    }
}