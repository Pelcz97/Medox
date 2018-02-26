using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using myMD.ViewModel.OverallViewModel;
using Plugin.FilePicker.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMDesktop.ViewModel.SendDataTabViewModel
{
    /// <summary>
    /// Die SelectDoctorsLetterViewModel Klasse bearbeitet die Auswahl der DoctorsLetter die zum Senden freigegeben werden sollen.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class SelectDoctorsLettersViewModel : OverallViewModel
    {

        public ObservableCollection<FileData> DoctorsLetters { get; set; }

        /// <summary>
        /// Konstruktor für das SelectDoctosLetterViewModel
        /// </summary>
        public SelectDoctorsLettersViewModel()
        {
            DoctorsLetters = new ObservableCollection<FileData>();
        }

        public void SelectionConfirmed()
        {
            //MessagingCenter.Send(this, "SelectedLetters", DoctorsLettersSendList);
            //MessagingCenter.Unsubscribe<SelectDoctorsLettersViewModel>(this, "SelectedLetters");
        }
    }
}
