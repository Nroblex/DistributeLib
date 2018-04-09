using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ComTestWCFService.AlarmStatus
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ComTestManagement" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ComTestManagement.svc or ComTestManagement.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class ComTestManagement : IComTestManagement
    {
        public void DoWork()
        {
        }

        public string GetVersion()
        {
            return "1.2.3";
        }

        public ServerReply PingResponse()
        {
            return new ServerReply() { reply = "OK", retCode = 200 };
        }
    }
}
