using Intuit.Ipp.Data;
using MvcCodeFlowClientManual.Config;
using MvcCodeFlowClientManual.Models;
using QBFC15Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
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
        //public IList<Models.Item> AllItemsList = new List<Models.Item>();
        List<object> SubItems = new List<object>();
      
        public Dictionary<string, List<Item>> GetItems()
        {
            Dictionary<string, List<Item>> itemDictionary = new Dictionary<string, List<Item>>();
            List<Item> serviceItems = new List<Item>();
            List<Item> nonInventoryItems = new List<Item>();
            List<Item> inventoryItems = new List<Item>();
            List<Item> inventoryAssemblyItems = new List<Item>();
            List<Item> fixedAssetItems = new List<Item>();
            List<Item> subTotalItems = new List<Item>();
            List<Item> discountItems = new List<Item>();
            List<Item> paymentItems = new List<Item>();
            List<Item> salesTaxItems = new List<Item>();
            List<Item> salesTaxGroupItems = new List<Item>();
            List<Item> groupItems = new List<Item>();
            List<Item> otherChargeItems = new List<Item>();

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
                        
                        for (int i = 0; i < itemsList.Count; i++)
                        {
                            
                            if (itemsList.GetAt(i).ItemServiceRet != null)
                            {
                                IItemServiceRet itemService = itemsList.GetAt(i).ItemServiceRet;
                                Item item = serviceItem(itemService);
                                serviceItems.Add(item);
                                // ItemCategory(itemId, item, itemDesc);
                            }

                            if (itemsList.GetAt(i).ItemNonInventoryRet != null)
                            {
                                IItemNonInventoryRet itemService = itemsList.GetAt(i).ItemNonInventoryRet;
                                Item item = nonInventoryItem(itemService);
                                nonInventoryItems.Add(item);

                                //if (!item.Contains(':'))
                                //{
                                //    SubItems = new List<object>();
                                //    AllItemsList.Add(new Models.Item(itemId, item, itemDesc, SubItems));
                                //}
                                //else
                                //{
                                //    string[] inventory = item.Split(':');
                                //    string itemName = inventory[0];
                                //    string subItem = inventory[1];

                                //    if (AllItemsList.Count > 0)
                                //    {
                                //        foreach (var x in AllItemsList)
                                //        {
                                //            if (x.Name.Equals(itemName))
                                //            {

                                //                SubItems.Add(new { name = subItem, description = itemDesc });
                                //            }
                                //        }
                                //    }
                                //}
                            }

                            if (itemsList.GetAt(i).ItemOtherChargeRet != null)
                            {
                                IItemOtherChargeRet itemService = itemsList.GetAt(i).ItemOtherChargeRet;
                                Item item = otherChargeItem(itemService);
                                otherChargeItems.Add(item);
                            }

                            if (itemsList.GetAt(i).ItemInventoryRet != null)
                            {
                                IItemInventoryRet itemService = itemsList.GetAt(i).ItemInventoryRet;
                                Item item = inventoryItem(itemService);
                                inventoryItems.Add(item);
                            }

                            if (itemsList.GetAt(i).ItemInventoryAssemblyRet != null)
                            {
                                IItemInventoryAssemblyRet itemService = itemsList.GetAt(i).ItemInventoryAssemblyRet;
                                Item item = inventoryAssemblyItem(itemService);
                                inventoryAssemblyItems.Add(item);
                            }

                            if (itemsList.GetAt(i).ItemFixedAssetRet != null)
                            {
                                IItemFixedAssetRet itemService = itemsList.GetAt(i).ItemFixedAssetRet;
                                Item item = fixedAssetItem(itemService);
                                fixedAssetItems.Add(item);
                            }

                            if (itemsList.GetAt(i).ItemSubtotalRet != null)
                            {
                                IItemSubtotalRet itemService = itemsList.GetAt(i).ItemSubtotalRet;
                                Item item = subTotalItem(itemService);
                                subTotalItems.Add(item);
                            }

                            if (itemsList.GetAt(i).ItemDiscountRet != null)
                            {
                                IItemDiscountRet itemService = itemsList.GetAt(i).ItemDiscountRet;
                                Item item = discountItem(itemService);
                                discountItems.Add(item);
                            }
                            
                            if (itemsList.GetAt(i).ItemPaymentRet != null)
                            {
                                IItemPaymentRet itemService = itemsList.GetAt(i).ItemPaymentRet;
                                Item item = paymentItem(itemService);
                                paymentItems.Add(item);
                            }

                            if (itemsList.GetAt(i).ItemSalesTaxRet != null)
                            {
                                IItemSalesTaxRet itemService = itemsList.GetAt(i).ItemSalesTaxRet;
                                Item item = salesTaxItem(itemService);
                                salesTaxItems.Add(item);
                            }

                            if (itemsList.GetAt(i).ItemSalesTaxGroupRet != null)
                            {
                                IItemSalesTaxGroupRet itemService = itemsList.GetAt(i).ItemSalesTaxGroupRet;
                                Item item = salesTaxGroupItem(itemService);
                                salesTaxGroupItems.Add(item);
                            }

                            if (itemsList.GetAt(i).ItemGroupRet != null)
                            {
                                IItemGroupRet itemService = itemsList.GetAt(i).ItemGroupRet;
                                Item item = groupItem(itemService);
                                groupItems.Add(item);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

            itemDictionary.Add("service", serviceItems);
            itemDictionary.Add("nonInventory", nonInventoryItems);
            itemDictionary.Add("inventory", inventoryItems);
            itemDictionary.Add("inventoryAssembly", inventoryAssemblyItems);
            itemDictionary.Add("fixedAsset", fixedAssetItems);
            itemDictionary.Add("subTotal", subTotalItems);
            itemDictionary.Add("discount", discountItems);
            itemDictionary.Add("payment", paymentItems);
            itemDictionary.Add("salesTax", salesTaxItems);
            itemDictionary.Add("salesTaxGroup", salesTaxGroupItems);
            itemDictionary.Add("group", groupItems);
            itemDictionary.Add("otherCharge", otherChargeItems);
            return (itemDictionary);    
        }

        
        private Item serviceItem(IItemServiceRet itemService)
        {
            
            string itemId = itemService.ListID != null ? itemService.ListID.GetValue(): null;
            string itemName = itemService.FullName.GetValue();
          
            string tax = itemService.SalesTaxCodeRef != null && itemService.SalesTaxCodeRef.FullName != null ? itemService.SalesTaxCodeRef.FullName.GetValue() : null;
            double orPrice = itemService.ORSalesPurchase != null && itemService.ORSalesPurchase.SalesOrPurchase != null && itemService.ORSalesPurchase.SalesOrPurchase.ORPrice != null ? itemService.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.GetValue() : 0 ;
            
            string description = itemService.ORSalesPurchase != null &&
                itemService.ORSalesPurchase.SalesOrPurchase != null && itemService.ORSalesPurchase.SalesOrPurchase.Desc
               != null ? itemService.ORSalesPurchase.SalesOrPurchase.Desc.GetValue() : null;
            
            
            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Service";
            item.Tax = tax;
            item.Amount = orPrice;
            
            return item;    
           

        }

        private Item nonInventoryItem(IItemNonInventoryRet nonInventoryItem)
        {

            string itemId = nonInventoryItem.ListID != null ? nonInventoryItem.ListID.GetValue() : null;
            string itemName = nonInventoryItem.FullName.GetValue();
            string tax = nonInventoryItem.SalesTaxCodeRef != null && nonInventoryItem.SalesTaxCodeRef.FullName != null ? nonInventoryItem.SalesTaxCodeRef.FullName.GetValue() : null;
            double orPrice = nonInventoryItem.ORSalesPurchase != null && nonInventoryItem.ORSalesPurchase.SalesOrPurchase != null && nonInventoryItem.ORSalesPurchase.SalesOrPurchase.ORPrice != null && nonInventoryItem.ORSalesPurchase.SalesOrPurchase.ORPrice.Price != null ? nonInventoryItem.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.GetValue() : 0;

            string description = nonInventoryItem.ORSalesPurchase != null && nonInventoryItem.ORSalesPurchase.SalesOrPurchase != null && nonInventoryItem.ORSalesPurchase.SalesOrPurchase.Desc
                != null ? nonInventoryItem.ORSalesPurchase.SalesOrPurchase.Desc.GetValue() : null;

            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Non Inventory";
            item.Tax = tax;
            item.Amount = orPrice;
            return item;


        }

        private Item otherChargeItem(IItemOtherChargeRet otherChargeItem)
        {
            string itemId = otherChargeItem.ListID != null ? otherChargeItem.ListID.GetValue() : null;
            string itemName = otherChargeItem.FullName.GetValue();

            string tax = otherChargeItem.SalesTaxCodeRef != null && otherChargeItem.SalesTaxCodeRef.FullName != null ? otherChargeItem.SalesTaxCodeRef.FullName.GetValue() : null;
            double orPrice = otherChargeItem.ORSalesPurchase.SalesOrPurchase.ORPrice.Price != null ? otherChargeItem.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.GetValue() : 0;

            string description = otherChargeItem.ORSalesPurchase != null && otherChargeItem.ORSalesPurchase.SalesOrPurchase != null && otherChargeItem.ORSalesPurchase.SalesOrPurchase.Desc
            != null ? otherChargeItem.ORSalesPurchase.SalesOrPurchase.Desc.GetValue() : null;
            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Other Charge";
            item.Tax = tax;
            item.Amount = orPrice;
            return item;
        }

        private Item inventoryItem(IItemInventoryRet inventoryItem)
        {
            string itemId = inventoryItem.ListID != null ? inventoryItem.ListID.GetValue() : null;
            string itemName = inventoryItem.FullName.GetValue();
            string tax = inventoryItem.SalesTaxCodeRef != null && inventoryItem.SalesTaxCodeRef.FullName != null ? inventoryItem.SalesTaxCodeRef.FullName.GetValue() : null;
            double orPrice = inventoryItem.SalesPrice != null ? inventoryItem.SalesPrice.GetValue() : 0;
            string description = inventoryItem.SalesDesc != null ? inventoryItem.SalesDesc.GetValue() : null;
               
            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Inventory";
            item.Tax = tax;
            item.Amount = orPrice;
            return item;
        }

        private Item inventoryAssemblyItem(IItemInventoryAssemblyRet inventoryAssemblyItem)
        {
            string itemId = inventoryAssemblyItem.ListID != null ? inventoryAssemblyItem.ListID.GetValue() : null;
            string itemName = inventoryAssemblyItem.FullName.GetValue();
            string tax = inventoryAssemblyItem.SalesTaxCodeRef != null && inventoryAssemblyItem.SalesTaxCodeRef.FullName != null ? inventoryAssemblyItem.SalesTaxCodeRef.FullName.GetValue() : null;
            double orPrice = inventoryAssemblyItem.SalesPrice != null ? inventoryAssemblyItem.SalesPrice.GetValue() : 0;
            string description = inventoryAssemblyItem.SalesDesc != null ? inventoryAssemblyItem.SalesDesc.GetValue() : null;

            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Inventory Assembly";
            item.Tax = tax;
            item.Amount = orPrice;
            return item;
        }

        private Item fixedAssetItem(IItemFixedAssetRet fixedAssetItem)
        {
            string itemId = fixedAssetItem.ListID != null ? fixedAssetItem.ListID.GetValue() : null;
            string itemName = fixedAssetItem.Name.GetValue();
            double salesPrice = fixedAssetItem.FixedAssetSalesInfo != null && fixedAssetItem.FixedAssetSalesInfo.SalesPrice != null ? fixedAssetItem.FixedAssetSalesInfo.SalesPrice.GetValue() : 0;
            string description = fixedAssetItem.FixedAssetSalesInfo != null && fixedAssetItem.FixedAssetSalesInfo.SalesDesc != null ? fixedAssetItem.FixedAssetSalesInfo.SalesDesc.GetValue() : null;

            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Fixed Asset";
            item.Amount = salesPrice;
            return item;
        }

        private Item subTotalItem(IItemSubtotalRet subTotalItem)
        {
            string itemId = subTotalItem.ListID != null ? subTotalItem.ListID.GetValue() : null;
            string itemName = subTotalItem.Name.GetValue();
            string description = subTotalItem.ItemDesc != null ? subTotalItem.ItemDesc.GetValue() : null;
        
            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            //ItemCategoryHelper.ParseEnum<ItemCategory>(otherChargeItem.ClassRef.FullName.GetValue())
            item.Category = "Sub Total";

            return item;
        }

        private Item discountItem(IItemDiscountRet discountItem)
        {
            string itemId = discountItem.ListID != null ? discountItem.ListID.GetValue() : null;
            string itemName = discountItem.FullName.GetValue();
            double discountRate = discountItem.ORDiscountRate != null && discountItem.ORDiscountRate.DiscountRate != null ? discountItem.ORDiscountRate.DiscountRate.GetValue() : 0;
            string description = discountItem.ItemDesc != null ? discountItem.ItemDesc.GetValue() : null;
            string tax = discountItem.SalesTaxCodeRef != null && discountItem.SalesTaxCodeRef.FullName != null ? discountItem.SalesTaxCodeRef.FullName.GetValue() : null;
           
            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Discount";
            item.Tax = tax;
            item.Rate = discountRate;
            return item;
        }

        private Item paymentItem(IItemPaymentRet paymentItem)
        {
            string itemId = paymentItem.ListID != null ? paymentItem.ListID.GetValue() : null;
            string itemName = paymentItem.Name.GetValue();
            //string depositAccount = paymentItem.DepositToAccountRef.FullName != null ? paymentItem.DepositToAccountRef.FullName.GetValue() : null;
            //string paymentMethod = paymentItem.PaymentMethodRef.FullName != null ? paymentItem.PaymentMethodRef.FullName.GetValue() : null;
            string description = paymentItem.ItemDesc != null ? paymentItem.ItemDesc.GetValue() : null;

            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Payment";

            return item;
        }

        private Item salesTaxItem(IItemSalesTaxRet salesTaxItem)
        {
            string itemId = salesTaxItem.ListID != null ? salesTaxItem.ListID.GetValue() : null;
            string itemName = salesTaxItem.Name.GetValue();
            string description = salesTaxItem.ItemDesc != null ? salesTaxItem.ItemDesc.GetValue() : null;
            double tax = salesTaxItem.TaxRate != null ? salesTaxItem.TaxRate.GetValue() : 0;
            //string taxVendorRef = salesTaxItem.TaxVendorRef.FullName != null ? salesTaxItem.TaxVendorRef.FullName.GetValue() : null;
            
            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Sales Tax";
            item.Tax = tax.ToString();
            return item;
        }

        private Item salesTaxGroupItem(IItemSalesTaxGroupRet salesTaxGroup)
        {
            string itemId = salesTaxGroup.ListID != null ? salesTaxGroup.ListID.GetValue() : null;
            string itemName = salesTaxGroup.Name.GetValue();
            string description = salesTaxGroup.ItemDesc != null ? salesTaxGroup.ItemDesc.GetValue() : null;

            IQBBaseRefList salesTaxGroupList = salesTaxGroup.ItemSalesTaxRefList;

            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Sales Tax Group";

            return item;
        }

        private Item groupItem(IItemGroupRet groupItem)
        {
            string itemId = groupItem.ListID != null ? groupItem.ListID.GetValue() : null;
            string itemName = groupItem.Name.GetValue();
            string description = groupItem.ItemDesc != null ? groupItem.ItemDesc.GetValue() : null;

            IItemGroupLineList groupItemsList = groupItem.ItemGroupLineList;

            Item item = new Item();
            item.ItemId = itemId;
            item.Name = itemName;
            item.Description = description;
            item.Category = "Group";

            return item;
        }
    }
}