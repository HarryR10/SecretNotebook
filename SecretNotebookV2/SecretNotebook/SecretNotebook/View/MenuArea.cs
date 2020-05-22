using SecretNotebook.Model;
using System;
using System.Collections.Generic;
using System.Text;
using SecretNotebook.Controller;

namespace SecretNotebook.View
{
    public class MenuArea : Area
    {        
        private int _menuCounter;

        public int GetMenuCounter => _menuCounter;

        public MenuArea(string header, Action action) : base (header, action)
        {
            _menuCounter = 0;
        }

        public void MenuDown()
        {
            if(_menuCounter < ConstantKeeper.CurrentSource.Notes.Count - 1)
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

        public override void Run()
        {
            Console.Clear();
            Console.Write(Header);

            foreach(var el in Controls.Menu)
            {
                Console.WriteLine(el.Value.Item1);
            }

            Console.WriteLine();

            if (_menuCounter > ConstantKeeper.CurrentSource.Notes.Count - 1) _menuCounter = 0;

            for (int i = 0; i < ConstantKeeper.CurrentSource.Notes.Count; i++)
            {
                if(_menuCounter == i)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(ConstantKeeper.CurrentSource.Notes[i].Name);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.WriteLine(ConstantKeeper.CurrentSource.Notes[i].Name);
                }
            }
            Action();

            ConstantKeeper.CurrentArea = this;
            this.Run();
        }
    }
}
