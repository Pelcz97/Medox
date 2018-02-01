using Xamarin.Forms.Internals;
using Xamarin.Forms;

namespace myMD.ViewModel.ProfileTabViewModel
{
    /// <summary>
    /// ViewModel zur Handhabung mehrerer Profile
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ProfileViewModel : ProfileItemViewModel
    {
        /// <summary>
        /// Das Profil des ProfileViewModels.
        /// </summary>
        ProfileItemViewModel ProfileItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.ViewModel.ProfileTabViewModel.ProfileViewModel"/> class.
        /// </summary>
        public ProfileViewModel()
        {
            ProfileItem = new ProfileItemViewModel();
            ProfileItem.Profile = ModelFacade.GetActiveProfile();

            MessagingCenter.Subscribe<ProfileEditViewModel>(this, "SavedProfileChanges", sender => {
                ReloadProfile();
            });
        }

        /// <summary>
        /// Methode, um die Änderungen am Profil sichtbar zu machen. Zunächst wird das Profil neu aus der Datenbank geladen, 
        /// anschließend werden alle relevanten Properties über etwaige Änderungen benachrichtigt.
        /// </summary>
        void ReloadProfile()
        {
            ProfileItem.Profile = ModelFacade.GetActiveProfile();
            OnPropertyChanged("LastName");
            OnPropertyChanged("Name");
            OnPropertyChanged("Birthdate");
            OnPropertyChanged("InsuranceNumber");
            OnPropertyChanged("BloodTypeName");
        }
    }
}
