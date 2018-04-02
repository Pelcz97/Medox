using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using myMD.Model.MedicationInformation;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.ProfileTabViewModel
{
    [Preserve(AllMembers = true)]
    public class DictionaryViewModel : OverallViewModel.OverallViewModel
    {
        public ObservableCollection<DictionaryEntryViewModel> DictionaryEntries { get; set; }

        public DictionaryViewModel()
        {
            DictionaryEntries = new ObservableCollection<DictionaryEntryViewModel>();
        }

        public async Task<int> GetDefinition(string input)
        {
            DictionaryEntries.Clear();

            try
            {
                var result = await ModelFacade.GetDefinition(input);

                if (result.Count > 0)
                {
                    foreach (DictionaryEntry entry in result)
                    {
                        DictionaryEntries.Add(new DictionaryEntryViewModel(entry));
                    }
                }
                return 1;
            } catch (HttpRequestException ex) {
                return 0;
            }
        }
    }
}
