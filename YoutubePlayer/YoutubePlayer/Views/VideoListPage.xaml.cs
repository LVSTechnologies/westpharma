using System;
using System.Collections.Generic;

using Xamarin.Forms;
using YoutubePlayer.ViewModels;

namespace YoutubePlayer.Views
{
    public partial class VideoListPage : ContentPage
    {
        private readonly VideoListViewModel _videoListViewModel;
        public VideoListPage()
        {
            InitializeComponent();
        }

        public VideoListPage(VideoListViewModel videoListViewModel) : this()
        {
            _videoListViewModel = videoListViewModel;
            BindingContext = _videoListViewModel;
        }
    }
}
