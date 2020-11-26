using System;
using System.Collections.Generic;
using System.Text;

namespace Bodoconsult.Core.Web.Ftp
{
    /// <summary>
    /// Credentials for SFTP login
    /// </summary>
    public class SshCredentials
    {
        /// <summary>
        /// FTP server address 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Username for the FTP server
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password for the FTP server
        /// </summary>
        public string Password { get; set; }

    }
}
