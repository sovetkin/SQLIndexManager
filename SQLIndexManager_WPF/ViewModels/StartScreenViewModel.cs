using System;

using DevExpress.Mvvm;

using SQLIndexManager_WPF.Models;
using SQLIndexManager_WPF.Services;

namespace SQLIndexManager_WPF.ViewModels
{
    /// <summary>
    /// Responsible for providing data for splash screen view.
    /// </summary>
    internal class StartScreenViewModel : ViewModelBase

    {
        private readonly SplashScreenModel _data;
        private readonly AssemblyDataService _service;

        public StartScreenViewModel() : base()
        {
            _data = new();
            _service = new();
            InitializeData();
        }

        private void InitializeData()
        {
            _data.Title = _service.GetTitle();
            _data.Subtitle = _service.GetVersion();
            _data.Copyright = $"Copyright © 2022 {_service.GetCopyright()} \nAll rights reserved.";
            _data.Logo = new Uri("pack://application:,,,/Resources/Images/icon.png");
            _data.Status = "Loading...";
            _data.IsIndeterminate = true;
        }

        public string Title => _data.Title;
        public string Subtitle => _data.Subtitle;
        public string Copyright => _data.Copyright;
        public Uri Logo => _data.Logo;
        public string Status => _data.Status;
        public double Progress => _data.Progress;
        public bool IsIndeterminate => _data.IsIndeterminate;
        public object Tag => _data.Tag;

        /// <summary>
        /// Operator realizing the implicit conversion from current class to DXSplashScreenViewModel class.
        /// </summary>
        /// <param name="vm">The current view model class</param>
        public static implicit operator DXSplashScreenViewModel(StartScreenViewModel vm) => new DXSplashScreenViewModel()
        {
            Title = vm.Title,
            Subtitle = vm.Subtitle,
            Copyright = vm.Copyright,
            Status = vm.Status,
            Logo = vm.Logo,
            IsIndeterminate = vm.IsIndeterminate,
            Progress = vm.Progress,
            Tag = vm.Tag
        };
    }
}
