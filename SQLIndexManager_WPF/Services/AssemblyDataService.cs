using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SQLIndexManager_WPF.Services
{
    /// <summary>
    /// Responsible for getting information related to assembly
    /// </summary>
    public class AssemblyDataService
    {
        private Assembly _assembly;
        public AssemblyDataService()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        internal string GetTitle() => (_assembly.GetCustomAttributes()
            .FirstOrDefault(p => p.GetType() == typeof(AssemblyTitleAttribute)) as AssemblyTitleAttribute).Title;
        internal string GetVersion() => _assembly.GetName().Version.ToString();
        internal string GetCopyright() => (_assembly.GetCustomAttributes()
            .FirstOrDefault(p => p.GetType() == typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute).Copyright;
        internal string GetProduct() => (_assembly.GetCustomAttributes()
            .FirstOrDefault(p => p.GetType() == typeof(AssemblyProductAttribute)) as AssemblyProductAttribute).Product;
        internal string GetExeName() => Process.GetCurrentProcess().ProcessName;
        internal string GetExePath() => Path.GetDirectoryName(_assembly.Location);
        internal string GetApplicationName() => $"{GetExeName()}-{Environment.ProcessId}";
        internal string GetLayoutFileName() => $"{GetExePath()}\\{GetExeName()}.layout";
        internal string GetSettingFileName() => $"{GetExePath()}\\{GetExeName()}.cfg";
        internal string GetLogFileName() => $"{GetExePath()}\\{GetExeName()}.log";
    }
}
