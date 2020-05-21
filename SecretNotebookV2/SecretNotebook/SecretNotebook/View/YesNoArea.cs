using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.View
{
    public class YesNoArea : Area
    {
        public YesNoArea(string header, Action action) : base (header, action)
        {
            Yes = false;
        }

        public bool Yes { get; set; }
    }
}
