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
    public class InventoryService
    {
        public QBConnection qBConnection = new QBConnection();

        private QBSessionManager sessionManager;
        private bool sessionBegun = false;
        private bool connectionOpen = false;
        //public ItemService itemService = new ItemService();
        //We will have to change the object name from Item as a current object within the qb lib already has that name, so there's a conflict
        public IList<Inventory> InventoryItems = new List<Inventory>();
        List<object> SubItems = new List<object>();
        public IList<Inventory> GetItems()
        {

            if (qBConnection.getSessionManager() != null)
            {
                try
                {
                    sessionManager = qBConnection.getSessionManager();
                    connectionOpen = true;

                    IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);

                    IItemInventoryQuery itemQuery = requestMsgSet.AppendItemInventoryQueryRq();
                    //IItemQuery itemQuery = requestMsgSet.AppendItemQueryRq();
                    sessionBegun = true;

                    IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

                    sessionManager.EndSession();
                    sessionBegun = false;
                    sessionManager.CloseConnection();
                    connectionOpen = false;

                    IResponseList responseList = responseMsgSet.ResponseList;

                    IResponse response = responseList.GetAt(0);

                    if (response.StatusCode >= 0)
                    {
                        IResponseType responseType = response.Type;

                        IItemInventoryRetList itemInventoryList = (IItemInventoryRetList)response.Detail;

                        for (int i = 0; i < itemInventoryList.Count; i++)
                        {
                            IItemInventoryRet itemInventoryRet = itemInventoryList.GetAt(i);

                            string item = itemInventoryRet.FullName.GetValue();
                            string itemDesc = itemInventoryRet.SalesDesc != null ? itemInventoryRet.SalesDesc.GetValue() : "";
                            int quantity = (int)itemInventoryRet.QuantityOnHand.GetValue();

                            if (!item.Contains(':'))
                            {
                                SubItems = new List<object>();
                                InventoryItems.Add(new Inventory(item, itemDesc, quantity, SubItems));
                            }
                            else
                            {
                                string[] inventory = item.Split(':');
                                string itemName = inventory[0];
                                string subItem = inventory[1];

                                if (InventoryItems.Count > 0)
                                {
                                    foreach (var x in InventoryItems)
                                    {
                                        if (x.Name.Equals(itemName))
                                        {
                                            SubItems.Add(new { subItem, itemDesc, quantity });
                                        }
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

            return (InventoryItems);
        }
    }
}