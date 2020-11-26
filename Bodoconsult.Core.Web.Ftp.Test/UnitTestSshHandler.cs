using System.IO;
using System.Linq;
using Bodoconsult.Core.Web.Ftp.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Web.Ftp.Test
{
    [TestFixture]
    public class UnitTestSshHandler
    {

        private readonly SshCredentials _credentials = TestHelper.GetCredentials();

        [Test]
        public void TestConnect()
        {

            // Arrange
            var s = new SshHandler(_credentials);
            
            // Act
            s.Connect();

            var erg = s.IsConnected;

            s.Disconnect();

            // Assert
            Assert.IsTrue(erg);
        }


        [Test]
        public void TestPutMainDirectory()
        {

            // Arrange
            var localPath = TestHelper.FtpTestFilePath;

            const string remotePath = "/"+TestHelper.FtpTestFileName;

            var s = new SshHandler(_credentials);


            // Act
            s.Connect();

            var erg1 = s.IsConnected;

            
            s.Put(localPath, remotePath);

            s.Disconnect();

            // Assert
            Assert.IsTrue(erg1);
        }


        [Test]
        public void TestPutDirectory()
        {

            // Arrange
            var localPath = TestHelper.FtpTestFilePath;

            var remotePath = $"{TestHelper.FtpSubDir}/{TestHelper.FtpTestFileName}";

            var s = new SshHandler(_credentials);


            // Act
            s.Connect();

            var erg1 = s.IsConnected;


            s.Put(localPath, remotePath);

            s.Disconnect();

            // Assert
            Assert.IsTrue(erg1);
        }


        [Test]
        public void TestRemoveFile()
        {

            // Arrange
            var localPath = TestHelper.FtpTestFilePath;

            var remotePath = $"{TestHelper.FtpSubDir}/{TestHelper.FtpTestFileName}";

            var s = new SshHandler(_credentials);


            // Act
            s.Connect();

            var erg1 = s.IsConnected;

            s.Put(localPath, remotePath);

            Assert.IsTrue(s.Exists(remotePath));

            // Act
            s.RemoveFile(remotePath);
            var erg2 = s.Exists(remotePath);
            s.Disconnect();

            // Assert
            Assert.IsTrue(erg1);
            Assert.IsFalse(erg2);
        }


        [Test]
        public void TestRemoveFileFileNotExisting()
        {

            // Arrange
            var localPath = TestHelper.FtpTestFilePath;

            var remotePath = $"{TestHelper.FtpSubDir}/AAA{TestHelper.FtpTestFileName}";

            var s = new SshHandler(_credentials);

            s.Connect();

            var erg1 = s.IsConnected;

            s.Put(localPath, remotePath);

            // Act
            s.RemoveFile(remotePath);
            var erg2 = s.Exists(remotePath);
            s.Disconnect();

            // Assert
            Assert.IsTrue(erg1);
            Assert.IsFalse(erg2);
        }


        [Test]
        public void TestCreateDirectory()
        {

            // Arrange
            var remotePath = TestHelper.FtpSubDirCreate;

            var s = new SshHandler(_credentials);


            // Act
            s.Connect();

            var erg1 = s.IsConnected;

            s.RemoveDirectory(remotePath);
            Assert.IsFalse(s.Exists(remotePath));

            // Act
            s.CreateDirectory(remotePath);
            var erg2 = s.Exists(remotePath);
            s.Disconnect();

            // Assert
            Assert.IsTrue(erg1);
            Assert.IsTrue(erg2);
        }


        [Test]
        public void TestListDirectoryRaw()
        {

            // Arrange
            const string remotePath = TestHelper.FtpSubDir;

            var remotePath1 = TestHelper.FtpSubDirCreate;

            var s = new SshHandler(_credentials);

            s.Connect();

            if (!s.Exists(remotePath1)) s.CreateDirectory(remotePath1);

            var erg1 = s.IsConnected;

            var localPath = TestHelper.FtpTestFilePath;

            var remotePathFile = $"{TestHelper.FtpSubDir}/{TestHelper.FtpTestFileName}";

            if (!s.Exists(remotePathFile)) s.Put(localPath, remotePathFile);

            // Act
            var erg2 = s.GetDirectoryItemsRaw(remotePath).ToList();

            s.Disconnect();

            // Assert
            Assert.IsTrue(erg1);
            Assert.IsTrue(erg2.Any());
            Assert.IsTrue(erg2.Any(x => x.IsDirectory));
            Assert.IsTrue(erg2.Any(x => !x.IsDirectory));
        }

        [Test]
        public void TestListDirectory()
        {

            // Arrange
            const string remotePath = TestHelper.FtpSubDir;

            var remotePath1 = TestHelper.FtpSubDirCreate;

            var s = new SshHandler(_credentials);

            s.Connect();

            if (!s.Exists(remotePath1)) s.CreateDirectory(remotePath1);

            var erg1 = s.IsConnected;

            var localPath = TestHelper.FtpTestFilePath;

            var remotePathFile = $"{TestHelper.FtpSubDir}/{TestHelper.FtpTestFileName}";

            if (!s.Exists(remotePathFile)) s.Put(localPath, remotePathFile);

            // Act
            var erg2 = s.GetDirectoryItems(remotePath).ToList();

            s.Disconnect();

            // Assert
            Assert.IsTrue(erg1);
            Assert.IsTrue(erg2.Any());
            Assert.IsTrue(erg2.Any(x => x.IsDirectory));
            Assert.IsTrue(erg2.Any(x => !x.IsDirectory));
        }

        [Test]
        public void TestDownloadFile()
        {

            // Arrange
            var remotePath = "/"+ TestHelper.FtpTestFileName;
            var localPath = Path.Combine(TestHelper.LocalTargetPath,"AAA.txt");

            var s = new SshHandler(_credentials);


            if (File.Exists(localPath))
            {
                File.Delete(localPath);
            }


            // Act
            s.Connect();

            var erg1 = s.IsConnected;

            // Act
            s.DownloadFile(remotePath, localPath);
            s.Disconnect();

            var erg2 = File.Exists(localPath);

            // Assert
            Assert.IsTrue(erg1);
            Assert.IsTrue(erg2);
        }


        [Test]
        public void TestRemoveDirectory()
        {

            // Arrange
            var remotePath1 = TestHelper.FtpSubDirCreate;

            var s = new SshHandler(_credentials);

            s.Connect();

            if (!s.Exists(remotePath1)) s.CreateDirectory(remotePath1);

            var erg1 = s.IsConnected;

            // Act
            s.RemoveDirectory(remotePath1);

            // Assert
            var erg2 = s.Exists(remotePath1);

            s.Disconnect();

            
            Assert.IsTrue(erg1);
            Assert.IsFalse(erg2);
        }

    }

}
