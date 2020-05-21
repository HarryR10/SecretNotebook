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

        [Test]
        public void SaveReadHashKeyWith_SaveHashKey_ReadHashKey()
        {
            string password = "password";
            var aHash = NotebookCryptography.GetHash(password);
            var aKey = NotebookCryptography.GenerateKey();
            
            Assert.IsTrue(NotebookModelIO.SaveHashKey(aHash, aKey));

            var extractHash = NotebookModelIO.ReadHashKey(true);
            var extractKey = NotebookModelIO.ReadHashKey(false);

            CollectionAssert.AreEqual(aHash, extractHash);
            CollectionAssert.AreEqual(aKey, extractKey);
        }

        [Test]
        public void SaveReadNotesWith_Serialize_Deserialize()
        {
            var source = new ModelSource();
            source.AddNote("example", "it's a note");

            ConstantKeeper.SetKey(NotebookCryptography.GenerateKey());
            ConstantKeeper.SetIV(NotebookCryptography.GenerateIV());

            source.SerializeNotes();
            source.DeSerializeNotes();
            var result = source.Notes;
            var name = result[0].Name;
            var txt = result[0].Txt;

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(name == "example");
            Assert.IsTrue(txt == "it's a note");
        }
    }
}