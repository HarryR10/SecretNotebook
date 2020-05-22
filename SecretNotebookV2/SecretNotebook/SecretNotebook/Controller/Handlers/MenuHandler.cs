using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller.Handlers
{
    class MenuHandler
    {
        public event EventHandler<EventArgs> MenuAction;

        public void OnMenuAction(EventArgs e)
        {
            EventHandler<EventArgs> handler = MenuAction;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
