using System;
using Autofac;
using Xamarin.Forms;
using YoutubePlayer.ViewModels;
using YoutubePlayer.Views;

namespace YoutubePlayer.Common.Modules
{
    public class DependencyServiceModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            RegisterSingleton<IReflection>(builder);
            builder.RegisterType<Events>().As<IEvents>().SingleInstance();
        }


        private void RegisterSingleton<TINTERFACE>(ContainerBuilder builder)
            where TINTERFACE : class
        {
            var instance = DependencyService.Get<TINTERFACE>();
            //if (instance != null)
            builder.RegisterInstance(instance).As<TINTERFACE>().SingleInstance();
        }

        public DependencyServiceModule()
        {
        }
    }
}
