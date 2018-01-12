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
            //SendFTP();
        }


        private void SendFTP()
        {
            FtpWebRequest ftpWebRequest;
            try
            {
                
                ftpWebRequest =(FtpWebRequest)WebRequest.Create(new Uri(string.Format(@"ftp://{0}/{1}",
                    mGitutilCommand.ExternalFTPPath, new FileInfo(mGitutilCommand.CreatedZipFile).Name)));

                ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.UsePassive = true;
                ftpWebRequest.KeepAlive = true;
                ftpWebRequest.Credentials = new NetworkCredential(mGitutilCommand.ExternalFTPUser.Normalize(),
                    mGitutilCommand.ExternalFTPPassword.Normalize());
                
                using (FileStream fs = File.OpenRead(mGitutilCommand.CreatedZipFile))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    Stream requestStream = ftpWebRequest.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Flush();
                    requestStream.Close();
                }
            }
            catch(Exception ep)
            {
                string error = ep.Message;
            }
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
                    
                    myWebClient.UploadFileAsync(new Uri(string.Format(@"ftp://{0}/{1}",
                    mGitutilCommand.ExternalFTPPath, new FileInfo(mGitutilCommand.CreatedZipFile).Name)), "STOR", mZipFileCreated.filePath, Guid.NewGuid().ToString());
                    
                    Uri tmp = new Uri(string.Format(@"ftp://{0}/{1}",
                    mGitutilCommand.ExternalFTPPath, new FileInfo(mGitutilCommand.CreatedZipFile).Name));


                    String s = ";";

                }
                catch (Exception e)
                {
                    string error = e.Message;
                }

                //long bytes = await Task.myWebClient.UploadFileTaskAsync("ftp://localhost", mZipFileCreated.filePath)
            }
        }

        private void OnFileUploadCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                string err = e.Error.Message;

            }
        }
    }
}
