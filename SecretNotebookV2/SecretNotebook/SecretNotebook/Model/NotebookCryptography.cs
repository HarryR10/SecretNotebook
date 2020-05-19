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
    }
}
