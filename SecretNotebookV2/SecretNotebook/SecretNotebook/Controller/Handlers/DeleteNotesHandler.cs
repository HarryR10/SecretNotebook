using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller.Handlers
{
    class DeleteNotesHandler
    {
        public event EventHandler<EventArgs> DeleteNote;

        public void OnDeleteNote(EventArgs e)
        {
            EventHandler<EventArgs> handler = DeleteNote;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
