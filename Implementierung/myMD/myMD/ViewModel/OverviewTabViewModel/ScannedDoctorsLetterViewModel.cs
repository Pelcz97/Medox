using System;
using System.Diagnostics;
using myMD.ModelInterface.DataModelInterface;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.ViewModel.OverviewTabViewModel
{
    [Preserve(AllMembers = true)]
    public class ScannedDoctorsLetterViewModel : DoctorsLetterViewModel
    {
        public string NewDoctorsName { get; set; }


        public string NewDoctorsField { get; set; }

        public DateTime NewLetterDate { get; set; }

        public string Diagnosis { get; set; }

        public bool SavingPossible { get; set; }

        public ScannedDoctorsLetterViewModel(string diagnosis)
        {
            NewLetterDate = DateTime.Today;
            SavingPossible = true;
            OnPropertyChanged("SavingPossible");
            Diagnosis = diagnosis;
        }

        public void SaveNewDoctorsLetter(){
            ModelFacade.GenerateDoctorsLetter(NewDoctorsName, NewDoctorsField, NewLetterDate, Diagnosis);
            MessagingCenter.Send(this, "AddNewLetter");
            MessagingCenter.Unsubscribe<ScannedDoctorsLetterViewModel>(this, "AddNewLetter");
        }
    }
}
