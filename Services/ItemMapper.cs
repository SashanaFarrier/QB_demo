using QBFC15Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Services
{
    public class ItemMapper<T>
    {
        //private Item itemNonInventoryItem(IItemNonInventoryRet itemService)
        //{

        //    string itemId = itemService.ListID != null ? itemService.ListID.GetValue() : null;
        //    string itemName = itemService.FullName.GetValue();

        //    string tax = itemService.SalesTaxCodeRef.FullName != null ? itemService.SalesTaxCodeRef.FullName.GetValue() : null;
        //    double orPrice = itemService.ORSalesPurchase.SalesOrPurchase.ORPrice.Price != null ? itemService.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.GetValue() : 0;

        //    string description = itemService.ORSalesPurchase.SalesOrPurchase.Desc
        //        != null ? itemService.ORSalesPurchase.SalesOrPurchase.Desc.GetValue() : null;
        //    Item item = new Item();
        //    item.ItemId = itemId;
        //    item.Name = itemName;
        //    item.Category = itemService.ClassRef.FullName.GetValue();

        //    return item;


        //}
    }
}