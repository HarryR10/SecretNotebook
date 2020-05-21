using SecretNotebook.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.View
{
    public class MenuArea : Area
    {        
        private int _menuCounter;
        
        public MenuArea(string header, Action action) : base (header, action)
        {
            _menuCounter = 0;
        }

        public void MenuDown()
        {
            if(_menuCounter < ConstantKeeper.CurrentSource.Notes.Count)
            {
                _menuCounter++;
            }
        }

        public void MenuUp()
        {
            if (_menuCounter > 0)
            {
                _menuCounter--;
            }
        }
    }
}
