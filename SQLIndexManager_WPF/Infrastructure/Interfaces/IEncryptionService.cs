namespace SQLIndexManager_WPF.Services
{
    public interface IEncryptionService
    {
        string Decrypt(string content);
        string Encrypt(string content);
    }
}