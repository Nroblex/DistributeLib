using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace DistributeLib
{
    public class Distribute : IDistributeService
    {
        [WebInvoke(Method ="GET",
            ResponseFormat =WebMessageFormat.Json,
            UriTemplate ="data/{id}")]
        public Person GetPerson(string id)
        {
            return new Person()
            {
                id = Convert.ToInt32(id),
                name="Anders Selborn"

            };
        }


        [WebInvoke(UriTemplate = "/UpgradeSoftware",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            Method = "POST")]
        public bool UpgradeSoftware(GitUtilCommnad order)
        {
            DistributeWorker distributeWorker = new DistributeWorker(order);

            return true;
        }

        public string GetValue(int value)
        {
            return "You enetered " + value;
        }
    }
}
