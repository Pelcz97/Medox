using myMD.View.AbstractPages;
using myMD.View.ProfileTabPages;
using myMD.ViewModel.OverviewTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using static Xamarin.Forms.Image;

namespace myMD.View.OverviewTabPages
{
    /// <summary>
    /// Code-Behind Klasse zur OverviewPage
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

        public async void DictionaryButton(object sender, System.EventArgs e)
        {
            var view = new Xamarin.Forms.NavigationPage(new DictionaryPage());
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            view.BarTextColor = Color.White;

            await Navigation.PushModalAsync(view);
        }

        async void KameraButton_Clicked(object sender, System.EventArgs e)
        {
            var answer = await DisplayActionSheet(null, "Abbrechen", null, "Arztbriefe scannen", "Arztbriefe empfangen");
            switch (answer)
            {
                case "Arztbriefe scannen":
                    SwitchToKameraPage();
                    break;
                case "Arztbriefe empfangen":
                    SwitchToReceiveTab();
                    break;
                default:
                    break;
            }


 
        }

        async void SwitchToKameraPage(){
            var view = new Xamarin.Forms.NavigationPage(new KameraPage());
            view.BarBackgroundColor = Color.FromRgb(25, 25, 40);
            view.BarTextColor = Color.White;
            await Navigation.PushModalAsync(view);

            /*
            var page = new KameraPage();
            page.On<iOS>().SetLargeTitleDisplay(LargeTitleDisplayMode.Never);
            Xamarin.Forms.NavigationPage.SetBackButtonTitle(page, "Übersicht");
            await Navigation.PushAsync(page);*/
        }

        void SwitchToReceiveTab(){
            var masterPage = this.Parent.Parent as TabbedPage;
            
            masterPage.CurrentPage = masterPage.Children[2];
        }

        /// <summary>
        /// Methode, wenn ein Arztbriefeintrag aus der Liste ausgewählt wird.
        /// Setzt SelectedItem auf null, um den Markiert-Status des Listeneintrags wieder zu deaktivieren
        /// </summary>
        /// <param name="sender">Sender der diese Methode aufruft</param>
        /// <param name="e">Event des Senders</param>
        async void DoctorsLetter_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedItem = ((ListView)sender).SelectedItem;
            if (selectedItem == null)
                return;
            
            var view = new DetailedDoctorsLetterPage(e.SelectedItem);

            await Navigation.PushModalAsync(view);
            ((ListView)sender).SelectedItem = null;

        }

    }
}
