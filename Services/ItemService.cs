using Intuit.Ipp.Data;
using MvcCodeFlowClientManual.Config;
using MvcCodeFlowClientManual.Models;
using QBFC15Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace MvcCodeFlowClientManual.Services
{
    public class ItemService
    {
        public QBConnection qBConnection = new QBConnection();

        private QBSessionManager sessionManager;

        //We will have to change the object name from Item as a current object within the qb lib already has that name, so there's a conflict
        public IList<Models.Item> itemList = new List<Models.Item>();
        public IList<Models.Item> GetItems()
        {
           
            if (qBConnection.getSessionManager() != null)
            {
                try
                {
                    sessionManager = qBConnection.getSessionManager();

                    IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);

                    IItemInventoryQuery itemQuery = requestMsgSet.AppendItemInventoryQueryRq();

                    IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

                    IResponseList responseList = responseMsgSet.ResponseList;

                    IResponse response = responseList.GetAt(0);

                    if (response.StatusCode == 0)
                    {
                        IResponseType responseType = response.Type;

                        IItemInventoryRetList itemInventoryList = (IItemInventoryRetList)response.Detail; 

                        for (int i = 0; i < itemInventoryList.Count; i++)
                        {
                            IItemInventoryRet itemInventoryRet = itemInventoryList.GetAt(i);
                            string itemId = itemInventoryRet.ListID.GetValue();
                            string itemName = itemInventoryRet.Name.GetValue();
                            string itemDesc = itemInventoryRet.SalesDesc.GetValue();
                            int itemQuantity = (int)itemInventoryRet.QuantityOnHand.GetValue();

                            var item = new Models.Item()
                            {
                                ItemId = itemId,
                                Name = itemName,
                                Description = itemDesc,
                                Quantity = itemQuantity,
                            };
                           
                            itemList.Add(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
           
            return (itemList);
        }
    }
}