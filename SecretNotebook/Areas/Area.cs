using System;
using System.Collections.Generic;
using SecretNotebook.InputMethods;

namespace SecretNotebook.Areas
{
    public class Area
    {
        public virtual string Header { get => ""; }

        //public abstract Dictionary<Key, Action> UsingKeys { get; }

        public virtual void Redraw()
        {
            Console.Clear();
            Console.WriteLine(Header);
            Execute();
        }

        public virtual void Execute()
        {
        }
    }
}
