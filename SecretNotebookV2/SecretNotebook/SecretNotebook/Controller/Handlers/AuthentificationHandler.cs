using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller.Handlers
{
    public class AuthentificationHandler
    {
        public event EventHandler<EventArgs> EnterPassword;

        public void OnEnterPassword(EventArgs e)
        {
            EventHandler<EventArgs> handler = EnterPassword;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
