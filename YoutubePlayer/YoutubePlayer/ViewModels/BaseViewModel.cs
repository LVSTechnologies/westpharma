using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using YoutubePlayer.Common;

namespace YoutubePlayer.ViewModels
{
    public class BaseViewModel : NotifyingTypedDictionary
    {
        private readonly IEvents _events;
        public BaseViewModel()
        {
        }

        public BaseViewModel(IEvents events)
        {
            _events = events;
        }

    }
}
