using System;
using SecretNotebook.Model;

namespace SecretNotebook.Areas
{
    public class NewItemArea : Area
    {
        public override string Header => "Write some text there:";

        public override Area PreviousArea { get; set; }

        public Note CurrentItem { get; set; }

        //private MainMenuArea _previousArea;

        public NewItemArea(Area previousArea)
        {
            PreviousArea = previousArea;

        }

        public override void Execute()
        {
            string text = Console.ReadLine();

            CurrentItem = new Note
            {
                Date = DateTime.Now,
                Name = text
            };

            var nameArea = new RenameArea(this);
            nameArea.Redraw();
            nameArea.Execute();

            if (PreviousArea is MainMenuArea)
            {
                var area = (MainMenuArea)PreviousArea;
                area.CurrentItem = CurrentItem;
                area.Notes.Add(CurrentItem);
                area.Position = area.Notes.Count;
            }
        }
    }
}
