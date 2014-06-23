// WebPrint.Framework reference https://github.com/LeafDuan/WebPrint/tree/master/WebPrint.Framework
using WebPrint.Framework;

namespace Server.Helper
{
    public static class TripleDesHelper
    {
        //8 
        private static readonly string iv = "12345678";
        //24 
        private static readonly string key = "123456789@owin.api2.auth";
        private static readonly TripleDes tripleDes;

        static TripleDesHelper()
        {
            tripleDes = new TripleDes(key, iv);
        }

        public static string Encrypt(string value)
        {
            return tripleDes.Encrypt(value);
        }

        public static string Decrypt(string value)
        {
            return tripleDes.Decrypt(value);
        }
    }
}
