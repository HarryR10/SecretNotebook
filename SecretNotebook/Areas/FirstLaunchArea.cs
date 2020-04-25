using System;
using SecretNotebook.Cryptography;

namespace SecretNotebook.Areas
{
    public class FirstLaunchArea : Area
    {
        public FirstLaunchArea()
        {
        }

        public override string Header => "Notes not found! Enter the new password:";

        public override Area PreviousArea { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Execute()
        {
            string password = Console.ReadLine();
            var newHash = HashOperator.CreateHash(password);

            //создать файл из байтов
        }
    }
}
