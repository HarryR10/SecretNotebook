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

        [Test]
        public void HashingCompareHashWith_GetHash_CompareHash()
        {
            string password = "password";
            string wrongPsw = "wrong";
            var hash = NotebookCryptography.GetHash(password);
            Assert.IsTrue(NotebookCryptography.CompareHash(password, hash));
            Assert.IsFalse(NotebookCryptography.CompareHash(wrongPsw, hash));
        }
    }
}