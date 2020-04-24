using System;
namespace SecretNotebook.InputMethods
{
    public abstract class Key
    {
        public abstract void OnChangePasswordArea();

        public abstract void OnMainMenuArea();

        public abstract void OnRenameArea();

        public abstract void OnTxtEditorArea();
    }
}
