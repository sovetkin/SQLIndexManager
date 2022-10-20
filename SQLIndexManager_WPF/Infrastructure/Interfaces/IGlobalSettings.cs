using System.Collections.Generic;

namespace SQLIndexManager_WPF.Infrastructure.Settings
{
    public interface IGlobalSettings
    {
        List<Host> Hosts { get; set; }
        Options Options { get; set; }
    }
}