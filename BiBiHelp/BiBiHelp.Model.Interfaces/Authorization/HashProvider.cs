using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BiBiHelp.Model.Interfaces.Authorization
{
    public class HashProvider
    {
        public byte[] GetHash(string data)
        {
            return new SHA1CryptoServiceProvider().ComputeHash(Encoding.Unicode.GetBytes(data));
        }

        public static string MD5Hash(string value)
        {
            return Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(value)));
        }

        public bool CheckHash(string data, byte[] hash)
        {
            if (data == null || hash == null)
                return false;
            var datahash = GetHash(data);
            if (datahash.Length != hash.Length)
                return false;
            for (int i = 0; i < hash.Length; i++)
                if (datahash[i] != hash[i])
                    return false;
            return true;
        }
    }
}
