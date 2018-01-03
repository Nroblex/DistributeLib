using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace DistributeLib
{
    [ServiceContract]
    public interface IDistributeService
    {
        [OperationContract]
        string GetValue(int value);

        [OperationContract]
        Person GetPerson(string id);

        [OperationContract]
        bool UpgradeSoftware(GitUtilCommnad order);
    }

    
}
