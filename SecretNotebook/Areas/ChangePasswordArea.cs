using System;
using System.Collections.Generic;
using System.Text;
using SecretNotebook.Cryptography;
using SecretNotebook.InputMethods;

namespace SecretNotebook.Areas
{
    public class ChangePasswordArea : Area
    {
        public override string Header { get => "Enter the new password"; }

        public override Area PreviousArea { get; set; }

        //private MainMenuArea _previousArea;

        //private string _password;

        public ChangePasswordArea(Area previousArea)
        {
            PreviousArea = previousArea;

            //запрос к файлу с хешем

        }

        public override void Execute()
        {
            //var currentKey = Console.ReadKey(false);
            //var cmd = currentKey.Key;
            //var txt = currentKey.ToString();

            //var currentCmd = AllKeysDictionary.Find(cmd);

            string password = Console.ReadLine();
            var newHash = HashOperator.CreateHash(password);
            var area = (MainMenuArea)PreviousArea;
            area.Hash = newHash;

            //_previousArea.Redraw();
        }
    }
}
