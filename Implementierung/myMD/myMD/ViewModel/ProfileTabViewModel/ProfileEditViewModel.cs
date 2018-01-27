using System;
using myMD.Model.ModelFacade;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.ProfileTabViewModel
{
    [Preserve(AllMembers = true)]
    public class ProfileEditViewModel : ProfileItemViewModel
    {
        public ProfileEditViewModel()
        {
            this.Profile = ModelFacade.GetActiveProfile();
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
