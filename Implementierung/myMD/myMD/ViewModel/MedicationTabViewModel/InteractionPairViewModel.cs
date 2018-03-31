using System;
using myMD.Model.MedicationInformation;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.MedicationTabViewModel
{
    [Preserve(AllMembers = true)]
    public class InteractionPairViewModel : OverallViewModel.OverallViewModel
    {
        public string ConflictingMeds { get; set; }

        public string Description { get; set; }

        public InteractionPairViewModel(InteractionPair pair)
        {
            ConflictingMeds = pair.med1 + " - " + pair.med2;
            Description = pair.InteractionDescription;
        }
    }
}
