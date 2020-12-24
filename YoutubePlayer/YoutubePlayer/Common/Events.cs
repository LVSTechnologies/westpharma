using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace YoutubePlayer.Common
{
    public class Events : IEvents
    {
        private ConcurrentDictionary<string, List<string>> eventToClasses = new ConcurrentDictionary<string, List<string>>();
        private ConcurrentDictionary<string, List<Action>> ClassToEvents = new ConcurrentDictionary<string, List<Action>>();

        public void Send<TEvent>(TEvent info = null)
            where TEvent : class
        {
            string eventName = typeof(TEvent).ToString();
            List<string> classesToNotify;
            if (eventToClasses.TryGetValue(eventName, out classesToNotify))
            {
                foreach (var filePath in classesToNotify)
                {
                    MessagingCenter.Send(this, eventName + filePath, info);
                }
            }
        }

        public void Receive<TEvent>(Action<TEvent> callback, [CallerFilePath] string filePath = null)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");
            string eventName = typeof(TEvent).ToString();
            Action unSubScribe = () =>
            {
                MessagingCenter.Unsubscribe<Events, TEvent>(this, eventName + filePath);
                List<string> classes;
                if (eventToClasses.TryGetValue(eventName, out classes))
                    classes.Remove(filePath);
            };
            eventToClasses.AddOrUpdate(eventName, new List<string> { filePath }, (k, v) => {
                v.Add(filePath); return v;
            });
            ClassToEvents.AddOrUpdate(filePath, new List<Action> { unSubScribe }, (k, v) => { v.Add(unSubScribe); return v; });
            MessagingCenter.Subscribe<Events, TEvent>(this, eventName + filePath, (sender, info) => callback(info));
        }

        public void UnsubscribeAll([CallerFilePath] string filePath = null)
        {
            List<Action> eventsToUnsubscribe;

            if (ClassToEvents.TryRemove(filePath, out eventsToUnsubscribe))
            {
                foreach (var unSubscribe in eventsToUnsubscribe)
                {
                    unSubscribe();
                }
            }
        }
    }
}
