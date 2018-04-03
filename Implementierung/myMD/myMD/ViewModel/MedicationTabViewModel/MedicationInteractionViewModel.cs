using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using myMD.Model.MedicationInformation;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.MedicationTabViewModel
{
    [Preserve(AllMembers = true)]
    public class MedicationInteractionViewModel : OverallViewModel.OverallViewModel
    {
        public ObservableCollection<InteractionPairViewModel> Interactions { get; set; }

        bool translateButtonEnabled { get; set; }
        public bool TranslateButtonEnabled { get => translateButtonEnabled; 
            set {
                translateButtonEnabled = value;
                OnPropertyChanged("TranslateButtonEnabled");
            }
        }

        public MedicationInteractionViewModel(object interactionList)
        {
            var list = (IList<InteractionPair>)interactionList;
            TranslateButtonEnabled = true;

            Debug.WriteLine(list.Count());
            Interactions = new ObservableCollection<InteractionPairViewModel>();

            foreach (InteractionPair pair in list) {
                Interactions.Add(new InteractionPairViewModel(pair));
            }
        }

        public async void TranslateEntries(){
            if (Interactions.Any())
            {
                IList<string> texts = new List<string>();
                foreach (InteractionPairViewModel description in Interactions)
                {
                    texts.Add(description.Description);
                }

                var result = await ModelFacade.TranslateText(texts);

                for (int i = 0; i < result.Count; i++){
                    Interactions[i].Description = result[i];
                }
                TranslateButtonEnabled = false;
            } else {
                TranslateButtonEnabled = true;
            }
        }
    }
}
