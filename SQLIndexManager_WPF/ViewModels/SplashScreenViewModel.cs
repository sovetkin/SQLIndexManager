using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLIndexManager_WPF.Models;
using SQLIndexManager_WPF.Services;

namespace SQLIndexManager_WPF.ViewModels
{
    internal class SplashScreenViewModel
    {
        private SplashScreenModel _data;
        private AssemblyDataService _service;

        public SplashScreenViewModel()
        {
            _service = new();
            InitializeData();
        }

        private void InitializeData() =>
            _data = new()
            {
                Title = _service.GetTitle(),
                Subtitle = _service.GetVersion(),
                Copyright = _service.GetCopyright()
            };

        public string Title => _data.Title;
        public string SubTitle => _data.Subtitle;
    }
}
