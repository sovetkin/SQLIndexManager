using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Mvvm;

using SQLIndexManager_WPF.Models;
using SQLIndexManager_WPF.Services;

namespace SQLIndexManager_WPF.ViewModels
{
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
            _data.Copyright = _service.GetCopyright();
            _data.Logo = new Uri("");
        }

        public string Title => _data.Title;
        public string Subtitle => _data.Subtitle;
        public string Copyright => _data.Copyright;
        public Uri Logo => _data.Logo;
    }
}
