using System;
using System.IO;
using System.Xml.Serialization;

using Microsoft.Extensions.Logging;

using SQLIndexManager_WPF.Infrastructure.Settings;

namespace SQLIndexManager_WPF.Services
{
    /// <summary>
    /// Responsible for serialization and deserialization of the objects into configuration file
    /// </summary>
    public class XMLConfigService : IConfigService
    {
        private AssemblyDataService _assembly;
        private ILogger _logger;
        private string _fileName;

        public XMLConfigService(AssemblyDataService assembly, ILogger logger)
        {
            _assembly = assembly;
            _logger = logger;

            _fileName = _assembly.GetSettingFileName();
        }

        public object DeserializeFromConfigFile()
        {
            if (IsFileExists())
            {
                try
                {
                    using (StreamReader reader = File.OpenText(_fileName))
                    {
                        return new XmlSerializer(typeof(GlobalSettings)).Deserialize(reader);
                    }
                }
                catch (Exception e)
                {
                    Output.Current.Add("Failed to load settings");

                    if (e.GetType() == typeof(UnauthorizedAccessException) ||
                        e.GetType() == typeof(PathTooLongException) || e.GetType() == typeof(DirectoryNotFoundException))
                    {
                        _logger.LogWarning(e, e.Message);
                    }

                    if (e.GetType() == typeof(ArgumentException) || e.GetType() == typeof(ArgumentNullException) ||
                        e.GetType() == typeof(NotSupportedException) || e.GetType() == typeof(FileNotFoundException))
                    {
                        _logger.LogError(e, e.Message);
                    }
                }
            }
            else
                _logger.LogInformation("The configuration file does not exist. The configuration settings can't be deserialized.");

            return null;
        }

        public void SerializeToConfigFile(object serializable)
        {
            if (IsFileExists())
            {
                try
                {
                    File.Delete(_fileName);
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(IOException) || e.GetType() == typeof(UnauthorizedAccessException) ||
                        e.GetType() == typeof(PathTooLongException) || e.GetType() == typeof(DirectoryNotFoundException))
                    {
                        _logger.LogWarning(e, e.Message);
                    }

                    if (e.GetType() == typeof(ArgumentException) || e.GetType() == typeof(ArgumentNullException) ||
                        e.GetType() == typeof(NotSupportedException))
                    {
                        _logger.LogError(e,e.Message);
                    }
                }

                try
                {
                    using (FileStream writer = File.OpenWrite(_fileName))
                    {
                        new XmlSerializer(serializable.GetType()).Serialize(writer, serializable);
                    }
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(UnauthorizedAccessException) ||
                        e.GetType() == typeof(PathTooLongException) || e.GetType() == typeof(DirectoryNotFoundException))
                    {
                        _logger.LogWarning(e, e.Message);
                    }

                    if (e.GetType() == typeof(ArgumentException) || e.GetType() == typeof(ArgumentNullException) ||
                        e.GetType() == typeof(NotSupportedException) || e.GetType() == typeof(InvalidOperationException))
                    {
                        _logger.LogError(e, e.Message);
                    }
                }
            }
            else
                _logger.LogInformation("The configuration file does not exist.");
        }

        private bool IsFileExists() => File.Exists(_fileName);
    }
}
