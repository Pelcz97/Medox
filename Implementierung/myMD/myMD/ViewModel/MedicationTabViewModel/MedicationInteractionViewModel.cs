using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using myMD.Model.MedicationInformation;

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class MedicationInteractionViewModel : OverallViewModel.OverallViewModel
    {
        public ObservableCollection<InteractionPairViewModel> Interactions { get; set; }

        public MedicationInteractionViewModel(object interactionList)
        {
            var list = (IList<InteractionPair>)interactionList;
            Interactions = new ObservableCollection<InteractionPairViewModel>();

            foreach (InteractionPair pair in list) {
                Interactions.Add(new InteractionPairViewModel(pair));
            }
        }
    }
}
