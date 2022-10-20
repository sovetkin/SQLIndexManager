using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLIndexManager_WPF.Infrastructure.Enums;
using SQLIndexManager_WPF.Infrastructure.Settings;
using SQLIndexManager_WPF.Services;

namespace SQLIndexManager_WPF.Infrastructure.Helpers
{
    public class SettingsHelper
    {
        private IEncryptionService _encryptor;
        private IConfigService _config;
        private IRegestryDataService _registry;

        public SettingsHelper(IEncryptionService encryptor, IConfigService config, IRegestryDataService registry)
        {
            _encryptor = encryptor;
            _config = config;
            _registry = registry;
        }

        public List<Host> GetAllHosts()
        {
            var result = new List<Host>();

            result.AddRange(GetHostFromConfig().Hosts);
            GetHostsFromRegistry(ref result);

            if (result.Count == 0)
            {
                AddHost(ref result, Environment.MachineName);
            }

            return result;
        }

        public IGlobalSettings GetHostFromConfig()
        {
            var settings = _config.DeserializeFromConfigFile() as IGlobalSettings;

            Delete(ref settings, h => h.Server == null);
            RecoverData(ref settings);

            return settings;
        }

        public void GetHostsFromRegistry(ref List<Host> hosts)
        {
            var result = _registry.GetHostsFromRegistry();

            foreach (string instanceName in result)
            {
                string host = (instanceName == "MSSQLSERVER") ? Environment.MachineName : $"{Environment.MachineName}\\{instanceName}";
                if (!hosts.Exists(h => string.Equals(h.Server, host, StringComparison.CurrentCultureIgnoreCase)))
                {
                    AddHost(ref hosts, host);
                }
            }
        }

        private static void Delete(ref IGlobalSettings settings, Predicate<Host> match) => settings.Hosts.RemoveAll(match);

        private void RecoverData(ref IGlobalSettings settings) =>
            settings.Hosts.ForEach(h =>
            {
                h.IsUserConnection = true;
                if (AuthTypes.Sql == h.AuthType && !string.IsNullOrEmpty(h.Password))
                {
                    h.Password = _encryptor.Decrypt(h.Password);
                }
            });

        private void AddHost(ref List<Host> hosts, string name) => hosts.Add(new Host() { Server = name });
    }
}
