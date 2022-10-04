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
        internal string GetTitle() => ((AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0]).Title;
        internal string GetVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();
        internal string GetCopyright() =>
            ((AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)[0]).Copyright;
        internal string GetProduct() => ((AssemblyProductAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
        internal string GetExeName() => Process.GetCurrentProcess().ProcessName;
        internal string GetExePath() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        internal string GetApplicationName() => $"{GetExeName()}-{Process.GetCurrentProcess().Id}";
        internal string GetLayoutFileName() => $"{GetExePath()}\\{GetExeName()}.layout";
        internal string GetSettingFileName() => $"{GetExePath()}\\{GetExeName()}.cfg";
        internal string GetLogFileName() => $"{GetExePath()}\\{GetExeName()}.log";
    }
}
