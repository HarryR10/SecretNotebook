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

                ICryptoTransform encryptor = crntAes.CreateEncryptor(crntAes.Key, crntAes.IV);

                //encryptor.TransformFinalBlock

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        //cs.Write(data, 0, data.Length);
                        //encrypted = ms.ToArray();
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(data);
                        }
                        encrypted = ms.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public static byte[] Decode(byte[] data, byte[] Key, byte[] IV)
        {
            byte[] decrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        //cs.Read(data, 0, data.Length);
                        //decrypted = ms.ToArray();
                        //using (StreamReader sw = new StreamReader(cs))
                        //{
                        //    sw.Read(data, 0, data.Length);
                        //}
                        //decrypted = sw.ToArray();
                    }
                }
            }

            return decrypted;
        }
    }
}
