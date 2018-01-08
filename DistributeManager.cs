using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
namespace DistributeLib
{
    public class DistributeManager
    {
        GitUtilCommnad mGitUtilCommand;

        public delegate void dlgOnZipCreated(object sender, ZipFileCreated e);
        public event dlgOnZipCreated eventFileZipped;

        
        protected virtual void OnFileZipped(ZipFileCreated zipFileCreated)
        {
            eventFileZipped?.Invoke(this, zipFileCreated);
        }
        

        public DistributeManager(GitUtilCommnad command)
        {

            mGitUtilCommand = command;
            var result = CreateZipArchive();
            
        }

        

        private void DropExistingArchive(FileInfo fil)
        {
            try
            {
                fil.Delete();
                Console.WriteLine("Deleted existing file.");
            }
            catch { }
        }
        private async Task CreateZipArchive()
        {
            /*
            Func<long> function = new Func<long>(() => ZipIt());
            long result = await Task.Run(function).ContinueWith(OnZipFileFinished);
            */

            Func<ZipFileCreated> zipFunction = new Func<ZipFileCreated>(() => ZipIt());
            ZipFileCreated myZipFile = await Task.Run(zipFunction).ContinueWith(OnZipFileFinished);
     
        }

     
        private ZipFileCreated OnZipFileFinished(Task<ZipFileCreated> arg)
        {
            Console.WriteLine("OnZipFileFinished says: {0}", arg.Result.ToString());

            ZipFileCreated fileCreated = new ZipFileCreated();
            fileCreated.filePath = arg.Result.filePath;
            fileCreated.fileSize = arg.Result.fileSize;

            OnFileZipped(fileCreated);

            return fileCreated;
        }

        

        private ZipFileCreated ZipIt()
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


            return new ZipFileCreated() { filePath = resultZip.FullName, fileSize = resultZip.Length };
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
