using System;
using System.Threading.Tasks;
using Android.App;
using Android.Webkit;
using Xamarin.Forms;
using Microsoft.Identity.Client;
using YoutubePlayer.Common;

[assembly: Dependency(typeof(YoutubePlayer.Droid.Authentication.Authenticator))]
namespace YoutubePlayer.Droid.Authentication
{
    public class Authenticator : IAuthenticator
    {
        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri, string appBundleId)
        {

            App.PCA = PublicClientApplicationBuilder.Create(clientId)
                    .WithB2CAuthority(authority)
                    .WithRedirectUri(returnUri)
                    .WithParentActivityOrWindow(() => App.ParentWindow)
                    .Build();

            ClearSessionCookie();

            AuthenticationResult authResult = null;

            try
            {
                authResult = await App.PCA.AcquireTokenInteractive(new string[] { resource })
                                                      .WithUseEmbeddedWebView(true)
                                                      .ExecuteAsync();
            }
            catch (Exception ex)
            {

            }

            return authResult;
        }

        private void ClearSessionCookie()
        {
            CookieManager cookieManager = CookieManager.Instance;
            cookieManager.RemoveSessionCookie();
        }
    }
}
