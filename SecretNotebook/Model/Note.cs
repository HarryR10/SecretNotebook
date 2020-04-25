using System;
namespace SecretNotebook.Model
{
    public struct Note
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string Txt { get; set; }
    }
}
