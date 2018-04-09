using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ComTestWCFService.AlarmStatus
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IComTestManagement" in both code and config file together.
    [ServiceContract]
    public interface IComTestManagement
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        [WebGet(UriTemplate ="/ping", RequestFormat =WebMessageFormat.Json, 
            ResponseFormat =WebMessageFormat.Json)]
        ServerReply PingResponse();

        [OperationContract]
        [WebGet(UriTemplate = "/version/", 
            RequestFormat =WebMessageFormat.Json, 
            ResponseFormat =WebMessageFormat.Json)]
        string GetVersion();

    }

}
