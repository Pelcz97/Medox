using System;
using myMD.View.AbstractPages;
using myMD.ViewModel.ProfileTabViewModel;
using Xamarin.Forms.Internals;

namespace myMD.View.ProfileTabPages
{
    /// <summary>
    /// Profile edit page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class ProfileEditPage : CustomContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        ProfileEditViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.ProfileTabPages.ProfileEditPage"/> class.
        /// </summary>
        public ProfileEditPage()
        {
            InitializeComponent();
            vm = new ProfileEditViewModel();
            BindingContext = vm;
        }

        /// <summary>
        /// Methode, wenn der Abbrechen-Button geklickt wird.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Methode, wenn der Speichern-Button geklickt wurde.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void SaveChangedProfileButton_Clicked(object sender, EventArgs e)
        {
            vm.SaveChangedProfile();
            await Navigation.PopModalAsync();
        }
    }
}
