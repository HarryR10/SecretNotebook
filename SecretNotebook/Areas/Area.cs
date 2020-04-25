using System;
using System.Collections.Generic;
using SecretNotebook.InputMethods;

namespace SecretNotebook.Areas
{
    public abstract class Area
    {
        public abstract string Header { get; }

        //public abstract Dictionary<Key, Action> UsingKeys { get; }

        public abstract Area PreviousArea { get; set; }

        public virtual void Redraw()
        {
            Console.Clear();
            Console.WriteLine(Header);
            //Execute();
        }

        public abstract void Execute();
    }
}
