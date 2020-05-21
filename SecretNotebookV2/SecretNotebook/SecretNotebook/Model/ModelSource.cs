using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SecretNotebook.Model
{
    public class ModelSource
    {
        private List<Note> _notes = new List<Note>();

        public List<Note> Notes => _notes;

        public void AddNote(string name, string txt)
        {
            var date = DateTime.Now;
            _notes.Add(new Note(date, name, txt));
        }

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

            using (var memory = new MemoryStream(decodedBytes))
            {
                var noteList = binFormatter.Deserialize(memory) as List<Note>;
                if (noteList != null)
                {
                    _notes = noteList;
                }
            }
        }
    }
}
