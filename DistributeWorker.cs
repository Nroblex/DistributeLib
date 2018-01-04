using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
namespace DistributeLib
{
    public class DistributeWorker : QueueManager
    {
        GitUtilCommnad mGitUtilCommand;
        public DistributeWorker(GitUtilCommnad command)
        {
            
            var task = ExecuteCommand();

            String s = "anders";
        }

     
        private async Task<Boolean> ExecuteCommand()
        {
            bool result = false;
            await Task.Run(() =>
            {
                //mGitUtilCommand.PathToBuild
                try
                {
                    ZipFile.CreateFromDirectory(@mGitUtilCommand.PathToBuild,
                        new DirectoryInfo(mGitUtilCommand.PathToBuild).Parent.FullName + "\\" + mGitUtilCommand.BuildNumber + ".zip", CompressionLevel.Optimal, true);
                    result = true;
                }
                catch (Exception ep)
                {
                    result = false;
                }


            });

            return result;
        }


        private async Task<bool> ExecuteAndZip()
        {
            await Task.Run(() =>
           {
               ZipFile.CreateFromDirectory(mGitUtilCommand.PathToBuild,
                    new DirectoryInfo(mGitUtilCommand.PathToBuild).Parent + "\\" + mGitUtilCommand.BuildNumber + ".zip", CompressionLevel.Optimal, true);


           });

            return true;

        }


    }
}
