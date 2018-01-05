using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
namespace DistributeLib
{
    public class DistributeWorker : QueueManager
    {
        GitUtilCommnad mGitUtilCommand;
        public delegate long dlgOnExecuteFinished();
        public event dlgOnExecuteFinished eventOnZipfileFinished;


        public DistributeWorker(GitUtilCommnad command)
        {
            mGitUtilCommand = command;
            //var task = ExecuteCommand();

            //Task<long> T = new Task<long>(CreateZipArchive);
            //T.ContinueWith(OnZipFileFinished);
            //T.Start();

            var result = CreateZipArchive();
            
        }

        private void OnZipFileFinished(Task obj)
        {

            eventOnZipfileFinished += DistributeWorker_Finished;
            eventOnZipfileFinished.Invoke();
        }

        private long DistributeWorker_Finished()
        {
            return 1;
        }

        private async Task<Boolean> ExecuteCommand()
        {
            bool result = false;
            await Task.Run(() =>
            {
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


            }).ContinueWith(OnZipFileFinished);

            return result;
        }

        private void DropExistingArchive(FileInfo fil)
        {
            try
            {
                fil.Delete();
            }
            catch { }
        }
        private async Task CreateZipArchive()
        {
            Func<long> function = new Func<long>(() => ZipIt());
            long result = await Task.Run(function);

            /*
            DirectoryInfo dir = new DirectoryInfo(mGitUtilCommand.PathToBuild);
            FileInfo resultZip = null;

            if (dir.Parent.Parent == null)
            {


                await Task.Run(() =>
                {
                    DropExistingArchive(new FileInfo(new DirectoryInfo(mGitUtilCommand.PathToBuild).Root + "\\" + mGitUtilCommand.BuildNumber + ".zip"));

                    ZipFile.CreateFromDirectory(mGitUtilCommand.PathToBuild,
                    new DirectoryInfo(mGitUtilCommand.PathToBuild).Root + "\\" + mGitUtilCommand.BuildNumber + ".zip", CompressionLevel.Optimal, true);
                    resultZip = new FileInfo(new DirectoryInfo(mGitUtilCommand.PathToBuild).Root + "\\" + mGitUtilCommand.BuildNumber + ".zip");

                });

            }
            else
            {
                await Task.Run(() =>
                {
                    DropExistingArchive(new FileInfo(new DirectoryInfo(mGitUtilCommand.PathToBuild).Parent.FullName + "\\" + mGitUtilCommand.BuildNumber + ".zip"));

                    ZipFile.CreateFromDirectory(mGitUtilCommand.PathToBuild,
                        new DirectoryInfo(mGitUtilCommand.PathToBuild).Parent.FullName + "\\" + mGitUtilCommand.BuildNumber + ".zip", CompressionLevel.Optimal, true);
                    resultZip = new FileInfo(new DirectoryInfo(mGitUtilCommand.PathToBuild).Parent.FullName + "\\" + mGitUtilCommand.BuildNumber + ".zip");
                });

            }
            return resultZip.Length;

            */
        }

        private long ZipIt()
        {
            DirectoryInfo dir = new DirectoryInfo(mGitUtilCommand.PathToBuild);
            FileInfo resultZip = null;

            if (dir.Parent.Parent == null)
            {

                DropExistingArchive(new FileInfo(new DirectoryInfo(mGitUtilCommand.PathToBuild).Root + "\\" + mGitUtilCommand.BuildNumber + ".zip"));

                ZipFile.CreateFromDirectory(mGitUtilCommand.PathToBuild,
                new DirectoryInfo(mGitUtilCommand.PathToBuild).Root + "\\" + mGitUtilCommand.BuildNumber + ".zip", CompressionLevel.Optimal, true);
                resultZip = new FileInfo(new DirectoryInfo(mGitUtilCommand.PathToBuild).Root + "\\" + mGitUtilCommand.BuildNumber + ".zip");


            }
            else
            {
                DropExistingArchive(new FileInfo(new DirectoryInfo(mGitUtilCommand.PathToBuild).Parent.FullName + "\\" + mGitUtilCommand.BuildNumber + ".zip"));

                ZipFile.CreateFromDirectory(mGitUtilCommand.PathToBuild,
                    new DirectoryInfo(mGitUtilCommand.PathToBuild).Parent.FullName + "\\" + mGitUtilCommand.BuildNumber + ".zip", CompressionLevel.Optimal, true);
                resultZip = new FileInfo(new DirectoryInfo(mGitUtilCommand.PathToBuild).Parent.FullName + "\\" + mGitUtilCommand.BuildNumber + ".zip");
            }

            return resultZip.Length;
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
