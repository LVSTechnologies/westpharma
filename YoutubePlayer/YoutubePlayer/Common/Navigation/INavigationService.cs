using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YoutubePlayer.Common.Navigation
{
    public interface INavigationService
    {
        Task PushPageAsync<P>() where P : Page;
        Task PushPageAsync(Page page);
        Task PushModalAsync<P>() where P : Page;
        Task PushModalAsync(Page modalPage);
        Task<Page> PopPageAsync();
        Task<Page> PopModalAsync();
        Task DisplayAlert(string title, string message, string cancel);
        Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
        Task PushModalAsync(Type pageType);
    }
}
