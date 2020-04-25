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
            start.Redraw();
            var cp = new ChangePasswordArea(start);
            cp.Redraw();
            
        }
    }
}
