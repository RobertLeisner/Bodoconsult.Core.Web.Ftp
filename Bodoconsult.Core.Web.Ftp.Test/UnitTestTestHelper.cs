using Bodoconsult.Core.Web.Ftp.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Web.Ftp.Test
{
    public class UnitTestTestHelper
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCreateCredentials()
        {

            const string filename = @"D:\temp\FtpCredentials.json";

            var c = new SshCredentials
            {
                Url = "YourFtpServerUrl", // www.test.de
                Password = PasswordHelper.Encrypt("YourUsername"), 
                Username = PasswordHelper.Encrypt("Password")
            };

            JsonHelper.SaveAsFile(filename, c);
        }

        [Test]
        public void TestGetCredentials()
        {

            // Act
            var result = TestHelper.GetCredentials();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}