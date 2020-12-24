using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace YoutubePlayer.Common
{
    public interface IAuthenticator
    {
        Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri, string appBundleId);
    }
}
