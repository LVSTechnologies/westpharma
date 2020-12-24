using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using YoutubePlayer.Common;
using YoutubePlayer.Common.EventArgs;
using YoutubePlayer.Common.Navigation;
using YoutubePlayer.Utilities;
using YoutubePlayer.Views;

namespace YoutubePlayer.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        private readonly IEvents _events;
        private readonly IAuthenticator _authenticator;
        private readonly INavigationService _navigationService;
        public LoginViewModel(IEvents events, IAuthenticator authenticator, INavigationService navigationService) : base(events)
        {
            _events = events;
            _authenticator = authenticator;
            _navigationService = navigationService;
            LoginCommand = new Command(async () => await LoginProcess());
            SocialLoginCommand = new Command(async () => await SocialLoginProcess());
            WelcomeBannerText = Constants.WelcomeBannerText;
        }

        


        #region Private Methods

        private async Task LoginProcess()
        {
            // Both Email and Password are required to Proceed
            if (string.IsNullOrEmpty(EmailAddress) || string.IsNullOrEmpty(Password))
            {
               await _navigationService.DisplayAlert(Constants.LoginErrorsTitle, Constants.MissingUserNamePassword, Constants.OK);
               return;
            }

            //Check if Email is in valid format
            if (!EmailAddress.ValidEmail())
            {
                await _navigationService.DisplayAlert(Constants.LoginErrorsTitle, Constants.InvalidEmailFormat, Constants.OK);
                return;
            }
                
            //Check if User name and Password is valid
            if (EmailAddress.ToLower() == Constants.UserName && Password == Constants.Password)
            {
                //Navigate to the Main page
                await _navigationService.PushPageAsync(App._container.Resolve<VideoListPage>());
                _events.Send(new AuthenticationEvent { UserName = EmailAddress });
            }
            else
            {
                await _navigationService.DisplayAlert(Constants.LoginErrorsTitle, Constants.AuthenticationFailed, Constants.OK);
                return;
            }

        }

        private async Task SocialLoginProcess()
        {
            AuthenticationResult authResult;

            try
            {
                string redirectUri = Device.RuntimePlatform == Device.iOS ? Constants.RETURN_URI_iOS : Constants.RETURN_URI_ANDROID;
                authResult = await _authenticator.Authenticate(Constants.Authority, Constants.ApplicationId, Constants.clientId, redirectUri, Constants.appBundleId);

                var token = authResult.IdToken;

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var decryptedToken = handler.ReadJwtToken(token);
                var displayName = Constants.Welcome + decryptedToken.Claims.FirstOrDefault(x => x.Type == Constants.nameattribute)?.Value;

                await _navigationService.PushPageAsync(App._container.Resolve<VideoListPage>());
                _events.Send(new AuthenticationEvent { UserName = displayName });
                
                
            }
            catch (Exception ex)
            {
                await _navigationService.DisplayAlert(Constants.LoginErrorsTitle, Constants.AcquireTokenFailed + ex.Message, Constants.OK);
                return;
            }
        }

        #endregion

        #region XAML Bound Properties
        public ICommand LoginCommand { get; set; }
        public ICommand SocialLoginCommand { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string WelcomeBannerText
        {
            get => Get<string>();
            set => SetReference(value);
        }
        #endregion
    }
}
