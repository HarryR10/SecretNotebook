using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Controller.Handlers
{
    class QuestionHandler
    {
        public event EventHandler<EventArgs> SetQuestion;

        public void OnSetQuestion(EventArgs e)
        {
            EventHandler<EventArgs> handler = SetQuestion;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
