using System.Collections.Generic;
using System.IO;
using Renci.SshNet;

namespace Bodoconsult.Core.Web.Ftp
{
    public class SshHandler
    {

        private SftpClient _sftp;

        private readonly SshCredentials _credentials;


        public SshHandler(SshCredentials credentials)
        {
            _credentials = credentials;
        }


        /// <summary>
        /// Connect to the SFTP-Server
        /// </summary>
        public void Connect()
        {

            _sftp = new SftpClient(_credentials.Url, _credentials.Username, _credentials.Password);

            _sftp.Connect();

        }

        /// <summary>
        /// Is SFTP server connected?
        /// </summary>
        public bool IsConnected => _sftp != null && _sftp.IsConnected;


        /// <summary>
        /// Disconnect from SFTP-Server
        /// </summary>
        public void Disconnect()
        {
            _sftp.Disconnect();
            _sftp.Dispose();
        }

        /// <summary>
        /// Delete a remote file on the SFTP server
        /// </summary>
        /// <param name="remotePath"></param>
        public void RemoveFile(string remotePath)
        {
            try
            {
                if (_sftp.Exists(remotePath))
                {
                    _sftp.DeleteFile(remotePath);
                }
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Upload a local file to the remote SFTP server
        /// </summary>
        /// <param name="localPath"></param>
        /// <param name="remotePath"></param>
        public void Put(string localPath, string remotePath)
        {

            var path = Path.GetDirectoryName(remotePath).Replace("\\", "/");
            var fileName = Path.GetFileName(remotePath);

            _sftp.ChangeDirectory(path);

            using (var fileStream = new FileStream(localPath, FileMode.Open))
            {
                _sftp.BufferSize = 4 * 1024; // bypass Payload error large files 
                _sftp.UploadFile(fileStream, fileName, true);
            }

        }

        /// <summary>
        /// Exists path on SFTP server
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool Exists(string path)
        {
            return _sftp.Exists(path);
        }



        /// <summary>
        /// Create a remote directory on the SFTP server
        /// </summary>
        /// <param name="remotePath"></param>
        public void CreateDirectory(string remotePath)
        {

            _sftp.CreateDirectory(remotePath);

        }


        public void RemoveDirectory(string remotePath)
        {
            try
            {
                if (_sftp.Exists(remotePath))
                {
                    _sftp.DeleteDirectory(remotePath);
                }
            }
            catch
            {
                // Ignored
            }

        }


        public IEnumerable<Renci.SshNet.Sftp.SftpFile> GetDirectoryItemsRaw(string remotePath)
        {
            return _sftp.ListDirectory(remotePath);

            //string remoteFileName = file.Name;
            //if ((!file.Name.StartsWith(".")) && ((file.LastWriteTime.Date == DateTime.Today))

            //    using (Stream file1 = File.OpenWrite(localDirectory + remoteFileName))
            //    { 
            //        sftp.DownloadFile(remoteDirectory + remoteFileName, file1);
            //    }
        }


        public IEnumerable<SftpFileItem> GetDirectoryItems(string remotePath)
        {
            var result = new List<SftpFileItem>();
            
            
            var ftp = _sftp.ListDirectory(remotePath);

            foreach (var item in ftp)
            {
                var i = new SftpFileItem
                {
                    Name = item.Name,
                    FullName = item.FullName,
                    IsDirectory = item.IsDirectory,
                    LastAccessTime = item.LastAccessTime,
                    LastAccessTimeUtc = item.LastAccessTimeUtc,
                    LastWriteTime = item.LastWriteTime,
                    LastWriteTimeUtc = item.LastWriteTimeUtc,
                    Length = item.Length
                };

                result.Add(i);
            }

            //string remoteFileName = file.Name;
            //if ((!file.Name.StartsWith(".")) && ((file.LastWriteTime.Date == DateTime.Today))

            //    using (Stream file1 = File.OpenWrite(localDirectory + remoteFileName))
            //    { 
            //        sftp.DownloadFile(remoteDirectory + remoteFileName, file1);
            //    }

            return result;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="remotePath"></param>
        /// <param name="localPath"></param>
        public void DownloadFile(string remotePath, string localPath)
        {
            using (Stream file1 = File.OpenWrite(localPath))
            {
                _sftp.DownloadFile(remotePath, file1);
            }

        }

    }
}
