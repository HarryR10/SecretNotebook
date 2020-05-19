using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace SecretNotebook.Model
{
    [Serializable]
    class Note
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string Txt { get; set; }
    }
}
