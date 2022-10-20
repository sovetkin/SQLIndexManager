namespace SQLIndexManager_WPF.Services
{
    public interface IConfigService
    {
        object DeserializeFromConfigFile();
        void SerializeToConfigFile(object serializable);
    }
}