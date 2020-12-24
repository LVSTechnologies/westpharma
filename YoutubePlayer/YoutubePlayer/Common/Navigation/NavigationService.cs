using System;
using System.Threading.Tasks;
using Autofac;
using Xamarin.Forms;

namespace YoutubePlayer.Common.Navigation
{
    public class NavigationService : INavigationService
    {
        private IEvents _events;

        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }

        private INavigation Navigation
        {
            get { return MainPage.Navigation; }
        }
        public NavigationService(IEvents events)
        {
            _events = events;
        }

        private IContainer _container;
        public void SetContainer(IContainer container)
        {
            _container = container;
        }

        public async Task PushPageAsync<P>() where P : Page
        {
            try
            {

                await Navigation.PushAsync(_container.Resolve<P>());

            }
            catch (Exception ex)
            {

            }
        }

        public async Task PushPageAsync(Page page)
        {
            await Navigation.PushAsync(page);
        }

        public async Task PushModalAsync<P>() where P : Page
        {
            await Navigation.PushModalAsync(_container.Resolve<P>());
        }

        public async Task PushModalAsync(Page modalPage)
        {
            await Navigation.PushModalAsync(modalPage);
        }
        public async Task PushModalAsync(Type pageType)
        {
            var page = _container.Resolve(pageType) as Page;
            await PushModalAsync(page);
        }
        public async Task<Page> PopPageAsync()
        {
            return await Navigation.PopAsync();
        }

        public async Task<Page> PopModalAsync()
        {
            return await Navigation.PopModalAsync();
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await MainPage.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await MainPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}
