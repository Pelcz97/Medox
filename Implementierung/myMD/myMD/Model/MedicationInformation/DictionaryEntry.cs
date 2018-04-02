using System;
namespace myMD.Model.MedicationInformation
{
    public class DictionaryEntry
    {
        public string Term { get; set; }
        public string Definition { get; set; }

        public DictionaryEntry(string term, string definition)
        {
            this.Term = term;
            this.Definition = definition;
        }
    }
}
