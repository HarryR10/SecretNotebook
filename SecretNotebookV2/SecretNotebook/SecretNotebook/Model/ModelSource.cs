using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SecretNotebook.Model
{
    class ModelSource
    {

        private List<Note> _notes = new List<Note>();

        public List<Note> GetNotes() => _notes;

        public void SerializeNotes()
        {
            var binFormatter = new BinaryFormatter();

            byte[] serialized;

            using (var memory = new MemoryStream())
            {
                binFormatter.Serialize(memory, _notes);
                serialized = memory.ToArray();
            }

            byte[] toFile = NotebookCryptography.Encode(
                serialized,
                ConstantKeeper.Key,
                ConstantKeeper.IV);

            NotebookModelIO.SaveNotesIV(toFile);
        }

        public void DeSerializeNotes()
        {
            byte[] readBytes = NotebookModelIO.ReadNotesIV();

            byte[] decodedBytes = NotebookCryptography.Decode(
                readBytes,
                ConstantKeeper.Key,
                ConstantKeeper.IV
                );

            var binFormatter = new BinaryFormatter();

            using (var memory = new MemoryStream())
            {
                memory.Write(decodedBytes, 0, decodedBytes.Length);
                _notes = binFormatter.Deserialize(memory) as List<Note>;
            }
        }
    }
}
