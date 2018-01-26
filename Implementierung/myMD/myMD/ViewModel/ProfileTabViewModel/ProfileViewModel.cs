using myMD.ModelInterface.DataModelInterface;
using System;
using Xamarin.Forms.Internals;
using Xamarin.Forms;
using System.Diagnostics;

namespace myMD.ViewModel.ProfileTabViewModel
{
    [Preserve(AllMembers = true)]
    public class ProfileViewModel : ProfileItemViewModel
    {

        private ProfileItemViewModel ProfileItem;

        public ProfileViewModel()
        {
            ProfileItem = new ProfileItemViewModel();
            ProfileItem.Profile = ModelFacade.GetActiveProfile();

            MessagingCenter.Subscribe<ProfileEditViewModel>(this, "SavedProfileChanges", sender => {
                ReloadProfile();
            });
        }

        private void ReloadProfile()
        {
            ProfileItem.Profile = ModelFacade.GetActiveProfile();
            OnPropertyChanged("LastName");
            OnPropertyChanged("Name");
            OnPropertyChanged("Birthdate");
            OnPropertyChanged("InsuranceNumber");
        }
    }
}
