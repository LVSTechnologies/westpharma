using System;
using System.Runtime.CompilerServices;

namespace YoutubePlayer.Common
{
    public interface IEvents
    {
        void Receive<TEvent>(Action<TEvent> callback, [CallerFilePath] string filePath = null);

        void Send<TEvent>(TEvent info)
            where TEvent : class;

        void UnsubscribeAll([CallerFilePath] string filePath = null);
    }
}
