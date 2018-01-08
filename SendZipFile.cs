using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DistributeLib
{
    public class SendZipFile : EventArgs
    {
        ZipFileCreated mZipFileCreated;
        string mFtpUser;
        string mFtpPassword;
        delegate void OnZipFileFTPFinished();

        public SendZipFile(ZipFileCreated zipFile, string ftpUser, string ftpPassword)
        {
            this.mZipFileCreated = zipFile;
            this.mFtpUser = ftpUser;
            this.mFtpPassword = ftpPassword;

            Send();
        }

        private void Send()
        {
            using (WebClient myWebClient = new WebClient())
            {
                myWebClient.Credentials = new NetworkCredential(mFtpUser.Normalize(), mFtpPassword.Normalize());
                
                myWebClient.UploadFileCompleted += OnFileUploadCompleted;
                myWebClient.UploadFileAsync(new Uri("ftp://localhost/file.zip"), "STOR", mZipFileCreated.filePath, Guid.NewGuid().ToString());

                //long bytes = await Task.myWebClient.UploadFileTaskAsync("ftp://localhost", mZipFileCreated.filePath)
            }
        }

        private void OnFileUploadCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            String s = ";";
        }
    }
}
