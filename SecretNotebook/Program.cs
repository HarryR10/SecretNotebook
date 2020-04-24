using System;
using System.Collections.Generic;
using System.Linq;
using SecretNotebook.Areas;

namespace SecretNotebook
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = new MainMenuArea();
            var cp = new ChangePasswordArea(start);
            cp.Execute();
        }
    }
}
