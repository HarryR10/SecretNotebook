using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.View
{
    public class Area
    {
        public virtual string Header { get; set; }

        public virtual Action Action { get; set; }

        public Area(string header, Action action)
        {
            Header = header + "\n";
            Action = action;
        }

        public virtual void Run()
        {
            Console.Clear();
            Console.Write(Header);
            Action();
        }
    }
}
