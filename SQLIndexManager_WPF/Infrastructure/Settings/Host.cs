using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using SQLIndexManager_WPF.Infrastructure.Enums;
using SQLIndexManager_WPF.Services;

namespace SQLIndexManager_WPF.Infrastructure.Settings
{
    [Serializable]
    public class Host
    {

        [XmlAttribute]
        public string Server { get; set; }

        [XmlAttribute]
        public AuthTypes AuthType { get; set; }

        [XmlAttribute]
        public string User { get; set; }

        [XmlAttribute]
        public string Password { get; set; }

        [XmlElement]
        public List<string> Databases { get; set; }

        [XmlIgnore]
        public bool IsUserConnection { get; set; }

        [XmlIgnore]
        public ServerInfo ServerInfo { get; set; }

        public Host()
        {
            AuthType = AuthTypes.Windows;
            Databases = new List<string>();
        }

    }
}
