using System;
using System.Collections.Generic;

using Xamarin.Forms;
using YoutubePlayer.ViewModels;

namespace YoutubePlayer.Views
{
    public partial class VideoDetailPage : ContentPage
    {
        private readonly VideoDetailViewModel _viewModel;
        public VideoDetailPage()
        {
            InitializeComponent();
        }

        public VideoDetailPage(VideoDetailViewModel videoDetailViewModel) : this()
        {
            _viewModel = videoDetailViewModel;
            BindingContext = _viewModel;
        }
    }
}
