using System;
using System.Collections.ObjectModel;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.ProfileTabViewModel
{
    [Preserve(AllMembers = true)]
    public class DictionaryViewModel : OverallViewModel.OverallViewModel
    {
        public ObservableCollection<DictionaryViewModel> dictionaryEntries { get; set; }

        public DictionaryViewModel()
        {
            GetDictionary();
        }

        public void GetDictionary()
        {
            ModelFacade.GetDefinition("Test");
        }
    }
}
