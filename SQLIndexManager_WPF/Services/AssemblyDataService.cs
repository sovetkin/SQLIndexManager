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
    /// Responsible to get information about assembly
    /// </summary>
    internal class AssemblyDataService
    {
        private Assembly _assembly;
        public AssemblyDataService()
        {
            _assembly = Assembly.GetExecutingAssembly();
        }

        internal string GetTitle() => ((AssemblyTitleAttribute)_assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0]).Title;
        internal string GetVersion() => _assembly.GetName().Version.ToString();
        internal string GetCopyright() =>
            ((AssemblyCopyrightAttribute)_assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
        internal string GetProduct() => ((AssemblyProductAttribute)_assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
        internal string GetExeName() => Process.GetCurrentProcess().ProcessName;
        internal string GetExePath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        internal string GetApplicationName() => $"{GetExeName()}-{Environment.ProcessId}";
        internal string GetLayoutFileName() => $"{GetExePath()}\\{GetExeName()}.layout";
        internal string GetSettingFileName() => $"{GetExePath()}\\{GetExeName()}.cfg";
        internal string GetLogFileName() => $"{GetExePath()}\\{GetExeName()}.log";
    }
}
