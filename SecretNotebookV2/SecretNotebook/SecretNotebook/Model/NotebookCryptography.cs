using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecretNotebook.Model
{
    public static class NotebookCryptography
    {
        public static byte[] Encode(byte[] data, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            using (Aes crntAes = Aes.Create())
            {
                crntAes.Key = Key;
                crntAes.IV = IV;

                using (ICryptoTransform encryptor = crntAes.CreateEncryptor(crntAes.Key, crntAes.IV))
                {
                    encrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }

            return encrypted;
        }

        public static byte[] Decode(byte[] data, byte[] Key, byte[] IV)
        {
            byte[] decrypted;

            using (Aes crntAes = Aes.Create())
            {
                crntAes.Key = Key;
                crntAes.IV = IV;

                using (ICryptoTransform decryptor = crntAes.CreateDecryptor(crntAes.Key, crntAes.IV))
                {
                    decrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
                }
            }

            return decrypted;
        }

        public static byte[] GetHash(byte[] data)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(data);
            }
        }

        public static byte[] GetHash(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return GetHash(bytes);
        }

        public static bool CompareHash(byte[] first, byte[] second)
        {
            if (first.Length != second.Length) return false;

            for(int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i]) return false;
            }

            return true;
        }

        public static bool CompareHash(string str, byte[] bytes)
        {
            byte[] fromString = GetHash(str);

            return CompareHash(fromString, bytes);
        }

        public static byte[] ProtectHashKey(byte[] data, byte[] aditionalEntropy)
        {
            try
            {
                return ProtectedData.Protect(data, aditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                throw e;
            }
        }

        public static byte[] UnprotectHashKey(byte[] data, byte[] aditionalEntropy)
        {
            try
            {
                return ProtectedData.Unprotect(data, aditionalEntropy, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                throw e;
            }
        }

        public static byte[] GenerateKey()
        {
            using (Aes crntAes = Aes.Create())
            {
                crntAes.GenerateKey();
                return crntAes.Key;
            }
        }

        public static byte[] GenerateIV()
        {
            using (Aes crntAes = Aes.Create())
            {
                crntAes.GenerateIV();
                return crntAes.IV;
            }
        }
    }
}
