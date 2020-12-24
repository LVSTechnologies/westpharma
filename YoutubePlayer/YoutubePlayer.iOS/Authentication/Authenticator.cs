using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using YoutubePlayer.Common;
using Xamarin.Forms;

[assembly: Dependency(typeof(YoutubePlayer.iOS.Authentication.Authenticator))]
namespace YoutubePlayer.iOS.Authentication
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri, string appBundleId)
        {
            App.PCA = PublicClientApplicationBuilder.Create(clientId)
                    .WithIosKeychainSecurityGroup(appBundleId)
                    .WithB2CAuthority(authority)
                    .WithRedirectUri(returnUri)
                    .Build();

            var authResult = await App.PCA.AcquireTokenInteractive(new string[] { resource })
                                                      .WithUseEmbeddedWebView(false)
                                                      .ExecuteAsync();

            return authResult;
        }
    }
}
