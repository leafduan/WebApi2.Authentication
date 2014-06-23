using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
// WebPrint.Framework reference https://github.com/LeafDuan/WebPrint/tree/master/WebPrint.Framework
using WebPrint.Framework;

namespace Server.Helper
{
    // references
    // http://www.codeproject.com/Articles/630986/Cross-Platform-Authentication-With-ASP-NET-Web-API
    // http://dgandalf.github.io/WebApiTokenAuthBootstrap/
    public class AuthenticationMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization == null)
            {
                var reply = request.CreateResponse(HttpStatusCode.Unauthorized, "Missing authorization token.");
                
                return Task.FromResult(reply);
            }

            try
            {
                var encryptedToken = request.Headers.Authorization.Parameter;
                var token = Token.Decrypt(encryptedToken);
                //bool isValidUser
                var isIpMathes = token.ClientIp.EqualTo(request.GetClinetIp());

                if (!isIpMathes)
                {
                    var reply = request.CreateResponse(HttpStatusCode.Unauthorized, "Invalid authorization token");
                    return Task.FromResult(reply);
                }

                var principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, token.UserId.ToString())
                }, "Basic"));

                // authorize attribute 
                request.GetRequestContext().Principal = principal;
            }
            catch (Exception ex)
            {
                var reply = request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
                return Task.FromResult(reply);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
