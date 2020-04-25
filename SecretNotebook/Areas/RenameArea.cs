using System;
using SecretNotebook.Model;

namespace SecretNotebook.Areas
{
    public class RenameArea : Area
    {
        public override string Header { get => "Write the new name:"; }

        public override Area PreviousArea { get; set; }

        //private Area _previousArea;

        public RenameArea(Area previousArea)
        {
            PreviousArea = previousArea;
        }

        public override void Execute()
        {
            Note crnt;

            if (PreviousArea is MainMenuArea) 
            {
                var area = (MainMenuArea)PreviousArea;
                crnt = area.CurrentItem;
                crnt.Name = Console.ReadLine();
            }
            else if (PreviousArea is NewItemArea)
            {
                var area = (NewItemArea)PreviousArea;
                crnt = area.CurrentItem;
                crnt.Name = Console.ReadLine();
            }

            //_previousArea.Redraw();
        }
    }
}
