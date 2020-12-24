using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using YoutubePlayer.Common;
using YoutubePlayer.Common.EventArgs;
using YoutubePlayer.Common.Navigation;
using YoutubePlayer.Models;
using YoutubePlayer.Utilities;
using YoutubePlayer.Views;

namespace YoutubePlayer.ViewModels
{
    public class VideoListViewModel : BaseViewModel
    {
        
        private readonly IEvents _events;
        private readonly INavigationService _navigationService;

        public VideoListViewModel(IEvents events, INavigationService navigationService) : base(events)
        {
            _events = events;
            _navigationService = navigationService;
            PlayVideoCommand = new Command(async (i) => PlayYouTubeVideo(i));
            InitializeDataAsync().GetAwaiter();

            _events.Receive<AuthenticationEvent>(x =>
            {
                UserName = x.UserName;
            });

        }

        #region Private Methods

        private async void PlayYouTubeVideo(object i)
        {
            var item = i as YoutubeItem;
            if (item == null) return;

            string sourceUrl = Constants.embedUrlBase + item.VideoId;
            await _navigationService.PushPageAsync(App._container.Resolve<VideoDetailPage>());
            _events.Send(new ItemSelected { SourceUrl = sourceUrl, Title = item.Title, Description = item.Description });

        }

        private async Task InitializeDataAsync()
        {
            _ = await GetVideoIdsFromChannelAsync();
        }

        private async Task<List<string>> GetVideoIdsFromChannelAsync()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(Constants.ApiUrlForChannel);

            var videoIds = new List<string>();

            try
            {
                JObject response = JsonConvert.DeserializeObject<dynamic>(json);

                var items = response.Value<JArray>("items");

                foreach (var item in items)
                {
                    videoIds.Add(item.Value<JObject>("id")?.Value<string>("videoId"));
                }

                YoutubeItems = await GetVideosDetailsAsync(videoIds);
            }
            catch (Exception ex)
            {
                await _navigationService.DisplayAlert(Constants.YouTubeDataErrors, Constants.ErrorJsonDeserialize + ex.Message, Constants.OK);
            }

            return videoIds;
        }

        private async Task<List<YoutubeItem>> GetVideosDetailsAsync(List<string> videoIds)
        {

            var videoIdsString = "";
            foreach (var s in videoIds)
            {
                videoIdsString += s + ",";
            }

            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(string.Format(Constants.ApiUrlForVideosDetails, videoIdsString));

            var youtubeItems = new List<YoutubeItem>();

            try
            {
                JObject response = JsonConvert.DeserializeObject<dynamic>(json);

                var items = response.Value<JArray>("items");

                foreach (var item in items)
                {
                    var snippet = item.Value<JObject>("snippet");
                    var statistics = item.Value<JObject>("statistics");

                    var youtubeItem = new YoutubeItem
                    {
                        Title = snippet.Value<string>("title"),
                        Description = snippet.Value<string>("description"),
                        ChannelTitle = snippet.Value<string>("channelTitle"),
                        PublishedAt = snippet.Value<DateTime>("publishedAt"),
                        VideoId = item?.Value<string>("id"),
                        DefaultThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("default")?.Value<string>("url"),
                        MediumThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("medium")?.Value<string>("url"),
                        HighThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("high")?.Value<string>("url"),
                        StandardThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("standard")?.Value<string>("url"),
                        MaxResThumbnailUrl = snippet?.Value<JObject>("thumbnails")?.Value<JObject>("maxres")?.Value<string>("url"),

                        ViewCount = statistics?.Value<int>("viewCount"),
                        LikeCount = statistics?.Value<int>("likeCount"),
                        DislikeCount = statistics?.Value<int>("dislikeCount"),
                        FavoriteCount = statistics?.Value<int>("favoriteCount"),
                        CommentCount = statistics?.Value<int>("commentCount"),

                        Tags = (from tag in snippet?.Value<JArray>("tags") select tag.ToString())?.ToList(),
                    };

                    youtubeItems.Add(youtubeItem);
                }

                return youtubeItems;
            }
            catch (Exception exception)
            {
                return youtubeItems;
            }
        }

        #endregion


        #region XAML Properties
        public List<YoutubeItem> YoutubeItems
        {
            get => Get<List<YoutubeItem>>();
            set => SetReference(value);
        }

        public string UserName
        {
            get => Get<string>();
            set => SetReference(value);
        }

        private YoutubeItem _selectedItem;
        public YoutubeItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;

                if (_selectedItem == null)
                    return;

                PlayYouTubeVideo(_selectedItem);
                _selectedItem = null;

            }
        }

        public ICommand PlayVideoCommand { get; set; }

        #endregion
    }
}
