using System;

using Microsoft.Extensions.Hosting;

namespace SQLIndexManager_WPF
{
    public static class Program
    {
        /// <summary>
        /// My Application Entry Point.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            var App = new App();
            App.InitializeComponent();
            _ = App.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices(App.ConfigureServices);
    }
}
