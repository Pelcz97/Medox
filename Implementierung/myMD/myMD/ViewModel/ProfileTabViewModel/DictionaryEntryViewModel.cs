using System;
using myMD.Model.MedicationInformation;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.ProfileTabViewModel
{
    [Preserve(AllMembers = true)]
    public class DictionaryEntryViewModel
    {
        public string expression { get; set; }
        public string term { get; set; }

        public DictionaryEntryViewModel(DictionaryEntry entry){
            expression = entry.Term;
            term = entry.Term;
        }
    }
}
