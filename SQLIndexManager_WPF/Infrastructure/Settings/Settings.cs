using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLIndexManager_WPF.Infrastructure.Enums;
using SQLIndexManager_WPF.Infrastructure.Helpers;
using SQLIndexManager_WPF.Services;

namespace SQLIndexManager_WPF.Infrastructure.Settings
{
    internal class Settings
    {
        private SettingsHelper _service;
        private IGlobalSettings _current;
        private static Host _activeHost;

        public Settings(SettingsHelper service, IGlobalSettings current)
        {
            _service = service;
            _current = current;

            _current.Hosts = _service.GetAllHosts();
            IgnoreFileSetting = false;
        }

        public bool IgnoreFileSetting { get; set; }

        public List<Host> Hosts
        {
            get => _current.Hosts;
            set => _current.Hosts = value;
        }

        public Options Options
        {
            get => _current.Options;
            set => _current.Options = value;
        }

        public ServerInfo ServerInfo => _activeHost.ServerInfo;

        public Host ActiveHost
        {
            get => _activeHost;
            set
            {
                _activeHost = value;

                Host oldHost = _current.Hosts.FirstOrDefault(h =>
                    String.Equals(h.Server, _activeHost.Server, StringComparison.CurrentCultureIgnoreCase));
                
                if (oldHost != null)
                {
                    value.Databases = oldHost.Databases;
                }

                _current.Hosts.Remove(oldHost);
                _current.Hosts.Insert(0, _activeHost);
            }
        }
    }
}
