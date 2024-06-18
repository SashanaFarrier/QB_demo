using Intuit.Ipp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Models
{
    public class SalesOrder
    {
        private string salesOrderId;

        public string SalesOrderId
        {
            get { return salesOrderId; }
            set { salesOrderId = value; }
        }
        private DateTime transactionDate;

        public DateTime TransactionDate
        {
            get { return transactionDate; }
            set { transactionDate = value; }
        }
        private string customerJob;

        public string CustomerJob
        {
            get { return customerJob; }
            set { customerJob = value; }
        }

        private string customerId;

        public string CustomerId
        {
            get { return customerId; }
            set { customerId = value; }
        }

        private List<Item> itemList = new List<Item>();
        public List<Item> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }
        //private Dictionary<ItemCategory, List<Item>> itemDictionary = new Dictionary<ItemCategory, List<Item>>();
        //public Dictionary<ItemCategory, List<Item>> ItemDictionary
        //{
        //    get { return itemDictionary; }
        //    set { itemDictionary = value; }
        //}
        private string customerSalesTaxCodeRef;

        public string CustomerSalesTaxCodeRef
        {
            get { return customerSalesTaxCodeRef; }
            set { customerSalesTaxCodeRef = value; }
        }

        private double totalTax;
        public double TotalTax
        {
            get { return totalTax; }
            set {  totalTax = value; }
        }
        
        private double subTotal;
        public double SubTotal
        {
            get { return subTotal; }
            set {  subTotal = value; }
        }

        private double total; 
        public double Total
        {
            get { return total; }
            set { total = value; }
        }


        //public List<Item> getItems(List<Item> items)
        //{

        //}
        //public List<Item> getItems(ItemCategory itemCategory)
        //{
        //    List<Item> items = itemDictionary[itemCategory];

        //    return items;
        //}



        //public string ItemName { get; set; }
        //public string ItemNumber { get; set; }
        //public string Description { get; set; }
        //public double Quantity { get; set; }
        //public double Rate { get; set; }
        //public double Amount { get; set; }
        // public double SubTotal { get; set; }
        // public double Total { get; set; }
    }

   
}