using System;
using System.Collections.Generic;

namespace SecretNotebook.InputMethods
{
    public static class AllKeysDictionary
    {
        private static Dictionary<ConsoleKey, Key> _keys = new Dictionary<ConsoleKey, Key>
        {
            { ConsoleKey.Insert, new InsertKey() },
            { ConsoleKey.R, new RKey() },
            { ConsoleKey.UpArrow, new UpArrowKey() },
            { ConsoleKey.DownArrow, new DownArrowKey() },
            { ConsoleKey.Delete, new DeleteKey() },
            { ConsoleKey.Enter, new EnterKey() },
            { ConsoleKey.Escape, new EscKey() },
            { ConsoleKey.C, new CKey() },
        };

        public static Key Find(ConsoleKey thisKey)
        {
            foreach (var el in _keys)
            {
                if(el.Key == thisKey)
                {
                    return el.Value;
                }
            }
            return new AnotherKey();
        }
    }
}
