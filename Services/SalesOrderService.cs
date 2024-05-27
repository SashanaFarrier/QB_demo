using MvcCodeFlowClientManual.Config;
using MvcCodeFlowClientManual.Models;
using QBFC15Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Services
{
    public class SalesOrderService
    {
        public QBConnection qBConnection = new QBConnection();
        private QBSessionManager sessionManager;

        public IList<Order> Orders = new List<Order>();

        public IList<Order> GetSalesOrders()
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

                        for (int i = 0; i < salesOrderList.Count; i++)
                        {
                            ISalesOrderRet salesOrderRet = salesOrderList.GetAt(i);

                            DateTime date = salesOrderRet.TxnDate.GetValue();
                            string customerName = salesOrderRet.CustomerRef.FullName.GetValue();
                            string itemName = string.Empty;
                            int number = salesOrderRet.TxnNumber.GetValue();
                            string description = string.Empty;
                            double quantity, rate;
                            double subtotal = salesOrderRet.Subtotal.GetValue();
                            double tax = salesOrderRet.SalesTaxTotal.GetValue();
                            double total = salesOrderRet.TotalAmount.GetValue();

                            if (salesOrderRet.ORSalesOrderLineRetList != null && salesOrderRet.ORSalesOrderLineRetList.GetAt(0).SalesOrderLineRet != null)
                            {
                                ISalesOrderLineRet salesOrderLineRet = salesOrderRet.ORSalesOrderLineRetList.GetAt(0).SalesOrderLineRet;

                                itemName = salesOrderLineRet.ItemRef.FullName.GetValue();
                                description = salesOrderLineRet.Desc.GetValue();
                                quantity = salesOrderLineRet.Quantity.GetValue();
                                rate = salesOrderLineRet.ORRate.Rate.GetValue();
                            }
                            else
                            {
                                itemName = "No item name found";
                                description = "No description found";
                                quantity = 0;
                                rate = 0;
                            }

                            var order = new Order()
                            {
                                Date = date,
                                //CustomerJob = customerName,
                                ItemName = itemName,
                                Number = number,
                                Description = description,
                                Quantity = quantity,
                                Rate = rate,
                                SubTotal = subtotal,
                                Tax = tax,
                                Total = total,
                            };

                            Orders.Add(order);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

            return (Orders);
        }

        //public string GetOrderDescription(ISalesOrderRet salesOrderRet)
        //{
        //    string description = string.Empty;

        //    var oRSalesOrderLineRetList = salesOrderRet.ORSalesOrderLineRetList;
            
        //    if(oRSalesOrderLineRetList != null)
        //    {
        //        for (int i = 0; i < oRSalesOrderLineRetList.Count; i++)
        //        {
        //            if(oRSalesOrderLineRetList.GetAt(i).SalesOrderLineRet.Desc.GetValue() != null)
        //            {
        //                description = oRSalesOrderLineRetList.GetAt(i).SalesOrderLineRet.Desc.GetValue();
        //            }
                    
        //        }
        //    }
          
        //    return description;
        //}

        //public int GetOrderQuantity(ISalesOrderRet salesOrderRet)
        //{
            
        //    int quantity = 0;
        //    var orderList = salesOrderRet.ORSalesOrderLineRetList;

        //    if (orderList != null)
        //    {
        //        for (int i = 0; i < orderList.Count; i++)
        //        {
        //            quantity = (int)orderList.GetAt(i).SalesOrderLineRet.Quantity.GetValue();
        //        }

        //    }

        //    return quantity;
        //}

        //public double GetOrderRate(ISalesOrderRet salesOrderRet)
        //{
        //    double rate = 0;
        //    var orderList = salesOrderRet.ORSalesOrderLineRetList;

        //    if (orderList != null)
        //    {
        //        for (int i = 0; i < orderList.Count; i++)
        //        {
        //            rate = orderList.GetAt(i).SalesOrderLineRet.ORRate.Rate.GetValue();
        //        }

        //    }


        //    return rate;
        //}
    }
}