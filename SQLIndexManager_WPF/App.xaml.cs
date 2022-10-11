using System;
using System.Windows;

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SQLIndexManager_WPF.ViewModels;

namespace SQLIndexManager_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _host;

        public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public App()
        {
            SplashScreenManager.Create(() =>
                new FluentSplashScreen(),
                new StartScreenViewModel())
                               .ShowOnStartup();
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
    }
}
