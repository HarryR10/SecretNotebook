using System;
using System.Collections.Generic;
using System.Text;
using SecretNotebook.InputMethods;

namespace SecretNotebook.Areas
{
    public class ChangePasswordArea : Area
    {
        public override string Header { get => "Enter the new password"; }

        private Area _previousArea;

        private string _password;

        public ChangePasswordArea(Area previousArea)
        {
            _previousArea = previousArea;

            //запрос к файлу с хешем

        }

        public override void Execute()
        {
            //var currentKey = Console.ReadKey(false);
            //var cmd = currentKey.Key;
            //var txt = currentKey.ToString();

            //var currentCmd = AllKeysDictionary.Find(cmd);

            _password = Console.ReadLine();

            //хеширование и шифрование

            _previousArea.Redraw();
        }
    }
}
