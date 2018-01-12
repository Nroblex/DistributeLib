using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DistributeLib
{
    public class SendZipFile : EventArgs
    {
        
        private ZipFileCreated mZipFileCreated;
        private GitUtilCommnad mGitutilCommand;
        
        delegate void OnZipFileFTPFinished();

        public SendZipFile(ZipFileCreated zipFile, GitUtilCommnad gitUtilCommand)
        {
            this.mGitutilCommand = gitUtilCommand;
            this.mZipFileCreated = zipFile;
            SendAsync();
        }


        private void SendAsync()
        {
            using (WebClient myWebClient = new WebClient())
            {
                try
                {
                    myWebClient.Credentials = new NetworkCredential(mGitutilCommand.ExternalFTPUser.Normalize(),
                    mGitutilCommand.ExternalFTPPassword.Normalize());

                    myWebClient.UploadFileCompleted += OnFileUploadCompleted;
                    myWebClient.UploadProgressChanged += OnProgressUpload;
                    
                    myWebClient.UploadFileAsync(new Uri(string.Format(@"ftp://{0}/{1}",
                        mGitutilCommand.ExternalFTPPath, 
                        new FileInfo(mGitutilCommand.CreatedZipFile).Name)), "STOR", mZipFileCreated.filePath, Guid.NewGuid().ToString());
                    
                    Uri tmp = new Uri(string.Format(@"ftp://{0}/{1}",
                    mGitutilCommand.ExternalFTPPath, new FileInfo(mGitutilCommand.CreatedZipFile).Name));

                }
                catch (Exception e)
                {
                    string error = e.Message;
                }
            }
        }

        private void OnProgressUpload(object sender, UploadProgressChangedEventArgs e)
        {
            Console.WriteLine("Sent {0} bytes of total {1}, percent = {2}", e.BytesSent, e.TotalBytesToSend, e.ProgressPercentage);
        }

        private void OnFileUploadCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine("There is a problem with the FTP transfer!");
                Console.WriteLine("If you are using a FTP sevrer behind a NAT, make sure to allow port redirect in firewall");
                Console.WriteLine();
                Console.WriteLine("Press return to exit.");
            }
            else
            {
                Console.WriteLine("File upload completed!");
                Console.WriteLine();
                Console.WriteLine("Press return to exit.");
            }
            
        }
    }
}
