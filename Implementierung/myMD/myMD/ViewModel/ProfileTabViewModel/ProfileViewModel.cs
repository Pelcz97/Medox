using myMD.ModelInterface.DataModelInterface;
using System;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.ProfileTabViewModel
{
    [Preserve(AllMembers = true)]
    public class ProfileViewModel : OverallViewModel.OverallViewModel
    {
        IProfile Profile { get; }
        public string LastName { get => Profile.LastName; }
        public string Name { get => Profile.Name; }
        public DateTime Birthdate { get => Profile.BirthDate; }
        public string InsuranceNumber { get => Profile.InsuranceNumber; }
        public BloodType BloodType { get => Profile.BloodType; }

        public bool LastNameVisible { get; set; }
        public bool NameVisible { get; set; }
        public bool BirthdateVisible { get; set; }
        public bool InsuranceNumberVisible { get; set; }
        public bool BloodTypeVisible { get; set; }



        public ProfileViewModel()
        {
            Profile = ModelFacade.GetActiveProfile();
        }
    }
}
