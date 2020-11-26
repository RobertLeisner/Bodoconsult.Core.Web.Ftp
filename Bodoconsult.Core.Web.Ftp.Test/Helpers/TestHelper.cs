using System.IO;
using System.Reflection;

namespace Bodoconsult.Core.Web.Ftp.Test.Helpers
{
    public static class TestHelper
    {

        private static readonly string TestDataPath;

        static TestHelper()
        {
            var path = (new FileInfo(Assembly.GetExecutingAssembly().Location)).Directory.Parent.Parent.Parent.FullName;

            TestDataPath = Path.Combine(path, "TestData");
        }


        public static SshCredentials GetCredentials()
        {
            var path = @"D:\Daten\Projekte\_work\Data\FtpCredentials.json";

            var c1 = JsonHelper.LoadJsonFile<SshCredentials>(path);

            var c2 = new SshCredentials
            {
                Url = c1.Url,
                Password = PasswordHelper.Decrypt(c1.Password),
                Username = PasswordHelper.Decrypt(c1.Username)
            };

            return c2;
        }


        public const string FtpSubDir = "/Test";

        public const string FtpSubDirCreate = "/Test/AAA";


        public const string FtpTestFileName = "A.txt";


        public static string FtpTestFilePath => Path.Combine(TestDataPath, FtpTestFileName);


        public const string LocalTargetPath = @"D:\temp";


        //public string LocalTestFile => 


    }
}