using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.ProfileTabViewModel
{
    /// <summary>
    /// ViewModel zur Editierung eines einzelnen Profils
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ProfileEditViewModel : ProfileItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.ViewModel.ProfileTabViewModel.ProfileEditViewModel"/> class.
        /// </summary>
        public ProfileEditViewModel()
        {
            Profile = ModelFacade.GetActiveProfile();
        }

        /// <summary>
        /// Saves the new medication.
        /// </summary>
        public void SaveChangedProfile()
        {
            ModelFacade.Update(Profile);

            MessagingCenter.Send(this, "SavedProfileChanges");
            MessagingCenter.Unsubscribe<ProfileEditViewModel>(this, "SavedProfileChanges");
        }
    }
}
