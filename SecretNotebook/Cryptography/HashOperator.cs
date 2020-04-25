using System;
using System.Security.Cryptography;
using System.Text;

namespace SecretNotebook.Cryptography
{
    public static class HashOperator
    {
        public static byte[] CreateHash(string password)
        {
            byte[] charPsw = Encoding.Default.GetBytes(password);

            var sha256 = new SHA256CryptoServiceProvider();

            return sha256.ComputeHash(charPsw);
        }

        public static bool CompareHash(string password, byte[] oldHash)
        {
            bool match = true;
            byte[] charPsw = Encoding.Default.GetBytes(password);

            for (int i = 0; i < oldHash.Length; i++)
            {
                if (charPsw[i] != oldHash[i])
                {
                    match = false;
                    break;
                }
            }
            return match;
        }
    }
}
