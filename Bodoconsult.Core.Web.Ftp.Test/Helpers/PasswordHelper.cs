namespace Bodoconsult.Core.Web.Ftp.Test.Helpers
{
    public static class PasswordHelper
    {

        public static string Encrypt(string raw)
        {
            return PasswordHandler.Encrypt(raw);
        }

        public static string Decrypt(string crypted)
        {
            return PasswordHandler.Decrypt(crypted);
        }

    }
}
