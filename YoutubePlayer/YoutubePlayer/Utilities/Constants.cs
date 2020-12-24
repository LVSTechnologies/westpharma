using System;
using Xamarin.Forms;

namespace YoutubePlayer.Utilities
{
    public static class Constants
    {
        public const string ApiKey = "AIzaSyBgG1FyDTFnUm0mIBjrJufcG6-5TIhuSCI";
        public const string ChannelId = "UCe-f02uZgEXdHmHpC3loAQg";  // "UCsMica-v34Irf9KVTh6xx-g"; // Channel Id for Microsoft Developer
        public const string PlayListId = "PLM75ZaNQS_FaHlFXg_NBjTqYvMSH63uz4";  //"PLM75ZaNQS_FaHlFXg_NBjTqYvMSH63uz4"; //Play list id for The Xamarin Show

        public const string apiUrlForChannel = "https://www.googleapis.com/youtube/v3/search?part=id&maxResults=20&channelId="
            + ChannelId //"UCCYR9GpcE3skVnyMU8Wx1kQ"
                        //+ "Your_ChannelId"
            + "&key="
            + ApiKey;

        public const string watchUrlBase = "https://www.youtube.com/watch?v=";
        public const string embedUrlBase = "https://www.youtube.com/embed/";

        // use this link to get an api_key : https://console.developers.google.com/apis/api/youtube/
        //public const string ApiKey = Constants.ApiKey;

        // doc : https://developers.google.com/apis-explorer/#p/youtube/v3/youtube.videos.list 
        public const string ApiUrlForChannel = "https://www.googleapis.com/youtube/v3/search?part=id&maxResults=20&channelId="
            + ChannelId  //"UCCYR9GpcE3skVnyMU8Wx1kQ"
                         //+ "Your_ChannelId"
            + "&key="
            + ApiKey;

        // doc : https://developers.google.com/apis-explorer/#p/youtube/v3/youtube.playlistItems.list
        public const string ApiUrlForPlaylist = "https://www.googleapis.com/youtube/v3/playlistItems?part=contentDetails&maxResults=20&playlistId="
            + PlayListId // "PLpbcUe4chE7-uGCH1S0-qeuCWOMa2Tmam"
            //+ "Your_PlaylistId"
            + "&key="
            + ApiKey;

        // doc : https://developers.google.com/apis-explorer/#p/youtube/v3/youtube.search.list
        public const string ApiUrlForVideosDetails = "https://www.googleapis.com/youtube/v3/videos?part=snippet,statistics&id="
            + "{0}"
            //+ "0ce22qhacyo,j8GU5hG-34I,_0aQJHoI1e8"
            //+ "Your_Videos_Id"
            + "&key="
            + ApiKey;

        //public const string channelUrl = $"https://www.googleapis.com/youtube/v3/search?part=id&maxResults=20&channelId={ChannelId}&key={ApiKey}";
        //public const string detailsUrl = "https://www.googleapis.com/youtube/v3/videos?part=snippet,statistics&key=" + ApiKey + "&id={0}";

        public const string LoginErrorsTitle = "Login Errors";
        public const string MissingUserNamePassword = "Missing User/Password.";
        public const string InvalidEmailFormat = "The email entered is in invalid format.";
        public const string AuthenticationFailed = "The email/password is not valid";
        public const string OK = "OK";
        public const string WelcomeBannerText = "Welcome to the YouTube Clone";
        public const string UserName = "snehikumar@gmail.com";
        public const string Password = "Password";
        public const string YouTubeDataErrors = "Data Errors";
        public const string ErrorJsonDeserialize = "Error while deserializing the JSON from Youtube: ";

        //Constants for the Azure B2C
        public static string Tenant = "snehik.onmicrosoft.com";
        public static string AzureADB2CHostname = "snehik.b2clogin.com";
        public static string TenantId = "e5b5da73-8680-44a8-9c91-ee86dc7f7995";
        public static string SignUpAndInPolicy = "B2C_1_SignUpIn";
        public static string AuthorityBase = $"https://{AzureADB2CHostname}/tfp/{Tenant}/";
        public static string Authority = $"{AuthorityBase}{SignUpAndInPolicy}";
        public const string RETURN_URI_iOS = "msauth.com.westpharma.youtubeplayer://auth";
        public const string RETURN_URI_ANDROID = "msauth://com.westpharma.youtubeplayer/ga0RGNYHvNM5d0SLGQfpQWAPGJ8%3D";
        public const string appBundleId = "com.westpharma.youtubeplayer";
        public const string clientId = "07bb32f5-d99b-48f0-ba44-d4e28df9b83b";
        public const string ApplicationId = "https://snehik.onmicrosoft.com/07bb32f5-d99b-48f0-ba44-d4e28df9b83b/wishlist.read";
        public static string IOSKeyChainGroup = "com.microsoft.adalcache";
        public static string nameattribute = "name";
        public static string Welcome = "Welcome ";
        public const string SocialLoginFailed = "Social Login Failed due to: ";
        public const string AcquireTokenFailed = "Acquiring token for Azure B2C Failed: ";

    }
}
