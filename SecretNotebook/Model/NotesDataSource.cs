using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using SecretNotebook.Exceptions;

namespace SecretNotebook.Model
{
    public class NotesDataSource
    {
        private string _path = @"/notes.nts";

        private List<Note> _notes = new List<Note>();

        public List<Note> GetNotes()
        {
            return _notes;
        }

        NotesDataSource()
        {

        }

        public byte[] ExtractHashByte()
        {
            if (File.Exists(_path))
            {
                
                byte[] bytes = new byte[32];

                using (var file = File.Open(_path, FileMode.Open))
                {
                    file.Read(bytes, 0, 32);
                }
                return bytes;
            }
            else
            {
                throw new NotesNotFoundException();
            }
        }

        public void FillList()
        {
            string line;

            if (File.Exists(_path))
            {
                using (FileStream fsSource = new FileStream(_path, FileMode.Open))
                {
                    byte[] bytes = new byte[fsSource.Length];
                    int numBytesToRead = (int)fsSource.Length;
                    int numBytesRead = 31;

                    while (numBytesToRead > 0)
                    {
                        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;
                    }

                    //encoding bytes

                    line = System.Text.Encoding.Default.GetString(bytes);
                }

                using (JsonDocument document = JsonDocument.Parse(line))
                {
                    JsonElement root = document.RootElement;
                    foreach (JsonElement el in root.EnumerateArray())
                    {
                        _notes.Add(JsonSerializer.Deserialize<Note>(el.GetRawText()));
                    }

                }
            }
            else
            {
                throw new NotesNotFoundException();
            }
        }
    }
}
