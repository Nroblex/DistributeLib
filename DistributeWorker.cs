using System.IO.Compression;
using System.Threading.Tasks;
namespace DistributeLib
{
    public class DistributeWorker
    {
        GitUtilCommnad mGitUtilCommand;
        public DistributeWorker(GitUtilCommnad command)
        {
            mGitUtilCommand = command;
            var t = ExecuteCommand();
        }

        private async Task ExecuteCommand()
        {
            await Task.Run(() =>
            {
                //mGitUtilCommand.PathToBuild
                ZipFile.CreateFromDirectory(mGitUtilCommand.PathToBuild, mGitUtilCommand.PathToBuild + "\\" + mGitUtilCommand.BuildNumber + ".zip");
                
            });
        }
    }
}
