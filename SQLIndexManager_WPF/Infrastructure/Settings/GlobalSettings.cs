using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using SQLIndexManager_WPF.Services;

namespace SQLIndexManager_WPF.Infrastructure.Settings
{
    [Serializable]
    public class GlobalSettings : IGlobalSettings
    {
        [XmlElement]
        public Options Options { get; set; }

        [XmlElement]
        public List<Host> Hosts { get; set; }
    }
}
