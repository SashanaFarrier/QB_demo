using Intuit.Ipp.Data;
using MvcCodeFlowClientManual.Config;
using MvcCodeFlowClientManual.Models;
using QBFC15Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Item = MvcCodeFlowClientManual.Models.Item;

namespace MvcCodeFlowClientManual.Services
{
    public class ItemService
    {
        public QBConnection qBConnection = new QBConnection();

        private QBSessionManager sessionManager;
        private bool sessionBegun = false;
        private bool connectionOpen = false;

        //We will have to change the object name from Item as a current object within the qb lib already has that name, so there's a conflict
        public IList<Models.Item> AllItemsList = new List<Models.Item>();
        List<object> SubItems = new List<object>();
        public IList<Models.Item> GetItems()
        {
           
            if (qBConnection.getSessionManager() != null)
            {
                try
                {
                    sessionManager = qBConnection.getSessionManager();
                    connectionOpen = true;

                    IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);

                    IItemQuery itemQuery = requestMsgSet.AppendItemQueryRq();
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

                        IORItemRetList itemsList = (IORItemRetList)response.Detail;
                        //IItemInventorRetList itemInventoryList = (IItemInventoryRetList)response.Detail;
                        //IItemServiceRet itemsRet = itemsList.GetAt(0).ItemServiceRet;
                        
                        for (int i = 0; i < itemsList.Count; i++)
                        {
                            
                            if(itemsList.GetAt(i).ItemServiceRet != null)
                            {
                                IItemServiceRet itemService = itemsList.GetAt(i).ItemServiceRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.FullName.GetValue();
                                string itemDesc = item;
                                ItemCategory(itemId, item, itemDesc);
                            }
                            if (itemsList.GetAt(i).ItemNonInventoryRet != null)
                            {
                                IItemNonInventoryRet itemService = itemsList.GetAt(i).ItemNonInventoryRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.FullName.GetValue();
                                string itemDesc = "Test";

                                if (!item.Contains(':'))
                                {
                                    SubItems = new List<object>();
                                    AllItemsList.Add(new Models.Item(itemId, item, itemDesc, SubItems));
                                }
                                else
                                {
                                    string[] inventory = item.Split(':');
                                    string itemName = inventory[0];
                                    string subItem = inventory[1];

                                    if (AllItemsList.Count > 0)
                                    {
                                        foreach (var x in AllItemsList)
                                        {
                                            if (x.Name.Equals(itemName))
                                            {

                                                SubItems.Add(new { name = subItem, description = itemDesc });
                                            }
                                        }
                                    }
                                }
                            }
                            if (itemsList.GetAt(i).ItemOtherChargeRet != null)
                            {
                                IItemOtherChargeRet itemService = itemsList.GetAt(i).ItemOtherChargeRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.FullName.GetValue();
                                string itemDesc = "Test";
                                ItemCategory(itemId, item, itemDesc);
                            }
                            if (itemsList.GetAt(i).ItemInventoryRet != null)
                            {
                                IItemInventoryRet itemService = itemsList.GetAt(i).ItemInventoryRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.FullName.GetValue();
                                string itemDesc = "Test";
                                ItemCategory(itemId, item, itemDesc);
                            }
                            if (itemsList.GetAt(i).ItemInventoryAssemblyRet != null)
                            {
                                IItemInventoryAssemblyRet itemService = itemsList.GetAt(i).ItemInventoryAssemblyRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.FullName.GetValue();
                                string itemDesc = "Test";
                                ItemCategory(itemId, item, itemDesc);
                            }
                            if (itemsList.GetAt(i).ItemFixedAssetRet != null)
                            {
                                IItemFixedAssetRet itemService = itemsList.GetAt(i).ItemFixedAssetRet;
                                //string item = itemService.ClassRef.FullName.GetValue() ?? "";
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.AssetAccountRef.FullName.GetValue();
                                string itemDesc = item;
                                ItemCategory(itemId, item, itemDesc);
                            }
                            if (itemsList.GetAt(i).ItemSubtotalRet != null)
                            {
                                IItemSubtotalRet itemService = itemsList.GetAt(i).ItemSubtotalRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.Name.GetValue();
                                string itemDesc = "Test";
                                ItemCategory(itemId, item, itemDesc);
                            }
                            if (itemsList.GetAt(i).ItemDiscountRet != null)
                            {
                                IItemDiscountRet itemService = itemsList.GetAt(i).ItemDiscountRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.Name.GetValue();
                                string itemDesc = "Test";
                                ItemCategory(itemId, item, itemDesc);
                            }
                            //if (itemsList.GetAt(i).ItemDiscountRet != null)
                            //{
                            //    IItemDiscountRet itemService = itemsList.GetAt(i).ItemDiscountRet;
                            //    string item = itemService.FullName.GetValue();
                            //    string itemDesc = itemService.ItemDesc.GetValue();
                            //    ItemCategory(item, itemDesc);
                            //}

                            if (itemsList.GetAt(i).ItemPaymentRet != null)
                            {
                                IItemPaymentRet itemService = itemsList.GetAt(i).ItemPaymentRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.Name.GetValue();
                                string itemDesc = "Test";
                                ItemCategory(itemId, item, itemDesc);
                            }

                            if (itemsList.GetAt(i).ItemSalesTaxRet != null)
                            {
                                IItemSalesTaxRet itemService = itemsList.GetAt(i).ItemSalesTaxRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.Name.GetValue();
                                string itemDesc = "Test";
                                ItemCategory(itemId, item, itemDesc);
                            }

                            if (itemsList.GetAt(i).ItemSalesTaxRet != null)
                            {
                                IItemSalesTaxRet itemService = itemsList.GetAt(i).ItemSalesTaxRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.Name.GetValue();
                                string itemDesc = "Test";
                                ItemCategory(itemId, item, itemDesc);
                            }

                            if (itemsList.GetAt(i).ItemSalesTaxGroupRet != null)
                            {
                                IItemSalesTaxGroupRet itemService = itemsList.GetAt(i).ItemSalesTaxGroupRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.Name.GetValue();
                                string itemDesc = "Test";
                                ItemCategory(itemId, item, itemDesc);
                            }

                            if (itemsList.GetAt(i).ItemGroupRet != null)
                            {
                                IItemGroupRet itemService = itemsList.GetAt(i).ItemGroupRet;
                                string itemId = itemService.ListID.GetValue();
                                string item = itemService.Name.GetValue();
                                string itemDesc = "Test";
                                ItemCategory(itemId, item, itemDesc);
                            }
                            //string itemDesc = oRSalesPurchase.SalesAndPurchase.SalesDesc != null ? itemService.ORSalesPurchase.SalesAndPurchase.SalesDesc.GetValue() : "";
                            //int quantity = (int)itemsRet.QuantityOnHand.GetValue();


                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
           
            return (AllItemsList);
        }

        public void ItemCategory(string itemId, string item, string itemDesc)
        {

            if (!item.Contains(':'))
            {
                SubItems = new List<object>();
                AllItemsList.Add(new Models.Item(itemId, item, itemDesc, SubItems));
            }
            else
            {
                string[] inventory = item.Split(':');
                string itemName = inventory[0];
                string subItem = inventory[1];

                if (AllItemsList.Count > 0)
                {
                    foreach (var x in AllItemsList)
                    {
                        if (x.Name.Equals(itemName))
                        {

                            SubItems.Add(new { name = subItem, description = itemDesc });
                        }
                    }
                }
            }
        }
    }
}