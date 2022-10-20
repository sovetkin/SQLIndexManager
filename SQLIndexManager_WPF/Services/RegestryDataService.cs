using System;

using Microsoft.Win32;

namespace SQLIndexManager_WPF.Services
{
    /// <summary>
    /// Responsible for getting all registered hosts in the registry
    /// </summary>
    public class RegestryDataService : IRegestryDataService
    {
        private RegistryView RegistryView => Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;

        public string[] GetHostsFromRegistry()
        {

            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView))
            {
                try
                {
                    return hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false)
                               .GetValueNames();
                }
                catch (Exception)
                {
                    Output.Current.Add("Failed to read registry");
                    return null;
                }
            }
        }
    }
}
