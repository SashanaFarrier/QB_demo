//The following sample code is generated as an illustration of
//Creating requests and parsing responses ONLY
//This code is NOT intended to show best practices or ideal code
//Use at your most careful discretion

using System;
using System.Net;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using QBFC15Lib;
using MvcCodeFlowClientManual.Config;
using MvcCodeFlowClientManual.Services;

namespace MvcCodeFlowClientManual.SalesOrderService
{
    public class SalesOrderService
    {
        public QBConnection qBConnection = new QBConnection();
        public CreateSalesOrderService createSalesOrder;
        private bool sessionBegun = false;
        private bool connectionOpen = false;
        private QBSessionManager sessionManager;
        public void DoSalesOrderAdd()
        {
            if(qBConnection.getSessionManager() != null)
            {
                try
                {

                    sessionManager = qBConnection.getSessionManager();
                    connectionOpen = true;

                    IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);

                    ISalesOrderAdd itemQuery = requestMsgSet.AppendSalesOrderAddRq();

                    sessionBegun = true;

                    IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

                    sessionManager.EndSession();
                    sessionBegun = false;
                    sessionManager.CloseConnection();
                    connectionOpen = false;


                    //Create the message set request object to hold our request
                   // createSalesOrder = new CreateSalesOrderService(requestMsgSet);

                    IResponseList responseList = responseMsgSet.ResponseList;
                    
                    if (responseList == null) return;
                    
                    IResponse response = responseList.GetAt(0);

                    if (response.StatusCode >= 0)
                    {
                        //the request-specific response is in the details, make sure we have some
                        if (response.Detail != null)
                        {
                            //make sure the response is the type we're expecting
                            ENResponseType responseType = (ENResponseType)response.Type.GetValue();
                            if (responseType == ENResponseType.rtSalesOrderAddRs)
                            {
                                //upcast to more specific type here, this is safe because we checked with response.Type check above
                                ISalesOrderRet SalesOrderRet = (ISalesOrderRet)response.Detail;
                                //WalkSalesOrderRet(SalesOrderRet);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (sessionBegun)
                    {
                        sessionManager.EndSession();
                    }
                    if (connectionOpen)
                    {
                        sessionManager.CloseConnection();
                    }
                }
            }
            
        }


        




        void WalkSalesOrderAddRs(IMsgSetResponse responseMsgSet)
        {
            if (responseMsgSet == null) return;

            IResponseList responseList = responseMsgSet.ResponseList;
            if (responseList == null) return;

            //if we sent only one request, there is only one response, we'll walk the list for this sample
            for (int i = 0; i < responseList.Count; i++)
            {
                IResponse response = responseList.GetAt(i);
                //check the status code of the response, 0=ok, >0 is warning
                if (response.StatusCode >= 0)
                {
                    //the request-specific response is in the details, make sure we have some
                    if (response.Detail != null)
                    {
                        //make sure the response is the type we're expecting
                        ENResponseType responseType = (ENResponseType)response.Type.GetValue();
                        if (responseType == ENResponseType.rtSalesOrderAddRs)
                        {
                            //upcast to more specific type here, this is safe because we checked with response.Type check above
                            ISalesOrderRet SalesOrderRet = (ISalesOrderRet)response.Detail;
                            //WalkSalesOrderRet(SalesOrderRet);
                        }
                    }
                }
            }
        }



        void WalkSalesOrderRet(ISalesOrderRet SalesOrderRet)
        {
            if (SalesOrderRet == null) return;

            //Go through all the elements of ISalesOrderRet
            //Get value of TxnID
            string TxnID176 = (string)SalesOrderRet.TxnID.GetValue();
            //Get value of TimeCreated
            DateTime TimeCreated177 = (DateTime)SalesOrderRet.TimeCreated.GetValue();
            //Get value of TimeModified
            DateTime TimeModified178 = (DateTime)SalesOrderRet.TimeModified.GetValue();
            //Get value of EditSequence
            string EditSequence179 = (string)SalesOrderRet.EditSequence.GetValue();
            //Get value of TxnNumber
            if (SalesOrderRet.TxnNumber != null)
            {
                int TxnNumber180 = (int)SalesOrderRet.TxnNumber.GetValue();
            }
            //Get value of ListID
            if (SalesOrderRet.CustomerRef.ListID != null)
            {
                string ListID181 = (string)SalesOrderRet.CustomerRef.ListID.GetValue();
            }
            //Get value of FullName
            if (SalesOrderRet.CustomerRef.FullName != null)
            {
                string FullName182 = (string)SalesOrderRet.CustomerRef.FullName.GetValue();
            }
            if (SalesOrderRet.ClassRef != null)
            {
                //Get value of ListID
                if (SalesOrderRet.ClassRef.ListID != null)
                {
                    string ListID183 = (string)SalesOrderRet.ClassRef.ListID.GetValue();
                }
                //Get value of FullName
                if (SalesOrderRet.ClassRef.FullName != null)
                {
                    string FullName184 = (string)SalesOrderRet.ClassRef.FullName.GetValue();
                }
            }
            if (SalesOrderRet.TemplateRef != null)
            {
                //Get value of ListID
                if (SalesOrderRet.TemplateRef.ListID != null)
                {
                    string ListID185 = (string)SalesOrderRet.TemplateRef.ListID.GetValue();
                }
                //Get value of FullName
                if (SalesOrderRet.TemplateRef.FullName != null)
                {
                    string FullName186 = (string)SalesOrderRet.TemplateRef.FullName.GetValue();
                }
            }
            //Get value of TxnDate
            DateTime TxnDate187 = (DateTime)SalesOrderRet.TxnDate.GetValue();
            //Get value of RefNumber
            if (SalesOrderRet.RefNumber != null)
            {
                string RefNumber188 = (string)SalesOrderRet.RefNumber.GetValue();
            }
            if (SalesOrderRet.BillAddress != null)
            {
                //Get value of Addr1
                if (SalesOrderRet.BillAddress.Addr1 != null)
                {
                    string Addr1189 = (string)SalesOrderRet.BillAddress.Addr1.GetValue();
                }
                //Get value of Addr2
                if (SalesOrderRet.BillAddress.Addr2 != null)
                {
                    string Addr2190 = (string)SalesOrderRet.BillAddress.Addr2.GetValue();
                }
                //Get value of Addr3
                if (SalesOrderRet.BillAddress.Addr3 != null)
                {
                    string Addr3191 = (string)SalesOrderRet.BillAddress.Addr3.GetValue();
                }
                //Get value of Addr4
                if (SalesOrderRet.BillAddress.Addr4 != null)
                {
                    string Addr4192 = (string)SalesOrderRet.BillAddress.Addr4.GetValue();
                }
                //Get value of Addr5
                if (SalesOrderRet.BillAddress.Addr5 != null)
                {
                    string Addr5193 = (string)SalesOrderRet.BillAddress.Addr5.GetValue();
                }
                //Get value of City
                if (SalesOrderRet.BillAddress.City != null)
                {
                    string City194 = (string)SalesOrderRet.BillAddress.City.GetValue();
                }
                //Get value of State
                if (SalesOrderRet.BillAddress.State != null)
                {
                    string State195 = (string)SalesOrderRet.BillAddress.State.GetValue();
                }
                //Get value of PostalCode
                if (SalesOrderRet.BillAddress.PostalCode != null)
                {
                    string PostalCode196 = (string)SalesOrderRet.BillAddress.PostalCode.GetValue();
                }
                //Get value of Country
                if (SalesOrderRet.BillAddress.Country != null)
                {
                    string Country197 = (string)SalesOrderRet.BillAddress.Country.GetValue();
                }
                //Get value of Note
                if (SalesOrderRet.BillAddress.Note != null)
                {
                    string Note198 = (string)SalesOrderRet.BillAddress.Note.GetValue();
                }
            }
            if (SalesOrderRet.BillAddressBlock != null)
            {
                //Get value of Addr1
                if (SalesOrderRet.BillAddressBlock.Addr1 != null)
                {
                    string Addr1199 = (string)SalesOrderRet.BillAddressBlock.Addr1.GetValue();
                }
                //Get value of Addr2
                if (SalesOrderRet.BillAddressBlock.Addr2 != null)
                {
                    string Addr2200 = (string)SalesOrderRet.BillAddressBlock.Addr2.GetValue();
                }
                //Get value of Addr3
                if (SalesOrderRet.BillAddressBlock.Addr3 != null)
                {
                    string Addr3201 = (string)SalesOrderRet.BillAddressBlock.Addr3.GetValue();
                }
                //Get value of Addr4
                if (SalesOrderRet.BillAddressBlock.Addr4 != null)
                {
                    string Addr4202 = (string)SalesOrderRet.BillAddressBlock.Addr4.GetValue();
                }
                //Get value of Addr5
                if (SalesOrderRet.BillAddressBlock.Addr5 != null)
                {
                    string Addr5203 = (string)SalesOrderRet.BillAddressBlock.Addr5.GetValue();
                }
            }
            if (SalesOrderRet.ShipAddress != null)
            {
                //Get value of Addr1
                if (SalesOrderRet.ShipAddress.Addr1 != null)
                {
                    string Addr1204 = (string)SalesOrderRet.ShipAddress.Addr1.GetValue();
                }
                //Get value of Addr2
                if (SalesOrderRet.ShipAddress.Addr2 != null)
                {
                    string Addr2205 = (string)SalesOrderRet.ShipAddress.Addr2.GetValue();
                }
                //Get value of Addr3
                if (SalesOrderRet.ShipAddress.Addr3 != null)
                {
                    string Addr3206 = (string)SalesOrderRet.ShipAddress.Addr3.GetValue();
                }
                //Get value of Addr4
                if (SalesOrderRet.ShipAddress.Addr4 != null)
                {
                    string Addr4207 = (string)SalesOrderRet.ShipAddress.Addr4.GetValue();
                }
                //Get value of Addr5
                if (SalesOrderRet.ShipAddress.Addr5 != null)
                {
                    string Addr5208 = (string)SalesOrderRet.ShipAddress.Addr5.GetValue();
                }
                //Get value of City
                if (SalesOrderRet.ShipAddress.City != null)
                {
                    string City209 = (string)SalesOrderRet.ShipAddress.City.GetValue();
                }
                //Get value of State
                if (SalesOrderRet.ShipAddress.State != null)
                {
                    string State210 = (string)SalesOrderRet.ShipAddress.State.GetValue();
                }
                //Get value of PostalCode
                if (SalesOrderRet.ShipAddress.PostalCode != null)
                {
                    string PostalCode211 = (string)SalesOrderRet.ShipAddress.PostalCode.GetValue();
                }
                //Get value of Country
                if (SalesOrderRet.ShipAddress.Country != null)
                {
                    string Country212 = (string)SalesOrderRet.ShipAddress.Country.GetValue();
                }
                //Get value of Note
                if (SalesOrderRet.ShipAddress.Note != null)
                {
                    string Note213 = (string)SalesOrderRet.ShipAddress.Note.GetValue();
                }
            }
            if (SalesOrderRet.ShipAddressBlock != null)
            {
                //Get value of Addr1
                if (SalesOrderRet.ShipAddressBlock.Addr1 != null)
                {
                    string Addr1214 = (string)SalesOrderRet.ShipAddressBlock.Addr1.GetValue();
                }
                //Get value of Addr2
                if (SalesOrderRet.ShipAddressBlock.Addr2 != null)
                {
                    string Addr2215 = (string)SalesOrderRet.ShipAddressBlock.Addr2.GetValue();
                }
                //Get value of Addr3
                if (SalesOrderRet.ShipAddressBlock.Addr3 != null)
                {
                    string Addr3216 = (string)SalesOrderRet.ShipAddressBlock.Addr3.GetValue();
                }
                //Get value of Addr4
                if (SalesOrderRet.ShipAddressBlock.Addr4 != null)
                {
                    string Addr4217 = (string)SalesOrderRet.ShipAddressBlock.Addr4.GetValue();
                }
                //Get value of Addr5
                if (SalesOrderRet.ShipAddressBlock.Addr5 != null)
                {
                    string Addr5218 = (string)SalesOrderRet.ShipAddressBlock.Addr5.GetValue();
                }
            }
            //Get value of PONumber
            if (SalesOrderRet.PONumber != null)
            {
                string PONumber219 = (string)SalesOrderRet.PONumber.GetValue();
            }
            if (SalesOrderRet.TermsRef != null)
            {
                //Get value of ListID
                if (SalesOrderRet.TermsRef.ListID != null)
                {
                    string ListID220 = (string)SalesOrderRet.TermsRef.ListID.GetValue();
                }
                //Get value of FullName
                if (SalesOrderRet.TermsRef.FullName != null)
                {
                    string FullName221 = (string)SalesOrderRet.TermsRef.FullName.GetValue();
                }
            }
            //Get value of DueDate
            if (SalesOrderRet.DueDate != null)
            {
                DateTime DueDate222 = (DateTime)SalesOrderRet.DueDate.GetValue();
            }
            if (SalesOrderRet.SalesRepRef != null)
            {
                //Get value of ListID
                if (SalesOrderRet.SalesRepRef.ListID != null)
                {
                    string ListID223 = (string)SalesOrderRet.SalesRepRef.ListID.GetValue();
                }
                //Get value of FullName
                if (SalesOrderRet.SalesRepRef.FullName != null)
                {
                    string FullName224 = (string)SalesOrderRet.SalesRepRef.FullName.GetValue();
                }
            }
            //Get value of FOB
            if (SalesOrderRet.FOB != null)
            {
                string FOB225 = (string)SalesOrderRet.FOB.GetValue();
            }
            //Get value of ShipDate
            if (SalesOrderRet.ShipDate != null)
            {
                DateTime ShipDate226 = (DateTime)SalesOrderRet.ShipDate.GetValue();
            }
            if (SalesOrderRet.ShipMethodRef != null)
            {
                //Get value of ListID
                if (SalesOrderRet.ShipMethodRef.ListID != null)
                {
                    string ListID227 = (string)SalesOrderRet.ShipMethodRef.ListID.GetValue();
                }
                //Get value of FullName
                if (SalesOrderRet.ShipMethodRef.FullName != null)
                {
                    string FullName228 = (string)SalesOrderRet.ShipMethodRef.FullName.GetValue();
                }
            }
            //Get value of Subtotal
            if (SalesOrderRet.Subtotal != null)
            {
                double Subtotal229 = (double)SalesOrderRet.Subtotal.GetValue();
            }
            if (SalesOrderRet.ItemSalesTaxRef != null)
            {
                //Get value of ListID
                if (SalesOrderRet.ItemSalesTaxRef.ListID != null)
                {
                    string ListID230 = (string)SalesOrderRet.ItemSalesTaxRef.ListID.GetValue();
                }
                //Get value of FullName
                if (SalesOrderRet.ItemSalesTaxRef.FullName != null)
                {
                    string FullName231 = (string)SalesOrderRet.ItemSalesTaxRef.FullName.GetValue();
                }
            }
            //Get value of SalesTaxPercentage
            if (SalesOrderRet.SalesTaxPercentage != null)
            {
                double SalesTaxPercentage232 = (double)SalesOrderRet.SalesTaxPercentage.GetValue();
            }
            //Get value of SalesTaxTotal
            if (SalesOrderRet.SalesTaxTotal != null)
            {
                double SalesTaxTotal233 = (double)SalesOrderRet.SalesTaxTotal.GetValue();
            }
            //Get value of TotalAmount
            if (SalesOrderRet.TotalAmount != null)
            {
                double TotalAmount234 = (double)SalesOrderRet.TotalAmount.GetValue();
            }
            if (SalesOrderRet.CurrencyRef != null)
            {
                //Get value of ListID
                if (SalesOrderRet.CurrencyRef.ListID != null)
                {
                    string ListID235 = (string)SalesOrderRet.CurrencyRef.ListID.GetValue();
                }
                //Get value of FullName
                if (SalesOrderRet.CurrencyRef.FullName != null)
                {
                    string FullName236 = (string)SalesOrderRet.CurrencyRef.FullName.GetValue();
                }
            }
            //Get value of ExchangeRate
            if (SalesOrderRet.ExchangeRate != null)
            {
                float ExchangeRate237 = SalesOrderRet.ExchangeRate.GetValue();
            }
            //Get value of TotalAmountInHomeCurrency
            if (SalesOrderRet.TotalAmountInHomeCurrency != null)
            {
                double TotalAmountInHomeCurrency238 = (double)SalesOrderRet.TotalAmountInHomeCurrency.GetValue();
            }
            //Get value of IsManuallyClosed
            if (SalesOrderRet.IsManuallyClosed != null)
            {
                bool IsManuallyClosed239 = (bool)SalesOrderRet.IsManuallyClosed.GetValue();
            }
            //Get value of IsFullyInvoiced
            if (SalesOrderRet.IsFullyInvoiced != null)
            {
                bool IsFullyInvoiced240 = (bool)SalesOrderRet.IsFullyInvoiced.GetValue();
            }
            //Get value of Memo
            if (SalesOrderRet.Memo != null)
            {
                string Memo241 = (string)SalesOrderRet.Memo.GetValue();
            }
            if (SalesOrderRet.CustomerMsgRef != null)
            {
                //Get value of ListID
                if (SalesOrderRet.CustomerMsgRef.ListID != null)
                {
                    string ListID242 = (string)SalesOrderRet.CustomerMsgRef.ListID.GetValue();
                }
                //Get value of FullName
                if (SalesOrderRet.CustomerMsgRef.FullName != null)
                {
                    string FullName243 = (string)SalesOrderRet.CustomerMsgRef.FullName.GetValue();
                }
            }
            //Get value of IsToBePrinted
            if (SalesOrderRet.IsToBePrinted != null)
            {
                bool IsToBePrinted244 = (bool)SalesOrderRet.IsToBePrinted.GetValue();
            }
            //Get value of IsToBeEmailed
            if (SalesOrderRet.IsToBeEmailed != null)
            {
                bool IsToBeEmailed245 = (bool)SalesOrderRet.IsToBeEmailed.GetValue();
            }
            if (SalesOrderRet.CustomerSalesTaxCodeRef != null)
            {
                //Get value of ListID
                if (SalesOrderRet.CustomerSalesTaxCodeRef.ListID != null)
                {
                    string ListID246 = (string)SalesOrderRet.CustomerSalesTaxCodeRef.ListID.GetValue();
                }
                //Get value of FullName
                if (SalesOrderRet.CustomerSalesTaxCodeRef.FullName != null)
                {
                    string FullName247 = (string)SalesOrderRet.CustomerSalesTaxCodeRef.FullName.GetValue();
                }
            }
            //Get value of Other
            if (SalesOrderRet.Other != null)
            {
                string Other248 = (string)SalesOrderRet.Other.GetValue();
            }
            //Get value of ExternalGUID
            if (SalesOrderRet.ExternalGUID != null)
            {
                string ExternalGUID249 = (string)SalesOrderRet.ExternalGUID.GetValue();
            }
            if (SalesOrderRet.LinkedTxnList != null)
            {
                for (int i250 = 0; i250 < SalesOrderRet.LinkedTxnList.Count; i250++)
                {
                    ILinkedTxn LinkedTxn = SalesOrderRet.LinkedTxnList.GetAt(i250);
                    //Get value of TxnID
                    string TxnID251 = (string)LinkedTxn.TxnID.GetValue();
                    //Get value of TxnType
                    ENTxnType TxnType252 = (ENTxnType)LinkedTxn.TxnType.GetValue();
                    //Get value of TxnDate
                    DateTime TxnDate253 = (DateTime)LinkedTxn.TxnDate.GetValue();
                    //Get value of RefNumber
                    if (LinkedTxn.RefNumber != null)
                    {
                        string RefNumber254 = (string)LinkedTxn.RefNumber.GetValue();
                    }
                    //Get value of LinkType
                    if (LinkedTxn.LinkType != null)
                    {
                        ENLinkType LinkType255 = (ENLinkType)LinkedTxn.LinkType.GetValue();
                    }
                    //Get value of Amount
                    double Amount256 = (double)LinkedTxn.Amount.GetValue();
                }
            }
            if (SalesOrderRet.ORSalesOrderLineRetList != null)
            {
                for (int i257 = 0; i257 < SalesOrderRet.ORSalesOrderLineRetList.Count; i257++)
                {
                    IORSalesOrderLineRet ORSalesOrderLineRet = SalesOrderRet.ORSalesOrderLineRetList.GetAt(i257);
                    if (ORSalesOrderLineRet.SalesOrderLineRet != null)
                    {
                        if (ORSalesOrderLineRet.SalesOrderLineRet != null)
                        {
                            //Get value of TxnLineID
                            string TxnLineID258 = (string)ORSalesOrderLineRet.SalesOrderLineRet.TxnLineID.GetValue();
                            if (ORSalesOrderLineRet.SalesOrderLineRet.ItemRef != null)
                            {
                                //Get value of ListID
                                if (ORSalesOrderLineRet.SalesOrderLineRet.ItemRef.ListID != null)
                                {
                                    string ListID259 = (string)ORSalesOrderLineRet.SalesOrderLineRet.ItemRef.ListID.GetValue();
                                }
                                //Get value of FullName
                                if (ORSalesOrderLineRet.SalesOrderLineRet.ItemRef.FullName != null)
                                {
                                    string FullName260 = (string)ORSalesOrderLineRet.SalesOrderLineRet.ItemRef.FullName.GetValue();
                                }
                            }
                            //Get value of Desc
                            if (ORSalesOrderLineRet.SalesOrderLineRet.Desc != null)
                            {
                                string Desc261 = (string)ORSalesOrderLineRet.SalesOrderLineRet.Desc.GetValue();
                            }
                            //Get value of Quantity
                            if (ORSalesOrderLineRet.SalesOrderLineRet.Quantity != null)
                            {
                                int Quantity262 = (int)ORSalesOrderLineRet.SalesOrderLineRet.Quantity.GetValue();
                            }
                            //Get value of UnitOfMeasure
                            if (ORSalesOrderLineRet.SalesOrderLineRet.UnitOfMeasure != null)
                            {
                                string UnitOfMeasure263 = (string)ORSalesOrderLineRet.SalesOrderLineRet.UnitOfMeasure.GetValue();
                            }
                            if (ORSalesOrderLineRet.SalesOrderLineRet.OverrideUOMSetRef != null)
                            {
                                //Get value of ListID
                                if (ORSalesOrderLineRet.SalesOrderLineRet.OverrideUOMSetRef.ListID != null)
                                {
                                    string ListID264 = (string)ORSalesOrderLineRet.SalesOrderLineRet.OverrideUOMSetRef.ListID.GetValue();
                                }
                                //Get value of FullName
                                if (ORSalesOrderLineRet.SalesOrderLineRet.OverrideUOMSetRef.FullName != null)
                                {
                                    string FullName265 = (string)ORSalesOrderLineRet.SalesOrderLineRet.OverrideUOMSetRef.FullName.GetValue();
                                }
                            }
                            if (ORSalesOrderLineRet.SalesOrderLineRet.ORRate != null)
                            {
                                if (ORSalesOrderLineRet.SalesOrderLineRet.ORRate.Rate != null)
                                {
                                    //Get value of Rate
                                    if (ORSalesOrderLineRet.SalesOrderLineRet.ORRate.Rate != null)
                                    {
                                        double Rate266 = (double)ORSalesOrderLineRet.SalesOrderLineRet.ORRate.Rate.GetValue();
                                    }
                                }
                                if (ORSalesOrderLineRet.SalesOrderLineRet.ORRate.RatePercent != null)
                                {
                                    //Get value of RatePercent
                                    if (ORSalesOrderLineRet.SalesOrderLineRet.ORRate.RatePercent != null)
                                    {
                                        double RatePercent267 = (double)ORSalesOrderLineRet.SalesOrderLineRet.ORRate.RatePercent.GetValue();
                                    }
                                }
                            }
                            if (ORSalesOrderLineRet.SalesOrderLineRet.ClassRef != null)
                            {
                                //Get value of ListID
                                if (ORSalesOrderLineRet.SalesOrderLineRet.ClassRef.ListID != null)
                                {
                                    string ListID268 = (string)ORSalesOrderLineRet.SalesOrderLineRet.ClassRef.ListID.GetValue();
                                }
                                //Get value of FullName
                                if (ORSalesOrderLineRet.SalesOrderLineRet.ClassRef.FullName != null)
                                {
                                    string FullName269 = (string)ORSalesOrderLineRet.SalesOrderLineRet.ClassRef.FullName.GetValue();
                                }
                            }
                            //Get value of Amount
                            if (ORSalesOrderLineRet.SalesOrderLineRet.Amount != null)
                            {
                                double Amount270 = (double)ORSalesOrderLineRet.SalesOrderLineRet.Amount.GetValue();
                            }
                            if (ORSalesOrderLineRet.SalesOrderLineRet.InventorySiteRef != null)
                            {
                                //Get value of ListID
                                if (ORSalesOrderLineRet.SalesOrderLineRet.InventorySiteRef.ListID != null)
                                {
                                    string ListID271 = (string)ORSalesOrderLineRet.SalesOrderLineRet.InventorySiteRef.ListID.GetValue();
                                }
                                //Get value of FullName
                                if (ORSalesOrderLineRet.SalesOrderLineRet.InventorySiteRef.FullName != null)
                                {
                                    string FullName272 = (string)ORSalesOrderLineRet.SalesOrderLineRet.InventorySiteRef.FullName.GetValue();
                                }
                            }
                            if (ORSalesOrderLineRet.SalesOrderLineRet.InventorySiteLocationRef != null)
                            {
                                //Get value of ListID
                                if (ORSalesOrderLineRet.SalesOrderLineRet.InventorySiteLocationRef.ListID != null)
                                {
                                    string ListID273 = (string)ORSalesOrderLineRet.SalesOrderLineRet.InventorySiteLocationRef.ListID.GetValue();
                                }
                                //Get value of FullName
                                if (ORSalesOrderLineRet.SalesOrderLineRet.InventorySiteLocationRef.FullName != null)
                                {
                                    string FullName274 = (string)ORSalesOrderLineRet.SalesOrderLineRet.InventorySiteLocationRef.FullName.GetValue();
                                }
                            }
                            if (ORSalesOrderLineRet.SalesOrderLineRet.ORSerialLotNumber != null)
                            {
                                if (ORSalesOrderLineRet.SalesOrderLineRet.ORSerialLotNumber.SerialNumber != null)
                                {
                                    //Get value of SerialNumber
                                    if (ORSalesOrderLineRet.SalesOrderLineRet.ORSerialLotNumber.SerialNumber != null)
                                    {
                                        string SerialNumber275 = (string)ORSalesOrderLineRet.SalesOrderLineRet.ORSerialLotNumber.SerialNumber.GetValue();
                                    }
                                }
                                if (ORSalesOrderLineRet.SalesOrderLineRet.ORSerialLotNumber.LotNumber != null)
                                {
                                    //Get value of LotNumber
                                    if (ORSalesOrderLineRet.SalesOrderLineRet.ORSerialLotNumber.LotNumber != null)
                                    {
                                        string LotNumber276 = (string)ORSalesOrderLineRet.SalesOrderLineRet.ORSerialLotNumber.LotNumber.GetValue();
                                    }
                                }
                            }
                            if (ORSalesOrderLineRet.SalesOrderLineRet.SalesTaxCodeRef != null)
                            {
                                //Get value of ListID
                                if (ORSalesOrderLineRet.SalesOrderLineRet.SalesTaxCodeRef.ListID != null)
                                {
                                    string ListID277 = (string)ORSalesOrderLineRet.SalesOrderLineRet.SalesTaxCodeRef.ListID.GetValue();
                                }
                                //Get value of FullName
                                if (ORSalesOrderLineRet.SalesOrderLineRet.SalesTaxCodeRef.FullName != null)
                                {
                                    string FullName278 = (string)ORSalesOrderLineRet.SalesOrderLineRet.SalesTaxCodeRef.FullName.GetValue();
                                }
                            }
                            //Get value of Invoiced
                            if (ORSalesOrderLineRet.SalesOrderLineRet.Invoiced != null)
                            {
                                int Invoiced279 = (int)ORSalesOrderLineRet.SalesOrderLineRet.Invoiced.GetValue();
                            }
                            //Get value of IsManuallyClosed
                            if (ORSalesOrderLineRet.SalesOrderLineRet.IsManuallyClosed != null)
                            {
                                bool IsManuallyClosed280 = (bool)ORSalesOrderLineRet.SalesOrderLineRet.IsManuallyClosed.GetValue();
                            }
                            //Get value of Other1
                            if (ORSalesOrderLineRet.SalesOrderLineRet.Other1 != null)
                            {
                                string Other1281 = (string)ORSalesOrderLineRet.SalesOrderLineRet.Other1.GetValue();
                            }
                            //Get value of Other2
                            if (ORSalesOrderLineRet.SalesOrderLineRet.Other2 != null)
                            {
                                string Other2282 = (string)ORSalesOrderLineRet.SalesOrderLineRet.Other2.GetValue();
                            }
                            if (ORSalesOrderLineRet.SalesOrderLineRet.DataExtRetList != null)
                            {
                                for (int i283 = 0; i283 < ORSalesOrderLineRet.SalesOrderLineRet.DataExtRetList.Count; i283++)
                                {
                                    IDataExtRet DataExtRet = ORSalesOrderLineRet.SalesOrderLineRet.DataExtRetList.GetAt(i283);
                                    //Get value of OwnerID
                                    if (DataExtRet.OwnerID != null)
                                    {
                                        string OwnerID284 = (string)DataExtRet.OwnerID.GetValue();
                                    }
                                    //Get value of DataExtName
                                    string DataExtName285 = (string)DataExtRet.DataExtName.GetValue();
                                    //Get value of DataExtType
                                    ENDataExtType DataExtType286 = (ENDataExtType)DataExtRet.DataExtType.GetValue();
                                    //Get value of DataExtValue
                                    string DataExtValue287 = (string)DataExtRet.DataExtValue.GetValue();
                                }
                            }
                        }
                    }
                    if (ORSalesOrderLineRet.SalesOrderLineGroupRet != null)
                    {
                        if (ORSalesOrderLineRet.SalesOrderLineGroupRet != null)
                        {
                            //Get value of TxnLineID
                            string TxnLineID288 = (string)ORSalesOrderLineRet.SalesOrderLineGroupRet.TxnLineID.GetValue();
                            //Get value of ListID
                            if (ORSalesOrderLineRet.SalesOrderLineGroupRet.ItemGroupRef.ListID != null)
                            {
                                string ListID289 = (string)ORSalesOrderLineRet.SalesOrderLineGroupRet.ItemGroupRef.ListID.GetValue();
                            }
                            //Get value of FullName
                            if (ORSalesOrderLineRet.SalesOrderLineGroupRet.ItemGroupRef.FullName != null)
                            {
                                string FullName290 = (string)ORSalesOrderLineRet.SalesOrderLineGroupRet.ItemGroupRef.FullName.GetValue();
                            }
                            //Get value of Desc
                            if (ORSalesOrderLineRet.SalesOrderLineGroupRet.Desc != null)
                            {
                                string Desc291 = (string)ORSalesOrderLineRet.SalesOrderLineGroupRet.Desc.GetValue();
                            }
                            //Get value of Quantity
                            if (ORSalesOrderLineRet.SalesOrderLineGroupRet.Quantity != null)
                            {
                                int Quantity292 = (int)ORSalesOrderLineRet.SalesOrderLineGroupRet.Quantity.GetValue();
                            }
                            //Get value of UnitOfMeasure
                            if (ORSalesOrderLineRet.SalesOrderLineGroupRet.UnitOfMeasure != null)
                            {
                                string UnitOfMeasure293 = (string)ORSalesOrderLineRet.SalesOrderLineGroupRet.UnitOfMeasure.GetValue();
                            }
                            if (ORSalesOrderLineRet.SalesOrderLineGroupRet.OverrideUOMSetRef != null)
                            {
                                //Get value of ListID
                                if (ORSalesOrderLineRet.SalesOrderLineGroupRet.OverrideUOMSetRef.ListID != null)
                                {
                                    string ListID294 = (string)ORSalesOrderLineRet.SalesOrderLineGroupRet.OverrideUOMSetRef.ListID.GetValue();
                                }
                                //Get value of FullName
                                if (ORSalesOrderLineRet.SalesOrderLineGroupRet.OverrideUOMSetRef.FullName != null)
                                {
                                    string FullName295 = (string)ORSalesOrderLineRet.SalesOrderLineGroupRet.OverrideUOMSetRef.FullName.GetValue();
                                }
                            }
                            //Get value of IsPrintItemsInGroup
                            bool IsPrintItemsInGroup296 = (bool)ORSalesOrderLineRet.SalesOrderLineGroupRet.IsPrintItemsInGroup.GetValue();
                            //Get value of TotalAmount
                            double TotalAmount297 = (double)ORSalesOrderLineRet.SalesOrderLineGroupRet.TotalAmount.GetValue();
                            if (ORSalesOrderLineRet.SalesOrderLineGroupRet.SalesOrderLineRetList != null)
                            {
                                for (int i298 = 0; i298 < ORSalesOrderLineRet.SalesOrderLineGroupRet.SalesOrderLineRetList.Count; i298++)
                                {
                                    ISalesOrderLineRet SalesOrderLineRet = ORSalesOrderLineRet.SalesOrderLineGroupRet.SalesOrderLineRetList.GetAt(i298);
                                    //Get value of TxnLineID
                                    string TxnLineID299 = (string)SalesOrderLineRet.TxnLineID.GetValue();
                                    if (SalesOrderLineRet.ItemRef != null)
                                    {
                                        //Get value of ListID
                                        if (SalesOrderLineRet.ItemRef.ListID != null)
                                        {
                                            string ListID300 = (string)SalesOrderLineRet.ItemRef.ListID.GetValue();
                                        }
                                        //Get value of FullName
                                        if (SalesOrderLineRet.ItemRef.FullName != null)
                                        {
                                            string FullName301 = (string)SalesOrderLineRet.ItemRef.FullName.GetValue();
                                        }
                                    }
                                    //Get value of Desc
                                    if (SalesOrderLineRet.Desc != null)
                                    {
                                        string Desc302 = (string)SalesOrderLineRet.Desc.GetValue();
                                    }
                                    //Get value of Quantity
                                    if (SalesOrderLineRet.Quantity != null)
                                    {
                                        int Quantity303 = (int)SalesOrderLineRet.Quantity.GetValue();
                                    }
                                    //Get value of UnitOfMeasure
                                    if (SalesOrderLineRet.UnitOfMeasure != null)
                                    {
                                        string UnitOfMeasure304 = (string)SalesOrderLineRet.UnitOfMeasure.GetValue();
                                    }
                                    if (SalesOrderLineRet.OverrideUOMSetRef != null)
                                    {
                                        //Get value of ListID
                                        if (SalesOrderLineRet.OverrideUOMSetRef.ListID != null)
                                        {
                                            string ListID305 = (string)SalesOrderLineRet.OverrideUOMSetRef.ListID.GetValue();
                                        }
                                        //Get value of FullName
                                        if (SalesOrderLineRet.OverrideUOMSetRef.FullName != null)
                                        {
                                            string FullName306 = (string)SalesOrderLineRet.OverrideUOMSetRef.FullName.GetValue();
                                        }
                                    }
                                    if (SalesOrderLineRet.ORRate != null)
                                    {
                                        if (SalesOrderLineRet.ORRate.Rate != null)
                                        {
                                            //Get value of Rate
                                            if (SalesOrderLineRet.ORRate.Rate != null)
                                            {
                                                double Rate307 = (double)SalesOrderLineRet.ORRate.Rate.GetValue();
                                            }
                                        }
                                        if (SalesOrderLineRet.ORRate.RatePercent != null)
                                        {
                                            //Get value of RatePercent
                                            if (SalesOrderLineRet.ORRate.RatePercent != null)
                                            {
                                                double RatePercent308 = (double)SalesOrderLineRet.ORRate.RatePercent.GetValue();
                                            }
                                        }
                                    }
                                    if (SalesOrderLineRet.ClassRef != null)
                                    {
                                        //Get value of ListID
                                        if (SalesOrderLineRet.ClassRef.ListID != null)
                                        {
                                            string ListID309 = (string)SalesOrderLineRet.ClassRef.ListID.GetValue();
                                        }
                                        //Get value of FullName
                                        if (SalesOrderLineRet.ClassRef.FullName != null)
                                        {
                                            string FullName310 = (string)SalesOrderLineRet.ClassRef.FullName.GetValue();
                                        }
                                    }
                                    //Get value of Amount
                                    if (SalesOrderLineRet.Amount != null)
                                    {
                                        double Amount311 = (double)SalesOrderLineRet.Amount.GetValue();
                                    }
                                    if (SalesOrderLineRet.InventorySiteRef != null)
                                    {
                                        //Get value of ListID
                                        if (SalesOrderLineRet.InventorySiteRef.ListID != null)
                                        {
                                            string ListID312 = (string)SalesOrderLineRet.InventorySiteRef.ListID.GetValue();
                                        }
                                        //Get value of FullName
                                        if (SalesOrderLineRet.InventorySiteRef.FullName != null)
                                        {
                                            string FullName313 = (string)SalesOrderLineRet.InventorySiteRef.FullName.GetValue();
                                        }
                                    }
                                    if (SalesOrderLineRet.InventorySiteLocationRef != null)
                                    {
                                        //Get value of ListID
                                        if (SalesOrderLineRet.InventorySiteLocationRef.ListID != null)
                                        {
                                            string ListID314 = (string)SalesOrderLineRet.InventorySiteLocationRef.ListID.GetValue();
                                        }
                                        //Get value of FullName
                                        if (SalesOrderLineRet.InventorySiteLocationRef.FullName != null)
                                        {
                                            string FullName315 = (string)SalesOrderLineRet.InventorySiteLocationRef.FullName.GetValue();
                                        }
                                    }
                                    if (SalesOrderLineRet.ORSerialLotNumber != null)
                                    {
                                        if (SalesOrderLineRet.ORSerialLotNumber.SerialNumber != null)
                                        {
                                            //Get value of SerialNumber
                                            if (SalesOrderLineRet.ORSerialLotNumber.SerialNumber != null)
                                            {
                                                string SerialNumber316 = (string)SalesOrderLineRet.ORSerialLotNumber.SerialNumber.GetValue();
                                            }
                                        }
                                        if (SalesOrderLineRet.ORSerialLotNumber.LotNumber != null)
                                        {
                                            //Get value of LotNumber
                                            if (SalesOrderLineRet.ORSerialLotNumber.LotNumber != null)
                                            {
                                                string LotNumber317 = (string)SalesOrderLineRet.ORSerialLotNumber.LotNumber.GetValue();
                                            }
                                        }
                                    }
                                    if (SalesOrderLineRet.SalesTaxCodeRef != null)
                                    {
                                        //Get value of ListID
                                        if (SalesOrderLineRet.SalesTaxCodeRef.ListID != null)
                                        {
                                            string ListID318 = (string)SalesOrderLineRet.SalesTaxCodeRef.ListID.GetValue();
                                        }
                                        //Get value of FullName
                                        if (SalesOrderLineRet.SalesTaxCodeRef.FullName != null)
                                        {
                                            string FullName319 = (string)SalesOrderLineRet.SalesTaxCodeRef.FullName.GetValue();
                                        }
                                    }
                                    //Get value of Invoiced
                                    if (SalesOrderLineRet.Invoiced != null)
                                    {
                                        int Invoiced320 = (int)SalesOrderLineRet.Invoiced.GetValue();
                                    }
                                    //Get value of IsManuallyClosed
                                    if (SalesOrderLineRet.IsManuallyClosed != null)
                                    {
                                        bool IsManuallyClosed321 = (bool)SalesOrderLineRet.IsManuallyClosed.GetValue();
                                    }
                                    //Get value of Other1
                                    if (SalesOrderLineRet.Other1 != null)
                                    {
                                        string Other1322 = (string)SalesOrderLineRet.Other1.GetValue();
                                    }
                                    //Get value of Other2
                                    if (SalesOrderLineRet.Other2 != null)
                                    {
                                        string Other2323 = (string)SalesOrderLineRet.Other2.GetValue();
                                    }
                                    if (SalesOrderLineRet.DataExtRetList != null)
                                    {
                                        for (int i324 = 0; i324 < SalesOrderLineRet.DataExtRetList.Count; i324++)
                                        {
                                            IDataExtRet DataExtRet = SalesOrderLineRet.DataExtRetList.GetAt(i324);
                                            //Get value of OwnerID
                                            if (DataExtRet.OwnerID != null)
                                            {
                                                string OwnerID325 = (string)DataExtRet.OwnerID.GetValue();
                                            }
                                            //Get value of DataExtName
                                            string DataExtName326 = (string)DataExtRet.DataExtName.GetValue();
                                            //Get value of DataExtType
                                            ENDataExtType DataExtType327 = (ENDataExtType)DataExtRet.DataExtType.GetValue();
                                            //Get value of DataExtValue
                                            string DataExtValue328 = (string)DataExtRet.DataExtValue.GetValue();
                                        }
                                    }
                                }
                            }
                            if (ORSalesOrderLineRet.SalesOrderLineGroupRet.DataExtRetList != null)
                            {
                                for (int i329 = 0; i329 < ORSalesOrderLineRet.SalesOrderLineGroupRet.DataExtRetList.Count; i329++)
                                {
                                    IDataExtRet DataExtRet = ORSalesOrderLineRet.SalesOrderLineGroupRet.DataExtRetList.GetAt(i329);
                                    //Get value of OwnerID
                                    if (DataExtRet.OwnerID != null)
                                    {
                                        string OwnerID330 = (string)DataExtRet.OwnerID.GetValue();
                                    }
                                    //Get value of DataExtName
                                    string DataExtName331 = (string)DataExtRet.DataExtName.GetValue();
                                    //Get value of DataExtType
                                    ENDataExtType DataExtType332 = (ENDataExtType)DataExtRet.DataExtType.GetValue();
                                    //Get value of DataExtValue
                                    string DataExtValue333 = (string)DataExtRet.DataExtValue.GetValue();
                                }
                            }
                        }
                    }
                }
            }
            if (SalesOrderRet.DataExtRetList != null)
            {
                for (int i334 = 0; i334 < SalesOrderRet.DataExtRetList.Count; i334++)
                {
                    IDataExtRet DataExtRet = SalesOrderRet.DataExtRetList.GetAt(i334);
                    //Get value of OwnerID
                    if (DataExtRet.OwnerID != null)
                    {
                        string OwnerID335 = (string)DataExtRet.OwnerID.GetValue();
                    }
                    //Get value of DataExtName
                    string DataExtName336 = (string)DataExtRet.DataExtName.GetValue();
                    //Get value of DataExtType
                    ENDataExtType DataExtType337 = (ENDataExtType)DataExtRet.DataExtType.GetValue();
                    //Get value of DataExtValue
                    string DataExtValue338 = (string)DataExtRet.DataExtValue.GetValue();
                }
            }
        }



    }

   

}

