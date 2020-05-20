using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace SecretNotebook.Model
{
    [Serializable]
    public class Note
    {
        public Note(DateTime date, string name, string txt)
        {
            Date = date;
            Name = name;
            Txt = txt;
        }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string Txt { get; set; }
    }
}
