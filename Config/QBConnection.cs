using QBFC15Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcCodeFlowClientManual.Config
{
    public class QBConnection
    {
        private QBSessionManager sessionManager;

        public QBConnection()
        {
            sessionManager = new QBSessionManager();
            sessionManager.OpenConnection("", "Test");
            sessionManager.BeginSession("", ENOpenMode.omDontCare);
        }

        //public IResponse GetQBResponse()
        //{
        //   //IMsgSetRequest requestMsgSet = sessionManager.CreateMsgSetRequest("US", 13, 0);


        //    //ICustomerQuery customerQuery = requestMsgSet.AppendCustomerQueryRq();

        //    IMsgSetResponse responseMsgSet = sessionManager.DoRequests(requestMsgSet);

        //    IResponseList responseList = responseMsgSet.ResponseList;

        //    IResponse response = responseList.GetAt(0);
        //    return response;
        //}

        public QBSessionManager getSessionManager()
        {
            return sessionManager;
        }

    }
}