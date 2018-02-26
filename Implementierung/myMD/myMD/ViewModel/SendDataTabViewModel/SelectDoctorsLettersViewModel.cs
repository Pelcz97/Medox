using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using myMD.ViewModel.OverviewTabViewModel;
using Plugin.BluetoothLE;
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
            var devices = CrossBleAdapter.Current.GetConnectedDevices();
            foreach (var device in devices)
            {
                Debug.WriteLine(device.Name);
            }

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
