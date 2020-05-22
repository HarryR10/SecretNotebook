using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller.Handlers
{
    class AddNoteHandler
    {
        public event EventHandler<EventArgs> AddNote;

        public void OnAddNote(EventArgs e)
        {
            EventHandler<EventArgs> handler = AddNote;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
