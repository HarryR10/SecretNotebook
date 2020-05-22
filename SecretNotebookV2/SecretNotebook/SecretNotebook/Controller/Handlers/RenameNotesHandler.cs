using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller.Handlers
{
    class RenameNotesHandler
    {
        public event EventHandler<EventArgs> RenameNote;

        public void OnRenameNote(EventArgs e)
        {
            EventHandler<EventArgs> handler = RenameNote;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
