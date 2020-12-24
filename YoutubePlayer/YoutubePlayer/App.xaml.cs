using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Microsoft.Identity.Client;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using YoutubePlayer.Common;
using YoutubePlayer.Common.Modules;
using YoutubePlayer.Common.Navigation;
using YoutubePlayer.ViewModels;
using YoutubePlayer.Views;

namespace YoutubePlayer
{
    public partial class App : Application
    {
        public static IPublicClientApplication PCA = null;
        public static object ParentWindow { get; set; }
        public static IContainer _container;
        public App()
        {
            InitializeComponent();

            // Dependency Injection using AutoFac
            _container = CreateDependencyInjectionContainer();
            MainPage = new NavigationPage(_container.Resolve<LoginPage>());
        }

        private IContainer CreateDependencyInjectionContainer()
        {
            var containerBuilder = new ContainerBuilder();
            var reflection = DependencyService.Get<IReflection>();
            var assemblies = reflection.Assemblies();
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            RegisterSingleton<IAuthenticator>(containerBuilder);
            RegisterSingles<BaseViewModel>(reflection, containerBuilder);
            RegisterSinglesAs(reflection, containerBuilder);
            RegisterSingles<Page>(reflection, containerBuilder);
            containerBuilder.RegisterAssemblyModules(assemblies);
            var container = containerBuilder.Build();
            return container;
        }

        private static void RegisterSingles<T>(IReflection reflection, ContainerBuilder builder)
        {
            var objTypes = reflection.AssignableTypes<T>();
            foreach (var objType in objTypes)
            {
                builder.RegisterType(objType).AsSelf().InstancePerDependency();
            }
        }

        private static void RegisterSinglesAs(IReflection reflection, ContainerBuilder builder)
        {
            var injectablesAsEnumerable = reflection.AssignableTypes<Common.Modules.IInjectableAsEnumerable>();
            foreach (var injectable in injectablesAsEnumerable)
            {
                builder.RegisterType(injectable).As(reflection.GetBaseType(injectable)).SingleInstance();
            }
        }

        private void RegisterSingleton<TINTERFACE>(ContainerBuilder builder)
            where TINTERFACE : class
        {
            var instance = DependencyService.Get<TINTERFACE>();
            builder.RegisterInstance(instance).As<TINTERFACE>().SingleInstance();
        }

    }
}
