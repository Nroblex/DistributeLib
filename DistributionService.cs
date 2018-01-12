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
        private GitUtilCommnad mGitUtilCommand;
        DistributeManager distributeWorker =null;
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "data/{id}")]
        public Person GetPerson(string id)
        {
            return new Person()
            {
                id = Convert.ToInt32(id),
                name = "Anders Selborn"

            };
        }


        [WebInvoke(UriTemplate = "/UpgradeSoftware",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            Method = "POST")]
        public bool UpgradeSoftware(GitUtilCommnad command)
        {

            //Task T = new Task(new Action(RunDistribute(order: order))).ContinueWith(OnDistributeFinished);
            mGitUtilCommand = command;

            distributeWorker = new DistributeManager(mGitUtilCommand);
            distributeWorker.eventFileZipped += BeginOnFileZipped;
           
          
            return true;
        }

        private void BeginOnFileZipped(object sender, EventArgs e)
        {
            ZipFileCreated myZipFile = (ZipFileCreated)e;
            Console.WriteLine("My ZipFile was created, name = {0}, size = {1}", myZipFile.filePath, myZipFile.fileSize);

            SendZipFile sendZipFile = new SendZipFile(myZipFile,mGitUtilCommand);
            

        }

       
        private void OnDistributeFinished(Task obj)
        {
            string r = "Distribute finished.";
        }

        public string GetValue(int value)
        {
            return "You enetered " + value;
        }
    }
}
