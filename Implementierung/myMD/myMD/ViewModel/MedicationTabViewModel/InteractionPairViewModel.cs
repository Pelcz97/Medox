using System;
using myMD.Model.MedicationInformation;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.MedicationTabViewModel
{
    [Preserve(AllMembers = true)]
    public class InteractionPairViewModel : OverallViewModel.OverallViewModel
    {
        string conflict { get; set; }
        public string ConflictingMeds { get => conflict;
            set
            {
                conflict = value;
                OnPropertyChanged("ConflictingMeds");

            }            
        }

        string description { get; set; }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public InteractionPairViewModel(InteractionPair pair)
        {
            ConflictingMeds = pair.med1 + " - " + pair.med2;
            Description = pair.InteractionDescription;
        }
    }
}
