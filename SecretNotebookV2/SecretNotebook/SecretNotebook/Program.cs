using SecretNotebook.Controller;
using SecretNotebook.Model;
using SecretNotebook.View;
using System;

namespace SecretNotebook
{
    class Program
    {
        static void Main(string[] args)
        {
            ConstantKeeper.CurrentArea = new Area("Вход в программу...", Controls.CheckPassword);
            ConstantKeeper.CurrentArea.Run();
        }
    }
}
