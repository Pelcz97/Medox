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

        public bool LastNameVisible { get => Profile.LastName != null; }
        public bool NameVisible { get => Profile.Name != null; }
        public bool BirthdateVisible { get => Profile.BirthDate != null; }
        public bool InsuranceNumberVisible { get => Profile.InsuranceNumber != null; }
        public bool BloodTypeVisible { get => Profile.BloodType != BloodType.Empty; }



        public ProfileViewModel()
        {
            Profile = ModelFacade.GetActiveProfile();
        }
    }
}
