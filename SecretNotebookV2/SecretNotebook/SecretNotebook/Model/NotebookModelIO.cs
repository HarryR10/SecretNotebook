using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SecretNotebook.Model;

namespace SecretNotebook.Model
{
    public static class NotebookModelIO
    {
        public static bool SaveHashKey(byte[] hash, byte[] key)
        {
            byte[] allBytes = hash.Concat(key).ToArray();

            using (FileStream newSource = new FileStream(ConstantKeeper.PathToKeys, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    var bytes = NotebookCryptography.ProtectHashKey(allBytes, ConstantKeeper.Entropy);
                    newSource.Write(bytes, 0, bytes.Length);
                }
                catch(CryptographicException)
                {
                    return false;
                }
                return true;
            }
        }

        public static byte[] ReadHashKey(bool getHash)
        {
            byte[] bytes;

            using (FileStream fsSource = new FileStream(ConstantKeeper.PathToKeys, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[fsSource.Length];
                int numBytesToRead = (int)fsSource.Length;
                int numBytesRead = 0;

                while (numBytesToRead > 0)
                {
                    int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }
            }

            bytes = NotebookCryptography.UnprotectHashKey(bytes, ConstantKeeper.Entropy);

            if (getHash)
            {
                Array.Resize(ref bytes, 32);
                return bytes;
            }
            else
            {
                byte[] key = new byte[bytes.Length - 32];
                for (int i = 32, j = 0;
                    i < bytes.Length;
                    i++, j++)
                {
                    key[j] = bytes[i];
                }
                return key;
            }
        }

        public static bool SaveNotesIV(byte[] data)
        {
            byte[] allBytes = data.Concat(ConstantKeeper.IV).ToArray();
            using (FileStream newSource = new FileStream(ConstantKeeper.PathToNotes, FileMode.Create, FileAccess.Write))
            {
                try
                {
                    newSource.Write(allBytes, 0, allBytes.Length);
                }
                catch //TODO: добавить exception, или привести к void
                {
                    return false;
                }
                return true;
            }
        }

        public static byte[] ReadNotesIV()
        {
            byte[] bytes;

            using (FileStream fsSource = new FileStream(ConstantKeeper.PathToNotes, FileMode.Open, FileAccess.Read))
            {
                bytes = new byte[fsSource.Length];
                int numBytesToRead = (int)fsSource.Length;
                int numBytesRead = 0;

                while (numBytesToRead > 0)
                {
                    int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }
            }

            ConstantKeeper.IV = new byte[16];
            for (int i = bytes.Length - 16, j = 0;
                i < bytes.Length; 
                i++, j++)
            {
                ConstantKeeper.IV[j] = bytes[i];
            }

            Array.Resize(ref bytes, bytes.Length - 16);
            return bytes;
        }
    }
}
