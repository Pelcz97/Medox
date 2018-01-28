using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.ProfileTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.ProfileTabPages
{
    /// <summary>
    /// Profile page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class ProfilePage : CustomContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        ProfileViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.ProfileTabPages.ProfilePage"/> class.
        /// </summary>
        public ProfilePage()
        {
            InitializeComponent();

            vm = new ProfileViewModel();
            BindingContext = vm;
        }

        /// <summary>
        /// Methode, wenn der Bearbeiten-Button geklickt wird.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public async void EditProfileButton(object sender, System.EventArgs e)
        {
            var view = new NavigationPage(new ProfileEditPage());
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            view.BarTextColor = Color.White;

            await Navigation.PushModalAsync(view);
        }
    }
}
