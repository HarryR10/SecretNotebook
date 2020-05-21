using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller.Handlers
{
    class FirstLaunchHandler
    {
        public event EventHandler<EventArgs> FirstLaunch;

        public void OnFirstLaunch(EventArgs e)
        {
            EventHandler<EventArgs> handler = FirstLaunch;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
