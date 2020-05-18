using NUnit.Framework;
using SecretNotebook.Model;
using System.Security.Cryptography;

namespace SecretNotebookTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void EncryptyngDecryptingWith_Encode_Decode()
        {
            var data = new byte[] { 1, 8, 47, 128 };
            using (Aes crntAes = Aes.Create())
            {
                byte[] encrypted = NotebookCryptography.Encode(data, crntAes.Key, crntAes.IV);
                byte[] decrypted = NotebookCryptography.Decode(encrypted, crntAes.Key, crntAes.IV);

                CollectionAssert.AreEqual(data, decrypted);
            }
        }
    }
}