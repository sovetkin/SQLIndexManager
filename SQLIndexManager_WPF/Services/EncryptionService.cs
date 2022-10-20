using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SQLIndexManager_WPF.Services
{
    /// <summary>
    /// Responsible for encrypt/decrypt any sensible data
    /// </summary>
    internal class EncryptionService : IEncryptionService
    {
        private static readonly byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static readonly byte[] _key2 = Encoding.UTF8.GetBytes("_SQLIndexManager");

        public string Encrypt(string content)
        {
            using (var des = Rijndael.Create())
            {
                byte[] input = Encoding.UTF8.GetBytes(content);
                des.Key = _key2;
                des.IV = _key1;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(input, 0, input.Length);
                        cs.FlushFinalBlock();
                        byte[] result = ms.ToArray();
                        //I commented it since operator using shall dispose the object correctly anyway
                        //I think these lines are excess
                        //cs.Close();
                        //ms.Close();
                        return Convert.ToBase64String(result);
                    }
                }
            }
        }

        public string Decrypt(string content)
        {
            using (var des = Rijndael.Create())
            {
                des.Key = _key2;
                des.IV = _key1;
                byte[] input = Convert.FromBase64String(content);
                byte[] result = new byte[input.Length];
                using (var ms = new MemoryStream(input))
                {
                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        _ = cs.Read(result, 0, result.Length);
                        //I commented it since operator using shall dispose the object correctly anyway
                        //I think these lines are excess
                        //cs.Close();
                        //ms.Close();
                        return Encoding.UTF8.GetString(result).TrimEnd('\0');
                    }
                }
            }
        }
    }
}
