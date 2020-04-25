using System;
using System.Collections.Generic;
using SecretNotebook.Model;

namespace SecretNotebook.Areas
{
    public class MainMenuArea : Area
    {
        public override string Header { get => "Choose menu item:"; }

        public byte[] Hash { get; set; }

        public Note CurrentItem { get; set; }

        public List<Note> Notes { get; set; }

        public override Area PreviousArea { get; set; }

        public int Position { get; set; }

        public MainMenuArea()
        {
        }

        public override void Execute()
        {
            
        }
    }
}
