using System;
// WebPrint.Framework reference https://github.com/LeafDuan/WebPrint/tree/master/WebPrint.Framework
using WebPrint.Framework;

namespace Server.Helper
{
    public class Token
    {
        public int UserId { get; set; }
        public string ClientIp { get; set; }

        public string Encrypt()
        {
            return TripleDesHelper.Encrypt(string.Format("{0},{1},{2}", UserId, ClientIp, Guid.NewGuid()));
        }

        public static Token Decrypt(string value)
        {
            var token = new Token();
            var tokenValue = TripleDesHelper.Decrypt(value);

            if (tokenValue.IsNullOrEmpty()) return token;

            var array = tokenValue.Split(',');
            token.UserId = array[0].AsInt();
            if (array.Length >= 2) token.ClientIp = array[1];

            return token;
        }
    }
}
