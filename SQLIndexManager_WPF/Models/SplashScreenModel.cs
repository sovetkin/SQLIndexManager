using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLIndexManager_WPF.Models
{
    internal class SplashScreenModel
    {
        public double Progress { get; set; }
        public bool IsIndeterminate { get; set; }
        public object Tag { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Status { get; set; }
        public string Copyright { get; set; }
        public Uri Logo { get; set; }
    }
}
