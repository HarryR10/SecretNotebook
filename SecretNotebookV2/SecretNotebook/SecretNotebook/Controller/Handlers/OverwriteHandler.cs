using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller.Handlers
{
    class OverwriteHandler
    {
        public event EventHandler<EventArgs> Overwrite;

        public void OnOverwrite(EventArgs e)
        {
            EventHandler<EventArgs> handler = Overwrite;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
