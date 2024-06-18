using Intuit.Ipp.Data;
using MvcCodeFlowClientManual.Config;
using MvcCodeFlowClientManual.Models;
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
        public async void CreateSalesOrder(Models.SalesOrder salesOrder)
        {
                if (qBConnection.getSessionManager() != null)
                {
                    try
                    {
                        sessionManager = new QBSessionManager();
                        IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);
                        requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

                        ISalesOrderAdd salesOrderAddRq = requestMsgSet.AppendSalesOrderAddRq();

                        salesOrderAddRq.defMacro.SetValue("IQBStringType");
                        
                        //salesOrderAddRq.CustomerRef.ListID.SetValue(salesOrder.CustomerId);
                        salesOrderAddRq.CustomerRef.FullName.SetValue(salesOrder.CustomerJob);
                        salesOrderAddRq.TxnDate.SetValue(salesOrder.TransactionDate);
                        salesOrderAddRq.CustomerSalesTaxCodeRef.FullName.SetValue(salesOrder.CustomerSalesTaxCodeRef);
                    //salesOrderAddRq.Memo.SetValue("Test if showing up in QB");



                    // IORSalesOrderLineAdd ORSalesOrderLineAddListElement170 = salesOrderAddRq.ORSalesOrderLineAddList.Append();
                    ISalesOrderLineAdd LineItemAdder;
                    string ORSalesOrderLineAddListElementType171 = "SalesOrderLineAdd";
                    if (ORSalesOrderLineAddListElementType171 == "SalesOrderLineAdd")
                    {

                            foreach (Models.Item item in salesOrder.ItemList)
                            {
                            LineItemAdder = salesOrderAddRq.ORSalesOrderLineAddList.Append().SalesOrderLineAdd;
                            //the name of the item is itemRef
                            LineItemAdder.ItemRef.FullName.SetValue(item.Name);
                            LineItemAdder.Quantity.SetValue(item.Quantity);
                            LineItemAdder.Amount.SetValue(item.Amount);
                            LineItemAdder.ORRatePriceLevel.Rate.SetValue(item.Rate);
                            LineItemAdder.SalesTaxCodeRef.FullName.SetValue(item.Tax);
                            //LineItemAdder = salesOrderAddRq.ORSalesOrderLineAddList.Append().SalesOrderLineAdd;
                            //ORSalesOrderLineAddListElement170.SalesOrderLineAdd.SalesTaxCodeRef.ListID.SetValue("10000-999022286");
                        }
                            

                        //ORSalesOrderLineAddListElement170.SalesOrderLineAdd.ORRatePriceLevel.Rate.SetValue(12000);
                        //listid for tax
                        //
                        //ORSalesOrderLineAddListElement170.SalesOrderLineAdd.SalesTaxCodeRef.FullName.SetValue("Non");

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