using System;
using SecretNotebook.Model;

namespace SecretNotebook.Areas
{
    public class TxtViewerArea : Area
    {
        public override string Header { get => CurrentItem.Date.ToString() +  " " +
                CurrentItem.Name; }

        public override Area PreviousArea { get; set; }

        //private MainMenuArea _previousArea;

        public Note CurrentItem { get; set; }

        private string _date;

        private string _name;

        public TxtViewerArea(Area previousArea)
        {
            PreviousArea = previousArea;

            var menuArea = (MainMenuArea)PreviousArea;
            CurrentItem = menuArea.CurrentItem;
        }

        public override void Redraw()
        {
            Console.Clear();
            Console.WriteLine(Header);
            Console.WriteLine(CurrentItem.Txt);
            //Execute();
        }

        public override void Execute()
        {
            Console.ReadLine();
            // возврат
        }
    }
}
