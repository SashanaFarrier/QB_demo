using MvcCodeFlowClientManual.Config;
using QBFC15Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Services
{
    public class ItemGroupQueryService
    {
        public QBConnection qBConnection = new QBConnection();

        private QBSessionManager sessionManager;
        private bool sessionBegun = false;
        private bool connectionOpen = false;

        public void GetItemGroup()
        {
            if (qBConnection.getSessionManager() != null)
            {
                try
                {
                    sessionManager = qBConnection.getSessionManager();
                    connectionOpen = true;

                    IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);

                    IItemGroupQuery itemGroupQuery = requestMsgSet.AppendItemGroupQueryRq();

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

                        IItemGroupRetList itemInventoryList = (IItemGroupRetList)response.Detail;
                        for (int i = 0; i < itemInventoryList.Count; i++)
                        {
                            IItemGroupRet itemInventoryRet = itemInventoryList.GetAt(i);
                            Console.WriteLine(itemInventoryRet.ItemGroupLineList.GetAt(i).ItemRef.FullName.GetValue());
                            Console.WriteLine(itemInventoryRet.ItemGroupLineList.GetAt(i).ItemRef.ListID.GetValue());
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        
                    
                    
        

                   

                        

       
                         
      }                      
    }
    
}