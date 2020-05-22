using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller.Handlers
{
    class OpenNotesHandler
    {
        public event EventHandler<EventArgs> OpenNotes;

        public void OnOpenNotes(EventArgs e)
        {
            EventHandler<EventArgs> handler = OpenNotes;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
