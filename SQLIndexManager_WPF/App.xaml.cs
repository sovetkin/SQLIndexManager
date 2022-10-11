using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using DevExpress.Mvvm;
using DevExpress.Xpf.Core;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SQLIndexManager_WPF.ViewModels;
using SQLIndexManager_WPF.Views;

using views = SQLIndexManager_WPF.Views;

namespace SQLIndexManager_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _host;

        public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        protected ISplashScreenManagerService SplashScreenManager => CreateSplashScreenManager();

        public App()
        {
            /*var splashScreenViewModel = new StartScreenViewModel();
            SplashScreenManager manager = DevExpress.Xpf.Core.SplashScreenManager.CreateThemed(splashScreenViewModel);
            manager.ShowOnStartup(false);*/
            SplashScreenManager.Show();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            await Host.StartAsync().ConfigureAwait(false);

        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            await Host.StopAsync().ConfigureAwait(false);
            Host.Dispose();
            _host = null;
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<StartScreenViewModel>();
        }

        private static ISplashScreenManagerService CreateSplashScreenManager()
        {
            return new SplashScreenManagerService()
            {
                PredefinedSplashScreenType = PredefinedSplashScreenType.Themed,
                SplashScreenType = typeof(StartScreenView),
                StartupLocation = WindowStartupLocation.CenterScreen
            };
        }
    }
}
