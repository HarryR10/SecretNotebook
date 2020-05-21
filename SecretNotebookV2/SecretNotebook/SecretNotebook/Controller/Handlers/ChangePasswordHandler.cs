using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller.Handlers
{
    public class ChangePasswordHandler
    {
        public event EventHandler<EventArgs> ChangePassword;

        public void OnChangePassword(EventArgs e)
        {
            EventHandler<EventArgs> handler = ChangePassword;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
