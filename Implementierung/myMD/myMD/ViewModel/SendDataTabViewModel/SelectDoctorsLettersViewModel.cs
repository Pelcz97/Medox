using System.Collections.ObjectModel;
using myMD.ModelInterface.DataModelInterface;
using myMD.ViewModel.OverviewTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Die SelectDoctorsLetterViewModel Klasse bearbeitet die Auswahl der DoctorsLetter die zum Senden freigegeben werden sollen.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SelectDoctorsLettersViewModel : OverallViewModel.OverallViewModel
    {
        /// <summary>
        /// Die Liste der vorhandenen Arztbriefe.
        /// </summary>
        /// <value>The doctors letters list.</value>
        public ObservableCollection<DoctorsLetterViewModel> DoctorsLettersSendList { get; }

        /// <summary>
        /// Konstruktor für das SelectDoctosLetterViewModel
        /// </summary>
        public SelectDoctorsLettersViewModel()
        {

            this.DoctorsLettersSendList = new ObservableCollection<DoctorsLetterViewModel>();
            foreach (IDoctorsLetter letter in ModelFacade.GetAllDoctorsLetters())
            {
                DoctorsLettersSendList.Add(new DoctorsLetterViewModel(letter));
            }
        }

        public void SelectionConfirmed(){
            MessagingCenter.Send(this, "SelectedLetters", DoctorsLettersSendList);
            MessagingCenter.Unsubscribe<SelectDoctorsLettersViewModel>(this, "SelectedLetters");
        }
    }
}
