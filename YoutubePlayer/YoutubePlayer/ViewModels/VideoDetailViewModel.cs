using System;
using YoutubePlayer.Common;
using YoutubePlayer.Common.EventArgs;

namespace YoutubePlayer.ViewModels
{
    public class VideoDetailViewModel: BaseViewModel
    {
        private readonly IEvents _events;

        public VideoDetailViewModel(IEvents events) : base(events)
        {
            _events = events;

            _events.Receive<ItemSelected>(x =>
            {
                SourceUrl = x.SourceUrl;
                Title = x.Title;
                Description = x.Description;
            });
        }

        #region XAML Properties
        public string SourceUrl
        {
            get => Get<string>();
            set => SetReference(value);
        }
        public string Title
        {
            get => Get<string>();
            set => SetReference(value);
        }
        public string Description
        {
            get => Get<string>();
            set => SetReference(value);
        }

        #endregion
    }
}
