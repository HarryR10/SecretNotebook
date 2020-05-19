using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Model
{
    class ModelSource
    {
        private string _path = @"notes.nts";

        private List<Note> _notes = new List<Note>();

        public List<Note> GetNotes() => _notes;

        
    }
}
