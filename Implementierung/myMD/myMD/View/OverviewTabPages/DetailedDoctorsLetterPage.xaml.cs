using System;
using myMD.View.AbstractPages;
using myMD.ViewModel.OverviewTabViewModel;
using Xamarin.Forms.Internals;

namespace myMD.View.OverviewTabPages
{
    /// <summary>
    /// Code-Behind Klasse zur DetailedDoctorsLetterPage
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class DetailedDoctorsLetterPage : CustomContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        DetailedDoctorsLetterViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.OverviewTabPages.DetailedDoctorsLetterPage"/> class.
        /// </summary>
        /// <param name="item">DetailedDoctorsLetter der geöffnet wird</param>
        public DetailedDoctorsLetterPage(object item)
        {
            InitializeComponent();
            vm = new DetailedDoctorsLetterViewModel(item);
            BindingContext = vm;
        }

        /// <summary>
        /// Methode, wenn der Schließen-Button geklickt wird.
        /// </summary>
        /// <param name="sender">Sender der diese Methode aufruft</param>
        /// <param name="e">Event des Senders</param>
        async void CancelDetailedPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
