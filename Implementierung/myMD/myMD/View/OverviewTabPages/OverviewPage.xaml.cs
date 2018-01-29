using myMD.View.AbstractPages;
using myMD.ViewModel.OverviewTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.OverviewTabPages
{
    /// <summary>
    /// Overview page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class OverviewPage : CustomContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        OverviewViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.OverviewTabPages.OverviewPage"/> class.
        /// </summary>
        public OverviewPage()
        {
            InitializeComponent();
            vm = new OverviewViewModel();
            BindingContext = vm;
        }

        /// <summary>
        /// Methode, wenn ein Arztbriefeintrag aus der Liste ausgewählt wird.
        /// Setzt SelectedItem auf null, um den Markiert-Status des Listeneintrags wieder zu deaktivieren
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void DoctorsLetter_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = ((ListView)sender).SelectedItem;
            if (selectedItem == null)
                return;
            
            var view = new DetailedDoctorsLetterPage(e.SelectedItem);
            Navigation.PushModalAsync(view);
            ((ListView)sender).SelectedItem = null;
        }

    }
}
