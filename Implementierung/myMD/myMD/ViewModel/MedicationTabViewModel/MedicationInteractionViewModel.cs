using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using myMD.Model.MedicationInformation;

namespace myMD.ViewModel.MedicationTabViewModel
{
    public class MedicationInteractionViewModel : OverallViewModel.OverallViewModel
    {
        public ObservableCollection<InteractionPairViewModel> Interactions { get; set; }

        public MedicationInteractionViewModel(object interactionList)
        {
            var list = (IList<InteractionPair>)interactionList;

            Debug.WriteLine(list.Count());
            Interactions = new ObservableCollection<InteractionPairViewModel>();

            foreach (InteractionPair pair in list) {
                Interactions.Add(new InteractionPairViewModel(pair));
            }
        }
    }
}
