using System;
using System.Collections.Generic;

using Xamarin.Forms;
using YoutubePlayer.ViewModels;

namespace YoutubePlayer.Views
{
    public partial class LoginPage : ContentPage
    {
        private LoginViewModel _loginViewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _loginViewModel;
        }

        public LoginPage(LoginViewModel loginViewModel) : this()
        {
            _loginViewModel = loginViewModel;
            BindingContext = _loginViewModel;
        }
    }
}
