using System.Net.Http;
using System.ServiceModel.Channels;
// WebPrint.Framework reference https://github.com/LeafDuan/WebPrint/tree/master/WebPrint.Framework
using WebPrint.Framework;

namespace Server.Helper
{
    public static class HttpRequestMessageHelper
    {
        public static string GetClinetIp(this HttpRequestMessage request)
        {
            var ip = request.GetOwinContext().Request.RemoteIpAddress;
            if (!ip.IsNullOrEmpty())
                return ip.Replace("::1", "127.0.0.1");

            if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                return ((RemoteEndpointMessageProperty) request.Properties[RemoteEndpointMessageProperty.Name]).Address;

            return string.Empty;
        }
    }
}
