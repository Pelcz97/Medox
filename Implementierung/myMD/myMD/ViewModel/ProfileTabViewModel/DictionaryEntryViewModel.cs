using System;
using myMD.Model.MedicationInformation;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.ProfileTabViewModel
{
    [Preserve(AllMembers = true)]
    public class DictionaryEntryViewModel
    {
        public string definition { get; set; }
        public string term { get; set; }

        public DictionaryEntryViewModel(DictionaryEntry entry){
            term = entry.Term;
            definition = entry.Definition;
        }
    }
}
